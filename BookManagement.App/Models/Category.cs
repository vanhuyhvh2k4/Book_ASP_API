namespace BookManagement.App.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
