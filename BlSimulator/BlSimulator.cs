using BlBaseClassesLib;
using BlSimulator.Interface;
using Common.Model;
using Common.ModelToBlClient.Invoice;
using DalMain.Interface;
using DalMain.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlSimulator
{
    public class BlSimulator : BlBase, IBlSimulator
    {
        IDalSimulatorWrite simulatorManagerEdit;
        IDalSimulatorRead simulatorManagerRead;
        Random random;
        public BlSimulator(IDalSimulatorWrite simulatorManagerEdit, IDalSimulatorRead simulatorManagerRead)
        {
            this.simulatorManagerEdit = simulatorManagerEdit;
            this.simulatorManagerRead = simulatorManagerRead;
            random = new Random();
        }
        public RequestStatus AddSimulationParameters(SimulationParameters simulationParameters)
        {
            try
            {
                if (simulationParameters == null) throw NewBLException(new StackTrace(true), "simulationParameters Sent empty");               
                if (simulationParameters.SMS == true)
                {
                    List<SMS> list = new List<SMS>();
                    for (int i = 0; i < simulationParameters.NumberOfCallsOrSMS; i++)
                    {
                        list.Add(new SMS().NewSMS(simulationParameters.LineId, RandomSendTo(simulationParameters.SendToOptions, simulationParameters.LineId), RandomDateTime()));
                    }
                    simulatorManagerEdit.AddListSMS(list);
                }
                else
                {
                    List<Call> list = new List<Call>();
                    for (int i = 0; i < simulationParameters.NumberOfCallsOrSMS; i++)
                    {
                        var randomDureation = RandomDureation(simulationParameters.MinDuration, simulationParameters.MaxDuration);
                        list.Add(new Call().NewCall(simulationParameters.LineId, randomDureation, RandomSendTo(simulationParameters.SendToOptions, simulationParameters.LineId), RandomDateTime()));
                    }
                    simulatorManagerEdit.AddListCall(list);
                }
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
            return RequestStatus.Succeeded;
        }
        public RequestStatus AddSimulationParameters(SimulationParameters simulationParameters, DateTime dateTime)
        {
            if (simulationParameters == null) throw NewBLException(new StackTrace(true), "simulation Parameters Sent empty");
            try
            {
                if (simulationParameters.SMS == true)
                {
                    List<SMS> list = new List<SMS>();
                    for (int i = 0; i < simulationParameters.NumberOfCallsOrSMS; i++)
                    {
                        list.Add(new SMS().NewSMS(simulationParameters.LineId, RandomSendTo(simulationParameters.SendToOptions, simulationParameters.LineId), RandomDateTime(dateTime)));
                    }
                    simulatorManagerEdit.AddListSMS(list);
                }
                else
                {
                    List<Call> list = new List<Call>();
                    for (int i = 0; i < simulationParameters.NumberOfCallsOrSMS; i++)
                    {
                        var randomDureation = RandomDureation(simulationParameters.MinDuration, simulationParameters.MaxDuration);
                        list.Add(new Call().NewCall(simulationParameters.LineId, randomDureation, RandomSendTo(simulationParameters.SendToOptions, simulationParameters.LineId), RandomDateTime(dateTime)));
                    }
                    simulatorManagerEdit.AddListCall(list);
                }
                return RequestStatus.Succeeded;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }

        }
        private double RandomDureation(double MinDuration, double MaxDuration)
        {
            return random.NextDouble() * (MaxDuration - MinDuration) + MinDuration;
        }
        private string RandomSendTo(SendToOptions sendToOptions, int id)
        {
            try
            {
                string numberResult = "";
                switch (sendToOptions)
                {
                    case SendToOptions.Friends:
                        numberResult = simulatorManagerRead.GetRandomSendToFriends(id);
                        break;
                    case SendToOptions.Family:
                        numberResult = simulatorManagerRead.GetRandomSendToFamily(id);
                        break;
                    case SendToOptions.General:
                        numberResult = simulatorManagerRead.GetRandomSendToGeneral(id);
                        break;
                    case SendToOptions.All:
                        numberResult = simulatorManagerRead.GetRandomSendToAll(id);
                        break;
                }
                return numberResult;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        private DateTime RandomDateTime(DateTime dateTime)
        {
            return RandomDayAndTime(dateTime);
        }
        private DateTime RandomDateTime()
        {
            DateTime dateTime = DateTime.Today;
            return RandomDayAndTime(dateTime);
        }
        private DateTime RandomDayAndTime(DateTime dateTime)
        {
            DateTime start = new DateTime(dateTime.Year, dateTime.Month, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range)).AddHours(random.Next(23)).AddMinutes(random.Next(59)).AddSeconds(random.Next(59));
        }
    }
}
