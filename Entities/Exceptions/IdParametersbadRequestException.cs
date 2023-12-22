using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class IdParametersbadRequestException : BadRequestException
    {
        public IdParametersbadRequestException()
            : base("Parameter ids is null")
        {  
        }
    }
}
