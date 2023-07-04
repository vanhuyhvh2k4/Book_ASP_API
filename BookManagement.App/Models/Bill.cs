namespace BookManagement.App.Models
{
    public class Bill
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int ReaderId { get; set; }

        public Reader Reader { get; set; }

        public Book Book { get; set; }

        public BillDetail BillDetail { get; set; }
    }
}
