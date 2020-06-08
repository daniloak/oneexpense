using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface ICompanyService : IDisposable
    {
        Task Add(Company company);
        Task Update(Company company);
        Task Delete(Guid id);
    }
}
