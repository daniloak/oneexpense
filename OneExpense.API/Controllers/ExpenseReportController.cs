using AutoMapper;
using Azure.Storage.Blobs;
using Flurl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OneExpense.API.ViewModel;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OneExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseReportController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IExpenseReportRepository _expenseReportRepository;
        private readonly IExpenseReportService _expenseReportService;
        private readonly IImageFileService _imageFileService;
        private readonly IConfiguration _configuration;

        public ExpenseReportController(IMapper mapper,
                                       IExpenseReportRepository expenseRepository,
                                       IExpenseReportService expenseService,
                                       IImageFileService imageFileService,
                                       IConfiguration configuration)
        {
            _mapper = mapper;
            _expenseReportRepository = expenseRepository;
            _expenseReportService = expenseService;
            _imageFileService = imageFileService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<ExpenseReportViewModel>> GetAll()
        {
            var expenses = _mapper.Map<IEnumerable<ExpenseReportViewModel>>(await _expenseReportRepository.GetCompleteExpenseReport());

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
            var expense = _mapper.Map<ExpenseReportViewModel>(await _expenseReportRepository.GetCompleteExpenseReportById(id));

            var blobURL = _configuration.GetValue<string>("Azure:AZURE_BLOB_URL");

            foreach (var expenseDetail in expense.Details.Where(p => p.Image != null))
            {
                expenseDetail.Image = blobURL.AppendPathSegment(expenseDetail.Image);
            }

            if (expense == null) return NotFound();

            return expense;
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

            await _expenseReportService.Add(expense);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ExpenseReportViewModel expenseReportViewModel)
        {
            await _expenseReportService.Update(_mapper.Map<ExpenseReport>(expenseReportViewModel));

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var expenseReportViewModel = _mapper.Map<ExpenseReportViewModel>(await _expenseReportRepository.GetCompleteExpenseReportById(id));

            if (expenseReportViewModel == null) return NotFound();

            await _expenseReportService.Delete(id);

            return NoContent();
        }
    }
}