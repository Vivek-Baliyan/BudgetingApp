using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly DataContext _context;
        public CategoryController(ICategoryRepository categoryRepository, DataContext context)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<MasterCategoryDto>>> GetCategories(int id)
        {
            var categories = await _categoryRepository.GetCategories(id);
            return Ok(categories);
        }

        [HttpPost("saveMaster")]
        public async Task<ActionResult<IEnumerable<MasterCategoryDto>>> SaveMasterCategory(MasterCategoryDto masterCategoryDto)
        {
            string categoryName = masterCategoryDto.CategoryName.ToLower();
            if (await CategoryExists(categoryName, masterCategoryDto.AppUserId)) return BadRequest("Category already exists");

            var category = new MasterCategory
            {
                CategoryName = masterCategoryDto.CategoryName,
            };

            var user = await _context.Users
            .Include(a => a.MasterCategories)
            .SingleOrDefaultAsync(x => x.Id == masterCategoryDto.AppUserId);

            user.MasterCategories.Add(category);

            if (await _categoryRepository.SaveAllAsync())
            {
                return await GetCategories(masterCategoryDto.AppUserId);
            }
            return BadRequest("Problem adding category");
        }
        [HttpPost("saveSub")]
        public async Task<ActionResult<IEnumerable<MasterCategoryDto>>> SaveSubCategory(SubCategoryDto subCategoryDto)
        {
            string categoryName = subCategoryDto.CategoryName.ToLower();
            if (await SubCategoryExists(categoryName, subCategoryDto.MasterCategoryId)) return BadRequest("Category already exists");

            var category = new SubCategory
            {
                CategoryName = subCategoryDto.CategoryName,
            };

            var masterCategory = await _context.MasterCategories
            .Include(a => a.SubCategories)
            .SingleOrDefaultAsync(x => x.Id == subCategoryDto.MasterCategoryId);

            masterCategory.SubCategories.Add(category);

            if (await _categoryRepository.SaveAllAsync())
            {
                return await GetCategories(masterCategory.AppUserId);
            }
            return BadRequest("Problem adding category");
        }
        private async Task<bool> CategoryExists(string categoryName, int appuserid)
        {
            return await _context.MasterCategories.Where(x => x.AppUserId == appuserid)
            .AnyAsync(x => x.CategoryName.ToLower() == categoryName);
        }
        private async Task<bool> SubCategoryExists(string categoryName, int masterCategoryId)
        {
            var masterCategory = await _context.MasterCategories
            .Include(x => x.SubCategories)
            .SingleOrDefaultAsync(x => x.Id == masterCategoryId);

            return masterCategory.SubCategories.Any(x => x.CategoryName == categoryName);

        }
    }

}