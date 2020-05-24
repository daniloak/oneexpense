using AutoMapper;
using OneExpense.API.ViewModel;
using OneExpense.Business.Models;

namespace OneExpense.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ExpenseReport, ExpenseReportViewModel>().ReverseMap();
            CreateMap<ExpenseReportDetail, ExpenseReportDetailViewModel>().ReverseMap();
        }
    }
}
