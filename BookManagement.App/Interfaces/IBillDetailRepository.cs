using BookManagement.App.Models;

namespace BookManagement.App.Interfaces
{
    public interface IBillDetailRepository
    {
        ICollection<BillDetail> GetBillDetails();

        BillDetail GetBillDetail(int billDetailId);

        bool BillDetailExists(int billDetailId);

        ICollection<BillDetail> GetBillDetailOfBill(int billId);

        bool CreateBillDetail(BillDetail billDetail);

        bool Save();
    }
}
