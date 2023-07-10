using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Wrappers.Concrete;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Tools.Queries
{
    public class GetAllToolsQuery : IRequest<DataResponse<IEnumerable<Tool>>>
    {
        public class GetAllToolsQueryHandler : IRequestHandler<GetAllToolsQuery, DataResponse<IEnumerable<Tool>>>
        {
            private readonly IToolRepository _ToolRepository;
            private readonly ILogger<GetAllToolsQueryHandler> _logger;

            public GetAllToolsQueryHandler(IToolRepository ToolRepository, ILogger<GetAllToolsQueryHandler> logger)
            {
                _ToolRepository = ToolRepository;
                _logger = logger;
            }

            public async Task<DataResponse<IEnumerable<Tool>>> Handle(GetAllToolsQuery request, CancellationToken cancellationToken)
            {
                var Tools = await _ToolRepository.GetAllToolsAsync();
                _logger.LogInformation("GetAllTools = {@GetAllTools}", Tools);
                return new DataResponse<IEnumerable<Tool>>(Tools, 200);
            }
        }
    }
}