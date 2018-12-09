using Common.Model;
using Common.ModelToBlClient.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlInvoice.Interface
{
   public interface IBlInvoiceCalculation
    {
        LineInvoice CalculationLine(Line line, DateTime date);
        LineInvoice CalculationLine(Line line, DateTime date, Package package);
        ClientInvoice InvoiceCalculationClient(List<Line> list, DateTime date);
    }
}
