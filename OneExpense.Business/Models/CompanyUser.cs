
using Microsoft.AspNetCore.Identity;
using System;

namespace OneExpense.Business.Models
{
    public class CompanyUser : IdentityUser
    {
        public Guid CompanyId { get; set; }
    }
}
