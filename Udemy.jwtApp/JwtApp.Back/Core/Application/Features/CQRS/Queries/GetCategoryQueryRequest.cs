using JwtApp.Back.Core.Application.Dto;
using MediatR;
using JwtApp.Back.Core.Domain;

namespace JwtApp.Back.Core.Application.Features.CQRS.Queries;

public class GetCategoryQueryRequest : IRequest<CategoriesListDto?>
{
    public int Id { get; set; }

    public GetCategoryQueryRequest(int id)
    {
        Id = id;
    }
}