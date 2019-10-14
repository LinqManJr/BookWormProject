﻿namespace BookWorm.Core.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string QuoteText { get; set; }    
        public BookUser BookUser { get; set; }
        public Book Book { get; set; }
        
    }
}
