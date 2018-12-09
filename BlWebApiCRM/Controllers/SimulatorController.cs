using BlSimulator;
using BlSimulator.Interface;
using Common.Model;
using Common.ModelToBlClient.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BlWebApiCRM.Controllers
{
    public class SimulatorController : ApiController
    {
        IBlSimulator blSimulator;

        ApiControllerBase apiControllerBase;
        public SimulatorController(IBlSimulator blSimulator)
        {
            this.blSimulator = blSimulator;
            apiControllerBase = new ApiControllerBase();
        }
        [HttpPost]
        public IHttpActionResult AddSimulationParameters(SimulationParameters simulationParameters)
        {
            try
            {
                RequestStatus status = blSimulator.AddSimulationParameters(simulationParameters);
                return Content(HttpStatusCode.OK, "");
            }
            catch (Exception e)
            {
                return apiControllerBase.HandleException(e);
            }
        }
    }
}
