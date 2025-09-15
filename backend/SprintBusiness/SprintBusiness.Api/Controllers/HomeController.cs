using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SprintBusiness.Features.Home.Commands.SendTemplate;
using SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos;

namespace SprintBusiness.Api.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMediator mediator , ILogger<HomeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new Exception("HOOOOO");
        }

        [HttpPost("{phoneNumber}/send-template")]
        public async Task<IActionResult> Get(string phoneNumber , [FromBody] SendTemplateDto dto)
        {
            return Ok(await _mediator
                .Send(new SendTemplateApiCommand(phoneNumber , dto)));
        }
    }

}
