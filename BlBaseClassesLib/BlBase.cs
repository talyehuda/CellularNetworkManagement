using BLExceptionLib;
using DalMain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace BlBaseClassesLib
{
    public abstract class BlBase
    {
        public BlBase()
        {

        }
        public Exception HandleException(Exception ex)
        {
            try
            {
                throw ex;
            }
            catch(BlException e)
            {
                return e;
            }
            catch (DALUnexpectedException)
            {
                throw new BlUnexpectedException();
            }
            catch (DALException e)
            {
                throw new BlException(e.Message);
            }
            catch (Exception e)
            {
                FileLogger fileLogger = new FileLogger();
                fileLogger.LogException("BL", e);
                throw new BlUnexpectedException();
            }
        }
        public Exception NewBLException(StackTrace stackTrace, string message)
        {
            Exception ex = new BlException(message);
            FileLogger fileLogger = new FileLogger();
            fileLogger.LogException("BL", ex, stackTrace.GetFrame(0).ToString());
            return ex;
        }
    }
}
