using QuizApp.Models;

namespace QuizApp.Services.Repositories
{
    public class AnswerOptionRepository : Repository<AnswerOption>, IRepository<AnswerOption>
    {
        private readonly DatabaseContext _context;

        public AnswerOptionRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public List<AnswerOption> GetAnswersToQuestion(Question question)
        {
            return _context.AnswerOptions
                .Where(a => a.QuestionId == question.Id)
                .ToList();
        }

        public void Delete(int id)
        {
            var entity = _context.AnswerOptions.Find(id);
            if (entity != null)
            {
                _context.AnswerOptions.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
