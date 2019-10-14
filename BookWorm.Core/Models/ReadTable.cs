using System;

namespace BookWorm.Core.Models
{
    public partial class ReadTable
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public BookUser User { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Resume { get; set; }
        public int Rating { get; set; }        
        public Quote Quote { get; set; }
        public bool QuoteExist { get; set; }
        public Status BookStatus { get; set; }
    }
    
}
