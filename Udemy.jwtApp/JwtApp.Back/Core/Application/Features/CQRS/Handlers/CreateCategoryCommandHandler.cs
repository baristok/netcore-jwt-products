using JwtApp.Back.Core.Application.Features.CQRS.Commands;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest>
{
    private readonly IRepository<Category> _categoryRepository;

    public CreateCategoryCommandHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        await this._categoryRepository.CreateAsync(new Category
        {
            Definition = request.Definition,
        });
    }
}