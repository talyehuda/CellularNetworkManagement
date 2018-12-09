using BLExceptionLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Utils;

namespace BlWebApiCRM
{
    public class ApiControllerBase: ApiController, IApiControllerBase
    {
        public ApiControllerBase()
        {

        }
        public NegotiatedContentResult<string> HandleException(Exception ex)
        {
            try
            {
                throw ex;
            }
            catch (ApiException e)
            {
                return Content(HttpStatusCode.MethodNotAllowed, e.Message);
            }
            catch (BlUnexpectedException e)
            {
                return Content(HttpStatusCode.MethodNotAllowed, e.Message);
            }
            catch (BlException e)
            {
                return Content(HttpStatusCode.MethodNotAllowed, e.Message);
            }
            catch (Exception e)
            {
                FileLogger fileLogger = new FileLogger();
                fileLogger.LogException("Api", e);
                return Content(HttpStatusCode.MethodNotAllowed, e.Message);
            }            
        }
        public Exception NewApiException(StackTrace stackTrace, string message)
        {
            Exception ex = new ApiException(message);
            FileLogger fileLogger = new FileLogger();
            fileLogger.LogException("Api", ex, stackTrace.GetFrame(0).ToString());
            return ex;
        }
    }
}