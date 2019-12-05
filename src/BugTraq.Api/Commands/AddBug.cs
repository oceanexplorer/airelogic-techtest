using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using MediatR;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BugTraq.Api.Commands
{
    public class AddBug
    {
        public class Command : IRequest
        {
            public string Title { get; set;}
            public string Description { get; set;}
            public int UserId { get; set;}
            public string Status { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly BugTraqContext _context;

            public Handler(BugTraqContext context)
            {
                _context = context;
            }
            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var bug = new Bug(request.Title, request.Description, request.Status, request.UserId);
                _context.Bugs.Add(bug);
                
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}