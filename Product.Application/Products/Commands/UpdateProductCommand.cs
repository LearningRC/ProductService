using MediatR;

namespace Product.Application.Products.Commands;

public class UpdateProductCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}