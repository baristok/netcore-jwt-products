using AutoMapper;
using JwtApp.Back.Core.Application.Dto;
using JwtApp.Back.Core.Application.Features.CQRS.Queries;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class GetCategoryQuertyHandler : IRequestHandler<GetCategoryQueryRequest, CategoriesListDto?>
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryQuertyHandler(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoriesListDto> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await this._categoryRepository.GetByFilterAsync(x => x.Id == request.Id);
        return _mapper.Map<CategoriesListDto>(result);
    }
}