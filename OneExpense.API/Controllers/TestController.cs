using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace OneExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : MainController
    {
        public string MyProperty { get; set; }
        public TestController(IConfiguration configuration)
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