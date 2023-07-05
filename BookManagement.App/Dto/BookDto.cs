namespace BookManagement.App.Dto
{
    public class BookDto
    {
        public int Id { get; set; }

        public string BookName { get; set; }

        public int InitQuantity { get; set; }

        public int CurrentQuantity { get; set; }
    }
}
