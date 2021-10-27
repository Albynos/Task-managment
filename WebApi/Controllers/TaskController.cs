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
            var query = new GetAllTasksQuery(UserId);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTaskDto>> Get(Guid id)
        {
            var query = new GetTaskQuery(UserId, id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddTaskDto taskDto)
        {
            var command = new AddTaskCommand(UserId, taskDto.Description,taskDto.Title);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteTaskCommand(UserId, id);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody]UpdateTaskDto updateTaskDto, Guid id)
        {
            var command = new UpdateTaskCommand(
                UserId,
                id,
                updateTaskDto.Title,
                updateTaskDto.Description,
                updateTaskDto.Status
            );
            await Mediator.Send(command);
            return NoContent();
        }
    }
}