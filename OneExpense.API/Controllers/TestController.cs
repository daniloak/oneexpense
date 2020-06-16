using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OneExpense.API.Interfaces;
using OneExpense.Business.Interfaces;

namespace OneExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : MainController
    {
        public string MyProperty { get; set; }
        public TestController(INotifier notifier, IConfiguration configuration,
            ICompanyUserService appUser) : base(appUser, notifier)
        {
            MyProperty = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public string Index()
        {
            return "Hello World " + MyProperty;
        }

    }
}