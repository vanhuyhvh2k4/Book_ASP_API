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

        public bool CreateBill(Bill bill)
        {
            _context.Bills.Add(bill);
            return Save();
        }

        public bool DeleteBill(Bill bill)
        {
            _context.Bills.Remove(bill);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBill(Bill bill)
        {
            var updateBill = _context.Bills.Find(bill.Id);

            _context.Attach(updateBill);

            updateBill.ReaderId = bill.ReaderId;
            updateBill.UpdatedAt = DateTime.Now;

            return Save();
        }
    }
}
