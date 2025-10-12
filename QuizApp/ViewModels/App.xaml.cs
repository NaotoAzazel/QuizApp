using Microsoft.Extensions.Configuration;
using QuizApp.Services;
using System.IO;
using System.Windows;

namespace QuizApp
{
    public partial class App : Application
    {
        public static IConfigurationRoot Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                .Build();

            var dbContext = new DatabaseContext();
            dbContext.Database.EnsureCreated();

            MessageBox.Show($"Database initialized successfully in {environmentName} environment!");
        }
    }
}
