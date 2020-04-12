using AutoMapper;
using OneExpense.API.ViewModel;
using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneExpense.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Expense, ExpenseViewModel>().ReverseMap();
        }
    }
}
