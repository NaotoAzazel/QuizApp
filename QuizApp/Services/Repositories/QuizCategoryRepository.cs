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

        public Category? GetByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Name == name);
        }

        public void SoftDelete(Category category)
        {
            category.IsDeleted = true;
            Update(category);
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
