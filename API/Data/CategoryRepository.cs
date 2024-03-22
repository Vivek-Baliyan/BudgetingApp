using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CategoryRepository(DataContext context, IMapper mapper) : ICategoryRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<MasterCategoryDto>> GetCategories(int AppUserId)
        {
            return await _context.MasterCategories
            .Include(s => s.SubCategories)
            .Where(x => x.AppUserId == AppUserId)
            .ProjectTo<MasterCategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateMaster(MasterCategory masterCategory)
        {
            _context.Entry(masterCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void UpdateSub(SubCategory subCategory)
        {
            _context.Entry(subCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}