using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OneExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : MainController
    {
        [HttpGet]
        public string Index()
        {
            return "Hello World";
        }
    }
}