using BlCRM;
using BlCRM.Interface;
using BLExceptionLib;
using Common.Model;
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
    public class LineAndPackageController : ApiController
    {
        IBlLineAndPackage blLineAndPackage;

        public LineAndPackageController(IBlLineAndPackage blLineAndPackage)
        {
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
        [HttpGet]
        public IHttpActionResult GetListLineByClient(int ClientId)
        {
            try
            {
               List<Line>  list = blLineAndPackage.GetListLineByClientId(ClientId);
                return Content(HttpStatusCode.OK, list);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpGet]
        public IHttpActionResult GetListLineByClient1(int ClientId)
        {
            try
            {
               List<Line> list = blLineAndPackage.GetListLineByClientId(ClientId);
                return Content(HttpStatusCode.OK, list);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpGet]
        public IHttpActionResult GetListPackageOptions()
        {
            try
            {
                List<Package> list = blLineAndPackage.GetListPackageOptions();
                return Content(HttpStatusCode.OK, list);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpGet]
        public IHttpActionResult GetIfLineIsAssigned(string Newnumber)
        {
            try
            {
               bool IfLineIsAssigned = blLineAndPackage.GetIfLineIsAssigned(Newnumber);
                return Content(HttpStatusCode.OK, IfLineIsAssigned);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpPost]
        public IHttpActionResult AddLine([FromBody]Line line, int idEmployee)
        {
            try
            {
                int? id = blLineAndPackage.AddLineReturnId(line, idEmployee);
                if (id == null) Content(HttpStatusCode.InternalServerError, "no Return Id");
                return Content(HttpStatusCode.OK, id);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpPost]
        public IHttpActionResult EditLine(Line line)
        {
            try
            {
               RequestStatus status = blLineAndPackage.EditLine(line);
                return Content(HttpStatusCode.OK, status);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpPost]
        public IHttpActionResult EditPackage(Package package)
        {
            try
            {
               RequestStatus status = blLineAndPackage.EditPackage(package);
                return Content(HttpStatusCode.OK, status);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpPost]
        public IHttpActionResult GetTotalFixedPrice(Package package)
        {
            try
            {
               double TotalPrice = blLineAndPackage.ReturnTotalFixedPrice(package);
                return Content(HttpStatusCode.OK, TotalPrice);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpPost]
        public IHttpActionResult AddSelectedNumbers(SelectedNumbers selectedNumbers)
        {
            try
            {
               int? id = blLineAndPackage.AddSelectedNumbersReturnId(selectedNumbers);
                return Content(HttpStatusCode.OK, id);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpPost]
        public IHttpActionResult AddPackage(Package package)
        {
            try
            {
               int? id = blLineAndPackage.AddPackageReturnId(package);
                return Content(HttpStatusCode.OK, id);
}
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpGet]
        public IHttpActionResult RemoveLine(int id)
        {
            try
            {
               RequestStatus status = blLineAndPackage.RemoveLine(id);
                return Content(HttpStatusCode.OK, status);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpGet]
        public IHttpActionResult GetSelectedNumbersById(int id)
        {
            try
            {
               SelectedNumbers selectedNumbers = blLineAndPackage.GetSelectedNumbersById(id);
                return Content(HttpStatusCode.OK, selectedNumbers);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpGet]
        public IHttpActionResult GetPackageById(int lineId)
        {
            try
            {
                Package package = blLineAndPackage.GetPackageById(lineId);
                return Content(HttpStatusCode.OK, package);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

    }
}
