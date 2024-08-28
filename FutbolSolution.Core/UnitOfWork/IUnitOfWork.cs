using System.Threading.Tasks;

namespace FutbolSolution.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
