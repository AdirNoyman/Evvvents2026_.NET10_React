
using Domain.entities;
using MediatR;
using Persistence.db;

namespace Application.ActivitiesFeature.Commands
{
    public class EditActivity
    {
        public class Command : IRequest
        {
            public required Activity Activity { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities.FindAsync([request.Activity.Id], cancellationToken) ?? throw new Exception("Activity not found 😫");

                activity.Title = request.Activity.Title;

                await context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}