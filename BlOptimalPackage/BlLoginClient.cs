using BlBaseClassesLib;
using BlCRM.Interface;
using BlLogin;
using BlLogin.Interface;
using BlOptimalPackage.Interface;
using Common.ModelToBlClient;
using DalMain.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlOptimalPackage
{
    public class BlLoginClient : BlBase, IBlLoginClient
    {
        IBlLoginMain blLoginMain;
        public BlLoginClient(IBlLoginMain blLoginMain)
        {
            this.blLoginMain = blLoginMain;
        }
        /// <summary>
        /// Check in DB  if the Manager's/employee/client ID card matches a Contact number. 
        /// </summary>
        /// <param name="ClientIdNumber">Id Number of Client, No id to DB</param>
        /// <param name="ContactNumber">Contact Nameber</param>
        /// <returns>Login model if Login successful,Login model UserAuthType.None if Login failed, null if If there's a problem</returns>
        public Login Login(int ClientIdNumber, string ContactNumber)
        {
            try
            {
                Login login = null;
                if (ClientIdNumber != 0 && ContactNumber != null)
                {

                    var client = blLoginMain.LoginClient(ClientIdNumber, ContactNumber);
                    if (client != null)
                        login = new Login(client.Id, client.NameToString(), Common.Model.UserAuthType.Client);
                    else throw NewBLException(new StackTrace(true), " Incorrect Clientb Id Number or Contact Number");
                }
                else throw NewBLException(new StackTrace(true), "Client Id Number or Contact Number Sent empty");
                return login;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
    }
}
