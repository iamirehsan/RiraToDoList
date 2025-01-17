using AutoMapper;
using RiraToDoList.Domain.Entites;
using RiraToDoList.Message.Dtos.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraToDoList.Service.Implimentation.MappingProfiles
{
    public class TaskToTaskDetailDtoProfile : Profile
    {
        public TaskToTaskDetailDtoProfile()
        {
            CreateMap<Domain.Entites.Task, TaskDetailDto>();
        }
    }
}
