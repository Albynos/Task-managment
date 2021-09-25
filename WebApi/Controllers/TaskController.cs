using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands;
using Application.Dto;
using Application.Queries;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private IMediator _mediator;
        private IMediator Mediator => _mediator ??=HttpContext.RequestServices.GetService<IMediator>();

        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllTasksVm>> Get()
        {
            var query = new GetAllTasksQuery { UserId = UserId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTaskDto>> Get(Guid id)
        {
            var query = new GetTaskQuery { Id = id, UserId = UserId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddTaskDto taskDto)
        {
            var command = new AddTaskCommand
            {
                Description = taskDto.Description,
                Title = taskDto.Title,
                UserId = UserId,
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}