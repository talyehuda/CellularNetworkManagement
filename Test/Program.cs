using BlCRM;
using BLExceptionLib;
using BlInvoice;
using BlOptimalPackage;
using BlReportsEngine;
using BlSimulator;
using BlWebApiCRM.Controllers;
using Common.Model;
using Common.ModelToBlClient;
using Common.ModelToBlClient.Invoice;
using Common.ModelToBlClient.OptimalPackage;
using DalMain;
using DalMain.Interface;
using DalMain.Manager;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Text;
using System.Threading.Tasks;
using Unity;
using BlCRM.Interface;
using BlLogin.Interface;
using BlLogin;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Datetimetest();


            //AddClient();
            //  ClientType clientType = new ClientType() { MinutePrice = 2.5, SMSPrice = 1.5, TypeName = "VIP" };
            //  ClientType clientType1 = new ClientType(0, "VIP", 2.5, 1.5);
            //  AddClientType(clientType1);
            //  clientType1 = ReturnClientType();
            //  clientType1.TypeName = "Tal";
            //  EditClientType(clientType1);
            //  ReturnClientType();
            //  Console.WriteLine("-----------------");
            // PlaySimulator();
            // PlayallDuration();
            //  Console.WriteLine(GetAmountLineToAll("0526654822"));
            //  GetBlInvoiceCalculation();
            //  AddLine();
            //  EditLine();
            //  LoginClient();
            //  LoginEmployee();
            //  LoginEmployeeManager();
            //   TestInvoice();
            //   //Export();
            //   OptimalPackage();
            //   BlReportTest();
            //  AddCall();
            //  GetListLineByIdWrite();
            //GetListNumber();
            //GetPotentialGrougs();
            Console.ReadLine();
        }
        private static void GetListNumber()
        {

           var a= GetListNumber2();
            var b = 5;
            b++;

        }
        private static List<string> GetListNumber2()
        {

                using (CellularContext context = new CellularContext())
                {
                    return context.Call.GroupBy(c => c.Line.Number).Select(c => c.Key).ToList();
                }

            

        }
        private static void GetPotentialGrougs()
        {
            BlReportManagerRead blReportManagerRead = new BlReportManagerRead();
            var list = blReportManagerRead.GetPotentialGrougs();

            foreach (var listItem in list)
            {
                foreach (var item in listItem)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void GetListLineByIdWrite()
        {
            Console.WriteLine(GetListLineById());
        }

        private static string GetListLineById()
        {
            BlLineAndPackage blLineAndPackage = new BlLineAndPackage(new CRMManagerRead(), new CRMManagerWrite());

            try
            {

                blLineAndPackage.GetListLineByClientId(0);
                return "ok";
            }
            catch (BlUnexpectedException)
            {
                return "server expected error";
            }
            catch (BlException e)
            {
                return e.Message;
            }
            catch (Exception)
            {
                return "server expected error";

            }
        }

        private static ClientType ReturnClientType()
        {
            using (CellularContext context = new CellularContext())
            {
                var type = context.ClientType.FirstOrDefault();
                Console.WriteLine(type.TypeName);
                return type;
            }
        }
        private static void AddClientType(ClientType clientType)
        {
            using (CellularContext context = new CellularContext())
            {
                context.ClientType.Add(clientType);
                context.SaveChanges();
            }
        }
        public static void EditClientType(ClientType clientType)
        {
            using (CellularContext context = new CellularContext())
            {
                context.ClientType.FirstOrDefault(c => c.Id == clientType.Id).EditClientType(clientType);
                context.SaveChanges();
            }
        }

        public static void GetCallDurationPerNumberForLineByDate(string number, DateTime dateStart, DateTime dateEnd)
        {
            using (CellularContext context = new CellularContext())
            {
                var list = context.Call.Where(c => c.Line.Number == number && c.Time >= dateStart && c.Time <= dateEnd).GroupBy(c => c.DestinationNumber)
                    .Select(st => new
                    {
                        Id = st.Key,
                        SumDuration = st.Sum(g => g.Duration)
                    }).ToList();
                //var h = list[0].ToList();
                foreach (var item in list)
                {
                    Console.WriteLine("Id = {0} SumDuration={1}", item.Id, item.SumDuration);
                }


                //.Sum(c => c.Duration).Select(c=>c.DestinationNumber)
            }
        }
        private static void PlaySimulatorSMSAndMinutes()
        {
            PlaySimulator(true);
            PlaySimulator(false);
        }

            private static void PlaySimulator(bool sms)
        {
            BlSimulator.BlSimulator blSimulator = new BlSimulator.BlSimulator(new SimulatorManagerWrite(), new SimulatorManagerRead());

            SimulationParameters simulationParameters1 = new SimulationParameters(1, 40, 60, 30, sms, SendToOptions.Family);
            blSimulator.AddSimulationParameters(simulationParameters1);
            SimulationParameters simulationParameters2 = new SimulationParameters(2, 40, 60, 30, sms, SendToOptions.Family);
            blSimulator.AddSimulationParameters(simulationParameters2);
            SimulationParameters simulationParameters3 = new SimulationParameters(3, 40, 60, 30, sms, SendToOptions.Family);
            blSimulator.AddSimulationParameters(simulationParameters3);
            SimulationParameters simulationParameters4 = new SimulationParameters(4, 40, 60, 30, sms, SendToOptions.Family);
            blSimulator.AddSimulationParameters(simulationParameters3);


            blSimulator.AddSimulationParameters(simulationParameters1, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters2, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters3, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters4, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters1, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters2, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters3, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters4, DateTime.Today.AddMonths(-2));


            SimulationParameters simulationParameters5 = new SimulationParameters(5, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters1);
            SimulationParameters simulationParameters6 = new SimulationParameters(6, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters2);                      
            SimulationParameters simulationParameters7 = new SimulationParameters(7, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);                      
            SimulationParameters simulationParameters8 = new SimulationParameters(8, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);

            blSimulator.AddSimulationParameters(simulationParameters5, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters6, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters7, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters8, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters5, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters6, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters7, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters8, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters5, DateTime.Today.AddMonths(-3));
            blSimulator.AddSimulationParameters(simulationParameters6, DateTime.Today.AddMonths(-3));
            blSimulator.AddSimulationParameters(simulationParameters7, DateTime.Today.AddMonths(-3));
            blSimulator.AddSimulationParameters(simulationParameters8, DateTime.Today.AddMonths(-3));


            SimulationParameters simulationParameters9 = new SimulationParameters(5, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters1);
            SimulationParameters simulationParameters10 = new SimulationParameters(6, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters2);
            SimulationParameters simulationParameters11 = new SimulationParameters(7, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);
            SimulationParameters simulationParameters12 = new SimulationParameters(8, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);

            blSimulator.AddSimulationParameters(simulationParameters9 , DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters10, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters11, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters12, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters9 , DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters10, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters11, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters12, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters9 , DateTime.Today.AddMonths(-3));
            blSimulator.AddSimulationParameters(simulationParameters10, DateTime.Today.AddMonths(-3));
            blSimulator.AddSimulationParameters(simulationParameters11, DateTime.Today.AddMonths(-3));
            blSimulator.AddSimulationParameters(simulationParameters12, DateTime.Today.AddMonths(-3));

            SimulationParameters simulationParameters13 = new SimulationParameters(5, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters1);
            SimulationParameters simulationParameters14 = new SimulationParameters(6, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters2);
            SimulationParameters simulationParameters15 = new SimulationParameters(7, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);
            SimulationParameters simulationParameters16 = new SimulationParameters(8, 10, 40, 20, sms, SendToOptions.All);
            blSimulator.AddSimulationParameters(simulationParameters3);

            blSimulator.AddSimulationParameters(simulationParameters9, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters10, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters11, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters12, DateTime.Today.AddMonths(-1));
            blSimulator.AddSimulationParameters(simulationParameters9, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters10, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters11, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters12, DateTime.Today.AddMonths(-2));
            blSimulator.AddSimulationParameters(simulationParameters9, DateTime.Today.AddMonths(-3));
            blSimulator.AddSimulationParameters(simulationParameters10, DateTime.Today.AddMonths(-3));
            blSimulator.AddSimulationParameters(simulationParameters11, DateTime.Today.AddMonths(-3));
            blSimulator.AddSimulationParameters(simulationParameters12, DateTime.Today.AddMonths(-3));

        }

        public static void PlayallDuration()
        {
            GetCallDurationPerNumberForLineByDate("0526654822", new DateTime(2018, 11, 1), new DateTime(2018, 11, 30));
        }
        public static void AddCall()
        {
            Call call0 = new Call(0, 1, null, 3, "0526654813", DateTime.Now);
            Call call1 = call0.NewCall(2, 4, "1234", DateTime.Now);
            Call call2 = call0.NewCall(1, 4, "123", DateTime.Now);
            SimulatorManagerWrite simulatorManagerEdit = new SimulatorManagerWrite();
            simulatorManagerEdit.AddCall(call0);
            simulatorManagerEdit.AddCall(call1);
            simulatorManagerEdit.AddCall(call2);
        }
        public static double? GetAmountLineToAll(string number)
        {
            using (CellularContext context = new CellularContext())
            {
                return context.Call.Where(c => c.Line.Number == number).Sum(c => c.Duration);
            }
        }

        public static void GetBlInvoiceCalculation()
        {
            Line line = new Line();
            BlInvoiceCalculation blInvoiceCalculation = new BlInvoiceCalculation(new InvoiceManagerRead());
            using (CellularContext context = new CellularContext())
            {
                line = context.Line.FirstOrDefault(l => l.Id == 6);
            }
            var calc = blInvoiceCalculation.CalculationLine(line, DateTime.Today);
            Console.WriteLine("MinuteAll");
            Console.WriteLine("TotalPrice" + calc.TotalPrice);
            Console.WriteLine("clientType.MinutePrice" + calc.TotalMinutesPrice);
            Console.WriteLine("MinuteGeneral" + calc.MinutesLeftInPackage);
            Console.WriteLine("Package.MaxMinute" + calc.Minutes + " calc.Package.FixedPrice " + calc.PackagePrice);
        }
        private static void AddLine()
        {
            Line line = new Line(0, 1, null, "555", Status.Used, 1, null, false);
            //Line line2 = new Line() {  ClientId=1,}
            BlLineAndPackage blLineAndPackage = new BlLineAndPackage(new CRMManagerRead(), new CRMManagerWrite());
            blLineAndPackage.AddLineReturnId(line, 1);
            var list = blLineAndPackage.GetListLineByClient(987654321);
            foreach (var item in list)
            {
                Console.WriteLine(item.Number);
            }
        }
        private static void AddClient()
        {
            var client = new Client().NewClient(12345658, "sanakkd", "kkfff", 1, "das", "1231", 1);
            //var container = new UnityContainer();
            //container.RegisterType(typeof(IDalCRMWrite),typeof(CRMManagerWrite));
            //container.RegisterType(typeof(IDalCRMRead), typeof(CRMManagerRead));

            //var blClient = container.Resolve<BlClient>();

            BlClient blClient = new BlClient(new CRMManagerRead(), new CRMManagerWrite());
            var a = blClient.AddClient(client);
            if (a == RequestStatus.Succeeded)
                Console.WriteLine("Succeeded");
            else Console.WriteLine("RequestStatus.systemError");

        }
        private static void EditLine()
        {
            Line line = new Line(7, 1, null, "222", Status.Used, 1, null, false);
            //Line line2 = new Line() {  ClientId=1,}
            BlLineAndPackage blLineAndPackage = new BlLineAndPackage(new CRMManagerRead(), new CRMManagerWrite());
            blLineAndPackage.EditLine(line);
            var list = blLineAndPackage.GetListLineByClient(987654321);
            foreach (var item in list)
            {
                Console.WriteLine(item.Number);
            }
        }
        private static void LoginClient()
        {
            Console.WriteLine("LoginClient");
            BlLoginClient blLoginClient = new BlLoginClient(new BlLoginMain(new LoginManagerRead()));
            Login login = blLoginClient.Login(1234, "1234");
            Console.WriteLine(login.Name);
            login = blLoginClient.Login(987654321, "052");
            Console.WriteLine(login.Name);
            login = blLoginClient.Login(456, "456");
            Console.WriteLine(login.Name);
        }
        private static void LoginEmployee()
        {
            try
            {
                Console.WriteLine("LoginEmployee");
                //BlLoginClient blLoginClient = new BlLoginClient();
                BlLoginEmployee blLoginEmployee = new BlLoginEmployee(new BlLoginMain(new LoginManagerRead()));
                Login login = blLoginEmployee.Login(1234, "1234");
                Console.WriteLine(login.Name);
                login = blLoginEmployee.Login(987654321, "052");
                Console.WriteLine(login.Name);
                login = blLoginEmployee.Login(456, "456");
                Console.WriteLine(login.Name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void LoginEmployeeManager()
        {
            try
            {
                Console.WriteLine("LoginEmployeeManager");
                //BlLoginClient blLoginClient = new BlLoginClient();
                //BlLoginEmployee blLoginEmployee = new BlLoginEmployee();
                BlLoginManager blLoginManager = new BlLoginManager(new BlLoginMain(new LoginManagerRead()));
                Login login = blLoginManager.Login(1234, "1234");
                Console.WriteLine(login.Name);
                login = blLoginManager.Login(987654321, "052");
                Console.WriteLine(login.Name);
                login = blLoginManager.Login(456, "456");
                Console.WriteLine(login.Name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void OptimalPackage()
        {
            // OptimalPackageController optimalPackageController = new OptimalPackageController();
            //var a =  optimalPackageController.GetOptimalPackage(6);
            BlOptimalPackage.BlOptimalPackage blOptimalPackage = new BlOptimalPackage.BlOptimalPackage(new BlInvoiceCalculation(new InvoiceManagerRead()), new OptimalPackageManagerRead(), new CRMManagerRead(), new BlLineAndPackage(new CRMManagerRead(), new CRMManagerWrite()));
            OptimalPackage optimalPackage = blOptimalPackage.GetOptimalPackage(6);

        }
        public static void Datetimetest()
        {
            DateTime date = DateTime.Today;
            DateTime dateEnd = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            Console.WriteLine(dateEnd);
        }

        public static void TestInvoice()
        {
            CRMManagerRead cRMManagerRead = new CRMManagerRead();
            Line line = cRMManagerRead.GetLineById(6);
            DateTime date = DateTime.Today.AddMonths(-1);
            BlInvoiceCalculation blInvoiceCalculation = new BlInvoiceCalculation(new InvoiceManagerRead());
            OptimalPackage optimalPackage = new OptimalPackage();
            Package package = new Package(1, "", 1, 1, 50, 1, null, true, true);
            LineInvoice invoiceCalculationLine = blInvoiceCalculation.CalculationLine(line, date, package);
            invoiceCalculationLine = blInvoiceCalculation.CalculationLine(line, date);
        }
        public static void Export()
        {
            CRMManagerRead cRMManagerRead = new CRMManagerRead();
            Line line = cRMManagerRead.GetLineById(6);
            Line line2 = cRMManagerRead.GetLineById(5);
            List<Line> list = new List<Line>()
            { line,line2 };
            //list.Add(line);
            //list.Add(line2);
            DateTime date = DateTime.Today.AddMonths(-1);
            BlInvoiceCalculation blInvoiceCalculation = new BlInvoiceCalculation(new InvoiceManagerRead());
            var clientInvoice = blInvoiceCalculation.InvoiceCalculationClient(list, date);
            ExportInvoice exportInvoice = new ExportInvoice();
            exportInvoice.Export(clientInvoice);
            clientInvoice = blInvoiceCalculation.InvoiceCalculationClient(list, date.AddMonths(1));
        }
        public static void Blsimple()
        {
            //var container = new Container();

            //// 2. Configure the container (register)
            //container.Register<IDalCRMWrite, CRMManagerWrite>();
            //container.Register<IDalCRMRead, CRMManagerRead>();
            //var test = container.GetInstance<>();

        }
        public static void BlReportTest()
        {
            BlReportManagerRead blReportManagerRead = new BlReportManagerRead();
            var a = blReportManagerRead.GetBIReport();
        }
    }
}
