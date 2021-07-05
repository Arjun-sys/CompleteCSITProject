using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCSIT.Areas.Identity.Data;
using EasyCSIT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyCSIT.Data
{
    public class LoginRegistrationDbContext : IdentityDbContext<LoginRegistrationUser>
    {
        public LoginRegistrationDbContext(DbContextOptions<LoginRegistrationDbContext> options)
            : base(options)
        {
        }
     
      
    }
}
