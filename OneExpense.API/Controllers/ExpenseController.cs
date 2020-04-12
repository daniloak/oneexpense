using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OneExpense.API.ViewModel;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseService _expenseService;

        public ExpenseController(IMapper mapper,
                                IExpenseRepository expenseRepository,
                                IExpenseService expenseService)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
            _expenseService = expenseService;
        }
            
        [HttpGet]
        public async Task<IEnumerable<ExpenseViewModel>> GetAll()
        {
            var expenses = _mapper.Map<IEnumerable<ExpenseViewModel>>(await _expenseRepository.GetAll());

            return expenses;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ExpenseViewModel>> GetById(Guid id)
        {
            var expense = _mapper.Map<ExpenseViewModel>(await _expenseRepository.GetById(id));

            if (expense == null) return NotFound();

            return expense;
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseViewModel>> Add(ExpenseViewModel expenseViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var expense = _mapper.Map<Expense>(expenseViewModel);
            await _expenseService.Add(expense);

            return Ok();
        }
    }
}