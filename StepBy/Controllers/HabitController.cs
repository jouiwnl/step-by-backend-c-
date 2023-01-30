using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StepBy.Data;
using StepBy.Representation;
using StepBy.Models;

namespace StepBy.Controllers
{
    [ApiController]
    [Route("habits")]
    public class HabitController : ControllerBase
    {
        public AppDbContext context;

        public HabitController([FromServices] AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery(Name = "user_id")] String user_id,
            [FromQuery(Name = "year")] int year)
        {
            var endYear = new DateTime(year, 12, 31);

            List<Habit> habits = await context
                .habits
                .Where(h =>
                   DateTime.Compare(DateTime.Parse(endYear.ToString("yyyy-MM-dd")), DateTime.Parse(endYear.ToString("yyyy-MM-dd"))) <= 0 &&
                   h.user_id == user_id
                )
                .Include(h => h.weekDays)
                .AsNoTracking()
                .ToListAsync();

            List<HabitRepresentation.ListRepresentation> finalHabits = habits
                .ConvertAll(HabitRepresentation.ListRepresentation.of)
                .ToList();

            return Ok(finalHabits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] string id)
        {
            Habit habit = await context
                .habits
                .Include(m => m.weekDays)
                .AsNoTracking()
                .FirstAsync(h => h.id == id);

            return Ok(HabitRepresentation.OneRepresentation.of(habit));
        }
    }
}
