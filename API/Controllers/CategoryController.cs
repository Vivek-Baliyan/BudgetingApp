using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<MasterCategoryDto>>> GetCategories(int id)
        {
            var categories = await _categoryRepository.GetCategories(id);
            return Ok(categories);
        }
    }
}