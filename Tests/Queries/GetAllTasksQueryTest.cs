using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries;
using Application.ViewModels;
using FluentAssertions;
using MediatR;
using NUnit.Framework;

namespace Tests.Queries
{
    [TestFixture]
    public class GetAllTasksQueryTest : TestQueryBase
    {
        private IRequestHandler<GetAllTasksQuery, GetAllTasksVm> _handler;

        [SetUp]
        public void Start()
        {
            _handler = new GetAllTasksQuery.GetAllTasksHandler(Context, Mapper);
        }

        [Test]
        public async Task Handle_ShouldGetAllTasks_WhenCorrectUserId()
        {
            var result = await _handler.Handle(new GetAllTasksQuery
            {
                UserId = TaskDbContextFactory.FirstUserId,
            }, CancellationToken.None);

            result.Tasks.Should().NotBeNull();
            result.Tasks.Should().HaveCount(2);
        }

        [Test]
        public async Task Handle_ShouldBeEmpty_WhenUnknownUserId()
        {
            var result = await _handler.Handle(new GetAllTasksQuery
            {
                UserId = Guid.NewGuid()
            }, CancellationToken.None);

            result.Tasks.Should().BeEmpty();
        }
    }
}