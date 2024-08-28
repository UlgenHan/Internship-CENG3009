using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Repository.Repositories
{
    public class MatchRepository : BaseRepository, IMatchRepository
    {
        public MatchRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(Match entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_HomeTeamId", OracleDbType.Int32) { Value = (object)entity.HomeTeamId ?? DBNull.Value },
                new OracleParameter("p_AwayTeamId", OracleDbType.Int32) { Value = (object)entity.AwayTeamId ?? DBNull.Value },
                new OracleParameter("p_MatchDate", OracleDbType.Date) { Value = (object)entity.MatchDate ?? DBNull.Value },
                new OracleParameter("p_Stadium", OracleDbType.Varchar2) { Value = entity.Stadium },
                new OracleParameter("p_RefereeId", OracleDbType.Int32) { Value = (object)entity.RefereeId ?? DBNull.Value },
                new OracleParameter("p_WeatherConditions", OracleDbType.Varchar2) { Value = entity.WeatherConditions },
                new OracleParameter("p_Importance", OracleDbType.Varchar2) { Value = entity.Importance },
                new OracleParameter("p_MatchId", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                }
            };

            await ExecuteNonQueryAsync("match_create", parameters);

            entity.MatchId = int.Parse(parameters.Last().Value.ToString());
        }

        public async Task AddRangeAsync(IEnumerable<Match> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public async Task<IQueryable<Match>> GetAllAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var matches = await ExecuteReaderAsync("match_getAll", parameters, reader => new Match
            {
                MatchId = int.Parse(reader["MATCHID"].ToString()),
                HomeTeamId = int.Parse(reader["HOMETEAMID"].ToString()),
                AwayTeamId = int.Parse(reader["AWAYTEAMID"].ToString()),
                MatchDate = reader["MATCHDATE"] as DateTime?,
                Stadium = reader["STADIUM"].ToString(),
                RefereeId = int.Parse(reader["REFEREEID"].ToString()),
                WeatherConditions = reader["WEATHERCONDITIONS"].ToString(),
                Importance = reader["IMPORTANCE"].ToString()
            });

            return matches.AsQueryable();
        }

        public async Task<Match> GetByIdAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_MatchId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("match_getById", parameters, reader => new Match
            {
                MatchId = int.Parse(reader["MATCHID"].ToString()),
                HomeTeamId = int.Parse(reader["HOMETEAMID"].ToString()),
                AwayTeamId = int.Parse(reader["AWAYTEAMID"].ToString()),
                MatchDate = reader["MATCHDATE"] as DateTime?,
                Stadium = reader["STADIUM"].ToString(),
                RefereeId = int.Parse(reader["REFEREEID"].ToString()),
                WeatherConditions = reader["WEATHERCONDITIONS"].ToString(),
                Importance = reader["IMPORTANCE"].ToString()
            });
        }

        public async Task RemoveAsync(Match entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_MatchId", OracleDbType.Int32) { Value = entity.MatchId }
            };

            await ExecuteNonQueryAsync("match_delete", parameters);
        }

        public async Task RemoveRangeAsync(IEnumerable<Match> entities)
        {
            foreach (var entity in entities)
            {
                await RemoveAsync(entity);
            }
        }

        public async Task UpdateAsync(Match entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_MatchId", OracleDbType.Int32) { Value = entity.MatchId },
                new OracleParameter("p_HomeTeamId", OracleDbType.Int32) { Value = (object)entity.HomeTeamId ?? DBNull.Value },
                new OracleParameter("p_AwayTeamId", OracleDbType.Int32) { Value = (object)entity.AwayTeamId ?? DBNull.Value },
                new OracleParameter("p_MatchDate", OracleDbType.Date) { Value = (object)entity.MatchDate ?? DBNull.Value },
                new OracleParameter("p_Stadium", OracleDbType.Varchar2) { Value = entity.Stadium },
                new OracleParameter("p_RefereeId", OracleDbType.Int32) { Value = (object)entity.RefereeId ?? DBNull.Value },
                new OracleParameter("p_WeatherConditions", OracleDbType.Varchar2) { Value = entity.WeatherConditions },
                new OracleParameter("p_Importance", OracleDbType.Varchar2) { Value = entity.Importance }
            };

            await ExecuteNonQueryAsync("match_update", parameters);
        }
    }
}
