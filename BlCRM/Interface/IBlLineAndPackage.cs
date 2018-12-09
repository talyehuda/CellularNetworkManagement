using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlCRM.Interface
{
    public interface IBlLineAndPackage
    {
        List<Line> GetListLineByClient(int ClientIdNumber);
        List<Line> GetListLineByClientId(int ClientId);
        List<Package> GetListPackageOptions();
        bool GetIfLineIsAssigned(string Newnumber);
        int? AddLineReturnId(Line line, int idEmployee);
        RequestStatus EditLine(Line line);
        int? AddPackageReturnId(Package package);
        int? AddSelectedNumbersReturnId(SelectedNumbers selectedNumbers);
        RequestStatus EditPackage(Package package);
        double ReturnTotalFixedPrice(Package package);
        Package GetPackageById(int lineId);
        RequestStatus RemoveLine(int id);
        SelectedNumbers GetSelectedNumbersById(int SelectedNumbersId);

    }
}
