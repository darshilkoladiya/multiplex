using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ServiceCollection = Microsoft.Extensions.DependencyInjection.ServiceCollection;

namespace Multiplex
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterComponents();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
        private static IServiceProvider CreateServices()
        {
            string cString = System.Configuration.ConfigurationManager.ConnectionStrings["MultiplexContext"].ConnectionString;

            // Add common FluentMigrator services
            return new ServiceCollection()
                .AddFluentMigratorCore()
                  .ConfigureRunner(config => config
                    .AddSqlServer()
                    .WithGlobalConnectionString(cString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All()) // define the assembly containing the migrations
                    .AddLogging(config => config.AddFluentMigratorConsole())
                    // Build the service provider
                    .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
