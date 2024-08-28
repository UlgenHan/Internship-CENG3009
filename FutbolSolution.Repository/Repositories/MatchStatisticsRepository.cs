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
    public class MatchStatisticsRepository : BaseRepository, IMatchStatisticsRepository
    {
        public MatchStatisticsRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(MatchStatistics entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_MatchId", OracleDbType.Int32) { Value = (object)entity.MatchId ?? DBNull.Value },
                new OracleParameter("p_HomeGoals", OracleDbType.Int32) { Value = entity.HomeGoals },
                new OracleParameter("p_AwayGoals", OracleDbType.Int32) { Value = entity.AwayGoals },
                new OracleParameter("p_HomePossession", OracleDbType.Decimal) { Value = (object)entity.HomePossession ?? DBNull.Value },
                new OracleParameter("p_AwayPossession", OracleDbType.Decimal) { Value = (object)entity.AwayPossession ?? DBNull.Value },
                new OracleParameter("p_HomeShots", OracleDbType.Int32) { Value = entity.HomeShots },
                new OracleParameter("p_AwayShots", OracleDbType.Int32) { Value = entity.AwayShots },
                new OracleParameter("p_HomeShotsOnTarget", OracleDbType.Int32) { Value = entity.HomeShotsOnTarget },
                new OracleParameter("p_AwayShotsOnTarget", OracleDbType.Int32) { Value = entity.AwayShotsOnTarget },
                new OracleParameter("p_HomeFouls", OracleDbType.Int32) { Value = entity.HomeFouls },
                new OracleParameter("p_AwayFouls", OracleDbType.Int32) { Value = entity.AwayFouls },
                new OracleParameter("p_HomeYellowCards", OracleDbType.Int32) { Value = entity.HomeYellowCards },
                new OracleParameter("p_AwayYellowCards", OracleDbType.Int32) { Value = entity.AwayYellowCards },
                new OracleParameter("p_HomeRedCards", OracleDbType.Int32) { Value = entity.HomeRedCards },
                new OracleParameter("p_AwayRedCards", OracleDbType.Int32) { Value = entity.AwayRedCards },
                new OracleParameter("p_MatchStatsId", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                }
            };

            await ExecuteNonQueryAsync("match_statistics_create", parameters);

            entity.MatchStatsId = int.Parse(parameters.Last().Value.ToString());
        }

        public async Task AddRangeAsync(IEnumerable<MatchStatistics> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public async Task<IQueryable<MatchStatistics>> GetAllAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var stats = await ExecuteReaderAsync("match_statistics_getAll", parameters, reader => new MatchStatistics
            {
                MatchStatsId = Convert.ToInt32(reader["MATCHSTATSID"]),
                MatchId = int.Parse(reader["MATCHID"].ToString()),
                HomeGoals = Convert.ToInt32(reader["HOMEGOALS"]),
                AwayGoals = Convert.ToInt32(reader["AWAYGOALS"]),
                HomePossession = decimal.Parse(reader["HOMEPOSSESSION"].ToString()),
                AwayPossession = decimal.Parse(reader["AWAYPOSSESSION"].ToString()),
                HomeShots = Convert.ToInt32(reader["HOMESHOTS"]),
                AwayShots = Convert.ToInt32(reader["AWAYSHOTS"]),
                HomeShotsOnTarget = Convert.ToInt32(reader["HOMESHOTSONTARGET"]),
                AwayShotsOnTarget = Convert.ToInt32(reader["AWAYSHOTSONTARGET"]),
                HomeFouls = Convert.ToInt32(reader["HOMEFOULS"]),
                AwayFouls = Convert.ToInt32(reader["AWAYFOULS"]),
                HomeYellowCards = Convert.ToInt32(reader["HOMEYELLOWCARDS"]),
                AwayYellowCards = Convert.ToInt32(reader["AWAYYELLOWCARDS"]),
                HomeRedCards = Convert.ToInt32(reader["HOMEREDCARDS"]),
                AwayRedCards = Convert.ToInt32(reader["AWAYREDCARDS"])
            });

            return stats.AsQueryable();
        }

        public async Task<MatchStatistics> GetByIdAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_MatchStatsId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("match_statistics_getById", parameters, reader => new MatchStatistics
            {
                MatchStatsId = Convert.ToInt32(reader["MATCHSTATSID"]),
                MatchId = int.Parse(reader["MATCHID"].ToString()),
                HomeGoals = Convert.ToInt32(reader["HOMEGOALS"]),
                AwayGoals = Convert.ToInt32(reader["AWAYGOALS"]),
                HomePossession = decimal.Parse(reader["HOMEPOSSESSION"].ToString()),
                AwayPossession = decimal.Parse(reader["AWAYPOSSESSION"].ToString()),
                HomeShots = Convert.ToInt32(reader["HOMESHOTS"]),
                AwayShots = Convert.ToInt32(reader["AWAYSHOTS"]),
                HomeShotsOnTarget = Convert.ToInt32(reader["HOMESHOTSONTARGET"]),
                AwayShotsOnTarget = Convert.ToInt32(reader["AWAYSHOTSONTARGET"]),
                HomeFouls = Convert.ToInt32(reader["HOMEFOULS"]),
                AwayFouls = Convert.ToInt32(reader["AWAYFOULS"]),
                HomeYellowCards = Convert.ToInt32(reader["HOMEYELLOWCARDS"]),
                AwayYellowCards = Convert.ToInt32(reader["AWAYYELLOWCARDS"]),
                HomeRedCards = Convert.ToInt32(reader["HOMEREDCARDS"]),
                AwayRedCards = Convert.ToInt32(reader["AWAYREDCARDS"])
            });
        }

        public async Task RemoveAsync(MatchStatistics entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_MatchStatsId", OracleDbType.Int32) { Value = entity.MatchStatsId }
            };

            await ExecuteNonQueryAsync("match_statistics_delete", parameters);
        }

        public async Task RemoveRangeAsync(IEnumerable<MatchStatistics> entities)
        {
            foreach (var entity in entities)
            {
                await RemoveAsync(entity);
            }
        }

        public async Task UpdateAsync(MatchStatistics entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_MatchStatsId", OracleDbType.Int32) { Value = entity.MatchStatsId },
                new OracleParameter("p_MatchId", OracleDbType.Int32) { Value = (object)entity.MatchId ?? DBNull.Value },
                new OracleParameter("p_HomeGoals", OracleDbType.Int32) { Value = entity.HomeGoals },
                new OracleParameter("p_AwayGoals", OracleDbType.Int32) { Value = entity.AwayGoals },
                new OracleParameter("p_HomePossession", OracleDbType.Decimal) { Value = (object)entity.HomePossession ?? DBNull.Value },
                new OracleParameter("p_AwayPossession", OracleDbType.Decimal) { Value = (object)entity.AwayPossession ?? DBNull.Value },
                new OracleParameter("p_HomeShots", OracleDbType.Int32) { Value = entity.HomeShots },
                new OracleParameter("p_AwayShots", OracleDbType.Int32) { Value = entity.AwayShots },
                new OracleParameter("p_HomeShotsOnTarget", OracleDbType.Int32) { Value = entity.HomeShotsOnTarget },
                new OracleParameter("p_AwayShotsOnTarget", OracleDbType.Int32) { Value = entity.AwayShotsOnTarget },
                new OracleParameter("p_HomeFouls", OracleDbType.Int32) { Value = entity.HomeFouls },
                new OracleParameter("p_AwayFouls", OracleDbType.Int32) { Value = entity.AwayFouls },
                new OracleParameter("p_HomeYellowCards", OracleDbType.Int32) { Value = entity.HomeYellowCards },
                new OracleParameter("p_AwayYellowCards", OracleDbType.Int32) { Value = entity.AwayYellowCards },
                new OracleParameter("p_HomeRedCards", OracleDbType.Int32) { Value = entity.HomeRedCards },
                new OracleParameter("p_AwayRedCards", OracleDbType.Int32) { Value = entity.AwayRedCards }
            };

            await ExecuteNonQueryAsync("match_statistics_update", parameters);
        }
    }
}
