using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessTrackerApi.Data;
using FitnessTrackerApi.Models;
using FitnessTrackerApi.Models.Dto;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FitnessTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Remove or comment out if you want to test without auth
    public class GoalsController : ControllerBase
    {
        private readonly FitnessContext _context;

        public GoalsController(FitnessContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GoalDto>> GetGoal()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var goal = await _context.Goals.FirstOrDefaultAsync(g => g.UserId == userId);

            if (goal == null)
            {
                return Ok(new GoalDto { WeeklyDuration = 150, WeeklyCalories = 2000 });
            }

            return Ok(new GoalDto
            {
                WeeklyDuration = goal.WeeklyDuration,
                WeeklyCalories = goal.WeeklyCalories
            });
        }

        [HttpPut]
        public async Task<ActionResult<GoalDto>> UpdateGoal([FromBody] GoalDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var goal = await _context.Goals.FirstOrDefaultAsync(g => g.UserId == userId);
            if (goal == null)
            {
                goal = new Goal
                {
                    UserId = userId,
                    WeeklyDuration = dto.WeeklyDuration,
                    WeeklyCalories = dto.WeeklyCalories
                };
                _context.Goals.Add(goal);
            }
            else
            {
                goal.WeeklyDuration = dto.WeeklyDuration;
                goal.WeeklyCalories = dto.WeeklyCalories;
            }
            await _context.SaveChangesAsync();

            return Ok(new GoalDto
            {
                WeeklyDuration = goal.WeeklyDuration,
                WeeklyCalories = goal.WeeklyCalories
            });
        }
    }
}