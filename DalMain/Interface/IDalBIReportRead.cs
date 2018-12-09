using Common.Model;
using Common.ModelToBlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Interface
{
    public interface IDalBIReportRead
    {
        List<Employee> GetBestSellers();
        List<Client> GetMostCallingToCenterClients();
        BIReport GetMostValueClients();
        BIReport GetBIReport();
        List<List<string>> GetPotentialGrougs();
    }
}
