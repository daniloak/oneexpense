using AutoMapper;
using Flurl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OneExpense.API.Authorization;
using OneExpense.API.Interfaces;
using OneExpense.API.ViewModel;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseReportController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IExpenseReportRepository _expenseReportRepository;
        private readonly IExpenseReportService _expenseReportService;
        private readonly IImageFileService _imageFileService;
        private readonly IConfiguration _configuration;
        private readonly IAuthorizationService _authorizationService;

        public ExpenseReportController(IMapper mapper,
                                       IExpenseReportRepository expenseRepository,
                                       IExpenseReportService expenseService,
                                       IImageFileService imageFileService,
                                       IConfiguration configuration,
                                       ICompanyUserService appUser,
                                       IAuthorizationService authorizationService) : base(appUser)
        {
            _mapper = mapper;
            _expenseReportRepository = expenseRepository;
            _expenseReportService = expenseService;
            _imageFileService = imageFileService;
            _configuration = configuration;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<ExpenseReportViewModel>> GetAll()
        {
            var expenses = _mapper.Map<IEnumerable<ExpenseReportViewModel>>(await _expenseReportRepository.GetCompleteExpenseReport(UserId));

            var blobURL = _configuration.GetValue<string>("Azure:AZURE_BLOB_URL");

            foreach (var expenseDetail in expenses.SelectMany(p => p.Details).Where(p => p.Image != null))
            {
                expenseDetail.Image = blobURL.AppendPathSegment(expenseDetail.Image);
            }

            return expenses;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ExpenseReportViewModel>> GetById(Guid id)
        {
            var expense = await _expenseReportRepository.GetCompleteExpenseReportById(id, UserId);
            if (expense == null) return BadRequest();

            var authResult = await _authorizationService.AuthorizeAsync(User, expense, ExpenseAuthorizationHandler.CAN_ACESS_EXPENSE);
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }
            
            var blobURL = _configuration.GetValue<string>("Azure:AZURE_BLOB_URL");

            foreach (var expenseDetail in expense.Details.Where(p => p.Image != null))
            {
                expenseDetail.Image = blobURL.AppendPathSegment(expenseDetail.Image);
            }

            var expenseViewModel = _mapper.Map<ExpenseReportViewModel>(expense);

            return expenseViewModel;
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseReportViewModel>> Add(ExpenseReportViewModel expenseReportViewModel)
        {
            foreach (var expenseReportDetail in expenseReportViewModel.Details.Where(p => p.Image != null))
            {
                var imageName = $"{Guid.NewGuid()}_{expenseReportDetail.Image}";
                await _imageFileService.Upload(expenseReportDetail.ImageUpload, imageName);

                expenseReportDetail.Image = imageName;
            }

            var expense = _mapper.Map<ExpenseReport>(expenseReportViewModel);
            expense.UserId = UserId;

            await _expenseReportService.Add(expense);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ExpenseReportViewModel expenseReportViewModel)
        {
            var expense = await _expenseReportRepository.GetCompleteExpenseReportById(id, UserId);
            if (expense == null) return BadRequest();

            var authResult = await _authorizationService.AuthorizeAsync(User, expense, ExpenseAuthorizationHandler.CAN_ACESS_EXPENSE);
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            var expenseToUpdate = _mapper.Map<ExpenseReport>(expenseReportViewModel);

            await _expenseReportService.Update(expenseToUpdate);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var expense = await _expenseReportRepository.GetCompleteExpenseReportById(id, UserId);
            if (expense == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, expense, ExpenseAuthorizationHandler.CAN_ACESS_EXPENSE);
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            await _expenseReportService.Delete(id);

            return NoContent();
        }
    }
}