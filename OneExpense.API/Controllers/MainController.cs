using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OneExpense.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneExpense.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        public readonly ICompanyUserService AppUser;
        protected ICollection<string> Errors = new List<string>();

        protected Guid UserId => AppUser.GetUserId(); 

        public MainController(ICompanyUserService appUser)
        {
            AppUser = appUser;
        }

        protected ActionResult ApiResponse(object result = null)
        {
            if (IsValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Details", Errors.ToArray() }
            }));
        }

        protected ActionResult ApiResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return ApiResponse();
        }

        protected bool IsValid() => !Errors.Any();
        protected void AddError(string error) => Errors.Add(error);
        protected void ClearErrors() => Errors.Clear();
    }
}
