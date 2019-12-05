using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using MediatR;

namespace BugTraq.Api.Commands
{
    public static class UpdateBugStatus
    {
        public class Command : IRequest
        {
            public int Id { get; }
            public string Status { get; }

            public Command(int id, string status)
            {
                Id = id;
                Status = status;
            }
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