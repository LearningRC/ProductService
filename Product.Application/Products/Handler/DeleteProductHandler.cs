using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Products.Commands;
using Product.Infrastructure.Persistence;

namespace Product.Application.Products.Handler;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly AppDbContext _context;

    public DeleteProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
            throw new KeyNotFoundException($"Product with Id '{request.Id}' not found.");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}