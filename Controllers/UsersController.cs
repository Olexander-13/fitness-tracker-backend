using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessTrackerApi.Data;
using FitnessTrackerApi.Models;
using System.Security.Claims;

namespace FitnessTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly FitnessContext _db;

        public UsersController(FitnessContext db)
        {
            _db = db;
        }

        [HttpGet("me")]
        public async Task<ActionResult<User>> GetMe()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _db.Users
                .AsNoTracking()
                .Select(u => new { u.Id, u.Name, u.Email })
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}