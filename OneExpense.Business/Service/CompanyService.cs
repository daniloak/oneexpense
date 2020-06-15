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

        public async Task<bool> Add(Company company)
        {
            _companyRepository.Add(company);

            return await _companyRepository.UnitOfWork.Commit();
        }

        public Task<bool> Update(Company company)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _companyRepository?.Dispose();
        }
    }
}
