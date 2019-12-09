using System;
using System.Linq;
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
    public class GetBug
    {
        public class Query : IRequest<Result>
        {
            public int Id { get; }

            public Query(int id)
            {
                Id = id;
            }
        }

        public class Result
        {
            public int BugId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Status { get; set; }
            public int UserId { get; set; }
            public string UserFirstName { get; set; }
            public string UserSurname { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly BugTraqContext _context;
            private readonly IMapper _mapper;
        
            public Handler(BugTraqContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            
            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _mapper
                    .ProjectTo<Result>(_context.Bugs.Include(e => e.User))
                    .Where(b => b.BugId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}