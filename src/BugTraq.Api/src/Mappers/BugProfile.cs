using AutoMapper;
using BugTraq.Api.Commands;
using BugTraq.Api.Models;
using BugTraq.Api.Queries;

namespace BugTraq.Api.Mappers
{
    public class BugProfile : Profile 
    {
        public BugProfile()
        {
            CreateMap<Bug, GetBugs.Result>();
            CreateMap<Bug, GetBug.Result>();
            CreateMap<Bug, AddBug.Command>();
            CreateMap<UpdateBug.Command, Bug>();
        }
    }
}