using Common.Model;
using Common.ModelToBlClient.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Interface
{
    public interface IDalInvoiceRead
    {
        //List<Line> GetListLineByIdClient(int id);
        //List<Call> GetListCallByIdLine(int id);
        //List<SMS> GetListSMSByIdLine(int id);
        //int GetAmountSMSByIdLine(int id);

        //List<string> GetListLineToFamily(int id);
        //List<string> GetListLineToFriends(string number);
        //List<string> GetListLineToGeneral(string number);
        //List<string> GetListLineToAll(string number);

        //double GetAmountLineToFamily(int id);
        //double GetAmountLineToFriends(string number);
        //double GetAmountLineToGeneral(string number);
        //double GetAmountLineToAll(string number);
        List<CallDurationPerNumber> GetCallDurationPerNumberForLineByDate(string number, DateTime dateStart, DateTime dateEnd);
        double GetAmountLineToAll(string number);
        int GetListSMSByIdLine(string number, DateTime dateStart, DateTime dateEnd);
        List<string> GetListLineToFriends(int idLine);
        List<string> GetListLineToFamily(int idLine);
        Package GetPackageForLine(int id);
        ClientType GetClientType(int idClient);
        string GetNameClientByLine(string number);
        int GetIdClientByLine(string number);
        int? CheckIfThereIsPayment(int clientId, DateTime date);

    }
}
