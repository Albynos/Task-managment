using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Application.Commands
{
    public class AddTaskCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public class AddTaskHandler : IRequestHandler<AddTaskCommand, Guid>
        {
            private readonly ApplicationDbContext _applicationDbContext;
            
            public AddTaskHandler(ApplicationDbContext applicationDbContext)
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