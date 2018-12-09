using BlCRM;
using BlCRM.Interface;
using BLExceptionLib;
using BlOptimalPackage;
using BlOptimalPackage.Interface;
using BlReportsEngine;
using BlReportsEngine.Interface;
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
    public class LoginController : ApiController
    {
        IBlLoginEmployee blLoginEmployee;
        IBlLoginClient blLoginClient;
        IBlLoginManager blLoginManager;

        public LoginController(IBlLoginEmployee blLoginEmployee, IBlLoginClient blLoginClient, IBlLoginManager blLoginManager)
        {
            this.blLoginEmployee = blLoginEmployee;
            this.blLoginManager = blLoginManager;
            this.blLoginClient = blLoginClient;

        }
        /// Check in DB  if the Manager's/employee ID card matches a Contact number. 
        /// </summary>
        /// <param name="ClientIdNumber">Id Number of Client, No id to DB</param>
        /// <param name="ContactNumber">Contact Nameber</param>
        /// <returns>Login model if Login successful,Login model UserAuthType.None if Login failed, null if If there's a problem</returns>
        [HttpGet]
        public IHttpActionResult LoginAsEmployee(int ClientIdNumber, string ContactNumber)
        {
            try
            {
                Common.ModelToBlClient.Login login = blLoginEmployee.Login(ClientIdNumber, ContactNumber);
                return Content(HttpStatusCode.OK, login);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        /// <summary>
        /// Check in DB  if the Manager's/employee/client ID card matches a Contact number. 
        /// </summary>
        /// <param name="ClientIdNumber">Id Number of Client, No id to DB</param>
        /// <param name="ContactNumber">Contact Nameber</param>
        /// <returns>Login model if Login successful,Login model UserAuthType.None if Login failed, null if If there's a problem</returns>
        [HttpGet]
        public IHttpActionResult LoginAsClient(int ClientIdNumber, string ContactNumber)
        {
            try
            {
                Common.ModelToBlClient.Login login = blLoginClient.Login(ClientIdNumber, ContactNumber);
                return Content(HttpStatusCode.OK, login);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        /// <summary>
        /// Check in DB  if the Manager's ID card matches a Contact number
        /// </summary>
        /// <param name="ClientIdNumber">Id Number of Client, No id to DB</param>
        /// <param name="ContactNumber">Contact Nameber</param>
        /// <returns>Login model if Login successful,Login model UserAuthType.None if Login failed, null if If there's a problem</returns>
        [HttpGet]
        public IHttpActionResult LoginAsManager(int ClientIdNumber, string ContactNumber)
        {
            try
            {
                Common.ModelToBlClient.Login login = blLoginManager.Login(ClientIdNumber, ContactNumber);
                return Content(HttpStatusCode.OK, login);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
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
    }
}