using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using MediatR;

namespace BugTraq.Api.Commands
{
    public class UpdateBugStatus
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
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
                var ticket = await _context.Bugs.FindAsync(request.Id);
                ticket.Status = request.Status;

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}