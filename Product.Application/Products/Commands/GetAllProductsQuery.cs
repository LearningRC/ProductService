using MediatR;
using Product.Application.Common;
using Product.Application.DTOs;

namespace Product.Application.Products.Commands;

public class GetAllProductsQuery : IRequest<PaginatedResult<ProductDto>>
{
    public string? Search { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public string SortBy { get; set; } = "name"; // name or price
    public string SortDirection { get; set; } = "asc"; // asc or desc

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}