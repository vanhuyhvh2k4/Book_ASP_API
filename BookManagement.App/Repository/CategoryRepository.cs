using BookManagement.App.Data;
using BookManagement.App.Interfaces;
using BookManagement.App.Models;

namespace BookManagement.App.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoriesExists(int categoryId)
        {
            return _context.Categories.Any(cate => cate.Id == categoryId);
        }

        public bool CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(cate => cate.Id).ToList();
        }

        public ICollection<Category> GetCategoriesOfBook(int bookId)
        {
            return _context.BookCategories.Where(bc => bc.BookId == bookId).Select(bc => bc.Category).ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            var updateCategory = _context.Categories.Find(category.Id);

            _context.Attach(updateCategory);

            updateCategory.Name = category.Name;
            updateCategory.UpdatedAt = DateTime.Now;

            return Save();
        }
    }
}
