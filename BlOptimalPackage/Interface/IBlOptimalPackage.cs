using Common.Model;
using Common.ModelToBlClient.OptimalPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlOptimalPackage.Interface
{
   public interface IBlOptimalPackage
    {
         OptimalPackage GetOptimalPackage(Line line, DateTime date);
         OptimalPackage GetOptimalPackage(int lineId);
    }
}
