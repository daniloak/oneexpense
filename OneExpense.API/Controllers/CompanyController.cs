using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneExpense.API.Interfaces;
using OneExpense.API.ViewModel;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using OneExpense.Business.Service;

namespace OneExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : MainController
    {
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        public CompanyController(INotifier notifier,
                                 IMapper mapper,
                                 ICompanyService companyService,
                                 ICompanyUserService appUser) : base(appUser, notifier)
        {
            _mapper = mapper;
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<ActionResult<CompanyViewModel>> Add(CompanyViewModel companyViewModel)
        {
            var company = _mapper.Map<Company>(companyViewModel);

            await _companyService.Add(company);

            return NoContent();
        }
    }
}