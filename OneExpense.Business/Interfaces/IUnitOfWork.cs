using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
