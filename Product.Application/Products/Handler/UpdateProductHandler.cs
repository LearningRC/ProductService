using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Products.Commands;
using Product.Infrastructure.Persistence;

namespace Product.Application.Products.Handler;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly AppDbContext _context;

    public UpdateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
            throw new KeyNotFoundException($"Product with Id '{request.Id}' not found.");

        product.Name = request.Name;
        product.Price = request.Price;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}