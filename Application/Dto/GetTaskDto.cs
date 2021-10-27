using System;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Dto
{
    public class GetTaskDto : IMapFor<TaskItem>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public TaskStatus Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TaskItem, GetTaskDto>()
                .ForMember(taskItemDto => taskItemDto.Id, 
                    opt => opt.MapFrom(task => task.Id))
                .ForMember(taskItemDto => taskItemDto.Title, 
                    opt => opt.MapFrom(task => task.Title))
                .ForMember(taskItemDto => taskItemDto.Description, 
                    opt => opt.MapFrom(task => task.Description))
                .ForMember(taskItemDto => taskItemDto.CreationDate, 
                    opt => opt.MapFrom(task => task.CreationDate))
                .ForMember(taskItemDto => taskItemDto.EditDate, 
                    opt => opt.MapFrom(task => task.EditTime))
                .ForMember(taskItemDto => taskItemDto.Status, 
                    opt => opt.MapFrom(task => task.Status));
        }
    }
}