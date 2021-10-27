using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.Commands
{
    [TestFixture]
    public class AddTaskCommandTest : TestCommandBase
    {
        private IRequestHandler<AddTaskCommand, Guid> _handler;

        [SetUp]
        public void Start()
        {
            _handler = new AddTaskCommand.AddTaskHandler(Context);
        }

        [Test]
        public async Task Handle_ShouldAddNewTask_WhenCorrectData()
        {
            const string description = "Task description from test";
            const string title = "task title from test";
            var creationTaskGuid = await _handler.Handle(
                new AddTaskCommand(TaskDbContextFactory.FirstTaskId,description,title),
                CancellationToken.None); 
            
            var result = await Context.TaskItems
                .SingleOrDefaultAsync(task => task.Id == creationTaskGuid);

            result.Should().NotBeNull();
            result.Description.Equals(description);
            result.Title.Equals(title);
            result.CreationDate.Should().BeSameDateAs(DateTime.Now);
        }
    }
}