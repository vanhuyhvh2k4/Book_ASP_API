using BookManagement.App.Models;

namespace BookManagement.App.Interfaces
{
    public interface IBillRepository
    {
        ICollection<Bill> GetBills();

        Bill GetBill(int billId);

        bool BillExists(int billId);

        ICollection<Bill> GetBillsOfReader(int readerId);

        bool CreateBill(Bill bill);

        bool Save();
    }
}
