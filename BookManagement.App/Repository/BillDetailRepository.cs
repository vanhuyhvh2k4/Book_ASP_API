using BookManagement.App.Data;
using BookManagement.App.Dto;
using BookManagement.App.Interfaces;
using BookManagement.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.App.Repository
{
    public class BillDetailRepository : IBillDetailRepository
    {
        private readonly DataContext _context;

        public BillDetailRepository(DataContext context)
        {
            _context = context;
        }

        public bool BillDetailExists(int billDetailId)
        {
            return _context.BillDetails.Any(bill => bill.Id == billDetailId);
        }

        public BillDetail GetBillDetail(int billDetailId)
        {
            return _context.BillDetails
                        .Where(bill => bill.Id == billDetailId)
                        .Include(bill => bill.Book)
                        .FirstOrDefault();
        }

        public ICollection<BillDetail> GetBillDetailOfBill(int billId)
        {
            return _context.BillDetails
                        .Where(bill => bill.BillId == billId)
                        .Include(bill => bill.Book)
                        .ToList();
        }

        public ICollection<BillDetail> GetBillDetails()
        {
            return _context.BillDetails
                .OrderBy(bill => bill.Id)
                .Include(bill => bill.Book)
                .ToList();
        }
    }
}
