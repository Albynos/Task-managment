using System.Collections.Generic;
using Application.Dto;

namespace Application.ViewModels
{
    public class GetAllTasksVm
    {
        public IList<GetAllTasksDto> Tasks { get; set; }
    }
}