using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = RiraToDoList.Domain.Entites.Task;

namespace RiraToDoList.Domain.Repository.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
    }
}
