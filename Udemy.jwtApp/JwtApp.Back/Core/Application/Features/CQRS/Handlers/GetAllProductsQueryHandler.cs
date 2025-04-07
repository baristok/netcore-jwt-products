using AutoMapper;
using JwtApp.Back.Core.Application.Dto;
using JwtApp.Back.Core.Application.Features.CQRS.Queries;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, List<ProductsListDto>>
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductsListDto>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await this._repository.GetAllAsync();
        return this._mapper.Map<List<Product>, List<ProductsListDto>>(data);
    }
}