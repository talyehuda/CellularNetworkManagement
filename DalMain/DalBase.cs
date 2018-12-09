using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DalMain
{
    public class DalBase
    {
        public DalBase()
        {

        }
        public Exception HandleException(Exception ex)
        {
            try
            {
                throw ex;
            }
            catch (DALException e)
            {
                return e;
            }

            catch (Exception e)
            {
                FileLogger fileLogger = new FileLogger();
                fileLogger.LogException("Dal", e);
                return new DALUnexpectedException();
            }
        }
        public Exception NEWDALException(string section, string message)
        {
            Exception ex= new DALException(message);
            FileLogger fileLogger = new FileLogger();
            fileLogger.LogMessage(section, message);
            return ex;
        }
        public Exception NewDALException(StackTrace stackTrace, string message)
        {
            Exception ex = new DALException(message);
            FileLogger fileLogger = new FileLogger();
            fileLogger.LogException("Dal", ex,stackTrace.GetFrame(0).ToString());
            return ex;
        }
    }
}
