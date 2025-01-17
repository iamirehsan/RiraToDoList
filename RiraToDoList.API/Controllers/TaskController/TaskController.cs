using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RiraToDoList.API.Base;
using RiraToDoList.Message.Commands.Task;
using RiraToDoList.Service.Service.Interfaces;

namespace RiraToDoList.API.Controllers.TaskController
{
    [Route("api/Tasks")]
    [ApiController]
    public class TaskController : ApiControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _taskService.GetAllTasks();
            return OkResult("لیست وظایف:", result, result.TaskDetails.Count());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var result = await _taskService.GetTaskById(id);
            return OkResult($"وظیقه با آیدی {id}", result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskCommand cmd)
        {
            await _taskService.CreateTask(cmd);
            return OkResult("وظیفه با موفقیت ساخته شد");

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand cmd, Guid id)
        {
            await _taskService.UpdateTask(cmd, id);
            return OkResult($"وظیفه {id} با موفقیت ویرایش شد");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _taskService.DeleteTask(id);
            return OkResult($"وظیفه {id} با موفقیت حذف شد");
        }
    }
}
