using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolSolution.Repository.Repositories
{
    public class PlayerRepository : BaseRepository, IPlayerRepository
    {
        public PlayerRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(Player entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Name", OracleDbType.Varchar2) { Value = entity.Name },
                new OracleParameter("p_Surname", OracleDbType.Varchar2) { Value = entity.Surname },
                new OracleParameter("p_Age", OracleDbType.Int32) { Value = entity.Age },
                new OracleParameter("p_DateOfBirth", OracleDbType.Date) { Value = entity.DateOfBirth },
                new OracleParameter("p_Nationality", OracleDbType.Varchar2) { Value = entity.Nationality },
                new OracleParameter("p_Position", OracleDbType.Varchar2) { Value = entity.Position },
                new OracleParameter("p_CurrentClub", OracleDbType.Varchar2) { Value = entity.CurrentClub },
                new OracleParameter("p_Height", OracleDbType.Decimal) { Value = entity.Height },
                new OracleParameter("p_Weight", OracleDbType.Decimal) { Value = entity.Weight },
                new OracleParameter("p_PreferredFoot", OracleDbType.Varchar2) { Value = entity.PreferredFoot },
                new OracleParameter("p_PlayerId", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                }
            };

            await ExecuteNonQueryAsync("football_playertable_create", parameters);
            entity.Id = int.Parse(parameters.Last().Value.ToString());
        }

        public async Task AddImageAsync(PlayerImage image)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = image.PlayerId },
                new OracleParameter("p_ImageData", OracleDbType.Blob) { Value = image.ImageData }
            };

            await ExecuteNonQueryAsync("player_image_create", parameters);
        }

        public async Task<PlayerImage> GetImageAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("player_image_getById", parameters, reader => new PlayerImage
            {
                PlayerId = Convert.ToInt32(reader["PlayerId"]),
                ImageData = (byte[])reader["ImageData"]
            });
        }

        public async Task UpdateImageAsync(PlayerImage image)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = image.PlayerId },
                new OracleParameter("p_ImageData", OracleDbType.Blob) { Value = image.ImageData }
            };

            await ExecuteNonQueryAsync("player_image_update", parameters);
        }

        public async Task RemoveImageAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = id }
            };

            await ExecuteNonQueryAsync("player_image_delete", parameters);
        }

        public async Task AddRangeAsync(IEnumerable<Player> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public async Task<IQueryable<Player>> GetAllAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var players = await ExecuteReaderAsync("football_playertable_getAll", parameters, reader => new Player
            {
                Id = Convert.ToInt32(reader["PLAYERID"]),
                Name = reader["NAME"].ToString(),
                Surname = reader["SURNAME"].ToString(),
                Age = Convert.ToInt32(reader["AGE"]),
                DateOfBirth = Convert.ToDateTime(reader["DATEOFBIRTH"]),
                Nationality = reader["NATIONALITY"].ToString(),
                Position = reader["POSITION"].ToString(),
                CurrentClub = reader["CURRENTCLUB"].ToString(),
                Height = Convert.ToDecimal(reader["HEIGHT"]),
                Weight = Convert.ToDecimal(reader["WEIGHT"]),
                PreferredFoot = reader["PREFERREDFOOT"].ToString(),
                PlayerStatsId = Convert.ToInt32(reader["PLAYERSTATSID"])
            });

            return players.AsQueryable();
        }

        public async Task<Player> GetByIdAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("football_playertable_getById", parameters, reader => new Player
            {
                Id = Convert.ToInt32(reader["PlayerId"]),
                Name = reader["Name"].ToString(),
                Surname = reader["Surname"].ToString(),
                Age = Convert.ToInt32(reader["Age"]),
                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                Nationality = reader["Nationality"].ToString(),
                Position = reader["Position"].ToString(),
                CurrentClub = reader["CurrentClub"].ToString(),
                Height = Convert.ToDecimal(reader["Height"]),
                Weight = Convert.ToDecimal(reader["Weight"]),
                PreferredFoot = reader["PreferredFoot"].ToString(),
                PlayerStatsId = Convert.ToInt32(reader["PlayerStatsId"])
            });
        }

        public async Task RemoveAsync(Player entity)
        {            
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = entity.Id }
            };

            await ExecuteNonQueryAsync("football_playertable_delete", parameters);

        }

        public async Task RemoveRangeAsync(IEnumerable<Player> entities)
        {
            foreach (var entity in entities)
            {
                await RemoveAsync(entity);
            }
        }

        public async Task UpdateAsync(Player entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = entity.Id },
                new OracleParameter("p_Name", OracleDbType.Varchar2) { Value = entity.Name },
                new OracleParameter("p_Surname", OracleDbType.Varchar2) { Value = entity.Surname },
                new OracleParameter("p_Age", OracleDbType.Int32) { Value = entity.Age },
                new OracleParameter("p_DateOfBirth", OracleDbType.Date) { Value = entity.DateOfBirth },
                new OracleParameter("p_Nationality", OracleDbType.Varchar2) { Value = entity.Nationality },
                new OracleParameter("p_Position", OracleDbType.Varchar2) { Value = entity.Position },
                new OracleParameter("p_CurrentClub", OracleDbType.Varchar2) { Value = entity.CurrentClub },
                new OracleParameter("p_Height", OracleDbType.Decimal) { Value = entity.Height },
                new OracleParameter("p_Weight", OracleDbType.Decimal) { Value = entity.Weight },
                new OracleParameter("p_PreferredFoot", OracleDbType.Varchar2) { Value = entity.PreferredFoot },
                new OracleParameter("p_PlayerStatsId", OracleDbType.Int32) { Value = entity.PlayerStatsId }
            };

            await ExecuteNonQueryAsync("football_playertable_update", parameters);
        }

        public async Task<IQueryable<PlayerImage>> GetImagesAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var playerImages = await ExecuteReaderAsync("player_image_getAll", parameters, reader => new PlayerImage
            {
                PlayerId = Convert.ToInt32(reader["PlayerId"]),
                ImageData = (byte[])reader["ImageData"]
            });
            return playerImages.AsQueryable();
        }
    }
}
