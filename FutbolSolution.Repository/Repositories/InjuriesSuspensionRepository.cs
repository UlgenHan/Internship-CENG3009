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
    public class InjuriesSuspensionsRepository : BaseRepository, IInjuriesSuspensionsRepository
    {
        public InjuriesSuspensionsRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(InjuriesSuspensions entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = (object)entity.PlayerId ?? DBNull.Value },
                new OracleParameter("p_Type", OracleDbType.Varchar2) { Value = entity.Type },
                new OracleParameter("p_Description", OracleDbType.Varchar2) { Value = entity.Description },
                new OracleParameter("p_StartDate", OracleDbType.Date) { Value = (object)entity.StartDate ?? DBNull.Value },
                new OracleParameter("p_EndDate", OracleDbType.Date) { Value = (object)entity.EndDate ?? DBNull.Value },
                new OracleParameter("p_InjurySuspensionId", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                }
            };

            await ExecuteNonQueryAsync("injuries_suspensions_create", parameters);

            entity.InjurySuspensionId = int.Parse(parameters.Last().Value.ToString());
        }

        public async Task AddRangeAsync(IEnumerable<InjuriesSuspensions> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public async Task<IQueryable<InjuriesSuspensions>> GetAllAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var injuriesSuspensions = await ExecuteReaderAsync("injuries_suspensions_getAll", parameters, reader => new InjuriesSuspensions
            {
                InjurySuspensionId = Convert.ToInt32(reader["INJURYSUSPENSIONID"]),
                PlayerId = int.Parse(reader["PLAYERID"].ToString()),
                Type = reader["TYPE"].ToString(),
                Description = reader["DESCRIPTION"].ToString(),
                StartDate = reader["STARTDATE"] as DateTime?,
                EndDate = reader["ENDDATE"] as DateTime?
            });

            return injuriesSuspensions.AsQueryable();
        }

        public async Task<InjuriesSuspensions> GetByIdAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_InjurySuspensionId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("injuries_suspensions_getById", parameters, reader => new InjuriesSuspensions
            {
                InjurySuspensionId = Convert.ToInt32(reader["INJURYSUSPENSIONID"]),
                PlayerId = int.Parse(reader["PLAYERID"].ToString()),
                Type = reader["TYPE"].ToString(),
                Description = reader["DESCRIPTION"].ToString(),
                StartDate = reader["STARTDATE"] as DateTime?,
                EndDate = reader["ENDDATE"] as DateTime?
            });
        }

        public async Task RemoveAsync(InjuriesSuspensions entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_InjurySuspensionId", OracleDbType.Int32) { Value = entity.InjurySuspensionId }
            };

            await ExecuteNonQueryAsync("injuries_suspensions_delete", parameters);
        }

        public async Task RemoveRangeAsync(IEnumerable<InjuriesSuspensions> entities)
        {
            foreach (var entity in entities)
            {
                await RemoveAsync(entity);
            }
        }

        public async Task UpdateAsync(InjuriesSuspensions entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_InjurySuspensionId", OracleDbType.Int32) { Value = entity.InjurySuspensionId },
                new OracleParameter("p_PlayerId", OracleDbType.Int32) { Value = (object)entity.PlayerId ?? DBNull.Value },
                new OracleParameter("p_Type", OracleDbType.Varchar2) { Value = entity.Type },
                new OracleParameter("p_Description", OracleDbType.Varchar2) { Value = entity.Description },
                new OracleParameter("p_StartDate", OracleDbType.Date) { Value = (object)entity.StartDate ?? DBNull.Value },
                new OracleParameter("p_EndDate", OracleDbType.Date) { Value = (object)entity.EndDate ?? DBNull.Value }
            };

            await ExecuteNonQueryAsync("injuries_suspensions_update", parameters);
        }


        public async Task AddInjuresLinkAsync(InjuriesLink entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_InjuriesSuspensionId", OracleDbType.Int32) { Value = entity.InjuriesSuspensionId },
                new OracleParameter("p_Severity", OracleDbType.Int32) { Value = entity.Severity },
                new OracleParameter("p_InjuresLinkId", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                }
            };

            await ExecuteNonQueryAsync("SP_CREATEINJURESLINK", parameters);

            entity.Id = int.Parse(parameters.Last().Value.ToString());
        }

        public async Task UpdateInjuresLinkAsync(InjuriesLink entity)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_InjuresLinkId", OracleDbType.Int32) { Value = entity.Id },
                new OracleParameter("p_InjuriesSuspensionId", OracleDbType.Int32) { Value = entity.InjuriesSuspensionId },
                new OracleParameter("p_Severity", OracleDbType.Int32) { Value = entity.Severity }
            };

            await ExecuteNonQueryAsync("SP_UPDATEINJURESLINK", parameters);
        }

        public async Task RemoveInjuresLinkAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_InjuresLinkId", OracleDbType.Int32) { Value = id }
            };

            await ExecuteNonQueryAsync("SP_DELETEINJURESLINK", parameters);
        }

        public async Task<IQueryable<InjuriesLink>> GetAllInjuresLinkAsync()
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            var injuriesLinks = await ExecuteReaderAsync("SP_GETALLINJURESLINK", parameters, reader => new InjuriesLink
            {
                Id = Convert.ToInt32(reader["INJURESLINKID"]),
                InjuriesSuspensionId = Convert.ToInt32(reader["INJURYSUSPENSIONID"]),
                Severity = Convert.ToInt32(reader["SEVERITY"])
            });

            return injuriesLinks.AsQueryable();
        }

        public async Task<InjuriesLink> GetInjuresLinkByIdAsync(int id)
        {
            var parameters = new OracleParameter[]
            {
                new OracleParameter("p_InjuresLinkId", OracleDbType.Int32) { Value = id },
                new OracleParameter("p_Cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return await ExecuteReaderSingleAsync("SP_GETINJURESLINKBYID", parameters, reader => new InjuriesLink
            {
                Id = Convert.ToInt32(reader["INJURESLINKID"]),
                InjuriesSuspensionId = Convert.ToInt32(reader["INJURYSUSPENSIONID"]),
                Severity = Convert.ToInt32(reader["SEVERITY"])
            });
        }
    }
}
