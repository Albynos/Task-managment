using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public record GetAllTasksQuery(Guid UserId) : IRequest<GetAllTasksVm>
    {
        public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, GetAllTasksVm>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;
            
            public GetAllTasksHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
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