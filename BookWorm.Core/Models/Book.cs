using BookWorm.Core.Models;

namespace BookWorm.Core
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Author Author { get; set; }
    }
}
