using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BugTraq.Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BugTraq.Api.Commands
{
    public class UpdateBug
    {
        public class Command : IRequest
        {
            public int BugId { get; set; }
            public string Title { get; set;}
            public string Description { get; set;}
            public Guid UserId { get; set;}
            public string Status { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly BugTraqContext _context;
            private readonly IMapper _mapper;

            public Handler(BugTraqContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var existingBug = await _context.Bugs.FindAsync(request.BugId);

                if (existingBug != null)
                {
                    _context.Entry(existingBug).State = EntityState.Modified;
                    _mapper.Map(request, existingBug);
                    
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}