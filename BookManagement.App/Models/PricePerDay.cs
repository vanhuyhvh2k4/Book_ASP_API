﻿namespace BookManagement.App.Models
{
    public class PricePerDay
    {
        public int Id { get; set; }

        public long Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
