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
                bIReport.PotentialGrougs = GetPotentialGrougs();
                return bIReport;
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        /// <summary>
        /// Checks if the groups are equal
        /// </summary>
        /// <param name="group1"></param>
        /// <param name="group2"></param>
        /// <returns></returns>
        private bool AreLineGroupsEqual(List<string> group1, List<string>group2)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (string str in group1)
            {
                set.Add(str);
            }
            foreach (string str in group2)
            {
                if (!set.Contains(str))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// calculates potential groups
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetPotentialGrougs()
        {
            
            List<List<string>> listPotentialGrougs = new List<List<string>>();
            try
            {

                var listNumber = GetAllPotentialGrougs();

                for (int i = 0; i < listNumber.Count - 3; i++)
                {
                    for (int j = i + 1; j < listNumber.Count - 2; j++)
                    {
                        if (!AreLineGroupsEqual(listNumber[i], listNumber[j]))
                            break;
                        for (int k = j + 1; k < listNumber.Count - 1; k++)
                        {
                            if (!AreLineGroupsEqual(listNumber[j], listNumber[k]))
                                break;
                            for (int l = k + 1; l < listNumber.Count; l++)
                            {
                                if (AreLineGroupsEqual(listNumber[k], listNumber[l]))
                                {
                                    //output a new potential group
                                    listPotentialGrougs.Add(listNumber[i]);

                                }
                            }
                        }
                    }
                }
                return listPotentialGrougs;
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
        /// <summary>
        /// Returns groups of four people each, that everyone has the three others calls them most
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Returns a list of all lines in the system
        /// </summary>
        /// <returns></returns>
        private List<string> GetListNumber()
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Call.GroupBy(c => c.Line.Number).Select(c => c.Key).ToList();
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

