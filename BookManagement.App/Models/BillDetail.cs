namespace BookManagement.App.Models
{
    public class BillDetail
    {
        public BillDetail()
        {
            IsReturned = false;
            PricePerDay = 2;
            Pay = 0;
            ReturnDate = null;
        }

        public  int Id { get; set; }

        public DateTime borrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int TotalDay { get; set; }

        public long PricePerDay { get; set; }

        public bool IsReturned { get; set; }

        public long Pay { get; set; }

        public int BillId { get; set; }

        public Bill Bill { get; set; }
    }
}
