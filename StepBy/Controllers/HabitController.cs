using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StepBy.Data;
using StepBy.Representation;
using StepBy.Models;
using System.Collections;

namespace StepBy.Controllers
{
    [ApiController]
    [Route("habits")]
    public class HabitController : ControllerBase
    {
        public List<int> weekDaysNumbers =  new List<int> { 0, 1, 2, 3, 4, 5, 6 };

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
                .FirstOrDefaultAsync(h => h.id == id);

            if (habit == null)
            {
                return NotFound();
            }

            return Ok(HabitRepresentation.OneRepresentation.of(habit));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] HabitRepresentation.CreateUpdateRepresentation body)
        {
            Habit habit = HabitRepresentation.CreateUpdateRepresentation.of(body);

            await context.habits.AddAsync(habit);
            await context.SaveChangesAsync();

            return Ok(HabitRepresentation.OneRepresentation.of(habit));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] HabitRepresentation.CreateUpdateRepresentation body,
            [FromRoute] String id)
        {
            context.habitWeekDays.RemoveRange(context.habitWeekDays.Where(w => w.habit_id == id));
            weekDaysNumbers.ForEach(dayNumber =>
            {
                if (weekDaysNumbers.Contains(dayNumber))
                {
                    return;
                }

                context.dayHabits.RemoveRange(context.dayHabits.Where(dh => dh.habit_id == id));
            });
            await context.SaveChangesAsync();

            Habit habit = HabitRepresentation.CreateUpdateRepresentation.of(body);

            await context.habits.AddAsync(habit);
            context.Entry(habit).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok(HabitRepresentation.OneRepresentation.of(habit));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] String id)
        {
            context.habitWeekDays.RemoveRange(context.habitWeekDays.Where(hw => hw.habit_id == id));
            context.dayHabits.RemoveRange(context.dayHabits.Where(dh => dh.habit_id == id));
            context.habits.RemoveRange(context.habits.Where(h => h.id == id));

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
