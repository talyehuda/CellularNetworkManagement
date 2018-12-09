using Common.Model;
using DalMain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Manager
{
    public class LoginManagerRead : DalBase,IDalLoginRead
    {
        /// <summary>
        /// Checks if the customer's ID card matches a Contact number
        /// </summary>
        /// <param name="ClientIdNumber">Id Number of Client, No id to DB</param>
        /// <param name="ContactNameber">ContactNameber</param>
        /// <returns>Client or Null </returns>
        public Client LoginClient(int ClientIdNumber, string ContactNameber)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Client.FirstOrDefault(c => (c.ClientIdNumber == ClientIdNumber) && (c.ContactNameber == ContactNameber));
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        /// <summary>
        /// Checks if the Employee's ID card matches a Contact number
        /// </summary>
        /// <param name="ClientIdNumber">Id Number of Client, No id to DB</param>
        /// <param name="ContactNameber">ContactNameber</param>
        /// <returns>Employee  or Null </returns>
        public Employee LoginEmployee(int ClientIdNumber, string ContactNameber)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Employee.FirstOrDefault(c => (c.ClientIdNumber == ClientIdNumber) && (c.ContactNameber == ContactNameber) && (c.UserAuthType == UserAuthType.Employee));
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        /// <summary>
        /// Checks if the Manager's ID card matches a Contact number
        /// </summary>
        /// <param name="ClientIdNumber">Id Number of Client, No id to DB</param>
        /// <param name="ContactNameber">ContactNameber</param>
        /// <returns>Employee  or Null </returns>
        public Employee LoginManager(int ClientIdNumber, string ContactNameber)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Employee.FirstOrDefault(c => ((c.ClientIdNumber == ClientIdNumber) && (c.ContactNameber == ContactNameber) && (c.UserAuthType == UserAuthType.Manager)));
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
    }
}
