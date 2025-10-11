


using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // ✅ GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // ✅ GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // ✅ POST: api/Users
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            var created = await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
        }

        // ✅ PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, User user)
        {
            var updated = await _userService.UpdateAsync(id, user);
            return Ok(updated);
        }

        // ✅ DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var deleted = await _userService.DeleteAsync(id);
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
//     public class UsersController : ControllerBase
//     {
//         private readonly IUserRepository _userRepository;

//         public UsersController(IUserRepository userRepository)
//         {
//             _userRepository = userRepository;
//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<User>>> GetUsers()
//         {
//             var users = await _userRepository.GetAllAsync();
//             return Ok(users);
//         }

//         [HttpGet("{id}")]
//         public async Task<ActionResult<User>> GetUser(Guid id)
//         {
//             var user = await _userRepository.GetByIdAsync(id);
//             if (user == null) return NotFound();
//             return Ok(user);
//         }

//         [HttpPost]
//         public async Task<ActionResult> CreateUser(User user)
//         {
//             await _userRepository.AddAsync(user);
//             return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
//         }

//         [HttpPut("{id}")]
//         public async Task<ActionResult> UpdateUser(Guid id, User user)
//         {
//             if (id != user.Id) return BadRequest();

//             var existing = await _userRepository.GetByIdAsync(id);
//             if (existing == null) return NotFound();

//             existing.FullName = user.FullName;
//             existing.Email = user.Email;
//             existing.PasswordHash = user.PasswordHash;

//             await _userRepository.UpdateAsync(existing);
//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         public async Task<ActionResult> DeleteUser(Guid id)
//         {
//             var existing = await _userRepository.GetByIdAsync(id);
//             if (existing == null) return NotFound();

//             await _userRepository.DeleteAsync(existing);
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
//     public class UsersController : ControllerBase
//     {
//         private readonly AppDbContext _context;

//         public UsersController(AppDbContext context)
//         {
//             _context = context;
//         }

//         // ✅ GET: api/Users
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<User>>> GetUsers()
//         {
//             var users = await _context.Users.ToListAsync();
//             return Ok(users);
//         }

//         // ✅ GET: api/Users/{id}
//         [HttpGet("{id}")]
//         public async Task<ActionResult<User>> GetUser(Guid id)
//         {
//             var user = await _context.Users.FindAsync(id);
//             if (user == null) return NotFound();
//             return Ok(user);
//         }
//     }
// }
