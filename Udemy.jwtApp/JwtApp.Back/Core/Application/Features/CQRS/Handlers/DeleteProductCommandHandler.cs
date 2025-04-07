using JwtApp.Back.Core.Application.Features.CQRS.Commands;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
{
    private readonly IRepository<Product> _productRepository;

    public DeleteProductCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var deleteEntity = await this._productRepository.GetByIdAsync(request.Id);
        if (deleteEntity != null)
        {
            await this._productRepository.RemoveAsync(deleteEntity);
        }
    }
}