using MediatR;

namespace Product.Application.Products.Commands;

public class DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }
}