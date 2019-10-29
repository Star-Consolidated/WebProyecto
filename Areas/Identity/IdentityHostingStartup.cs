using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebProyecto.Areas.Identity.Data;

[assembly: HostingStartup(typeof(WebProyecto.Areas.Identity.IdentityHostingStartup))]
namespace WebProyecto.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WebProyectoIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityConnection")));

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<WebProyectoIdentityDbContext>();
            });
        }
    }
}