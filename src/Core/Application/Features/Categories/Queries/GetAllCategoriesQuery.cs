using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Wrappers.Concrete;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<DataResponse<IEnumerable<Category>>>
    {
        public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, DataResponse<IEnumerable<Category>>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly ILogger<GetAllCategoriesQueryHandler> _logger;

            public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, ILogger<GetAllCategoriesQueryHandler> logger)
            {
                _categoryRepository = categoryRepository;
                _logger = logger;
            }

            public async Task<DataResponse<IEnumerable<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                _logger.LogInformation("GetAllCategories = {@GetAllCategories}", categories);
                return new DataResponse<IEnumerable<Category>>(categories, 200);
            }
        }
    }
}