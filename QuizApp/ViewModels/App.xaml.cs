using QuizApp.Services;
using System.Windows;

namespace QuizApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var dbContext = new DatabaseContext();
            dbContext.Database.EnsureCreated();

            MessageBox.Show("Database initialized successfully!");
        }


    }

}
