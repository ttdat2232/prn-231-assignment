namespace Domain.Models
{
    public class PaginationResult<T> where T : class
    {
        public IList<T> Values { get; set; } = new List<T>();
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
