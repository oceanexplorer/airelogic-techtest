using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BugTraq.Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace BugTraq.Api.Queries
{
    public static class GetBugs
    {
        public class Query : IRequest<List<Result>>
        {
        }

        public class Result
        {
            public int BugId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Status { get; set; }
            public int UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Result>>
        {
            private readonly BugTraqContext _context;
            private readonly IMapper _mapper;
        
            public Handler(BugTraqContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            
            public async Task<List<Result>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _mapper
                    .ProjectTo<Result>(_context.Bugs)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}