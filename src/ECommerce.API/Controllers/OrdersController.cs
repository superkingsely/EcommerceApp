

using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // ✅ GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        // ✅ GET: api/Orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // ✅ POST: api/Orders
        [HttpPost]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            var created = await _orderService.CreateAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = created.Id }, created);
        }

        // ✅ PUT: api/Orders/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(Guid id, Order order)
        {
            var updated = await _orderService.UpdateAsync(id, order);
            return Ok(updated);
        }

        // ✅ DELETE: api/Orders/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var deleted = await _orderService.DeleteAsync(id);
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
//     public class OrdersController : ControllerBase
//     {
//         private readonly IOrderRepository _orderRepository;

//         public OrdersController(IOrderRepository orderRepository)
//         {
//             _orderRepository = orderRepository;
//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
//         {
//             var orders = await _orderRepository.GetAllAsync();
//             return Ok(orders);
//         }

//         [HttpGet("{id}")]
//         public async Task<ActionResult<Order>> GetOrder(Guid id)
//         {
//             var order = await _orderRepository.GetByIdAsync(id);
//             if (order == null) return NotFound();
//             return Ok(order);
//         }

//         [HttpPost]
//         public async Task<ActionResult> CreateOrder(Order order)
//         {
//             await _orderRepository.AddAsync(order);
//             return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
//         }

//         [HttpPut("{id}")]
//         public async Task<ActionResult> UpdateOrder(Guid id, Order order)
//         {
//             if (id != order.Id) return BadRequest();

//             var existing = await _orderRepository.GetByIdAsync(id);
//             if (existing == null) return NotFound();

//             existing.TotalAmount = order.TotalAmount;
//             existing.UserId = order.UserId;
//             existing.OrderItems = order.OrderItems;
//             existing.Status = order.Status;

//             await _orderRepository.UpdateAsync(existing);
//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         public async Task<ActionResult> DeleteOrder(Guid id)
//         {
//             var existing = await _orderRepository.GetByIdAsync(id);
//             if (existing == null) return NotFound();

//             await _orderRepository.DeleteAsync(existing);
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
//     public class OrdersController : ControllerBase
//     {
//         private readonly AppDbContext _context;

//         public OrdersController(AppDbContext context)
//         {
//             _context = context;
//         }

//         // ✅ GET: api/Orders
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
//         {
//             var orders = await _context.Orders
//                 .Include(o => o.User)
//                 .Include(o => o.OrderItems)
//                 .ToListAsync();
//             return Ok(orders);
//         }

//         // ✅ GET: api/Orders/{id}
//         [HttpGet("{id}")]
//         public async Task<ActionResult<Order>> GetOrder(Guid id)
//         {
//             var order = await _context.Orders
//                 .Include(o => o.User)
//                 .Include(o => o.OrderItems)
//                 .FirstOrDefaultAsync(o => o.Id == id);

//             if (order == null) return NotFound();
//             return Ok(order);
//         }

//         // ✅ POST: api/Orders
//         [HttpPost]
//         public async Task<ActionResult<Order>> CreateOrder(Order order)
//         {
//             _context.Orders.Add(order);
//             await _context.SaveChangesAsync();
//             return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
//         }

//         // ✅ PUT: api/Orders/{id}
//         [HttpPut("{id}")]
//         public async Task<IActionResult> UpdateOrder(Guid id, Order order)
//         {
//             if (id != order.Id) return BadRequest();

//             _context.Entry(order).State = EntityState.Modified;
//             await _context.SaveChangesAsync();
//             return NoContent();
//         }

//         // ✅ DELETE: api/Orders/{id}
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteOrder(Guid id)
//         {
//             var order = await _context.Orders.FindAsync(id);
//             if (order == null) return NotFound();

//             _context.Orders.Remove(order);
//             await _context.SaveChangesAsync();
//             return NoContent();
//         }
//     }
// }
