using Common.Model;
using Common.ModelToBlClient.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlSimulator.Interface
{
    public interface IBlSimulator
    {
         RequestStatus AddSimulationParameters(SimulationParameters simulationParameters);
         RequestStatus AddSimulationParameters(SimulationParameters simulationParameters, DateTime dateTime);
    }
}
