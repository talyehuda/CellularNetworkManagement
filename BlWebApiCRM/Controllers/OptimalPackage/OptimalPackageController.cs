using BlOptimalPackage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BlWebApiCRM.Controllers.OptimalPackage
{
    public class OptimalPackageController : ApiController
    {
        IBlOptimalPackage blOptimalPackage;
        ApiControllerBase apiControllerBase;
        public OptimalPackageController(IBlOptimalPackage blOptimalPackage)
        {
            this.blOptimalPackage = blOptimalPackage;
            apiControllerBase = new ApiControllerBase();
        }
        [HttpGet]
        public IHttpActionResult GetOptimalPackage(int lineId)
        {
            try
            {
                Common.ModelToBlClient.OptimalPackage.OptimalPackage optimalPackage = blOptimalPackage.GetOptimalPackage(lineId);
                return Content(HttpStatusCode.OK, optimalPackage);
            }
            catch (Exception e)
            {
                return apiControllerBase.HandleException(e);
            }
        }


    }
}
