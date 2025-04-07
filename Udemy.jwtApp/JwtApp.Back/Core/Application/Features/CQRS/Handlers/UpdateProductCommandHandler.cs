using JwtApp.Back.Core.Application.Features.CQRS.Commands;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
{
    private readonly IRepository<Product> _productRepository;

    public UpdateProductCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var updateProduct = await this._productRepository.GetByIdAsync(request.Id);
        if (updateProduct != null)
        {
            updateProduct.CategoryId = request.CategoryId;
            updateProduct.Name = request.Name;
            updateProduct.Stock = request.Stock;
            updateProduct.Price = request.Price;
            await this._productRepository.UpdateAsync(updateProduct);
        }
    }
}