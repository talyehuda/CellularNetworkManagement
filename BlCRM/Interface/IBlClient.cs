using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlCRM.Interface
{
    public interface IBlClient
    {
        List<int> GetListClientsIdNumber();
        Client GetClientById(int ClientIdNumber);
        RequestStatus AddClient(Client client);
        RequestStatus EditClient(Client client);
        List<ClientType> GetListClientType();
        RequestStatus DeleteClientWithLines(int id);

    }
}
