using AutoMapper;
using RiraToDoList.Domain;
using RiraToDoList.Domain.Base;
using RiraToDoList.Message.Commands.Task;
using RiraToDoList.Message.Dtos.Task;
using RiraToDoList.Service.Service.Interfaces;
using Task = System.Threading.Tasks.Task;
using TaskEntity = RiraToDoList.Domain.Entites.Task;


namespace RiraToDoList.Service.Implimentation.Service.Implimentations
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateTask(CreateTaskCommand command)
        {
            if (_unitOfWork.TaskRepository.Any(z => z.Title == command.Title))
                throw new ManagedException("نام تکراری برای وظیفه غیرمجاز است.لطفا تصحیح کنید.");
            var taskEntity = TaskEntity.Create(command.Title, command.Description, command.IsCompleted, command.DueDate);
            await _unitOfWork.TaskRepository.AddAsync(taskEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateTask(UpdateTaskCommand command, Guid id)
        {
            if (_unitOfWork.TaskRepository.Any(z => z.Title == command.Title && z.Id != id))
                throw new ManagedException("نام تکراری برای وظیفه غیرمجاز است.لطفا تصحیح کنید.");
            if (_unitOfWork.TaskRepository.Any(z => z.Id != id))
                throw new ManagedException("وظیفه ای با آیدی دریافتی یافت نشد");
            var taskEntity = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            taskEntity.Update(command.Title, command.Description, command.IsCompleted, command.DueDate);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteTask(Guid id)
        {
            if (_unitOfWork.TaskRepository.Any(z => z.Id != id))
                throw new ManagedException("وظیفه ای با آیدی دریافتی یافت نشد");
            var taskEntity = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            _unitOfWork.TaskRepository.Remove(taskEntity);
            await _unitOfWork.CommitAsync();


        }

        public async Task<TasksDto> GetAllTasks()
        {
            var taskEntities = await _unitOfWork.TaskRepository.GetAllAsync();
            var taskDtos = taskEntities?.Select(_mapper.Map<TaskDetailDto>);
            return new TasksDto(taskDtos);

        }

        public async Task<TaskDetailDto> GetTaskById(Guid id)
        {
            var taskEntity = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            return _mapper.Map<TaskDetailDto>(taskEntity);
        }
    }
}
