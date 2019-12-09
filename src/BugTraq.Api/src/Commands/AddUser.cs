using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using MediatR;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BugTraq.Api.Commands
{
    public class AddUser
    {
        public class Command : IRequest
        {
            public string FirstName { get; set;}
            public string Surname { get; set;}
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
                var user = new User(request.FirstName, request.Surname);
                _context.Users.Add(user);
                
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}