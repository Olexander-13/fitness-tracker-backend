using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessTrackerApi.Data;
using FitnessTrackerApi.Models;
using FitnessTrackerApi.Services;

namespace FitnessTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly FitnessContext _db;
    private readonly JwtService _jwt;

    public AuthController(FitnessContext db, JwtService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest req)
    {
        if (await _db.Users.AnyAsync(u => u.Email == req.Email))
            return BadRequest("Email already exists");
        var hash = AuthService.HashPassword(req.Password);
        var user = new User { Name = req.Name, Email = req.Email, PasswordHash = hash };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var token = _jwt.GenerateToken(user);
        return new AuthResponse { Token = token, Name = user.Name, Email = user.Email };
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest req)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == req.Email);
        if (user == null || !AuthService.VerifyPassword(req.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");
        var token = _jwt.GenerateToken(user);
        return new AuthResponse { Token = token, Name = user.Name, Email = user.Email };
    }
}