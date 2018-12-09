using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ModelToBlClient
{
   public class BIReport
    {
        /// <summary>
        /// Returns a list of customers with the highest value
        /// </summary>
        public List<Client> MostValueClients { get; set; }
        /// <summary>
        /// Top Clients Last Month Most Value
        /// </summary>
        public List<double> TopClientsLastMonthMostValue { get; set; }
        /// <summary>
        /// Returns a list of customers who call the most to the center
        /// </summary>
        public List<Client> MostCallingToCenterClients { get; set; }
        /// <summary>
        /// Returns list of best sellers
        /// </summary>
        public List<Employee> BestSellers { get; set; }
        /// <summary>
        /// Returns list of potential groups
        /// </summary>
        public List<List<string>> PotentialGrougs { get; set; }
    }
}
