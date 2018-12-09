using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain.Interface
{
    public interface IDalSimulatorWrite
    {
        //call
        RequestStatus AddCall(Call call);
        RequestStatus RemoveCall(int id);
        RequestStatus EditCall(Call call);
        RequestStatus AddListCall(List<Call> list);

        //sms
        RequestStatus AddSMS(SMS sms);
        RequestStatus RemoveSMS(int id);
        RequestStatus EditSMS(SMS sms);
        RequestStatus AddListSMS(List<SMS> list);
    }
}
