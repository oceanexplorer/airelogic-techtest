using AutoMapper;
using BugTraq.Api.Models;
using BugTraq.Api.Queries;

namespace BugTraq.Api.Mappers
{
    public class BugProfile : Profile 
    {
        public BugProfile()
        {
            CreateMap<Bug, GetBugs.Result>();
        }
    }
}