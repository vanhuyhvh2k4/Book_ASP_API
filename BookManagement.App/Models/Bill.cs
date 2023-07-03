namespace BookManagement.App.Models
{
    public class Bill
    {
        public Bill()
        {
            IsRetruned = false;
        }

        public int Id { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public bool IsRetruned { get; set; }

        public long Price { get; set; }

        public int BookId { get; set; }

        public int ReaderId { get; set; }

        public Reader Reader { get; set; }

        public Book Book { get; set; }
    }
}
