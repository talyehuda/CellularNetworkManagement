using BlCRM;
using BlCRM.Interface;
using BLExceptionLib;
using BlInvoice;
using BlInvoice.Interface;
using Common.Model;
using Common.ModelToBlClient.Invoice;
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
    public class InvoiceController : ApiController
    {
        IBlLineAndPackage blLineAndPackage;
        IBlInvoiceCalculation blInvoiceCalculation;
        public InvoiceController(IBlInvoiceCalculation blInvoiceCalculation, IBlLineAndPackage blLineAndPackage)
        {
            this.blInvoiceCalculation = blInvoiceCalculation;
            this.blLineAndPackage = blLineAndPackage;
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
        [HttpPost]
        public IHttpActionResult CalculateInvoice([FromBody]List<Line> Lines, int Month, int Year)
        {
            try
            {
                ClientInvoice invoiceCalculationClient = blInvoiceCalculation.InvoiceCalculationClient(Lines, new DateTime(Year, Month, 1));
                return Content(HttpStatusCode.OK, invoiceCalculationClient);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpGet]
        public IHttpActionResult GetListLineByClient(int ClientIdNumber)
        {
            try
            {
                List<Line> list = blLineAndPackage.GetListLineByClient(ClientIdNumber);
                return Content(HttpStatusCode.OK, list);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
