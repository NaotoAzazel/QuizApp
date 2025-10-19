using QuizApp.Models;

namespace QuizApp.Services.Repositories
{
    public class UserQuizResultRepository : Repository<UserQuizResult>, IRepository<UserQuizResult>
    {
        private readonly DatabaseContext _context;

        public UserQuizResultRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var entity = _context.UserQuizResult.Find(id);
            if (entity != null)
            {
                _context.UserQuizResult.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
