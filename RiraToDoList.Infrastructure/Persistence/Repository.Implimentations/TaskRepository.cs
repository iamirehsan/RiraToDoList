using RiraToDoList.Domain.Repository.Interfaces;
using RiraToDoList.Infrastructure.Persistence.Context;
using Task = RiraToDoList.Domain.Entites.Task;


namespace RiraToDoList.Infrastructure.Persistence.Repository.Implimentations
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
