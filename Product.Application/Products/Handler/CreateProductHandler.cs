using MediatR;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly AppDbContext _context;
    public CreateProductHandler(AppDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product.Domain.Entities.Product { Id = Guid.NewGuid(), Name = request.Name, Price = request.Price };
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}