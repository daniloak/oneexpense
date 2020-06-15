using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface ICompanyService : IDisposable
    {
        Task<bool> Add(Company company);
        Task<bool> Update(Company company);
        Task<bool> Delete(Guid id);
    }
}
