using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlWebApiCRM
{
    public class ApiException :Exception
    {
        public ApiException()
        {

        }
        public ApiException(string message) : base(message)
        {

        }
    }
    public class ApiUnexpectedException : ApiException
    {
        public ApiUnexpectedException() : base("Api - unexpected error occured")
        {

        }
    }

}