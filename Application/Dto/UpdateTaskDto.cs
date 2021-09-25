using System;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Dto
{
    public class UpdateTaskDto : IMapFor<TaskItem>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; } 
    }
}