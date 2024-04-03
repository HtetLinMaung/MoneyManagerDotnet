namespace MoneyManager.Utilities
{
    public class PaginatedList<T> : BaseResponse
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int TotalCount { get; set; } = 0;

        public int PageCounts { get; set; } = 0;

        // public PaginatedList(IEnumerable<T> items, int totalCount, int pageCounts)
        // {
        //     Items = items;
        //     TotalCount = totalCount;
        //     PageCounts = pageCounts;
        // }
    }
}