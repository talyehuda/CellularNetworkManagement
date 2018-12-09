using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace BlWebApiCRM
{
    public interface IApiControllerBase
    {
        NegotiatedContentResult<string> HandleException(Exception ex);
        Exception NewApiException(StackTrace stackTrace, string message);
    }
}