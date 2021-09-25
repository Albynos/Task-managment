using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetAllTasksQuery : IRequest<GetAllTasksVm>
    {
        public Guid UserId { get; set; }

        public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, GetAllTasksVm>
        {
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;
            
            public GetAllTasksHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }
            
            public async Task<GetAllTasksVm> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
            {
                var tasksQuery =  await _applicationDbContext.TaskItems
                    .Where(t => t.UserId == request.UserId)
                    .ProjectTo<GetAllTasksDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                return new GetAllTasksVm {Tasks = tasksQuery};
            }
        }
    }
}