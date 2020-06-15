using AutoMapper;
using Flurl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OneExpense.API.Interfaces;
using OneExpense.API.ViewModel;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using System;
using System.Threading.Tasks;

namespace OneExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                                             IConfiguration configuration,
                                             ICompanyUserService appUser) : base(appUser)
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

            if (expense == null) return BadRequest();

            return expense;
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseReportDetailViewModel>> Add(ExpenseReportDetailViewModel expenseReportViewModel)
        {
            var imageName = $"{Guid.NewGuid()}_{expenseReportViewModel.Image}";
            await _imageFileService.Upload(expenseReportViewModel.ImageUpload, imageName);

            expenseReportViewModel.Image = imageName;

            var expense = _mapper.Map<ExpenseReportDetail>(expenseReportViewModel);
            var success = await _expenseReportDetailService.Add(expense);

            if (!success) return BadRequest();

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ExpenseReportDetailViewModel expenseReportDetailViewModel)
        {
            var success =  await _expenseReportDetailService.Update(_mapper.Map<ExpenseReportDetail>(expenseReportDetailViewModel));

            if (!success) return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var expenseReportDetailViewModel = _mapper.Map<ExpenseReportDetailViewModel>(await _expenseReportDetailRepository.GetById(id));

            if (expenseReportDetailViewModel == null) return NotFound();

            var success =  await _expenseReportDetailService.Delete(id);

            if (!success) return BadRequest();

            return NoContent();
        }
    }
}