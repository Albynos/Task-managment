using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetTaskQuery : IRequest<GetTaskDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public class GetTaskHandler : IRequestHandler<GetTaskQuery, GetTaskDto>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _applicationDbContext;

            public GetTaskHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
            {
                _mapper = mapper;
                _applicationDbContext = applicationDbContext;
            }

            public async Task<GetTaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken)
            {
                var result = await _applicationDbContext.TaskItems
                    .FirstOrDefaultAsync(task => task.UserId == request.UserId && task.Id == request.Id,
                        cancellationToken: cancellationToken);
                if (result == null)
                {
                    //TODO replace it by custom exception
                    throw new NullReferenceException($"task {request.Id} not found");
                }
                
                return _mapper.Map<GetTaskDto>(result);
            }
        }
    }
}