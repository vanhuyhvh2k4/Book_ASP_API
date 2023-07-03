using BookManagement.App.Models;

namespace BookManagement.App.Interfaces
{
    public interface IReaderRepository
    {
        ICollection<Reader> GetReaders();

        Reader GetReader(int readerId);

        bool ReaderExists(int readerId);

        Reader GetReaderByBill(int billId);
    }
}
