using BookManagement.App.Data;
using BookManagement.App.Interfaces;
using BookManagement.App.Models;

namespace BookManagement.App.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public bool BookExists(int bookId)
        {
            return _context.Books.Any(book => book.Id == bookId);
        }

        public bool CreateBook(int categoryId, Book book)
        {
            var category = _context.Categories.Find(categoryId);

            var bookCategory = new BookCategory()
            {
                Category = category,
                Book= book,
            };

            _context.BookCategories.Add(bookCategory);
            _context.Books.Add(book);
            return Save();
        }

        public Book GetBook(int bookId)
        {
            return _context.Books.Find(bookId);
        }

        public ICollection<Book> GetBooks()
        {
            return _context.Books.OrderBy(book => book.Id).ToList();
        }

        public ICollection<Book> GetBooksByCategory(int categoryId)
        {
            return _context.BookCategories.Where(bookCategory => bookCategory.CategoryId == categoryId).Select(bc => bc.Book).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
