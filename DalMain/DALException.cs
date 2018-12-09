using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain
{
    public class DALException : Exception
    {
        public DALException()
        {
             
        }
        public DALException(string message) : base(message)
        {

        }
    }
    public class DALUnexpectedException : DALException
    {
        public DALUnexpectedException() :base("unexpected error occured")
        {

        }

    }
}
