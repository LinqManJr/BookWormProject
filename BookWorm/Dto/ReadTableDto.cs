using BookWorm.Core;
using System;
using static BookWorm.Core.Models.ReadTable;

namespace BookWorm.Dto
{
    public class ReadTableDto
    {
        public Guid UserId { get; set; }
        public Book Book { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Resume { get; set; }
        public int Rating { get; set; }        
        public Status BookStatus { get; set; }
    }
}
