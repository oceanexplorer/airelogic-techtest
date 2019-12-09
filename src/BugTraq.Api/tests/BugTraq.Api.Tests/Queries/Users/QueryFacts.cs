using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BugTraq.Api.Mappers;
using BugTraq.Api.Models;
using BugTraq.Api.Queries;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BugTraq.Api.Tests.Queries.Users
{
    public class QueryFacts
    {
        private DbContextOptions<BugTraqContext> _inMemoryDbOptions;
        private readonly IMapper _mapper;

        public QueryFacts()
        {
           _inMemoryDbOptions = new DbContextOptionsBuilder<BugTraqContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
           
           _mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new UserProfile()); }).CreateMapper();
        }
        
        public class GetUsersQuery : QueryFacts
        {
            [Fact]
            public async Task ShouldReturnSameNumberOfRecordsAsAdded()
            {
                // Arrange
                var userOne = new User (new Guid("00000000-0000-0000-0000-000000000001"), "Sarah", "Smith");
                var userTwo = new User (new Guid("00000000-0000-0000-0000-000000000002"), "Jenny", "Ross");
                var userThree = new User (new Guid("00000000-0000-0000-0000-000000000003"), "Michael", "Bailey");
                var getUsersQuery = new GetUsers.Query();

                await using (var dbContext = new BugTraqContext(_inMemoryDbOptions))
                {
                    dbContext.Add(userOne);
                    dbContext.Add(userTwo);
                    dbContext.Add(userThree);
                    dbContext.SaveChanges();
                }
                
                // Act
                await using (var dbContext = new BugTraqContext(_inMemoryDbOptions))
                {
                    var bugs = await new GetUsers.Handler(dbContext,  _mapper)
                        .Handle(getUsersQuery, It.IsAny<CancellationToken>());
                    
                    // Assert
                    bugs.Count.Should().Be(3, "because three records were added to the database so three should have been returned");
                }
            }
        }
    }
}