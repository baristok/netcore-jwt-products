using JwtApp.Back.Core.Application.Features.CQRS.Commands;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
{
    private readonly IRepository<Category> _categoryRepository;

    public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var updatedEntity = await this._categoryRepository.GetByIdAsync(request.Id);
        if (updatedEntity != null)
        {
            updatedEntity.Definition = request.Definition;
            await this._categoryRepository.UpdateAsync(updatedEntity);
        }
    }
}