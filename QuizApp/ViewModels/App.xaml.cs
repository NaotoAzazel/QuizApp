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

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // TODO: load config file considering the currently environment
                .AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true)
                .Build();

            var dbContext = new DatabaseContext();
            dbContext.Database.EnsureCreated();

            MessageBox.Show("Database initialized successfully!");
        }
    }
}
