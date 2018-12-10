using DalMain.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Manager
{
    public class SimulatorManagerRead : DalBase, IDalSimulatorRead
    {
        Random random = new Random();
        public string GetRandomSendToAll(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var list = context.Line.Where(c => c.Id != id && !c.Deleted).Select(c => c.Number).ToList();
                    if (list.Count != 0) return list[random.Next(list.Count)];
                    else throw NewDALException(new StackTrace(true), "No lines in the system");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public string GetRandomSendToFamily(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Line.FirstOrDefault(i => i.Id == id && !i.Deleted);
                    if (item == null) throw NewDALException(new StackTrace(true), "Line does not exist");
                    else
                    {
                        var list = context.Line.Where(c => c.Number != item.Number && c.ClientId == item.ClientId && !c.Deleted).Select(c => c.Number).ToList();
                        if (list.Count != 0) return list[random.Next(list.Count)];
                        else throw NewDALException(new StackTrace(true), "This line has no Family");
                    }
                }
            }

            catch (Exception ex)
            {
                throw HandleException(ex);
            }

        }
        public string GetRandomSendToFriends(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var list = GetListSendToFriends(id);
                    if (list.Count != 0) return list[random.Next(list.Count)];
                    else throw NewDALException(new StackTrace(true), "This line has no friends");
                }
            }

            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        public string GetRandomSendToGeneral(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var clientId = context.Line.FirstOrDefault(i => i.Id == id && !i.Deleted).ClientId;
                    var list = context.Line.Where(c => c.ClientId != clientId && !c.Deleted).Select(c => c.Number).ToList();
                    var listFriends = GetListSendToFriends(id);
                    if (listFriends.Count != 0)
                    {
                        foreach (var item in listFriends)
                        {
                            var a = list.FirstOrDefault(c => c == item);
                            if (a != null)
                                list.Remove(item);
                        }
                    }
                    if (list.Count != 0) return list[random.Next(list.Count)];
                    else throw NewDALException(new StackTrace(true), "This line has no General");
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        private List<string> GetListSendToFriends(int id)
        {
            try
            {
                using (CellularContext context = new CellularContext())
                {
                    var item = context.Line.FirstOrDefault(i => i.Id == id && !i.Deleted);
                    if (item == null)
                        throw NewDALException(new StackTrace(true), "Line does not exist");
                    else
                    {
                        if (item.PackageId != null)
                        {
                            var SelectedNumbers = context.Package.Where(c => c.Id == item.PackageId).Select(c => c.SelectedNumbers).FirstOrDefault();
                            if (SelectedNumbers == null)
                                throw NewDALException(new StackTrace(true), "Selected Numbers does not exist to this line");
                            else return new List<string>() { SelectedNumbers.FirstNumber, SelectedNumbers.SecondNumber, SelectedNumbers.ThirdNumber };
                        }
                        else throw NewDALException(new StackTrace(true), "Selected Numbers does not exist to this line");

                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
    }
}
