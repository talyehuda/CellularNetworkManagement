using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Interface
{
    public interface IDalLoginRead
    {
        Client LoginClient(int ClientIdNumber, string ContactNameber);
        Employee LoginEmployee(int ClientIdNumber, string ContactNameber);
        Employee LoginManager(int ClientIdNumber, string ContactNameber);
    }
}
