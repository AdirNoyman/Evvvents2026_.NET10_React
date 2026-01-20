using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.db;

namespace Application.ActivitiesFeature.Queries
{
    public class GetActivitiesList
    {
        public class Query : IRequest<List<Activity>> { }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Activity>>
        {
            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Activities.ToListAsync(cancellationToken);
            }
        }
    }
}