namespace CustomerPlatform.Dtos
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; }
        public int TotalRecords { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public PaginatedResult(IEnumerable<T> items, int totalRecords, int pageNumber, int pageSize)
        {
            Items = items;
            TotalRecords = totalRecords;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
