using System;
using EasyCSIT.Areas.Identity.Data;
using EasyCSIT.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EasyCSIT.Areas.Identity.IdentityHostingStartup))]
namespace EasyCSIT.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<LoginRegistrationDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LoginRegistrationDbContextConnection")));

                services.AddDefaultIdentity<LoginRegistrationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LoginRegistrationDbContext>();
            });
        }
    }
}