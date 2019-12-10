using System.Collections.Generic;
using System.Threading.Tasks;
using BugTraq.Api.Commands;
using Microsoft.AspNetCore.Mvc;
using BugTraq.Api.Queries;
using MediatR;

namespace BugTraq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<GetUsers.Result>> GetUsers()
        {
            return await _mediator.Send(new GetUsers.Query());
        }

        [HttpPost]
        public async Task AddUser([FromBody]AddUser.Command user)
        {
            await _mediator.Send(user);
        }
    }
}
