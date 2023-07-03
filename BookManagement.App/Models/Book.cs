namespace BookManagement.App.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string BookName { get; set; }

        public int Quantity { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }

        public ICollection<Bill> Bills { get; set; }
    }
}
