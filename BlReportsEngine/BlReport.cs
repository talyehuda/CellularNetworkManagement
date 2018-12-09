using BlBaseClassesLib;
using BlReportsEngine.Interface;
using Common.ModelToBlClient;
using DalMain.Interface;
using DalMain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlReportsEngine
{
   public class BlReport :BlBase, IBlReport
    {
        IDalBIReportRead blReportManagerRead;
        public BlReport(IDalBIReportRead blReportManagerRead)
        {
            this.blReportManagerRead = blReportManagerRead;
        }
        public BIReport GetBIReport()
        {
            try
            {
                return blReportManagerRead.GetBIReport();
            }
            catch (Exception e)
            {
                throw HandleException(e);

            }
        }
    }
}
