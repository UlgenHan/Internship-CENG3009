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
    public class TeamStatisticsRepository : BaseRepository, ITeamStatisticsRepository
    {
        public TeamStatisticsRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(TeamStatistics entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = entity.TeamId ?? (object)DBNull.Value },
                new OracleParameter("p_SeasonYear", OracleDbType.Varchar2) { Value = entity.SeasonYear },
                new OracleParameter("p_GoalsScored", OracleDbType.Int32) { Value = entity.GoalsScored },
                new OracleParameter("p_GoalsConceded", OracleDbType.Int32) { Value = entity.GoalsConceded },
                new OracleParameter("p_Wins", OracleDbType.Int32) { Value = entity.Wins },
                new OracleParameter("p_Draws", OracleDbType.Int32) { Value = entity.Draws },
                new OracleParameter("p_Losses", OracleDbType.Int32) { Value = entity.Losses },
                new OracleParameter("p_HomeWins", OracleDbType.Int32) { Value = entity.HomeWins },
                new OracleParameter("p_AwayWins", OracleDbType.Int32) { Value = entity.AwayWins },
                new OracleParameter("p_RecentForm", OracleDbType.Varchar2) { Value = entity.RecentForm },
                new OracleParameter("p_TeamStatsId", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                }
            };

            await ExecuteNonQueryAsync("football_teamstatisticstable_create", parameters);
        }

        public async Task AddRangeAsync(IEnumerable<TeamStatistics> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public async Task<IQueryable<TeamStatistics>> GetAllAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var teamStatistics = await ExecuteReaderAsync("football_teamstatisticstable_getAll", parameters, reader => new TeamStatistics
            {
                TeamStatsId = Convert.ToInt32(reader["TEAMSTATSID"]),
                TeamId =  Convert.ToInt32(reader["TEAMID"]),
                SeasonYear = reader["SEASONYEAR"].ToString(),
                GoalsScored = Convert.ToInt32(reader["GOALSSCORED"]),
                GoalsConceded = Convert.ToInt32(reader["GOALSCONCEDED"]),
                Wins = Convert.ToInt32(reader["WINS"]),
                Draws = Convert.ToInt32(reader["DRAWS"]),
                Losses = Convert.ToInt32(reader["LOSSES"]),
                HomeWins = Convert.ToInt32(reader["HOMEWINS"]),
                AwayWins = Convert.ToInt32(reader["AWAYWINS"]),
                RecentForm = reader["RECENTFORM"].ToString()
            });

            return teamStatistics.AsQueryable();
        }

        public async Task<TeamStatistics> GetByIdAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamStatsId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("football_teamstatisticstable_getById", parameters, reader => new TeamStatistics
            {
                TeamStatsId = Convert.ToInt32(reader["TEAMSTATSID"]),
                TeamId = Convert.ToInt32(reader["TEAMID"]),
                SeasonYear = reader["SEASONYEAR"].ToString(),
                GoalsScored = Convert.ToInt32(reader["GOALSSCORED"]),
                GoalsConceded = Convert.ToInt32(reader["GOALSCONCEDED"]),
                Wins = Convert.ToInt32(reader["WINS"]),
                Draws = Convert.ToInt32(reader["DRAWS"]),
                Losses = Convert.ToInt32(reader["LOSSES"]),
                HomeWins = Convert.ToInt32(reader["HOMEWINS"]),
                AwayWins = Convert.ToInt32(reader["AWAYWINS"]),
                RecentForm = reader["RECENTFORM"].ToString()
            });
        }

        public async Task RemoveAsync(TeamStatistics entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamStatsId", OracleDbType.Int32) { Value = entity.TeamStatsId }
            };

            await ExecuteNonQueryAsync("football_teamstatisticstable_delete", parameters);
        }

        public async Task RemoveRangeAsync(IEnumerable<TeamStatistics> entities)
        {
            foreach (var entity in entities)
            {
                await RemoveAsync(entity);
            }
        }

        public async Task UpdateAsync(TeamStatistics entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_TeamStatsId", OracleDbType.Int32) { Value = entity.TeamStatsId },
                new OracleParameter("p_TeamId", OracleDbType.Int32) { Value = entity.TeamId ?? (object)DBNull.Value },
                new OracleParameter("p_SeasonYear", OracleDbType.Varchar2) { Value = entity.SeasonYear },
                new OracleParameter("p_GoalsScored", OracleDbType.Int32) { Value = entity.GoalsScored },
                new OracleParameter("p_GoalsConceded", OracleDbType.Int32) { Value = entity.GoalsConceded },
                new OracleParameter("p_Wins", OracleDbType.Int32) { Value = entity.Wins },
                new OracleParameter("p_Draws", OracleDbType.Int32) { Value = entity.Draws },
                new OracleParameter("p_Losses", OracleDbType.Int32) { Value = entity.Losses },
                new OracleParameter("p_HomeWins", OracleDbType.Int32) { Value = entity.HomeWins },
                new OracleParameter("p_AwayWins", OracleDbType.Int32) { Value = entity.AwayWins },
                new OracleParameter("p_RecentForm", OracleDbType.Varchar2) { Value = entity.RecentForm }
            };

            await ExecuteNonQueryAsync("football_teamstatisticstable_update", parameters);
        }
    }
}
