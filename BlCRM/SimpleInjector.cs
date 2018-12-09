using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.WebApi;
using DalMain.Interface;
using DalMain.Manager;

namespace BlCRM
{
    public class SimpleInjector
    {
        public static void Inject()
        {
            // 1. Create a new Simple Injector container
            var container = new Container();

            // 2. Configure the container (register)
            container.Register<IDalCRMWrite, CRMManagerWrite>();
            container.Register<IDalCRMRead, CRMManagerRead>();

            // 3. Verify your configuration
            container.Verify();
        }
    }
}
