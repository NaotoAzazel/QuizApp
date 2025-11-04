using QuizApp.Models;

namespace QuizApp.Services.Repositories
{
    public class QuizCategoryRepository : Repository<Category>, IRepository<Category>
    {
        private readonly DatabaseContext _context;

        public QuizCategoryRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var category = Get(id);
            if (category != null)
            {
                Delete(category);
            }
        }
    }
}
