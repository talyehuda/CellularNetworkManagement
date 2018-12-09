using BlBaseClassesLib;
using BlCRM.Interface;
using BlLogin;
using BlLogin.Interface;
using Common.ModelToBlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlCRM
{
    public class BlLoginEmployee : BlBase, IBlLoginEmployee
    {
        IBlLoginMain blLoginMain;
        public BlLoginEmployee(IBlLoginMain blLoginMain)
        {
            this.blLoginMain = blLoginMain;
        }
        /// <summary>
        /// Check in DB  if the Manager's/employee ID card matches a Contact number.
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
                    var employeeManager = blLoginMain.LoginManager(ClientIdNumber, ContactNumber);
                    if (employeeManager != null)
                        login = new Login(employeeManager.Id, employeeManager.NameToString(), employeeManager.UserAuthType);
                    else
                    {
                        var employee = blLoginMain.LoginEmployee(ClientIdNumber, ContactNumber);
                        if (employee != null)
                            login = new Login(employee.Id, employee.NameToString(), employee.UserAuthType);
                        else throw NewBLException(new StackTrace(true), " Incorrect ClientIdNumber or ContactNumber");
                    }
                }
                else throw NewBLException(new StackTrace(true), "ClientIdNumber or ContactNumber Sent empty");
                return login;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
    }
}
