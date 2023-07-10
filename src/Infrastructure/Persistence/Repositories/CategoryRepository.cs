using Application.Dtos;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CategoryRepository : EfEntityRepository<Category, CAContext, Guid>, ICategoryRepository
    {
        public CategoryRepository(CAContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.AsNoTracking().Select(category => new Category
            {
                Id = category.Id,
                Name = category.Name
            }).ToListAsync();
        }
    }
}
