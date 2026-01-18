using Domain.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.db;

namespace API.Controllers
{

    public class ActivitiesController(AppDbContext context) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {

            return await context.Activities.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityDetails(string id)
        {

            var activity = await context.Activities.FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null) return NotFound();

            return activity;

        }
    }
}