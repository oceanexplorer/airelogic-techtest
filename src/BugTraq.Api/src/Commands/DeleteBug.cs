using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using MediatR;
using Newtonsoft.Json;

namespace BugTraq.Api.Commands
{
    public class DeleteBug
    {
        public class Command : IRequest
        {
            public int Id { get; }

            public Command(int id)
            {
                Id = id;
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
                var bug = await _context.Bugs.FindAsync(request.Id);

                if (bug != null)
                {
                    _context.Bugs.Remove(bug);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}