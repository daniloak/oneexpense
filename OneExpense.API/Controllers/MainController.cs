using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OneExpense.API.Interfaces;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneExpense.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        public readonly ICompanyUserService AppUser;

        protected Guid UserId => AppUser.GetUserId();

        public MainController(ICompanyUserService appUser,
                              INotifier notifier)
        {
            AppUser = appUser;
            _notifier = notifier;
        }

        protected ActionResult ApiResponse(object result = null)
        {
            if (IsValid())
            {
                return Ok(result);
            }

            ModelStateDictionary errors = new ModelStateDictionary();
            foreach (var notification in _notifier.GetNotifications())
            {
                errors.AddModelError(notification.Property, notification.Message);
            }

            return BadRequest(new ValidationProblemDetails(errors));
        }

        protected bool IsValid() => !_notifier.NotifactionExists();
        protected void AddError(string property, string error) => _notifier.Handle(new Notification(property, error));
    }
}
