using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraToDoList.Domain.Base
{
    [Serializable]
    public class ManagedException : Exception
    {

        public ManagedException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }



        public string ErrorMessage { get; }
    }
}
