using Application.Features.Tools.Commands;
using Application.Features.Tools.Queries;
using Application.Wrappers.Abstract;
using Infrastructure.Settings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolController : BaseController
    {
        private readonly IMediator _mediator;
        //private readonly BaseUrlSettings _baseUrlSettings;

        public ToolController(IMediator mediator)
        {
            _mediator = mediator;
            //_baseUrlSettings = optionsBaseUrl.Value;
        }

        [HttpPost]
        public async Task<IResponse> AddTool(AddNewToolCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<IResponse> GetAllTools()
        {
            return await _mediator.Send(new GetAllToolsQuery());
        }
    }
}
