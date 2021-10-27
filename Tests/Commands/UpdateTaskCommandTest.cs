using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Tests.Commands
{
    public class UpdateTaskCommandTest : TestCommandBase
    {
        private IRequestHandler<UpdateTaskCommand> _handler;

        [SetUp]
        public void Start()
        {
            _handler = new UpdateTaskCommand.UpdateTaskHandle(Context);
        }

        [Test]
        public async Task Handle_ShouldUpdateTask_WhenCorrectTaskIdAndUserId()
        {
            const string newDescription = "new description";
            const string newTitle = "new title";
            const TaskStatus newStatus = TaskStatus.Done;

            await _handler.Handle(
                new UpdateTaskCommand(
                    TaskDbContextFactory.FirstUserId,
                    TaskDbContextFactory.FirstTaskId,
                    newTitle,
                    newDescription,
                    newStatus), CancellationToken.None);

            var taskItem = await Context.TaskItems.SingleOrDefaultAsync(task => task.Id == TaskDbContextFactory.FirstTaskId);
            
            taskItem.Should().NotBeNull();
            taskItem.Description.Should().Be(newDescription);
            taskItem.Title.Should().Be(newTitle);
            taskItem.Status.Should().Be(newStatus);
        }

        [Test]
        public async Task Handle_ShouldThrowAnException_WhenUnknownTaskId()
        {
            Func<Task> result = async () => await _handler.Handle(
                new UpdateTaskCommand(TaskDbContextFactory.FirstUserId, Guid.NewGuid(), "","", TaskStatus.Undone),
                CancellationToken.None);

            await result.Should().ThrowAsync<NullReferenceException>();
        }
        
        [Test]
        public async Task Handle_ShouldThrowAnException_WhenUnknownUserId()
        {
            Func<Task> result = async () => await _handler.Handle(
                new UpdateTaskCommand(Guid.NewGuid(), TaskDbContextFactory.FirstTaskId, "","", TaskStatus.Undone),
                CancellationToken.None);

            await result.Should().ThrowAsync<NullReferenceException>();
        }
    }
}