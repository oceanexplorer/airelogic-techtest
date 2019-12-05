using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTraq.Api.Queries
{
    public static class GetBugs
    {
        public class Query : IRequest<List<Bug>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Bug>>
        {
            private readonly BugTraqContext _context;
        
            public Handler(BugTraqContext context)
            {
                _context = context;
            }
            
            public async Task<List<Bug>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Bugs.ToListAsync(cancellationToken);
            }
        }
    }
}