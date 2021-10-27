using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    public class BaseApiController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??=HttpContext.RequestServices.GetService<IMediator>();
        
        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public BaseApiController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }
    }
}