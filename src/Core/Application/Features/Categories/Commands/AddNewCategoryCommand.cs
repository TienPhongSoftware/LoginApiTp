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

namespace Application.Features.Categories.Commands
{
    public class AddNewCategoryCommand : IRequest<IResponse>
    {
        public string Name { get; set; }

        public class AddNewCategoryCommandHandler : IRequestHandler<AddNewCategoryCommand, IResponse>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IEmailService _emailService;
            private readonly IMapper _mapper;

            public AddNewCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IEmailService emailService, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _unitOfWork = unitOfWork;
                _emailService = emailService;
                _mapper = mapper;
            }

            public async Task<IResponse> Handle(AddNewCategoryCommand request, CancellationToken cancellationToken)
            {
                var existcategory = await _categoryRepository.GetAsync(x => x.Name == request.Name, noTracking: true);
                if (existcategory?.Name == request.Name)
                    throw new ApiException(400, Messages.CategoryIsAlreadyExist);

                var category = _mapper.Map<Category>(request);
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.AddCategorySuccessfully);
            }
        }
    }
}