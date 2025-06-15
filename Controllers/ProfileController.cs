using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessTrackerApi.Data;
using FitnessTrackerApi.Models.Dto;
using FitnessTrackerApi.Services;
using System.Security.Claims;

namespace FitnessTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly FitnessContext _db;

        public ProfileController(FitnessContext db)
        {
            _db = db;
        }

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // GET: api/profile
        [HttpGet]
        public async Task<ActionResult> GetProfile()
        {
            var user = await _db.Users.AsNoTracking()
                .Select(u => new { u.Id, u.Name, u.Email })
                .FirstOrDefaultAsync(u => u.Id == UserId);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // PUT: api/profile
        [HttpPut]
        public async Task<ActionResult> UpdateName(UpdateNameDto dto)
        {
            var user = await _db.Users.FindAsync(UserId);
            if (user == null) return NotFound();
            user.Name = dto.Name;
            await _db.SaveChangesAsync();
            return Ok(new { user.Id, user.Name, user.Email });
        }

        // PUT: api/profile/password
        [HttpPut("password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var user = await _db.Users.FindAsync(UserId);
            if (user == null) return NotFound();

            if (!AuthService.VerifyPassword(dto.CurrentPassword, user.PasswordHash))
                return BadRequest(new { message = "Current password is incorrect." });

            user.PasswordHash = AuthService.HashPassword(dto.NewPassword);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Password changed." });
        }
    }
}