using JwtApp.Back.Core.Application.Features.CQRS.Commands;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest>
{
    private readonly IRepository<Category> _categoryRepository;

    public DeleteCategoryCommandHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var deletedEntity = await this._categoryRepository.GetByIdAsync(request.Id);
        if (deletedEntity != null)
        {
            await this._categoryRepository.RemoveAsync(deletedEntity);
        }
    }
}