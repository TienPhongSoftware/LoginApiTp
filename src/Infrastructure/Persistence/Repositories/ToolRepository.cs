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
    public class ToolRepository : EfEntityRepository<Tool, CAContext, Guid>, IToolRepository
    {
        public ToolRepository(CAContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<Tool>> GetAllToolsAsync()
        {
            return await _context.Tools.AsNoTracking().Select(Tool => new Tool
            {
                Id = Tool.Id,
                Name = Tool.Name,
                CategoryId = Tool.CategoryId,
                Image = Tool.Image,
                Technologies = Tool.Technologies,
                Title = Tool.Title,
                Body = Tool.Body,
                UserId = Tool.UserId,
                Active = Tool.Active,
                CreatedDate = Tool.CreatedDate,
            }).ToListAsync();
        }
    }
}
