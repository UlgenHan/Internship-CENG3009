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
    public class PlayerStatsRepository : BaseRepository, IPlayerStatsRepository
    {
        public PlayerStatsRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(PlayerStats entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Goals", OracleDbType.Int32) { Value = entity.Goals },
                new OracleParameter("p_Assists", OracleDbType.Int32) { Value = entity.Assists },
                new OracleParameter("p_TotalMinutesIn", OracleDbType.Int32) { Value = entity.TotalMinutesIn },
                new OracleParameter("p_PassAccuracy", OracleDbType.Decimal) { Value = entity.PassAccuracy },
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
                new OracleParameter("p_PlayerStatsId", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                }
            };

            await ExecuteNonQueryAsync("football_playerstatstablev_create", parameters);

            entity.PlayerStatsId = int.Parse(parameters.Last().Value.ToString()); 
        }

        public async Task AddRangeAsync(IEnumerable<PlayerStats> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public async Task<IQueryable<PlayerStats>> GetAllAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var playerStatsList = await ExecuteReaderAsync("football_playerstatstablev_getAll", parameters, reader => new PlayerStats
            {
                PlayerStatsId = Convert.ToInt32(reader["PLAYERSTATSID"]),
                Goals = Convert.ToInt32(reader["GOALS"]),
                Assists = Convert.ToInt32(reader["ASSISTS"]),
                TotalMinutesIn = Convert.ToInt32(reader["TOTALMINUTESIN"]),
                PassAccuracy = reader["PASSACCURACY"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["PASSACCURACY"]) : null,
                Tackles = Convert.ToInt32(reader["TACKLES"]),
                Interceptions = Convert.ToInt32(reader["INTERCEPTIONS"]),
                Clearances = Convert.ToInt32(reader["CLEARANCES"]),
                Shots = Convert.ToInt32(reader["SHOTS"]),
                ShotsOnTarget = Convert.ToInt32(reader["SHOTSONTARGET"]),
                DribblesCompleted = Convert.ToInt32(reader["DRIBBLESCOMPLETED"]),
                AerialDuelsWon = Convert.ToInt32(reader["AERIALDUELSWON"]),
                YellowCards = Convert.ToInt32(reader["YELLOWCARDS"]),
                RedCards = Convert.ToInt32(reader["REDCARDS"]),
                FoulsCommitted = Convert.ToInt32(reader["FOULSCOMMITTED"]),
                FoulsSuffered = Convert.ToInt32(reader["FOULSSUFFERED"]),
                Offsides = Convert.ToInt32(reader["OFFSIDES"]),
                Saves = Convert.ToInt32(reader["SAVES"]),
                CleanSheets = Convert.ToInt32(reader["CLEANSHEETS"])
            });

            return playerStatsList.AsQueryable();
        }

        public async Task<PlayerStats> GetByIdAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerStatsId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("football_playerstatstablev_getById", parameters, reader => new PlayerStats
            {
                PlayerStatsId = Convert.ToInt32(reader["PLAYERSTATSID"]),
                Goals = Convert.ToInt32(reader["GOALS"]),
                Assists = Convert.ToInt32(reader["ASSISTS"]),
                TotalMinutesIn = Convert.ToInt32(reader["TOTALMINUTESIN"]),
                PassAccuracy = reader["PASSACCURACY"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["PASSACCURACY"]) : null,
                Tackles = Convert.ToInt32(reader["TACKLES"]),
                Interceptions = Convert.ToInt32(reader["INTERCEPTIONS"]),
                Clearances = Convert.ToInt32(reader["CLEARANCES"]),
                Shots = Convert.ToInt32(reader["SHOTS"]),
                ShotsOnTarget = Convert.ToInt32(reader["SHOTSONTARGET"]),
                DribblesCompleted = Convert.ToInt32(reader["DRIBBLESCOMPLETED"]),
                AerialDuelsWon = Convert.ToInt32(reader["AERIALDUELSWON"]),
                YellowCards = Convert.ToInt32(reader["YELLOWCARDS"]),
                RedCards = Convert.ToInt32(reader["REDCARDS"]),
                FoulsCommitted = Convert.ToInt32(reader["FOULSCOMMITTED"]),
                FoulsSuffered = Convert.ToInt32(reader["FOULSSUFFERED"]),
                Offsides = Convert.ToInt32(reader["OFFSIDES"]),
                Saves = Convert.ToInt32(reader["SAVES"]),
                CleanSheets = Convert.ToInt32(reader["CLEANSHEETS"])
            });
        }

        public async Task RemoveAsync(PlayerStats entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerStatsId", OracleDbType.Int32) { Value = entity.PlayerStatsId }
            };

            await ExecuteNonQueryAsync("football_playerstatstablev_delete", parameters);
        }

        public async Task RemoveRangeAsync(IEnumerable<PlayerStats> entities)
        {
            foreach (var entity in entities)
            {
                await RemoveAsync(entity);
            }
        }

        public async Task UpdateAsync(PlayerStats entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerStatsId", OracleDbType.Int32) { Value = entity.PlayerStatsId },
                new OracleParameter("p_Goals", OracleDbType.Int32) { Value = entity.Goals },
                new OracleParameter("p_Assists", OracleDbType.Int32) { Value = entity.Assists },
                new OracleParameter("p_TotalMinutesIn", OracleDbType.Int32) { Value = entity.TotalMinutesIn },
                new OracleParameter("p_PassAccuracy", OracleDbType.Decimal) { Value = entity.PassAccuracy },
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

            await ExecuteNonQueryAsync("football_playerstatstablev_update", parameters);
        }
    }
}
