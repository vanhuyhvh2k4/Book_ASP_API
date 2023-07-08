namespace BookManagement.App.Models
{
    public class Reader
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }  
        
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Bill> Bills { get; set; }
    }
}
