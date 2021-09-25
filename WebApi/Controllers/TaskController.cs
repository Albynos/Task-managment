using System;
using System.Threading.Tasks;
using Application.Commands;
using Application.Dto;
using Application.Queries;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : BaseApiController
    {
        public TaskController(ILogger<TaskController> logger) : base(logger) { }

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteTaskCommand {Id = id, UserId = UserId};
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody]UpdateTaskDto updateTaskDto, Guid id)
        {
            var command = new UpdateTaskCommand
            {
                Id = id,
                UserId = UserId,
                Description = updateTaskDto.Description,
                Status = updateTaskDto.Status,
                Title = updateTaskDto.Title,
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}