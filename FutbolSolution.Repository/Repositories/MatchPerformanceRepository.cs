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
    public class MatchPerformanceRepository : BaseRepository, IMatchPerformanceRepository
    {
        public MatchPerformanceRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(MatchPerformance entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = (object)entity.PlayerId ?? DBNull.Value },
                new OracleParameter("p_MatchId", OracleDbType.Int32) { Value = (object)entity.MatchId ?? DBNull.Value },
                new OracleParameter("p_Goals", OracleDbType.Int32) { Value = entity.Goals },
                new OracleParameter("p_Assists", OracleDbType.Int32) { Value = entity.Assists },
                new OracleParameter("p_MinutesPlayed", OracleDbType.Int32) { Value = entity.MinutesPlayed },
                new OracleParameter("p_PassAccuracy", OracleDbType.Decimal) { Value = (object)entity.PassAccuracy ?? DBNull.Value },
                new OracleParameter("p_Tackles", OracleDbType.Int32) { Value = entity.Tackles },
                new OracleParameter("p_Interceptions", OracleDbType.Int32) { Value = entity.Interceptions },
                new OracleParameter("p_Clearances", OracleDbType.Int32) { Value = entity.Clearances },
                new OracleParameter("p_Shots", OracleDbType.Int32) { Value = entity.Shots },
                new OracleParameter("p_ShotsOnTarget", OracleDbType.Int32) { Value = entity.ShotsOnTarget },
                new OracleParameter("p_DribblesCompleted", OracleDbType.Int32) { Value = entity.DribblesCompleted },
                new OracleParameter("p_AerialDuelsWon", OracleDbType.Int32) { Value = entity.AerialDuelsWon },
                new OracleParameter("p_YellowCards", OracleDbType.Int32) { Value = entity.YellowCards },
                new OracleParameter("p_RedCards", OracleDbType.Int32) { Value = entity.RedCards },
                new OracleParameter("p_FoulsCommitted", OracleDbType.Int32) { Value = entity.FoulsCommitted },
                new OracleParameter("p_FoulsSuffered", OracleDbType.Int32) { Value = entity.FoulsSuffered },
                new OracleParameter("p_Offsides", OracleDbType.Int32) { Value = entity.Offsides },
                new OracleParameter("p_Saves", OracleDbType.Int32) { Value = entity.Saves },
                new OracleParameter("p_CleanSheets", OracleDbType.Int32) { Value = entity.CleanSheets },
                new OracleParameter("p_MatchPerformanceId", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                }
            };

            await ExecuteNonQueryAsync("match_performance_create", parameters);

            entity.MatchPerformanceId = int.Parse(parameters.Last().Value.ToString());
        }

        public async Task AddRangeAsync(IEnumerable<MatchPerformance> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public async Task<IQueryable<MatchPerformance>> GetAllAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var performances = await ExecuteReaderAsync("match_performance_getAll", parameters, reader => new MatchPerformance
            {
                MatchPerformanceId = int.Parse(reader["MATCHPERFORMANCEID"].ToString()),
                PlayerId = int.Parse(reader["PLAYERID"].ToString()),
                MatchId = int.Parse(reader["MATCHID"].ToString()),
                Goals = int.Parse(reader["GOALS"].ToString()),
                Assists = int.Parse(reader["ASSISTS"].ToString()),
                MinutesPlayed = int.Parse(reader["MINUTESPLAYED"].ToString()),
                PassAccuracy = decimal.Parse(reader["PASSACCURACY"].ToString()),
                Tackles = int.Parse(reader["TACKLES"].ToString()),
                Interceptions = int.Parse(reader["INTERCEPTIONS"].ToString()),
                Clearances = int.Parse(reader["CLEARANCES"].ToString()),
                Shots = int.Parse(reader["SHOTS"].ToString()),
                ShotsOnTarget = int.Parse(reader["SHOTSONTARGET"].ToString()),
                DribblesCompleted = int.Parse(reader["DRIBBLESCOMPLETED"].ToString()),
                AerialDuelsWon = int.Parse(reader["AERIALDUELSWON"].ToString()),
                YellowCards = int.Parse(reader["YELLOWCARDS"].ToString()),
                RedCards = int.Parse(reader["REDCARDS"].ToString()),
                FoulsCommitted = int.Parse(reader["FOULSCOMMITTED"].ToString()),
                FoulsSuffered = int.Parse(reader["FOULSSUFFERED"].ToString()),
                Offsides = int.Parse(reader["OFFSIDES"].ToString()),
                Saves = int.Parse(reader["SAVES"].ToString()),
                CleanSheets = int.Parse(reader["CLEANSHEETS"].ToString())
            });

            return performances.AsQueryable();
        }

        public async Task<MatchPerformance> GetByIdAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_MatchPerformanceId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("match_performance_getById", parameters, reader => new MatchPerformance
            {
                MatchPerformanceId = int.Parse(reader["MATCHPERFORMANCEID"].ToString()),
                PlayerId = int.Parse(reader["PLAYERID"].ToString()),
                MatchId = int.Parse(reader["MATCHID"].ToString()),
                Goals = int.Parse(reader["GOALS"].ToString()),
                Assists = int.Parse(reader["ASSISTS"].ToString()),
                MinutesPlayed = int.Parse(reader["MINUTESPLAYED"].ToString()),
                PassAccuracy = decimal.Parse(reader["PASSACCURACY"].ToString()),
                Tackles = int.Parse(reader["TACKLES"].ToString()),
                Interceptions = int.Parse(reader["INTERCEPTIONS"].ToString()),
                Clearances = int.Parse(reader["CLEARANCES"].ToString()),
                Shots = int.Parse(reader["SHOTS"].ToString()),
                ShotsOnTarget = int.Parse(reader["SHOTSONTARGET"].ToString()),
                DribblesCompleted = int.Parse(reader["DRIBBLESCOMPLETED"].ToString()),
                AerialDuelsWon = int.Parse(reader["AERIALDUELSWON"].ToString()),
                YellowCards = int.Parse(reader["YELLOWCARDS"].ToString()),
                RedCards = int.Parse(reader["REDCARDS"].ToString()),
                FoulsCommitted = int.Parse(reader["FOULSCOMMITTED"].ToString()),
                FoulsSuffered = int.Parse(reader["FOULSSUFFERED"].ToString()),
                Offsides = int.Parse(reader["OFFSIDES"].ToString()),
                Saves = int.Parse(reader["SAVES"].ToString()),
                CleanSheets = int.Parse(reader["CLEANSHEETS"].ToString())
            });
        }

        public async Task RemoveAsync(MatchPerformance entity)
        {
            Console.WriteLine("This is current situation");
            Console.WriteLine(entity.MatchPerformanceId);
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_MatchPerformanceId", OracleDbType.Int32) { Value = entity.MatchPerformanceId }
            };

            await ExecuteNonQueryAsync("match_performance_delete", parameters);
        }

        public async Task RemoveRangeAsync(IEnumerable<MatchPerformance> entities)
        {
            foreach (var entity in entities)
            {
                await RemoveAsync(entity);
            }
        }

        public async Task UpdateAsync(MatchPerformance entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_MatchPerformanceId", OracleDbType.Int32) { Value = entity.MatchPerformanceId },
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = (object)entity.PlayerId ?? DBNull.Value },
                new OracleParameter("p_MatchId", OracleDbType.Int32) { Value = (object)entity.MatchId ?? DBNull.Value },
                new OracleParameter("p_Goals", OracleDbType.Int32) { Value = entity.Goals },
                new OracleParameter("p_Assists", OracleDbType.Int32) { Value = entity.Assists },
                new OracleParameter("p_MinutesPlayed", OracleDbType.Int32) { Value = entity.MinutesPlayed },
                new OracleParameter("p_PassAccuracy", OracleDbType.Decimal) { Value = (object)entity.PassAccuracy ?? DBNull.Value },
                new OracleParameter("p_Tackles", OracleDbType.Int32) { Value = entity.Tackles },
                new OracleParameter("p_Interceptions", OracleDbType.Int32) { Value = entity.Interceptions },
                new OracleParameter("p_Clearances", OracleDbType.Int32) { Value = entity.Clearances },
                new OracleParameter("p_Shots", OracleDbType.Int32) { Value = entity.Shots },
                new OracleParameter("p_ShotsOnTarget", OracleDbType.Int32) { Value = entity.ShotsOnTarget },
                new OracleParameter("p_DribblesCompleted", OracleDbType.Int32) { Value = entity.DribblesCompleted },
                new OracleParameter("p_AerialDuelsWon", OracleDbType.Int32) { Value = entity.AerialDuelsWon },
                new OracleParameter("p_YellowCards", OracleDbType.Int32) { Value = entity.YellowCards },
                new OracleParameter("p_RedCards", OracleDbType.Int32) { Value = entity.RedCards },
                new OracleParameter("p_FoulsCommitted", OracleDbType.Int32) { Value = entity.FoulsCommitted },
                new OracleParameter("p_FoulsSuffered", OracleDbType.Int32) { Value = entity.FoulsSuffered },
                new OracleParameter("p_Offsides", OracleDbType.Int32) { Value = entity.Offsides },
                new OracleParameter("p_Saves", OracleDbType.Int32) { Value = entity.Saves },
                new OracleParameter("p_CleanSheets", OracleDbType.Int32) { Value = entity.CleanSheets }
            };

            await ExecuteNonQueryAsync("match_performance_update", parameters);
        }
    }
}
