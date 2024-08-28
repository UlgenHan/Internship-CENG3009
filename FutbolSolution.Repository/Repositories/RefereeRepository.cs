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
    public class RefereeRepository : BaseRepository, IRefereeRepository
    {
        public RefereeRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(Referee entity)
        {
            try
            {
                var parameters = new OracleParameter[]
                {
                    new OracleParameter("p_Name", OracleDbType.Varchar2) { Value = entity.Name },
                    new OracleParameter("p_Surname", OracleDbType.Varchar2) { Value = entity.Surname },
                    new OracleParameter("p_Nationality", OracleDbType.Varchar2) { Value = entity.Nationality },
                    new OracleParameter("p_ExperienceYears", OracleDbType.Int32) { Value = (object)entity.ExperienceYears ?? DBNull.Value },
                    new OracleParameter("p_Bias", OracleDbType.Int32) { Value = (object)entity.Bias ?? DBNull.Value },
                    new OracleParameter("p_RefereeId", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                await ExecuteNonQueryAsync("football_refereetable_create", parameters);

                // entity.RefereeId = Convert.ToInt32(parameters.Last().Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddRangeAsync(IEnumerable<Referee> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    await AddAsync(entity);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IQueryable<Referee>> GetAllAsync()
        {
            try
            {
                var parameters = new OracleParameter[]
                {
                    new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                var referees = await ExecuteReaderAsync("football_refereetable_getAll", parameters, reader => new Referee
                {
                    RefereeId = Convert.ToInt32(reader["REFEREEID"]),
                    Name = reader["NAME"].ToString(),
                    Surname = reader["SURNAME"].ToString(),
                    Nationality = reader["NATIONALITY"].ToString(),
                    ExperienceYears = decimal.Parse(reader["EXPERIENCEYEARS"].ToString()),
                    Bias = decimal.Parse(reader["BIAS"].ToString())
                });

                return referees.AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Referee> GetByIdAsync(int id)
        {
            try
            {
                var parameters = new OracleParameter[]
                {
                    new OracleParameter("p_RefereeId", OracleDbType.Int32) { Value = id },
                    new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                return await ExecuteReaderSingleAsync("football_refereetable_getById", parameters, reader => new Referee
                {
                    RefereeId = Convert.ToInt32(reader["REFEREEID"]),
                    Name = reader["NAME"].ToString(),
                    Surname = reader["SURNAME"].ToString(),
                    Nationality = reader["NATIONALITY"].ToString(),
                    ExperienceYears = decimal.Parse(reader["EXPERIENCEYEARS"].ToString()),
                    Bias = decimal.Parse(reader["BIAS"].ToString())
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveAsync(Referee entity)
        {
            try
            {
                var parameters = new OracleParameter[]
                {
                    new OracleParameter("p_RefereeId", OracleDbType.Int32) { Value = entity.RefereeId }
                };

                await ExecuteNonQueryAsync("football_refereetable_delete", parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<Referee> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    await RemoveAsync(entity);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Referee entity)
        {
            try
            {
                var parameters = new OracleParameter[]
                {
                    new OracleParameter("p_RefereeId", OracleDbType.Int32) { Value = entity.RefereeId },
                    new OracleParameter("p_Name", OracleDbType.Varchar2) { Value = entity.Name },
                    new OracleParameter("p_Surname", OracleDbType.Varchar2) { Value = entity.Surname },
                    new OracleParameter("p_Nationality", OracleDbType.Varchar2) { Value = entity.Nationality },
                    new OracleParameter("p_ExperienceYears", OracleDbType.Int32) { Value = (object)entity.ExperienceYears ?? DBNull.Value },
                    new OracleParameter("p_Bias", OracleDbType.Int32) { Value = (object)entity.Bias ?? DBNull.Value }
                };

                await ExecuteNonQueryAsync("football_refereetable_update", parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
