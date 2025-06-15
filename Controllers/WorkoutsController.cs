using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessTrackerApi.Data;
using FitnessTrackerApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FitnessTrackerApi.Models.Dto;

namespace FitnessTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkoutsController : ControllerBase
    {
        private readonly FitnessContext _db;
        public WorkoutsController(FitnessContext db) => _db = db;

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> Get()
        {
            var ws = await _db.Workouts.Where(w => w.UserId == UserId).ToListAsync();
            return Ok(ws);
        }

        [HttpPost]
        public async Task<ActionResult<Workout>> Add([FromBody] WorkoutDto w)
        {
            var workout = new Workout
            {
                Date = w.Date,
                Type = w.Type,
                Duration = w.Duration,
                Calories = w.Calories,
                Notes = w.Notes,
                UserId = UserId
            };
            _db.Workouts.Add(workout);
            await _db.SaveChangesAsync();
            return Ok(workout);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Workout w)
        {
            if (id != w.Id) return BadRequest();
            var workout = await _db.Workouts.FirstOrDefaultAsync(x => x.Id == id && x.UserId == UserId);
            if (workout == null) return NotFound();
            workout.Date = w.Date;
            workout.Type = w.Type;
            workout.Duration = w.Duration;
            workout.Calories = w.Calories;
            workout.Notes = w.Notes;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var workout = await _db.Workouts.FirstOrDefaultAsync(x => x.Id == id && x.UserId == UserId);
            if (workout == null) return NotFound();
            _db.Workouts.Remove(workout);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}