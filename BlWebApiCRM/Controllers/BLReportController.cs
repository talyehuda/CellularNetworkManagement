using BLExceptionLib;
using BlReportsEngine;
using BlReportsEngine.Interface;
using Common.ModelToBlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Utils;

namespace BlWebApiCRM.Controllers
{
    public class BLReportController : ApiController
    {
        IBlReport blReport;
        public BLReportController(IBlReport blReport)
        {
            this.blReport = blReport;            
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
        [HttpGet]
        public IHttpActionResult GetBIReport()
        {
            try
            {
                BIReport item = blReport.GetBIReport();
                return Content(HttpStatusCode.OK, item);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

        }
    }
}
