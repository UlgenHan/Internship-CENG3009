using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Repositories
{
    public interface IInjuriesSuspensionsRepository : IGenericRepository<InjuriesSuspensions>
    {
        Task AddInjuresLinkAsync(InjuriesLink entity);
        Task UpdateInjuresLinkAsync(InjuriesLink entity);
        Task RemoveInjuresLinkAsync(int id);
        Task<IQueryable<InjuriesLink>> GetAllInjuresLinkAsync();
        Task<InjuriesLink> GetInjuresLinkByIdAsync(int id);
    }
}
