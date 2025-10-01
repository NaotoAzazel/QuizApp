using QuizApp.Services;
using System.Windows;

namespace QuizApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // TODO: add loading the config file
            Database database = Database.Instance;
            database.Connect("Server=localhost;Port=PORT;Database=DATABASE; User Id = USER; Password=PASSWORD;"); // TODO: get connection string from config file
        }
    }

}
