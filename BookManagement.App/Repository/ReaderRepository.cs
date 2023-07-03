using BookManagement.App.Data;
using BookManagement.App.Interfaces;
using BookManagement.App.Models;

namespace BookManagement.App.Repository
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly DataContext _context;

        public ReaderRepository(DataContext context)
        {
            _context = context;
        }

        public Reader GetReader(int readerId)
        {
            return _context.Readers.Find(readerId);
        }

        public Reader GetReaderByBill(int billId)
        {
            return _context.Bills.Where(bill => bill.Id == billId).Select(r => r.Reader).FirstOrDefault();
        }

        public ICollection<Reader> GetReaders()
        {
            return _context.Readers.OrderBy(reader => reader.Id).ToList();
        }

        public bool ReaderExists(int readerId)
        {
            return _context.Readers.Any(reader => reader.Id == readerId);
        }
    }
}
