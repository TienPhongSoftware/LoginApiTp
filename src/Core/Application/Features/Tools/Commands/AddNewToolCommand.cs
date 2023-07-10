using Application.Constants;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers.Abstract;
using Application.Wrappers.Concrete;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Application.Features.Tools.Commands
{
    public class AddNewToolCommand : IRequest<IResponse>
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public byte[] Image { get; set; }
        public string Technologies { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }

        public class AddNewToolCommandHandler : IRequestHandler<AddNewToolCommand, IResponse>
        {
            private readonly IToolRepository _ToolRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public AddNewToolCommandHandler(IToolRepository ToolRepository, IUnitOfWork unitOfWork, IEmailService emailService, IMapper mapper)
            {
                _ToolRepository = ToolRepository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IResponse> Handle(AddNewToolCommand request, CancellationToken cancellationToken)
            {
                var existTool = await _ToolRepository.GetAsync(x => x.Name == request.Name, noTracking: true);
                if (existTool?.Name == request.Name)
                    throw new ApiException(400, Messages.ToolIsAlreadyExist);

                var Tool = _mapper.Map<Tool>(request);
                await _ToolRepository.AddAsync(Tool);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.AddToolSuccessfully);
            }
        }
    }
}