namespace BookManagement.App.Models
{
    public class BillDetail
    {
        public  int Id { get; set; }

        public int BillId { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public Bill Bill { get; set; }

        public Book Book { get; set; }
    }
}
