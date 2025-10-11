






using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // ✅ GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // ✅ GET: api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // ✅ POST: api/Categories
        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            var created = await _categoryService.CreateAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = created.Id }, created);
        }

        // ✅ PUT: api/Categories/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(Guid id, Category category)
        {
            var updated = await _categoryService.UpdateAsync(id, category);
            return Ok(updated);
        }

        // ✅ DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}




// using Microsoft.AspNetCore.Mvc;
// using ECommerce.Application.Interfaces.Repositories;
// using ECommerce.Domain.Entities;

// namespace ECommerce.API.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class CategoriesController : ControllerBase
//     {
//         private readonly ICategoryRepository _categoryRepository;

//         public CategoriesController(ICategoryRepository categoryRepository)
//         {
//             _categoryRepository = categoryRepository;
//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
//         {
//             var categories = await _categoryRepository.GetAllAsync();
//             return Ok(categories);
//         }

//         [HttpGet("{id}")]
//         public async Task<ActionResult<Category>> GetCategory(Guid id)
//         {
//             var category = await _categoryRepository.GetByIdAsync(id);
//             if (category == null) return NotFound();
//             return Ok(category);
//         }

//         [HttpPost]
//         public async Task<ActionResult> CreateCategory(Category category)
//         {
//             await _categoryRepository.AddAsync(category);
//             return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
//         }

//         [HttpPut("{id}")]
//         public async Task<ActionResult> UpdateCategory(Guid id, Category category)
//         {
//             if (id != category.Id) return BadRequest();

//             var existing = await _categoryRepository.GetByIdAsync(id);
//             if (existing == null) return NotFound();

//             existing.Name = category.Name;
//             existing.Description = category.Description;

//             await _categoryRepository.UpdateAsync(existing);
//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         public async Task<ActionResult> DeleteCategory(Guid id)
//         {
//             var existing = await _categoryRepository.GetByIdAsync(id);
//             if (existing == null) return NotFound();

//             await _categoryRepository.DeleteAsync(existing);
//             return NoContent();
//         }
//     }
// }




















// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using ECommerce.Infrastructure.Persistence;
// using ECommerce.Domain.Entities;

// namespace ECommerce.API.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class CategoriesController : ControllerBase
//     {
//         private readonly AppDbContext _context;

//         public CategoriesController(AppDbContext context)
//         {
//             _context = context;
//         }

//         // ✅ GET: api/Categories
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
//         {
//             return Ok(await _context.Categories.ToListAsync());
//         }

//         // ✅ GET: api/Categories/{id}
//         [HttpGet("{id}")]
//         public async Task<ActionResult<Category>> GetCategory(Guid id)
//         {
//             var category = await _context.Categories.FindAsync(id);
//             if (category == null) return NotFound();
//             return Ok(category);
//         }

//         // ✅ POST: api/Categories
//         [HttpPost]
//         public async Task<ActionResult<Category>> CreateCategory(Category category)
//         {
//             _context.Categories.Add(category);
//             await _context.SaveChangesAsync();
//             return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
//         }

//         // ✅ PUT: api/Categories/{id}
//         [HttpPut("{id}")]
//         public async Task<IActionResult> UpdateCategory(Guid id, Category category)
//         {
//             if (id != category.Id) return BadRequest();

//             _context.Entry(category).State = EntityState.Modified;
//             await _context.SaveChangesAsync();
//             return NoContent();
//         }

//         // ✅ DELETE: api/Categories/{id}
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteCategory(Guid id)
//         {
//             var category = await _context.Categories.FindAsync(id);
//             if (category == null) return NotFound();

//             _context.Categories.Remove(category);
//             await _context.SaveChangesAsync();
//             return NoContent();
//         }
//     }
// }
