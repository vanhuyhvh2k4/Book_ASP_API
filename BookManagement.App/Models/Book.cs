namespace BookManagement.App.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string BookName { get; set; }

        public int InitQuantity { get; set; }

        public int CurrentQuantity { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }

        public ICollection<BillDetail> BillDetails { get; set; }
    }
}
