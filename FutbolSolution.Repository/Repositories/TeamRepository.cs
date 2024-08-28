using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Repository.Repositories
{
    public class TeamRepository : BaseRepository, ITeamRepository
    {
        public TeamRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(Team entity)
        {
            var parameters = new OracleParameter[]
            {
        new OracleParameter("p_Name", OracleDbType.Varchar2) { Value = entity.Name },
        new OracleParameter("p_Stadium", OracleDbType.Varchar2) { Value = entity.Stadium },
        new OracleParameter("p_Coach", OracleDbType.Varchar2) { Value = entity.Coach },
        new OracleParameter("p_FoundedYear", OracleDbType.Int32) { Value = (object)entity.FoundedYear ?? DBNull.Value },
        new OracleParameter("p_City", OracleDbType.Varchar2) { Value = entity.City },
        new OracleParameter("p_TeamId", OracleDbType.Int32)
        {
            Direction = ParameterDirection.Output
        }
            };

            await ExecuteNonQueryAsync("football_teamtable_create", parameters);

            // Safely handle OracleDecimal conversion
            var outputParam = parameters.Last().Value;
            entity.TeamId = int.Parse(parameters.Last().Value.ToString());
            
        }

        public async Task AddRangeAsync(IEnumerable<Team> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public async Task<IQueryable<Team>> GetAllAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var teams = await ExecuteReaderAsync("football_teamtable_getAll", parameters, reader => new Team
            {
                TeamId = Convert.ToInt32(reader["TEAMID"]),
                Name = reader["NAME"].ToString(),
                Stadium = reader["STADIUM"].ToString(),
                Coach = reader["COACH"].ToString(),
                FoundedYear = Decimal.Parse(reader["FOUNDEDYEAR"].ToString()),
                City = reader["CITY"].ToString()
            });

            return teams.AsQueryable();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("football_teamtable_getById", parameters, reader => new Team
            {
                TeamId = Int32.Parse(reader["TEAMID"].ToString()),
                Name = reader["NAME"].ToString(),
                Stadium = reader["STADIUM"].ToString(),
                Coach = reader["COACH"].ToString(),
                FoundedYear = Decimal.Parse(reader["FOUNDEDYEAR"].ToString()),
                City = reader["CITY"].ToString()
            });
        }

        public async Task RemoveAsync(Team entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = entity.TeamId }
            };

            await ExecuteNonQueryAsync("football_teamtable_delete", parameters);
        }

        public async Task RemoveRangeAsync(IEnumerable<Team> entities)
        {
            foreach (var entity in entities)
            {
                await RemoveAsync(entity);
            }
        }

        public async Task UpdateAsync(Team entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = entity.TeamId },
                new OracleParameter("p_Name", OracleDbType.Varchar2) { Value = entity.Name },
                new OracleParameter("p_Stadium", OracleDbType.Varchar2) { Value = entity.Stadium },
                new OracleParameter("p_Coach", OracleDbType.Varchar2) { Value = entity.Coach },
                new OracleParameter("p_FoundedYear", OracleDbType.Int32) { Value = (object)entity.FoundedYear ?? DBNull.Value },
                new OracleParameter("p_City", OracleDbType.Varchar2) { Value = entity.City }
            };

            await ExecuteNonQueryAsync("football_teamtable_update", parameters);
        }

        public async Task AddImageAsync(TeamImage image)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = image.TeamId },
                new OracleParameter("p_ImageData", OracleDbType.Blob)
                {
                    Value = image.ImageData
                }
            };

            await ExecuteNonQueryAsync("team_image_create", parameters);
        }


        public async Task UpdateImageAsync(TeamImage image)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = image.TeamId },
                new OracleParameter("p_ImageData", OracleDbType.Blob)
                {
                    Value = image.ImageData
                }
            };

            await ExecuteNonQueryAsync("team_image_update", parameters);
        }

        public async Task<TeamImage> GetImageAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("team_image_getById", parameters, reader => new TeamImage
            {
                TeamId = Convert.ToInt32(reader["TEAMID"]),
                ImageData = reader["IMAGEDATA"] as byte[]
            });
        }

        public async Task<List<TeamImage>> GetImagesAsync()
        {
            var parameters = new OracleParameter[]
           {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
           };

            return await ExecuteReaderAsync("team_image_getAll", parameters, reader => new TeamImage
            {
                TeamId = Convert.ToInt32(reader["TEAMID"]),
                ImageData = reader["IMAGEDATA"] as byte[]
            });
        }

        public async Task RemoveImageAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = id }
            };

            await ExecuteNonQueryAsync("team_image_delete", parameters);
        }
    }
}
