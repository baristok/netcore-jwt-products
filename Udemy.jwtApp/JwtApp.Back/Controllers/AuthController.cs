using System.IdentityModel.Tokens.Jwt;
using JwtApp.Back.Core.Application.Features.CQRS.Commands;
using JwtApp.Back.Core.Application.Features.CQRS.Queries;
using JwtApp.Back.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest request)
        {
            await this._mediator.Send(request);
            return Created("", request);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] CheckUserQueryRequest request)
        {
            var dto = await this._mediator.Send(request);
            if (dto.IsExist)
            {
                return Created("",JwtTokenGenerator.GenerateJwtToken(dto));
            }
            else
            {
                return BadRequest("Bilgiler Hatalı");
            }
        }
        
    }
}
