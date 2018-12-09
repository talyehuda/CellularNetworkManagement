using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Interface
{
    public interface IDalCRMRead 
    {
        Client GetClientById(int ClientIdNumber);
        List<int> GetListClientsIdNumber();
        List<ClientType> GetListClientType();
        List<Line> GetListLineByClientIdNumber(int ClientIdNumber);
        bool GetIfLineIsAssigned(string Newnumber);
        List<PackageOptions> GetListPackageOptions();
        Client GetClientDeadOrAlive(int ClientIdNumber);
        Package GetPackageForLine(int packageId);
        SelectedNumbers GetSelectedNumbersById(int SelectedNumbersId);
        List<Line> GetListLineByClient(int id);
        Line GetLineById(int lineId);
    }
}
