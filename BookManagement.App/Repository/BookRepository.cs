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

        public Book GetBook(int bookId)
        {
            return _context.Books.Find(bookId);
        }

        public Book GetBookByBill(int billId)
        {
            return _context.Bills.Where(bill => bill.Id == billId).Select(b => b.Book).FirstOrDefault();
        }

        public ICollection<Book> GetBooks()
        {
            return _context.Books.OrderBy(book => book.Id).ToList();
        }

        public ICollection<Book> GetBooksByCategory(int categoryId)
        {
            return _context.BookCategories.Where(bookCategory => bookCategory.CategoryId == categoryId).Select(bc => bc.Book).ToList();
        }
    }
}
