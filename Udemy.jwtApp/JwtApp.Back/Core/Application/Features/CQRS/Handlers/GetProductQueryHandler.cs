using AutoMapper;
using JwtApp.Back.Core.Application.Dto;
using JwtApp.Back.Core.Application.Features.CQRS.Queries;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;
using NuGet.Protocol.Plugins;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, ProductsListDto>
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _repository = productRepository;
        this._mapper = mapper;
    }

    public async Task<ProductsListDto> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await this._repository.GetByFilterAsync(x => x.Id == request.Id);
        return this._mapper.Map<ProductsListDto>(data);
    }
}