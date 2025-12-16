using QuizApp.Models;

namespace QuizApp.Lib.Extensions
{
    public static class CategoryExtensions
    {
        public static string ToDisplayName(this Category? category)
        {
            if (category == null)
                return "Mixed";

            if (category.IsDeleted)
                return $"(Deleted {category.Name})";

            return category.Name;
        }
    }
}
