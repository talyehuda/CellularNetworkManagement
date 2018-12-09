using Common.ModelToBlClient.OptimalPackage;
using DalMain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Manager
{
    public class OptimalPackageManagerRead : DalBase, IDalOptimalPackage
    {
        public double GetAmountOfInvoices(int idClient)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    DateTime dateTime = DateTime.Today;
                    dateTime = new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(-3);
                    var list = context.Payment.Where(l => l.ClientId == idClient && l.Month >= dateTime).ToList();
                    return list.Count != 0 ? list.Sum(l => l.TotalPayment) : 0;
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public int GetCallsToCenter(int idClient)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Client.FirstOrDefault(l => l.Id == idClient && !l.Deleted).CallsToCenter;
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public ClientValue GetClientValueByClient(int idClient)
        {
            try
            {
                ClientValue clientValue = new ClientValue();
                clientValue.AmountOfInvoices = GetAmountOfInvoices(idClient);
                clientValue.CallsToCenter = GetCallsToCenter(idClient);
                clientValue.NumberOfLines = GetNumberOfLines(idClient);
                return clientValue;
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public int GetNumberOfLines(int idClient)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Line.Count(l => l.ClientId == idClient && !l.Deleted);
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
    }
}
