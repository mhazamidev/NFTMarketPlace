using MediatR;
using Microsoft.AspNetCore.Mvc;
using NETCareer.Application.Core.CQRS.CommandHandling;
using NETCareer.Application.Core.CQRS.QueryHandling;

namespace NFTMarketPlace.WebAPI.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async new Task<IActionResult> Response<TResult>(Query<TResult> query)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var queryHandlerResult = await _mediator.Send(query);
                return queryHandlerResult.ValidationResult.IsValid
                    ? OkActionResult(queryHandlerResult.Result)
                    : BadRequestActionResult(queryHandlerResult.ValidationResult.Errors);
            }
            catch (Exception e)
            {
                return BadRequestActionResult(e.Message);
            }
        }

        protected async new Task<IActionResult> Response<TResult>(Command<TResult> command)
            where TResult : struct
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var commandHandlerResult = await _mediator.Send(command);
                return commandHandlerResult.ValidationResult.IsValid ? OkActionResult(commandHandlerResult.Id)
                    : BadRequestActionResult(commandHandlerResult.ValidationResult.Errors);
            }
            catch (Exception e)
            {
                return BadRequestActionResult(e.Message);
            }
        }

        private IActionResult BadRequestActionResult(dynamic resultErrors)
        {
            return BadRequest(new
            {
                success = false,
                message = resultErrors
            });
        }

        private IActionResult OkActionResult(dynamic resultData)
        {
            return Ok(new
            {
                success = true,
                data = resultData
            });
        }
    }
}
