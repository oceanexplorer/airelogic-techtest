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
    public class GetUsers
    {
        public class Query : IRequest<List<Result>>
        {
        }

        public class Result
        {
            public Guid UserId { get; set; }
            public string FirstName { get; set; }
            public string Surname { get; set; }
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
                    .ProjectTo<Result>(_context.Users)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}