using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using Application.Features.Users.Queries;
using Application.Wrappers.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediator;
        //private readonly BaseUrlSettings _baseUrlSettings;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
            //_baseUrlSettings = optionsBaseUrl.Value;
        }

        [HttpPost]
        public async Task<IResponse> AddCategory(AddNewCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<IResponse> GetAllCategories()
        {
            return await _mediator.Send(new GetAllCategoriesQuery());
        }
    }
}
