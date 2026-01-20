using Application.ActivitiesFeature.Queries;
using Domain.entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    public class ActivitiesController(IMediator mediator) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {

            return await mediator.Send(new GetActivitiesList.Query());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityDetails(string id)
        {

            return await mediator.Send(new GetActivityDetails.Query { Id = id });

        }
    }
}