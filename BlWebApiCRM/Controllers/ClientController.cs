using BlCRM;
using BlCRM.Interface;
using BLExceptionLib;
using Common.Model;
using Common.ModelToBlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using Utils;

namespace BlWebApiCRM.Controllers
{
    public class ClientController : ApiController
    {
        //ApiControllerBase apiControllerBase;
        IBlClient blClient;
        public ClientController(IBlClient blClient)
        {
            this.blClient = blClient;
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
        public IHttpActionResult GetListClientsIdNumber()
        {
            try
            {
                List<int> list = blClient.GetListClientsIdNumber();
                return Content(HttpStatusCode.OK, list);
            }
            catch(Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpGet]
        public IHttpActionResult GetClientById(int ClientIdNumber)
        {
            try
            {
                Client client= blClient.GetClientById(ClientIdNumber);
                return Content(HttpStatusCode.OK, client);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpPost]
        public IHttpActionResult AddClient(Client client)
        {
            try
            {
                RequestStatus status = blClient.AddClient(client);
                return Content(HttpStatusCode.OK, status);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpPost]
        public IHttpActionResult EditClient(Client client)
        {
            try
            {
                RequestStatus status = blClient.EditClient(client);
                return Content(HttpStatusCode.OK, status);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        [HttpGet]
        public IHttpActionResult RemoveClient(int id)
        {
            try
            {
                RequestStatus status = blClient.DeleteClientWithLines(id);
                return Content(HttpStatusCode.OK, status);
            }
            catch (Exception e)
            {
               
                return HandleException(e);
            }
        }
        [HttpGet]
        public IHttpActionResult GetListClientType()
        {
            try
            {
                List<ClientType> list = blClient.GetListClientType();
                return Content(HttpStatusCode.OK, list);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
