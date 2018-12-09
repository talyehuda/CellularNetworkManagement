using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Interface
{
   public interface IDalSimulatorRead
    {
       // List<string> GetListSendToFamily(int id);
       //List<string> GetListSendToFriends(string number);
       // List<string> GetListSendToGeneral(string number);
       // List<string> GetListSendToAll(string number);

        string GetRandomSendToGeneral(int id);
        string GetRandomSendToFriends(int id);
        string GetRandomSendToFamily(int id);
        string GetRandomSendToAll(int id);
    }
}
