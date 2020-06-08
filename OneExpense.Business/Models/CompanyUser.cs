
using Microsoft.AspNetCore.Identity;
using System;

namespace OneExpense.Business.Models
{
    public class CompanyUser : IdentityUser
    {
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public Guid CompanyId { get; set; }
    }
}
