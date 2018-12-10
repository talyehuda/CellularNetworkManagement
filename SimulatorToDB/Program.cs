using BlSimulator;
using Common.ModelToBlClient.Invoice;
using DalMain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorToDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Datetimetest();
            PlaySimulatorSMSAndMinutes();
            Console.WriteLine("done");
        }
        public static void Datetimetest()
        {
            DateTime date = DateTime.Today;
            DateTime dateEnd = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            Console.WriteLine(dateEnd);
        }
        private static void PlaySimulatorSMSAndMinutes()
        {
            PlaySimulator(true);
            PlaySimulator(false);
        }

        private static void PlaySimulator(bool sms)
        {
            BlSimulator.BlSimulator blSimulator = new BlSimulator.BlSimulator(new SimulatorManagerWrite(), new SimulatorManagerRead());
            DateTime dateTime = DateTime.Today;
            DateTime dateTime1 = dateTime.AddMonths(-1);
            DateTime dateTime2 = dateTime.AddMonths(-2);
            DateTime dateTime3 = dateTime.AddMonths(-3);

            SimulationParameters simulationParameters1 = new SimulationParameters(1, 40, 60, 30, sms, SendToOptions.Family);
            blSimulator.AddSimulationParameters(simulationParameters1);
            SimulationParameters simulationParameters2 = new SimulationParameters(2, 40, 60, 30, sms, SendToOptions.Family);
            blSimulator.AddSimulationParameters(simulationParameters2);
            SimulationParameters simulationParameters3 = new SimulationParameters(3, 40, 60, 30, sms, SendToOptions.Family);
            blSimulator.AddSimulationParameters(simulationParameters3);
            SimulationParameters simulationParameters4 = new SimulationParameters(4, 40, 60, 30, sms, SendToOptions.Family);
            blSimulator.AddSimulationParameters(simulationParameters3);


            blSimulator.AddSimulationParameters(simulationParameters1, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters2, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters3, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters4, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters1, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters2, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters3, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters4, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters1, dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters2, dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters3, dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters4, dateTime3);


            SimulationParameters simulationParameters5 = new SimulationParameters(5, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters1);
            SimulationParameters simulationParameters6 = new SimulationParameters(6, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters2);
            SimulationParameters simulationParameters7 = new SimulationParameters(7, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);
            SimulationParameters simulationParameters8 = new SimulationParameters(8, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);

            blSimulator.AddSimulationParameters(simulationParameters5, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters6, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters7, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters8, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters5, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters6, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters7, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters8, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters5, dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters6, dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters7, dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters8, dateTime3);


            SimulationParameters simulationParameters9 = new SimulationParameters(9, 10, 20, 15, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters1);
            SimulationParameters simulationParameters10 = new SimulationParameters(10, 5, 20, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters2);
            SimulationParameters simulationParameters11 = new SimulationParameters(11, 10, 30, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);
            SimulationParameters simulationParameters12 = new SimulationParameters(12, 5, 20, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);

            blSimulator.AddSimulationParameters(simulationParameters9,  dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters10, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters11, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters12, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters9,  dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters10, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters11, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters12, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters9,  dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters10, dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters11, dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters12, dateTime3);

            SimulationParameters simulationParameters13 = new SimulationParameters(13, 10, 20, 25, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters1);
            SimulationParameters simulationParameters14 = new SimulationParameters(14, 5, 15, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters2);
            SimulationParameters simulationParameters15 = new SimulationParameters(15, 10, 40, 10, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);
            SimulationParameters simulationParameters16 = new SimulationParameters(16, 5, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);

            blSimulator.AddSimulationParameters(simulationParameters13 ,  dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters14, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters15, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters16, dateTime1);
            blSimulator.AddSimulationParameters(simulationParameters13,  dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters14, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters15, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters16, dateTime2);
            blSimulator.AddSimulationParameters(simulationParameters13,  dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters14, dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters15, dateTime3);
            blSimulator.AddSimulationParameters(simulationParameters16, dateTime3);

        }
    }
}
