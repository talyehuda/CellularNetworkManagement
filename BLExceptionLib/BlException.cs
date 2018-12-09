using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLExceptionLib
{
    public class BlException : Exception
    {
        public BlException()
        {

        }
        public BlException(string message) : base(message)
        {

        }


    }
    public class BlUnexpectedException : BlException
    {

        public BlUnexpectedException() : base("unexpected error occured")
        {

        }
    }

}
