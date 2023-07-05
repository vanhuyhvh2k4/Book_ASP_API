using BookManagement.App.Data;
using BookManagement.App.Interfaces;
using BookManagement.App.Models;

namespace BookManagement.App.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly DataContext _context;

        public BillRepository(DataContext context)
        {
            _context = context;
        }

        public bool BillExists(int billId)
        {
            return _context.Bills.Any(bill => bill.Id == billId);
        }

        public Bill GetBill(int billId)
        {
            return _context.Bills.Find(billId);
        }

        public ICollection<Bill> GetBills()
        {
            return _context.Bills.OrderBy(bill => bill.Id).ToList();
        }

        public ICollection<Bill> GetBillsOfReader(int readerId)
        {
            return _context.Bills.Where(bill => bill.ReaderId == readerId).ToList();
        }
    }
}
