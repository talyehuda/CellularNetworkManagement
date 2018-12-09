using Common.Model;
using Common.ModelToBlClient.Invoice;
using DalMain.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Manager
{
    public class InvoiceManagerRead : DalBase, IDalInvoiceRead
    {
        public List<CallDurationPerNumber> GetCallDurationPerNumberForLineByDate(string number, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Call.Where(c => c.Line.Number == number && c.Time >= dateStart && c.Time <= dateEnd).GroupBy(c => c.DestinationNumber)
                        .Select(st => new CallDurationPerNumber
                        {
                            Number = st.Key,
                            SumDuration = st.Sum(g => g.Duration)
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public double GetAmountLineToAll(string number)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.Call.Where(c => c.Line.Number == number).Sum(c => c.Duration);
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public int GetListSMSByIdLine(string number, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    return context.SMS.Where(c => c.Line.Number == number && c.Time >= dateStart && c.Time <= dateEnd).Count();
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public List<string> GetListLineToFriends(int idLine)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var line = context.Line.FirstOrDefault(i => i.Id == idLine);
                    if (line == null) throw NewDALException(new StackTrace(true), "line does not exist");
                    else
                    {
                        var package = context.Package.FirstOrDefault(p => p.Id == line.PackageId);
                        if (package == null) throw NewDALException(new StackTrace(true), "package does not exist");
                        else
                        {
                            var SelectedNumbers = context.SelectedNumbers.FirstOrDefault(s => s.Id == package.FavouriteNumbers);
                            if (SelectedNumbers == null) throw NewDALException(new StackTrace(true), "SelectedNumbers does not exist");
                            return new List<string>() { SelectedNumbers.FirstNumber, SelectedNumbers.SecondNumber, SelectedNumbers.ThirdNumber };
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public List<string> GetListLineToFamily(int idLine)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Line.FirstOrDefault(i => i.Id == idLine);
                    if (item == null) throw NewDALException(new StackTrace(true), "item does not exist");
                    else return context.Line.Where(c => c.Number != item.Number && c.ClientId == item.ClientId)
                            .Select(c => c.Number).ToList();

                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public Package GetPackageForLine(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Line.FirstOrDefault(i => i.Id == id);
                    if (item == null) throw NewDALException(new StackTrace(true), "item does not exist");
                    else return context.Package.FirstOrDefault(c => c.Id == item.PackageId);
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public ClientType GetClientType(int idClient)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var client = context.Client.FirstOrDefault(c => c.Id == idClient);
                    if (client == null) throw NewDALException(new StackTrace(true), "client does not exist");
                    else return context.ClientType.FirstOrDefault(c => c.Id == client.ClientTypeId);
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public string GetNameClientByLine(string number)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    Line line = context.Line.FirstOrDefault(l => l.Number == number && !l.Deleted);
                    if (line == null) throw NewDALException(new StackTrace(true), "line does not exist");
                    else
                    {
                        Client client = context.Client.FirstOrDefault(c => c.Id == line.ClientId);
                        if (client == null) throw NewDALException(new StackTrace(true), "client does not exist");
                        else return client.Name + " " + client.LastName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public int GetIdClientByLine(string number)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    Line line = context.Line.FirstOrDefault(l => l.Number == number && !l.Deleted);
                    if (line == null) throw NewDALException(new StackTrace(true), "line does not exist");
                    else return line.ClientId;
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public int? CheckIfThereIsPayment(int clientId, DateTime date)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    Payment payment = context.Payment.FirstOrDefault(p => p.ClientId == clientId && p.Month.Month == date.Month && p.Month.Year == date.Year);
                    return payment?.Id;
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
    }
}
