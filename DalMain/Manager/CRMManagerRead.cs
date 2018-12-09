using Common.Model;
using DalMain;
using DalMain.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Manager
{
    public class CRMManagerRead : DalBase, IDalCRMRead
    {
        /// <summary>
        /// Checks if the customer exists and active in the system by ID Client
        /// </summary>
        /// <param name="idClient">Client Id Number</param>
        /// <returns>true if the customer exists</returns>
        public Client GetClientById(int ClientIdNumber)
        {
            using (CellularContext context = new CellularContext())
            {
                return context.Client.FirstOrDefault(c => c.ClientIdNumber == ClientIdNumber && !c.Deleted);
            }
        }
        /// <summary>
        /// GetListClientsIdNumber
        /// </summary>
        /// <returns>List all Clients (active) Id Number</returns>
        public List<int> GetListClientsIdNumber()
        {
            using (CellularContext context = new CellularContext())
            {
                return context.Client.Where(c => !c.Deleted).Select(c => c.ClientIdNumber).ToList();
            }
        }
        /// <summary>
        /// Get List Client Type Model
        /// </summary>
        /// <returns>List Client Type</returns>
        public List<ClientType> GetListClientType()
        {
            using (CellularContext context = new CellularContext())
            {
                return context.ClientType.ToList();
            }
        }
        /// <summary>
        ///  Checks if the line exists and active in the system by number phone
        /// </summary>
        /// <param name="Newnumber"> number phone</param>
        /// <returns>true if the line exists and active, false if free</returns>
        public bool GetIfLineIsAssigned(string Newnumber)
        {
            using (CellularContext context = new CellularContext())
            {
                var item = context.Line.FirstOrDefault(c => (c.Number == Newnumber && !c.Deleted));
                return (item != null);
            }
        }
        /// <summary>
        /// List of all active lines of the customer By Client Id
        /// </summary>
        /// <param name="id">Client Id</param>
        /// <returns>List of all active lines of the customer</returns>
        public List<Line> GetListLineByClient(int id)
        {
            using (CellularContext context = new CellularContext())
                try
                {
                    {
                        if (ClientExists(id))
                            return context.Line.Where(l => l.ClientId == id && !l.Deleted).ToList();
                        else
                            throw NewDALException(new StackTrace(true), "client does not exist");
                    }
                }
                catch (Exception ex)
                {
                    throw HandleException(ex);
                }
        }
        /// <summary>
        /// Checks if the customer exists and active in the system by ID Client
        /// </summary>
        /// <param name="idClient"> ID Client</param>
        /// <returns>true if the customer exists</returns>
        private bool ClientExists(int idClient)
        {
            using (CellularContext context = new CellularContext())
            {
                return context.Client.FirstOrDefault(c => c.Id == idClient && !c.Deleted) != null;
            }
        }
        /// <summary>
        /// List of all active lines of the customer active By Client Id Number
        /// </summary>
        /// <param name="ClientIdNumber">Client Id Number</param>
        /// <returns>List of all active lines of the customer active</returns>
        public List<Line> GetListLineByClientIdNumber(int ClientIdNumber)
        {
            using (CellularContext context = new CellularContext())
            {
                try
                {
                    {
                        var client = GetClientById(ClientIdNumber);
                        if (client != null)
                            return context.Line.Where(l => l.ClientId == client.Id && !l.Deleted).ToList();
                        else
                            throw NewDALException(new StackTrace(true), "client does not exist");
                    }
                }
                catch (Exception ex)
                {
                    throw HandleException(ex);
                }
            }
        }
        /// <summary>
        /// Get List Package Options
        /// </summary>
        /// <returns>Get List Package Options</returns>
        public List<PackageOptions> GetListPackageOptions()
        {
            using (CellularContext context = new CellularContext())
            {
                return context.PackageOptions.ToList();
            }
        }
        /// <summary>
        ///  Get Client if Alive.
        /// </summary>
        /// <param name="ClientIdNumber">Client Id Number</param>
        /// <returns>Client if Alive,null if Deed</returns>
        public Client GetClientDeadOrAlive(int ClientIdNumber)
        {
            using (CellularContext context = new CellularContext())
            {
                return context.Client.FirstOrDefault(c => c.ClientIdNumber == ClientIdNumber);
            }
        }
        /// <summary>
        /// Get Package
        /// </summary>
        /// <param name="packageId">package Id</param>
        /// <returns>Package if exists</returns>
        public Package GetPackageForLine(int packageId)
        {
            using (CellularContext context = new CellularContext())
            {
                return context.Package.FirstOrDefault(c => c.Id == packageId);
            }
        }
        /// <summary>
        /// Get Line By Id
        /// </summary>
        /// <param name="lineId">line Id</param>
        /// <returns>Line if exists</returns>
        public Line GetLineById(int lineId)
        {
            using (CellularContext context = new CellularContext())
            {
                return context.Line.FirstOrDefault(c => c.Id == lineId);
            }
        }
        /// <summary>
        /// Get model Selected Numbers By Id
        /// </summary>
        /// <param name="SelectedNumbersId">Selected Numbers Id</param>
        /// <returns>Selected Numbers if exists </returns>
        public SelectedNumbers GetSelectedNumbersById(int SelectedNumbersId)
        {
            using (CellularContext context = new CellularContext())
            {
                return context.SelectedNumbers.FirstOrDefault(c => c.Id == SelectedNumbersId);
            }
        }
    }
}

