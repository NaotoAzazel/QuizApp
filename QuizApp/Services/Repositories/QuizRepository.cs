using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Services.Repositories
{
    public class QuizRepository : Repository<Question>, IRepository<Question>
    {
        private readonly DatabaseContext _context;

        public QuizRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public List<Question> GetByCategory(Category category)
        {
            return _context.Questions
                .Include(q => q.Options)
                .Include(q => q.Category)
                .Where(q => q.CategoryId == category.Id)
                .ToList();
        }

        public List<Question> GetAllQuestions()
        {
            return _context.Questions
                .Include(q => q.Options)
                .Include(q => q.Category)
                .ToList();
        }

        public List<Question> GetRandomQuestionsByCategory(Category? category = null, int count = 20)
        {
            var query = _context.Questions
                .Include(q => q.Options)
                .Include(q => q.Category)
                .AsQueryable();

            if (category != null)
            {
                query = query.Where(q => q.CategoryId == category.Id);
            }

            return query
                .OrderBy(q => Guid.NewGuid())
                .Take(count)
                .ToList();
        }

        public List<Question> GetRandomQuestions(int count)
        {
            return _context.Questions
                .OrderBy(q => Guid.NewGuid())
                .Take(count)
                .ToList();
        }

        public void Delete(int id)
        {
            var entity = _context.Questions.Find(id);
            if (entity != null)
            {
                _context.Questions.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
