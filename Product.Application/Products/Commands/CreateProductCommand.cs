using MediatR;

public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}