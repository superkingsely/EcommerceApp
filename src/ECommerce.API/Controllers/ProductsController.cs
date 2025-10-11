



using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // ✅ GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // ✅ GET: api/Products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // ✅ POST: api/Products
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            var created = await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
        }

        // ✅ PUT: api/Products/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(Guid id, Product product)
        {
            var updated = await _productService.UpdateAsync(id, product);
            return Ok(updated);
        }

        // ✅ DELETE: api/Products/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            var deleted = await _productService.DeleteAsync(id);
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
//     public class ProductsController : ControllerBase
//     {
//         private readonly IProductRepository _productRepository;

//         public ProductsController(IProductRepository productRepository)
//         {
//             _productRepository = productRepository;
//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
//         {
//             var products = await _productRepository.GetAllAsync();
//             return Ok(products);
//         }

//         [HttpGet("{id}")]
//         public async Task<ActionResult<Product>> GetProduct(Guid id)
//         {
//             var product = await _productRepository.GetByIdAsync(id);
//             if (product == null) return NotFound();
//             return Ok(product);
//         }

//         [HttpPost]
//         public async Task<ActionResult> CreateProduct(Product product)
//         {
//             await _productRepository.AddAsync(product);
//             return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
//         }

//         [HttpPut("{id}")]
//         public async Task<ActionResult> UpdateProduct(Guid id, Product product)
//         {
//             if (id != product.Id) return BadRequest();

//             var existing = await _productRepository.GetByIdAsync(id);
//             if (existing == null) return NotFound();

//             existing.Name = product.Name;
//             existing.Price = product.Price;
//             existing.Description = product.Description;
//             existing.CategoryId = product.CategoryId;

//             await _productRepository.UpdateAsync(existing);
//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         public async Task<ActionResult> DeleteProduct(Guid id)
//         {
//             var existing = await _productRepository.GetByIdAsync(id);
//             if (existing == null) return NotFound();

//             await _productRepository.DeleteAsync(existing);
//             return NoContent();
//         }
//     }
// }









// // using Microsoft.AspNetCore.Mvc;
// // using Microsoft.EntityFrameworkCore;
// // using ECommerce.Infrastructure.Persistence;
// // using ECommerce.Domain.Entities;

// // namespace ECommerce.API.Controllers
// // {
// //     [Route("api/[controller]")]
// //     [ApiController]
// //     public class ProductsController : ControllerBase
// //     {
// //         private readonly AppDbContext _context;

// //         public ProductsController(AppDbContext context)
// //         {
// //             _context = context;
// //         }

// //         // ✅ GET: api/Products
// //         [HttpGet]
// //         public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
// //         {
// //             var products = await _context.Products
// //                 .Include(p => p.Category)  // include category info
// //                 .ToListAsync();
// //             return Ok(products);
// //         }

// //         // ✅ GET: api/Products/{id}
// //         [HttpGet("{id}")]
// //         public async Task<ActionResult<Product>> GetProduct(Guid id)
// //         {
// //             var product = await _context.Products
// //                 .Include(p => p.Category)
// //                 .FirstOrDefaultAsync(p => p.Id == id);

// //             if (product == null) return NotFound();
// //             return Ok(product);
// //         }

// //         // ✅ POST: api/Products
// //         [HttpPost]
// //         public async Task<ActionResult<Product>> CreateProduct(Product product)
// //         {
// //             _context.Products.Add(product);
// //             await _context.SaveChangesAsync();
// //             return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
// //         }

// //         // ✅ PUT: api/Products/{id}
// //         [HttpPut("{id}")]
// //         public async Task<IActionResult> UpdateProduct(Guid id, Product product)
// //         {
// //             if (id != product.Id) return BadRequest();

// //             _context.Entry(product).State = EntityState.Modified;
// //             await _context.SaveChangesAsync();
// //             return NoContent();
// //         }

// //         // ✅ DELETE: api/Products/{id}
// //         [HttpDelete("{id}")]
// //         public async Task<IActionResult> DeleteProduct(Guid id)
// //         {
// //             var product = await _context.Products.FindAsync(id);
// //             if (product == null) return NotFound();

// //             _context.Products.Remove(product);
// //             await _context.SaveChangesAsync();
// //             return NoContent();
// //         }
// //     }
// // }
