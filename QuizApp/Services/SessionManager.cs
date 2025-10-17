using QuizApp.Models;

namespace QuizApp.Services
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; private set; }

        public static void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }

        public static void Clear()
        {
            CurrentUser = null;
        }

        public static bool IsAuthenticated => CurrentUser != null;
    }
}
