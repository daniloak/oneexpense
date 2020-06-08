using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using System;
using System.Threading.Tasks;

namespace OneExpense.Business.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task Add(Company company)
        {
            await _companyRepository.Add(company);
        }

        public Task Update(Company company)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _companyRepository?.Dispose();
        }
    }
}
