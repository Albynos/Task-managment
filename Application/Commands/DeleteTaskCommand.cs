using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands
{
    public class DeleteTaskCommand : IRequest
    { 
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public DeleteTaskHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
            {
                var foundTask = await _applicationDbContext.TaskItems
                        .SingleOrDefaultAsync(task => task.UserId == request.UserId && task.Id == request.Id, cancellationToken);
                if (foundTask == null)
                {
                    //TODO replace it by custom exception
                    throw new NullReferenceException($"task {request.Id} not found");
                }

                _applicationDbContext.TaskItems.Remove(foundTask);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}