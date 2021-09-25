using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Application.Commands
{
    public class UpdateTaskCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }

        public class UpdateTaskHandle : IRequestHandler<UpdateTaskCommand>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public UpdateTaskHandle(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
            {
                var foundTask = await _applicationDbContext.TaskItems
                    .SingleOrDefaultAsync(task => task.Id == request.Id && task.UserId == request.UserId, cancellationToken: cancellationToken);

                //TODO do message more clear for wrong userId
                if (foundTask == null)
                {
                    //TODO Replace it by custom exception
                    throw new NullReferenceException($"task {request.Id} not found");
                }

                foundTask.Title = request.Title;
                foundTask.Description = request.Description;
                foundTask.EditTime = DateTime.Now;
                foundTask.Status = request.Status;

                _applicationDbContext.TaskItems.Update(foundTask);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}