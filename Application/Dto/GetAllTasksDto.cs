using System;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto
{
    public class GetAllTasksDto : IMapFor<TaskItem>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TaskItem, GetAllTasksDto>()
                .ForMember(taskDto => taskDto.Id,
                    opt => opt.MapFrom(task => task.Id))
                .ForMember(taskDto => taskDto.Title,
                    opt => opt.MapFrom(task => task.Title))
                .ForMember(taskDto => taskDto.Description,
                    opt => opt.MapFrom(task => task.Description));
        }
    }
}