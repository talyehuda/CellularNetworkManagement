using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public interface ILogger
    {
       void LogException(string section, Exception ex);
        void LogMessage(string section,string message);
    }
    public class FileLogger :ILogger
    {

        private string filePath = null;
        public FileLogger()
        {
            try
            {

                filePath = Environment.ExpandEnvironmentVariables("%appdata%\\cellularnetworklog");
                System.IO.Directory.CreateDirectory(filePath);
                filePath += "\\Log.txt";
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// A method to write to the log file
        /// </summary>
        /// <param name="logMessage">the message that will be written in the log</param>
        public void LogException(string section, Exception ex)
        {
            try
            {
                string strDate = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                using (StreamWriter sw = new StreamWriter(filePath,true))
                {
                    sw.WriteLine("date: " + strDate + ", section: " + section + "exception: " + ex.GetType().Name );
                    sw.WriteLine("message: " + ex.Message);
                    sw.WriteLine("stack trace: " + ex.StackTrace);
                    sw.WriteLine();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
    }
        /// <summary>
        /// A method to write to the log file
        /// </summary>
        /// <param name="logMessage">the message that will be written in the log</param>
        public void LogException(string section, Exception ex,string StackTrace)
        {
            try
            {
                string strDate = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine("date: " + strDate + ", section: " + section  + ", exception: " + ex.GetType().Name);
                    sw.WriteLine("message: " + ex.Message);
                    sw.WriteLine("stack trace: " + StackTrace);
                    sw.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void LogMessage(string section, string message)
        {
            try
            {
                //DateTime dateTime = DateTime.Now;
                // string strDate = System.DateTime.Today.ToString("dd/MM/yyyy");
                string strDate = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine("date: " + strDate + ", section: " + section +", message: " + message);
                     sw.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
