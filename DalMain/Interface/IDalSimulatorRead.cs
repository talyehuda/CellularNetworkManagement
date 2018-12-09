using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Interface
{
   public interface IDalSimulatorRead
    {
        string GetRandomSendToGeneral(int id);
        string GetRandomSendToFriends(int id);
        string GetRandomSendToFamily(int id);
        string GetRandomSendToAll(int id);
    }
}
