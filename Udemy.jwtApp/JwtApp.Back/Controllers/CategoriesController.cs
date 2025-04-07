using JwtApp.Back.Core.Application.Features.CQRS.Commands;
using JwtApp.Back.Core.Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JwtApp.Back.Core.Application.Dto;

namespace JwtApp.Back.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await this._mediator.Send(new GetCatogoriesQueryRequest());
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await this._mediator.Send(new GetCategoryQueryRequest(id));
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommandRequest request)
        {
            await this._mediator.Send(request);
            return Created("", request);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommandRequest request)
        {
            await this._mediator.Send(request);
            return NoContent();
        }
 
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await this._mediator.Send(new DeleteCategoryCommandRequest(id));
            return Ok();
        }
    }
}
