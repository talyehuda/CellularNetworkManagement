using BlBaseClassesLib;
using BlLogin.Interface;
using Common.Model;
using DalMain.Interface;
using DalMain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlLogin
{
    public class BlLoginMain : BlBase, IBlLoginMain
    {
        IDalLoginRead loginManagerRead;
        public BlLoginMain(IDalLoginRead loginManagerRead)
        {
            this.loginManagerRead = loginManagerRead;
        }
        /// <summary>
        /// Check in DB  if the customer's ID card matches a Contact number
        /// </summary>
        /// <param name="ClientIdNumber">Id Number of Client, No id to DB</param>
        /// <param name="ContactNameber">Contact Nameber</param>
        /// <returns>Client or Null</returns>
        public Client LoginClient(int ClientIdNumber, string ContactNameber)
        {
            try
            {
                return loginManagerRead.LoginClient(ClientIdNumber, ContactNameber);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Check in DB  if the Employee's ID card matches a Contact number
        /// </summary>
        /// <param name="ClientIdNumber">Id Number of Client, No id to DB</param>
        /// <param name="ContactNameber">Contact Nameber</param>
        /// <returns>Employee or Null</returns>
        public Employee LoginEmployee(int ClientIdNumber, string ContactNameber)
        {
            try
            {
                return loginManagerRead.LoginEmployee(ClientIdNumber, ContactNameber);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Check in DB  if the Manager's ID card matches a Contact number
        /// </summary>
        /// <param name="ClientIdNumber">Id Number of Client, No id to DB</param>
        /// <param name="ContactNameber">Contact Nameber</param>
        /// <returns>Employee or Null</returns>
        public Employee LoginManager(int ClientIdNumber, string ContactNameber)
        {
            try
            {
                return loginManagerRead.LoginManager(ClientIdNumber, ContactNameber);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
    }
}
