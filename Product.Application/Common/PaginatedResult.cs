namespace Product.Application.Common;

public class PaginatedResult<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PaginatedResult(List<T> items, int count, int page, int size)
    {
        Items = items;
        TotalCount = count;
        PageNumber = page;
        PageSize = size;
    }
}