using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Interface
{
    public interface IDalCRMWrite
    {
        //Client
        RequestStatus AddClient(Client client);
        RequestStatus RemoveClient(int id);
        RequestStatus EditClient(Client client);

        //Client Type
        RequestStatus AddClientType(ClientType clientType);
        RequestStatus RemoveClientType(int id);
        RequestStatus EditClientType(ClientType clientType);

        //Line
        int? AddLineReturnId(Line line);
        RequestStatus RemoveLine(int id);
        RequestStatus EditLine(Line line);
        int? AddLineReturnId(Line line, int idEmployee);

        //Package
        RequestStatus AddPackage(Package package);
        RequestStatus RemovePackage(int id);
        RequestStatus EditPackage(Package package);
        int? AddPackageReturnId(Package package);

        //SelectedNumbers
        int? AddSelectedNumbersReturnId(SelectedNumbers selectedNumbers);
        RequestStatus RemoveSelectedNumbers(int id);
        RequestStatus EditSelectedNumbers(SelectedNumbers selectedNumbers);

        //Payment
        RequestStatus AddPayment(Payment payment);
        RequestStatus RemovePayment(int id);
        RequestStatus EditPaymentById(Payment payment);

        RequestStatus RemoveLines(int idClient);
        RequestStatus ReturningCustomer(int idClient);

    }
}
