using Common.Model;
using DalMain.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Manager
{
    public class SimulatorManagerWrite : DalBase, IDalSimulatorWrite
    {

        public RequestStatus AddListCall(List<Call> list)
        {
            try
            {
                if (list == null) throw NewDALException(new StackTrace(true), "List Call Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.Call.AddRange(list);
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
        public RequestStatus AddListSMS(List<SMS> list)
        {
            try
            {
                if (list == null) throw NewDALException(new StackTrace(true), "list SMS Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.SMS.AddRange(list);
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

        public RequestStatus AddCall(Call call)
        {
            try
            {
                if (call == null) throw NewDALException(new StackTrace(true), "call Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.Call.Add(call);
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
        public RequestStatus AddSMS(SMS sms)
        {
            try
            {
                if (sms == null) throw NewDALException(new StackTrace(true), "sms Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        context.SMS.Add(sms);
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

        public RequestStatus EditCall(Call call)
        {
            try
            {
                if (call == null) throw NewDALException(new StackTrace(true), "call Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        var item = context.Call.FirstOrDefault(c => c.Id == call.Id);
                        if (item != null)
                        {
                            item.EditCall(call);
                            context.SaveChanges();
                            return RequestStatus.Succeeded;
                        }
                        else throw NewDALException(new StackTrace(true), "call does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus EditSMS(SMS sms)
        {
            try
            {
                if (sms == null) throw NewDALException(new StackTrace(true), "sms Sent empty");
                else
                {
                    using (CellularContext context = new CellularContext())
                    {
                        var item = context.SMS.FirstOrDefault(c => c.Id == sms.Id);
                        if (item != null)
                        {
                            item.EditSMS(sms);
                            context.SaveChanges();
                            return RequestStatus.Succeeded;
                        }
                        else throw NewDALException(new StackTrace(true), "sms does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }

        public RequestStatus RemoveCall(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Call.FirstOrDefault(c => c.Id == id);
                    if (item != null)
                    {
                        context.Call.Remove(item);
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "Call does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public RequestStatus RemoveSMS(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.SMS.FirstOrDefault(c => c.Id == id);
                    if (item != null)
                    {
                        context.SMS.Remove(item);
                        context.SaveChanges();
                        return RequestStatus.Succeeded;
                    }
                    else throw NewDALException(new StackTrace(true), "SMS does not exist");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
    }
}
