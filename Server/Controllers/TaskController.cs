using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Model;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> Get()
        {
            var result = _taskService.GetAll();
            if (result == null)
            {
                var errorMessage = "Can't find any tasks";
                _logger.Log(LogLevel.Error,errorMessage);
                return BadRequest(errorMessage);
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]

        public async Task<ActionResult<TaskItem>> Get(Guid guid)
        {
            var result = _taskService.GetByGuid(guid);
            if (result == null)
            {
                var errorMessage = $"Unknown task guid {guid}";
                _logger.Log(LogLevel.Error, errorMessage);
                return BadRequest(errorMessage);
            }

            return Ok(result);
        }
    }
}