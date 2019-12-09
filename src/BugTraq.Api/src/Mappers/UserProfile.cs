using AutoMapper;
using BugTraq.Api.Models;
using BugTraq.Api.Queries;

namespace BugTraq.Api.Mappers
{
    public class UserProfile : Profile 
    {
        public UserProfile()
        {
            CreateMap<User, GetUsers.Result>();
        }
    }
}