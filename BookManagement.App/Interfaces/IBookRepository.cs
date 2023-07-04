using BookManagement.App.Models;

namespace BookManagement.App.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();

        Book GetBook(int bookId);

        bool BookExists(int bookId);

        ICollection<Book> GetBooksByCategory(int categoryId);
    }
}
