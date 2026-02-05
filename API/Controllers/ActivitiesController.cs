using Application.ActivitiesFeature.Commands;
using Application.ActivitiesFeature.Queries;
using Domain.entities;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    public class ActivitiesController() : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {

            return await Mediator.Send(new GetActivitiesList.Query());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityDetails(string id)
        {

            return await Mediator.Send(new GetActivityDetails.Query { Id = id });

        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateActivity(Activity activity)
        {
            return await Mediator.Send(new CreateActivity.Command { Activity = activity });
        }

        [HttpPut]
        public async Task<IActionResult> EditActivity(Activity activity)
        {            
            await Mediator.Send(new EditActivity.Command { Activity = activity });
            return Ok();
        }
    }
}