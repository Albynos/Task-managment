using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Application.Commands
{
    public record AddTaskCommand(Guid UserId, string Title, string Description) : IRequest<Guid>
    {
        public class AddTaskHandler : IRequestHandler<AddTaskCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public AddTaskHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            
            public async Task<Guid> Handle(AddTaskCommand request, CancellationToken cancellationToken)
            {
                var taskId = Guid.NewGuid();
                var task = new TaskItem
                {
                    CreationDate = DateTime.Now,
                    Description = request.Description,
                    Title = request.Title,
                    UserId = request.UserId,
                    Status = TaskStatus.Undone,
                    Id = taskId,
                    EditTime = null,
                };

                await _applicationDbContext.TaskItems.AddAsync(task, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return taskId;
            }
        }
    }
}