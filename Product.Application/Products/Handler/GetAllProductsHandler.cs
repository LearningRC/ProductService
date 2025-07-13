using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Common;
using Product.Application.DTOs;
using Product.Application.Products.Commands;
using Product.Infrastructure.Persistence;

namespace Product.Application.Products.Handler;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, PaginatedResult<ProductDto>>
{
    private readonly AppDbContext _context;

    public GetAllProductsHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsQueryable();

        // 🔍 Filter
        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(p => p.Name.Contains(request.Search));

        if (request.MinPrice.HasValue)
            query = query.Where(p => p.Price >= request.MinPrice.Value);

        if (request.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= request.MaxPrice.Value);

        // ↕️ Sorting
        query = (request.SortBy.ToLower(), request.SortDirection.ToLower()) switch
        {
            ("price", "des") => query.OrderByDescending(p => p.Price),
            ("price", "asc") => query.OrderBy(p => p.Price),
            (_, "des") => query.OrderByDescending(p => p.Name),
            _ => query.OrderBy(p => p.Name),
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            })
            .ToListAsync(cancellationToken);

        return new PaginatedResult<ProductDto>(items, totalCount, request.PageNumber, request.PageSize);
    }

}