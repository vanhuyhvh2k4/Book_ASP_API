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

        public bool CreateReader(Reader reader)
        {
            _context.Readers.Add(reader);
            return Save();
        }

        public bool DeleteReader(Reader reader)
        {
            _context.Readers.Remove(reader);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateReader(Reader reader)
        {
            var updateReader = _context.Readers.Find(reader.Id);

            _context.Attach(updateReader);

            updateReader.FullName = reader.FullName;
            updateReader.Email = reader.Email;
            updateReader.Phone = reader.Phone;
            updateReader.UpdatedAt = DateTime.Now;

            return Save();
        }
    }
}
