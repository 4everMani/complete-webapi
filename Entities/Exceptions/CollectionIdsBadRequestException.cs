using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class CollectionIdsBadRequestException : BadRequestException
    {
        public CollectionIdsBadRequestException()
            :base("Collection count mismatch comparing to input ids.")
        {
        }
    }
}
