using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraToDoList.Domain.Entites
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; private set ; }
        public DateTime CreatedAt { get; private set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}
