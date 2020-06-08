using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace OneExpense.API.Interfaces
{
    public interface ICompanyUserService
    {
        Guid UserId { get; }
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
