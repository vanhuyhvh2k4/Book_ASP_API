namespace BookManagement.App.Models
{
    public class Bill
    {
        public int Id { get; set; }

        public int ReaderId { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int? TotalDate { get; set; }

        public int TotalBooks { get; set; }

        public long PricePerDay { get; set; }

        public bool IsReturned { get; set; }

        public long? Pay { get; set; }

        public Reader Reader { get; set; }

        public ICollection<BillDetail> BillDetails { get; set; }
    }
}
