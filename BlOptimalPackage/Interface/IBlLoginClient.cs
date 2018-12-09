using Common.ModelToBlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlOptimalPackage.Interface
{
   public interface IBlLoginClient
    {
        Login Login(int ClientIdNumber, string ContactNumber);
    }
}
