using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.Queries;
using FluentAssertions;
using MediatR;
using NUnit.Framework;

namespace Tests.Queries
{
    public class GetTaskQueryTest : TestQueryBase
    {
        private IRequestHandler<GetTaskQuery, GetTaskDto> _handler;

        [SetUp]
        public void Start()
        {
            _handler = new GetTaskQuery.GetTaskHandler(Mapper, Context);
        }

        [Test]
        public async Task Handle_ShouldGetTask_WhenCorrectDate()
        { 
            var result = await _handler.Handle(
                new GetTaskQuery (TaskDbContextFactory.FirstUserId, TaskDbContextFactory.FirstTaskId), 
                CancellationToken.None);
            ;
            result.Should().NotBeNull();
            result.Id.Should().Be(TaskDbContextFactory.FirstTaskId);
        }

        [Test]
        public async Task Handle_ShouldThrowAnException_WhenUnknownTaskId()
        {
            Func<Task> result = async () => await _handler.Handle(
                new GetTaskQuery(Guid.NewGuid(),TaskDbContextFactory.FirstUserId), 
                CancellationToken.None);
            await result.Should().ThrowAsync<NullReferenceException>();
        }
        
        [Test]
        public async Task Handle_ShouldThrowAnException_WhenUnknownUserId()
        {
            Func<Task> result = async () => await _handler.Handle(
                new GetTaskQuery(TaskDbContextFactory.FirstUserId,Guid.NewGuid()), 
                CancellationToken.None);
            await result.Should().ThrowAsync<NullReferenceException>();
        }
    }
}