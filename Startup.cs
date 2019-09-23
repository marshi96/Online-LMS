using System;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using LibraryManagementSystem.Filters;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LibraryManagementSystem.Startup))]

namespace LibraryManagementSystem
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            }, new SqlServerStorage("LibraryManagementSystem"));

            GlobalConfiguration.Configuration
            .UseSqlServerStorage("LibraryManagementSystem");

            var options = new BackgroundJobServerOptions
            {
                Queues = new[] { "default" }
            };

            app.UseHangfireServer(options);
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }

        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                return true;
            }
        }
    }
}
