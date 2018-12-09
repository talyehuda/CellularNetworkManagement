using Common.ModelToBlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlCRM.Interface
{
   public interface IBlLoginEmployee
    {
        Login Login(int ClientIdNumber, string ContactNumber);
    }
}
