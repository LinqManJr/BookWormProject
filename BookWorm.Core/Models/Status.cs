namespace BookWorm.Core.Models
{
    public partial class ReadTable
    {
        public enum Status
        {
            Read = 0,
            Done = 1,
            InFuture = 2
        }
    }
}
