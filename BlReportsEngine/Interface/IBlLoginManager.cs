using Common.ModelToBlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlReportsEngine.Interface
{
    public interface IBlLoginManager
    {
        Login Login(int ClientIdNumber, string ContactNumber);

    }
}
