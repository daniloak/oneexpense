﻿using AutoMapper;
using Flurl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OneExpense.API.ViewModel;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OneExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseReportDetailController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IExpenseReportDetailRepository _expenseReportDetailRepository;
        private readonly IExpenseReportDetailService _expenseReportDetailService;
        private readonly IImageFileService _imageFileService;
        private readonly IConfiguration _configuration;

        public ExpenseReportDetailController(IMapper mapper,
                                             IExpenseReportDetailRepository expenseReportDetailRepository,
                                             IExpenseReportDetailService expenseReportDetailService,
                                             IImageFileService imageFileService,
                                             IConfiguration configuration)
        {
            _mapper = mapper;
            _expenseReportDetailRepository = expenseReportDetailRepository;
            _expenseReportDetailService = expenseReportDetailService;
            _imageFileService = imageFileService;
            _configuration = configuration;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ExpenseReportDetailViewModel>> GetById(Guid id)
        {
            var expense = _mapper.Map<ExpenseReportDetailViewModel>(await _expenseReportDetailRepository.GetById(id));
            var blobURL = _configuration.GetValue<string>("Azure:AZURE_BLOB_URL");
            expense.Image = blobURL.AppendPathSegment(expense.Image);

            if (expense == null) return NotFound();

            return expense;
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseReportDetailViewModel>> Add(ExpenseReportDetailViewModel expenseReportViewModel)
        {
            var imageName = $"{Guid.NewGuid()}_{expenseReportViewModel.Image}";
            await _imageFileService.Upload(expenseReportViewModel.ImageUpload, imageName);

            expenseReportViewModel.Image = imageName;

            var expense = _mapper.Map<ExpenseReportDetail>(expenseReportViewModel);
            await _expenseReportDetailService.Add(expense);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ExpenseReportDetailViewModel expenseReportDetailViewModel)
        {
            await _expenseReportDetailService.Update(_mapper.Map<ExpenseReportDetail>(expenseReportDetailViewModel));

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var expenseReportDetailViewModel = _mapper.Map<ExpenseReportDetailViewModel>(await _expenseReportDetailRepository.GetById(id));

            if (expenseReportDetailViewModel == null) return NotFound();

            await _expenseReportDetailService.Delete(id);

            return NoContent();
        }
    }
}