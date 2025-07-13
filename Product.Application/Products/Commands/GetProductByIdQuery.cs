using MediatR;
using Product.Application.DTOs;

namespace Product.Application.Products.Commands;

public class GetProductByIdQuery : IRequest<ProductDto?>
{
    public Guid Id { get; set; }
}