using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Repository.Repositories;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Repository
{
    public class PlayerTeamLinkRepository : BaseRepository, IPlayerTeamLinkRepository
    {
        public PlayerTeamLinkRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(PlayerTeamLink entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = (object)entity.PlayerId ?? DBNull.Value },
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = (object)entity.TeamId ?? DBNull.Value },
                new OracleParameter("p_StartDate", OracleDbType.Date) { Value = (object)entity.StartDate ?? DBNull.Value },
                new OracleParameter("p_EndDate", OracleDbType.Date) { Value = (object)entity.EndDate ?? DBNull.Value },
                new OracleParameter("p_IsLoan", OracleDbType.Varchar2) { Value = entity.IsLoan },
                new OracleParameter("p_PlayerTeamLinkId", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                }
            };

            await ExecuteNonQueryAsync("player_teamlink_create", parameters);

            entity.PlayerTeamLinkId = int.Parse(parameters.Last().Value.ToString());
        }

        public async Task AddRangeAsync(IEnumerable<PlayerTeamLink> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public async Task<IQueryable<PlayerTeamLink>> GetAllAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var playerTeamLinks = await ExecuteReaderAsync("player_teamlink_getAll", parameters, reader => new PlayerTeamLink
            {
                PlayerTeamLinkId = int.Parse(reader["PLAYERTEAMLINKID"].ToString()),
                PlayerId = int.Parse(reader["PLAYERID"].ToString()),
                TeamId = int.Parse(reader["TEAMID"].ToString()),
                StartDate = reader["STARTDATE"] as DateTime?,
                EndDate = reader["ENDDATE"] as DateTime?,
                IsLoan = reader["ISLOAN"] as string
            });

            return playerTeamLinks.AsQueryable();
        }

        public async Task<PlayerTeamLink> GetByIdAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerTeamLinkId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("player_teamlink_getById", parameters, reader => new PlayerTeamLink
            {
                PlayerTeamLinkId = int.Parse(reader["PLAYERTEAMLINKID"].ToString()),
                PlayerId = int.Parse(reader["PLAYERID"].ToString()),
                TeamId = int.Parse(reader["TEAMID"].ToString()),
                StartDate = reader["STARTDATE"] as DateTime?,
                EndDate = reader["ENDDATE"] as DateTime?,
                IsLoan = reader["ISLOAN"] as string
            });
        }

        public async Task RemoveAsync(PlayerTeamLink entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerTeamLinkId", OracleDbType.Int32) { Value = entity.PlayerTeamLinkId }
            };

            await ExecuteNonQueryAsync("player_teamlink_delete", parameters);
        }

        public async Task RemoveRangeAsync(IEnumerable<PlayerTeamLink> entities)
        {
            foreach (var entity in entities)
            {
                await RemoveAsync(entity);
            }
        }

        public async Task UpdateAsync(PlayerTeamLink entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerTeamLinkId", OracleDbType.Int32) { Value = entity.PlayerTeamLinkId },
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = (object)entity.PlayerId ?? DBNull.Value },
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = (object)entity.TeamId ?? DBNull.Value },
                new OracleParameter("p_StartDate", OracleDbType.Date) { Value = (object)entity.StartDate ?? DBNull.Value },
                new OracleParameter("p_EndDate", OracleDbType.Date) { Value = (object)entity.EndDate ?? DBNull.Value },
                new OracleParameter("p_IsLoan", OracleDbType.Varchar2) { Value = entity.IsLoan }
            };

            await ExecuteNonQueryAsync("player_teamlink_update", parameters);
        }
    }
}
