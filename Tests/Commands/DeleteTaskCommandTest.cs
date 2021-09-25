using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using FluentAssertions;
using MediatR;
using NUnit.Framework;

namespace Tests.Commands
{
    public class DeleteTaskCommandTest : TestCommandBase
    {
        private IRequestHandler<DeleteTaskCommand> _handler;

        [SetUp]
        public void Start()
        {
            _handler = new DeleteTaskCommand.DeleteTaskHandler(Context);
        }

        [Test]
        public async Task Handle_ShouldDeleteTaskItem_WithCorrectData()
        {
            var result = Handle(TaskDbContextFactory.FirstTaskId, TaskDbContextFactory.FirstUserId);
            await result.Invoke().ConfigureAwait(false);
            var taskItem = Context.TaskItems.SingleOrDefault(t => t.Id == TaskDbContextFactory.FirstTaskId);
            taskItem.Should().BeNull();
        }

        [Test]
        public async Task Handle_ShouldThrowAnException_WhenUnknownTaskId()
        {
            var result = Handle(Guid.NewGuid(), TaskDbContextFactory.FirstUserId);
            //TODO Replace NullReferenceException By Custom exception
            await result.Should().ThrowAsync<NullReferenceException>();
        }

        [Test]
        public async Task Handle_ShouldThrowAnException_WhenUnknownUserId()
        {
            var result = Handle(TaskDbContextFactory.FirstTaskId, Guid.NewGuid());
            await result.Should().ThrowAsync<NullReferenceException>();
        }

        private Func<Task> Handle(Guid id, Guid UserId)
        {
            async Task Result() => 
                await _handler.Handle(new DeleteTaskCommand
                {
                    Id = id, UserId = UserId
                }, CancellationToken.None);

            return Result;
        }
    }
}