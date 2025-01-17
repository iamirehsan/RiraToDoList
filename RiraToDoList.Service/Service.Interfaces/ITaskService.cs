using RiraToDoList.Message.Commands.Task;
using RiraToDoList.Message.Dtos.Task;

namespace RiraToDoList.Service.Service.Interfaces
{
    public interface ITaskService
    {
        Task CreateTask(CreateTaskCommand command);
        Task UpdateTask(UpdateTaskCommand command, Guid id);
        Task DeleteTask(Guid id);
        Task<TasksDto> GetAllTasks();
        Task<TaskDetailDto> GetTaskById(Guid id);
    }
}
