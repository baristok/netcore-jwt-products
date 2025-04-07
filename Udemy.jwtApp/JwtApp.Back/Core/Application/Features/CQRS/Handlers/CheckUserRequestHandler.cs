using JwtApp.Back.Core.Application.Dto;
using JwtApp.Back.Core.Application.Features.CQRS.Queries;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class CheckUserRequestHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
{
    private readonly IRepository<AppUser> _appUserRepository;
    private readonly IRepository<AppRole> _roleRepository;

    public CheckUserRequestHandler(IRepository<AppUser> appUserRepository, IRepository<AppRole> roleRepository)
    {
        this._appUserRepository = appUserRepository;
        this._roleRepository = roleRepository;
    }

    public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
    {
        var dto = new CheckUserResponseDto();
        var user = await this._appUserRepository.GetByFilterAsync(x => x.Username == request.Username && x.Password == request.Password);

        if (user == null)
        {
            dto.IsExist = false;
        }
        else
        {
            dto.Username = user.Username;
            dto.Id = user.Id;
            dto.IsExist = true;
            var role = await this._roleRepository.GetByFilterAsync(x => x.Id == user.AppRoleId);
            dto.Role = role?.Definition;
        }
        return dto;
    }

}