using Common.Model;
using Common.ModelToBlClient;
using Common.ModelToBlClient.Invoice;
using DalMain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Manager
{
    public class BlReportManagerRead : DalBase, IDalBIReportRead
    {
        /// <summary>
        /// Returns the top 10 employees, the employees who added the most customers
        /// </summary>
        /// <returns>the top 10 who added the most customers</returns>
        public List<Employee> GetBestSellers()
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Employee.OrderByDescending(e => e.NumberOfLineCustomersAdded).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        /// <summary>
        /// Returns the report to managers
        ///Which includes 10 customers who call the center most, 10 best employees, 10 best customers
        /// </summary>
        /// <returns></returns>
        public BIReport GetBIReport()
        {
            try
            {
                BIReport bIReport = GetMostValueClients();
                bIReport.BestSellers = GetBestSellers();
                bIReport.MostCallingToCenterClients = GetMostCallingToCenterClients();
                //bIReport.PotentialGrougs = GetPotentialGrougs();
                return bIReport;
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public List<List<string>> GetPotentialGrougs()
        {
            try
            {

                //var listNumber = GetAllPotentialGrougs();
                var list1 = new List<string> { "1", "2", "3", "4" };
                var list2 = new List<string> { "1", "2", "3", "4" };
                var list3 = new List<string> { "1", "2", "3", "4" };
                var list4 = new List<string> { "1", "2", "3", "4" };
                var list5 = new List<string> { "5", "6", "7", "8" };
                var list6 = new List<string> { "5", "6", "7", "8" };
                var list7 = new List<string> { "5", "6", "7", "8" };
                var list8 = new List<string> { "5", "6", "7", "8" };
                var list9 = new List<string> { "1", "6", "7", "8" };
                var list10 = new List<string> { "1", "6", "7", "8" };

                List<List<string>> listNumber = new List<List<string>>()
                {
                    list1
                    ,list2
                    ,list3
                    ,list4
                    ,list5
                    ,list6
                    ,list7
                    ,list8
                    ,list9
                    ,list10
                };
                if (list1 == list2) Console.WriteLine("list1 == list2");

                var listGetPotentialGrougs = listNumber.GroupBy(c => c).Select(c => new
                {
                    list = c.Key,
                    count = c.Count()
                }).ToList();

                List<List<string>> listend = new List<List<string>>();
                foreach (var item in listGetPotentialGrougs)
                {
                   var count=  listNumber.Where(c => c == item.list).Count();
                    if (count == 4) listend.Add(item.list);

                }
                return listGetPotentialGrougs.Where(c=>c.count==4).Select(c=>c.list).ToList();
               // return listGetPotentialGrougs;

            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        /// <summary>
        /// Returns the groups of customers who speak the most
        /// </summary>
        /// <returns></returns>
        private List<List<string>> GetAllPotentialGrougs()
        {
            try
            {

                List<List<string>> listGetPotentialGrougs = new List<List<string>>();
                var listNumber = GetListNumber();
                foreach (var item in listNumber)
                {
                    listGetPotentialGrougs.Add(GetListNumber3TopCall(item));
                }
                return listGetPotentialGrougs;

            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        private List<string> GetListNumber3TopCall(string number)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var listCallDurationPerNumber = context.Call.Where(c => c.Line.Number == number).GroupBy(c => c.DestinationNumber)
.Select(st => new CallDurationPerNumber
{
    Number = st.Key,
    SumDuration = st.Sum(g => g.Duration)
}).ToList().OrderByDescending(e => e.SumDuration).Select(c => c.Number).Take(3).ToList();
                    listCallDurationPerNumber.Add(number);
                    listCallDurationPerNumber.OrderBy(c => c).ToList();
                    return listCallDurationPerNumber;
                }

            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        private List<string> GetListNumber()
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Call.Select(c => c.Line.Number).ToList();
                }

            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }

        /// <summary>
        /// Returns the 10 customers who called the center the most
        /// </summary>
        /// <returns>the 10 customers who called the center the most</returns>
        public List<Client> GetMostCallingToCenterClients()
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Client.OrderByDescending(e => e.CallsToCenter).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        /// <summary>
        /// Returns the 10 clients with the highest value score
        /// </summary>
        /// <returns></returns>
        public BIReport GetMostValueClients()
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var listpay = context.Payment.GroupBy(c => c.ClientId)
        .Select(st => new
        {
            ClientId = st.Key,
            countPayment = st.Count(),
            Price = st.Sum(g => g.TotalPayment),
            Client = st.Select(c => c.Client).FirstOrDefault(),

        }).ToList();
                    BIReport bIReport = new BIReport();
                    if (listpay.Count != 0)
                    {
                        var list = listpay.OrderByDescending(c => c.Price / c.countPayment).Take(10).ToList();
                        bIReport.TopClientsLastMonthMostValue = new List<double>();
                        bIReport.MostValueClients = new List<Client>();
                        foreach (var item in list)
                        {

                            var lastMonth = context.Payment.Where(c => c.ClientId == item.ClientId).OrderByDescending(c => c.Month).FirstOrDefault();
                            bIReport.MostValueClients.Add(item.Client);
                            if (lastMonth != null)
                                bIReport.TopClientsLastMonthMostValue.Add(Math.Round(lastMonth.TotalPayment, 2));
                        }
                    }
                    return bIReport;
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
    }
}

