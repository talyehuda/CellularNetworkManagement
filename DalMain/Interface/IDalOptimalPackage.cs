using Common.ModelToBlClient.OptimalPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Interface
{
    public interface IDalOptimalPackage
    {
        ClientValue GetClientValueByClient(int idClient);
        int GetNumberOfLines(int idClient);
        double GetAmountOfInvoices(int idClient);
        int GetCallsToCenter(int idClient);
    }
}

