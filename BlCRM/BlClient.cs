using BlBaseClassesLib;
using BlCRM.Interface;
using Common.Model;
using DalMain.Interface;
using DalMain.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlCRM
{
    public class BlClient : BlBase, IBlClient
    {

        IDalCRMRead cRMManagerRead;
        IDalCRMWrite cRMManagerEdit;
        public BlClient(IDalCRMRead cRMManagerRead, IDalCRMWrite cRMManagerEdit)
        {
            this.cRMManagerEdit = cRMManagerEdit;
            this.cRMManagerRead = cRMManagerRead;
        }
        /// <summary>
        /// A function that returns a list of the customer's ID number
        /// </summary>
        /// <returns>list of the customer's ID number</returns>
        public List<int> GetListClientsIdNumber()
        {
            try
            {
                return cRMManagerRead.GetListClientsIdNumber();
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// A function that receives a customer's ID number and returns the client object
        /// </summary>
        /// <param name="ClientIdNumber">customer's ID number</param>
        /// <returns> the client object</returns>
        public Client  GetClientById(int ClientIdNumber)
        {
            try
            {
                return cRMManagerRead.GetClientById(ClientIdNumber);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// A function adds a client if it does not exist.
        ///If the customer exists, but has been deleted in the past it updates the client active.
        /// </summary>
        /// <param name="client">Function accepts the client object</param>
        /// <returns>The function returns the status-successful object</returns>
        public RequestStatus AddClient(Client client)
        {
            try
            {
                if (client == null) throw NewBLException(new StackTrace(true), "client Sent empty");

                RequestStatus requestStatus = RequestStatus.Succeeded;
                var tmpClient = cRMManagerRead.GetClientDeadOrAlive(client.ClientIdNumber);
                if (tmpClient!=null)
                {
                    cRMManagerEdit.ReturningCustomer(tmpClient.Id);
                    client.Id = tmpClient.Id;
                    requestStatus= cRMManagerEdit.EditClient(client);
                }
                else
                    requestStatus= cRMManagerEdit.AddClient(client);
                return requestStatus;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        ///A function updates a client if it exists.
        /// </summary>
        /// <param name="client">Function accepts the client object</param>
        /// <returns>The function returns the status-successful object</returns>
        public RequestStatus EditClient(Client client)
        {
            try
            {
                return cRMManagerEdit.EditClient(client);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// The function returns all client type objects.
        /// </summary>
        /// <returns>The function returns the status-successful object</returns>
        public List<ClientType> GetListClientType()
        {
            try
            {
                return cRMManagerRead.GetListClientType();
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// A function deletes the client. The actual modifier parameter is deleted.
        /// The client will remain in the database as a delete.
        /// </summary>
        /// <param name="id">  The function accepts the client number</param>
        /// <returns>The function returns the status-successful object</returns>
        /// 
        public RequestStatus DeleteClientWithLines(int id)
        {
            try
            {
                cRMManagerEdit.RemoveLines(id);
                return cRMManagerEdit.RemoveClient(id);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
    }
}
