namespace BookManagement.App.Models
{
    public class Bill
    {
        public int Id { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public bool IsRetruned { get; set; }

        public long Price { get; set; }
    }
}
