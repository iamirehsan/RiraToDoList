using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraToDoList.Message.Dtos.Task
{
    public record TasksDto(IEnumerable<TaskDetailDto> TaskDetails)
    {
    }
}
