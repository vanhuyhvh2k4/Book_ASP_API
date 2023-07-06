using BookManagement.App.Models;

namespace BookManagement.App.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();

        Category GetCategory(int categoryId);

        bool CategoriesExists(int categoryId);

        ICollection<Category> GetCategoriesOfBook(int bookId);

        bool CreateCategory(Category category);

        bool Save();
    }
}
