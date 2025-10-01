using Npgsql;

namespace QuizApp.Services
{
    internal class Database
    {
        private Database() { }
        private static Database _instance;

        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        public async void Connect(string connectionString)
        {
            try
            {
                NpgsqlConnection sqlConnection = new NpgsqlConnection(connectionString);
                sqlConnection.Open();
                Console.WriteLine("Successfully connected to database");
            } catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }

        public void Get<T>(T item) { }

        public void Create<T>(T item) { }

        public void Update<T>(T item) { }

        public void Delete<T>(T item) { }
    }
}
