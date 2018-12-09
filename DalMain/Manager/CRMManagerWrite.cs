using Common.Model;
using DalMain;
using DalMain.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Manager
{
    public class CRMManagerWrite : DalBase, IDalCRMWrite
    {
        public RequestStatus AddClient(Client client)
        {
            try
            {
                if (client == null) throw NewDALException(new StackTrace(true), "client Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.Client.Add(client);
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }

        }
        public RequestStatus AddClientType(ClientType clientType)
        {
            try
            {
                if (clientType == null) throw NewDALException(new StackTrace(true), "clientType Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.ClientType.Add(clientType);
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public int? AddLineReturnId(Line line)
        {
            try
            {
                if (line == null) throw NewDALException(new StackTrace(true), "line Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.Line.Add(line);
                        context.SaveChanges();
                        return line.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus AddPackage(Package package)
        {
            try
            {
                if (package == null) throw NewDALException(new StackTrace(true), "package Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.Package.Add(package);
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus AddPayment(Payment payment)
        {
            try
            {
                if (payment == null) throw NewDALException(new StackTrace(true), "payment Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.Payment.Add(payment);
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public int? AddSelectedNumbersReturnId(SelectedNumbers selectedNumbers)
        {
            try
            {
                if (selectedNumbers == null) throw NewDALException(new StackTrace(true), "selectedNumbers Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.SelectedNumbers.Add(selectedNumbers);
                        context.SaveChanges();
                        return selectedNumbers.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public int? AddPackageReturnId(Package package)
        {
            try
            {
                if (package == null) throw NewDALException(new StackTrace(true), "package Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.Package.Add(package);
                        context.SaveChanges();
                        return package.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }

        public RequestStatus EditClient(Client client)
        {
            try
            {
                if (client == null) throw NewDALException(new StackTrace(true), "client Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        var item = context.Client.FirstOrDefault(c => c.Id == client.Id);
                        if (item != null)
                        {
                            item.EditClient(client);
                            context.SaveChanges();
                            return RequestStatus.Succeeded;
                        }
                        else throw NewDALException(new StackTrace(true), "client does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }

        }
        public RequestStatus EditClientType(ClientType clientType)
        {
            try
            {
                if (clientType == null) throw NewDALException(new StackTrace(true), "clientType Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        var item = context.ClientType.FirstOrDefault(c => c.Id == clientType.Id);
                        if (item != null)
                        {
                            item.EditClientType(clientType);
                            context.SaveChanges();
                            return RequestStatus.Succeeded;
                        }
                        else throw NewDALException(new StackTrace(true), "clientType does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus EditLine(Line line)
        {
            try
            {
                if (line == null) throw NewDALException(new StackTrace(true), "line Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        var item = context.Line.FirstOrDefault(c => c.Id == line.Id);
                        if (item != null)
                        {
                            item.EditLine(line);
                            context.SaveChanges();
                            return RequestStatus.Succeeded;
                        }
                        else throw NewDALException(new StackTrace(true), "line does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus EditPackage(Package package)
        {
            try
            {
                if (package == null) throw NewDALException(new StackTrace(true), "package Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        var item = context.Package.FirstOrDefault(c => c.Id == package.Id);
                        if (item != null)
                        {
                            item.EditPackage(package);
                            context.SaveChanges();
                            return RequestStatus.Succeeded;
                        }
                        else throw NewDALException(new StackTrace(true), "package does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus EditPaymentById(Payment payment)
        {
            try
            {
                if (payment == null) throw NewDALException(new StackTrace(true), "payment Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        var item = context.Payment.FirstOrDefault(c => c.Id == payment.Id);
                        if (item != null)
                        {
                            item.EditPayment(payment);
                            context.SaveChanges();
                            return RequestStatus.Succeeded;
                        }
                        else throw NewDALException(new StackTrace(true), "payment does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus EditSelectedNumbers(SelectedNumbers selectedNumbers)
        {
            try
            {
                if (selectedNumbers == null) throw NewDALException(new StackTrace(true), "selectedNumbers Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        var item = context.SelectedNumbers.FirstOrDefault(c => c.Id == selectedNumbers.Id);
                        if (item != null)
                        {
                            item.EditSelectedNumbers(selectedNumbers);
                            context.SaveChanges();
                            return RequestStatus.Succeeded;
                        }
                        else throw NewDALException(new StackTrace(true), "selectedNumbers does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }

        public RequestStatus RemoveClient(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Client.FirstOrDefault(c => c.Id == id);
                    if (item != null)
                    {
                        item.DeleteClient();
                        context.SaveChanges();
                        RemoveLines(id);
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "Client does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus RemoveClientType(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.ClientType.FirstOrDefault(c => c.Id == id);
                    if (item != null)
                    {
                        context.ClientType.Remove(item);
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "ClientType does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus RemoveLines(int idClient)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var list = context.Line.Where(l => l.ClientId == idClient && !l.Deleted).ToList();
                    if (list != null)
                    {
                        foreach (var item in list)
                        {
                            item.DeleteLine();
                        }
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "Line does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus RemoveLine(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Line.FirstOrDefault(c => c.Id == id);
                    if (item != null)
                    {
                        item.DeleteLine();
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "Line does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus RemovePackage(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Package.FirstOrDefault(c => c.Id == id);
                    if (item != null)
                    {
                        context.Package.Remove(item);
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "Package does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus RemovePayment(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Payment.FirstOrDefault(c => c.Id == id);
                    if (item != null)
                    {
                        context.Payment.Remove(item);
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "Payment does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus RemoveSelectedNumbers(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.SelectedNumbers.FirstOrDefault(c => c.Id == id);
                    if (item != null)
                    {
                        context.SelectedNumbers.Remove(item);
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "SelectedNumbers does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }

        public RequestStatus ReturningCustomer(int idClient)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Client.FirstOrDefault(c => c.Id == idClient);
                    if (item != null)
                    {
                        item.AnDeleteClient();
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "Client does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus AddNumberOfLineCustomersAdded(int idEmployee)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Employee.FirstOrDefault(c => c.Id == idEmployee);
                    if (item != null)
                    {
                        item.AddNumberOfCustomersAdded();
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "Employee does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public int? AddLineReturnId(Line line, int idEmployee)
        {
            try
            {
                int? idLine = AddLineReturnId(line);
                AddNumberOfLineCustomersAdded(idEmployee);
                return idLine;
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }


    }
}
