using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OneExpense.API.Interfaces;

namespace OneExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : MainController
    {
        public string MyProperty { get; set; }
        public TestController(IConfiguration configuration,
            ICompanyUserService appUser) : base(appUser)
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