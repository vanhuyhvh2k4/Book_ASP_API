﻿namespace BookManagement.App.Models
{
    public class Bill
    {
        public int Id { get; set; }

        public int ReaderId { get; set; }

        public DateTime BorrowDate { get; set; }

        public Reader Reader { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<BillDetail> BillDetails { get; set; }
    }
}
