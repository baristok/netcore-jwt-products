using AutoMapper;
using JwtApp.Back.Core.Application.Dto;
using JwtApp.Back.Core.Application.Features.CQRS.Queries;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class GetCategoriesQueryHandler: IRequestHandler<GetCatogoriesQueryRequest, List<CategoriesListDto>>
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    public GetCategoriesQueryHandler(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoriesListDto>> Handle(GetCatogoriesQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await this._categoryRepository.GetAllAsync();
        return _mapper.Map<List<CategoriesListDto>>(data);
    }
}