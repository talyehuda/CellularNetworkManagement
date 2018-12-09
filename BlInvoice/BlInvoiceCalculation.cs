using BlBaseClassesLib;
using BlInvoice.Interface;
using Common.Model;
using Common.ModelToBlClient.Invoice;
using DalMain.Interface;
using DalMain.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlInvoice
{
    public class BlInvoiceCalculation : BlBase, IBlInvoiceCalculation
    {
        IDalInvoiceRead dalInvoiceRead;
        public BlInvoiceCalculation(IDalInvoiceRead dalInvoiceRead)
        {
            this.dalInvoiceRead = dalInvoiceRead;
        }
        private List<CallDurationPerNumber> GetListClientsIdNumber(Line line, DateTime date)
        {
            try
            {
                DateTime dateStart = date.AddDays(-(date.Day - 1));
                DateTime dateEnd = date.AddMonths(1).AddDays(-(date.Day));
                return dalInvoiceRead.GetCallDurationPerNumberForLineByDate(line.Number, dateStart, dateEnd);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        public LineInvoice CalculationLine(Line line, DateTime date)
        {
            try
            {
                Package package = dalInvoiceRead.GetPackageForLine(line.Id);
                if (package == null) throw NewBLException(new StackTrace(true), "package Sent empty");
                else return CalculationLine(line, date, package);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }

        }
        public LineInvoice CalculationLine(Line line, DateTime date, Package package)
        {
            try
            {
                LineInvoice invoiceCalculationLine = new LineInvoice();
                var list = GetListClientsIdNumber(line, date);
                if (package != null)
                {
                    invoiceCalculationLine.PackageInfo = package.PackageName;
                    invoiceCalculationLine.PackagePrice = package.FixedPrice ?? 0;
                    foreach (var item in list)
                    {
                        item.SendTo = SendToOptions.General;
                    }
                    if (package.InsideFamilyCalls)
                    {
                        List<string> listToRemove = dalInvoiceRead.GetListLineToFamily(line.Id);
                        foreach (var item in listToRemove)
                        {
                            var CallDurationPerNumber = list.FirstOrDefault(c => c.Number == item);
                            if (CallDurationPerNumber != null) CallDurationPerNumber.SendTo = SendToOptions.Family;
                        }
                        invoiceCalculationLine.FamilyNumbersMinutes = ReturnSumDuration(list, SendToOptions.Family);
                    }
                    if (package.MostCalledNumber)
                    {
                        var CallDurationPerNumber = list.Where(c => c.SendTo != SendToOptions.Family).OrderByDescending(c => c.SumDuration).FirstOrDefault();
                        if (CallDurationPerNumber != null)
                        {
                            CallDurationPerNumber.SendTo = SendToOptions.All;
                            invoiceCalculationLine.MostCalledNumbersMinutes = CallDurationPerNumber.SumDuration;
                        }
                    }
                    if (package.FixedPrice != null) invoiceCalculationLine.PackagePrice += 9.9;
                    if (package.FavouriteNumbers != null)
                    {
                        List<string> listToRemove = dalInvoiceRead.GetListLineToFriends(line.Id);
                        foreach (var item in listToRemove)
                        {
                            var CallDurationPerNumber = list.FirstOrDefault(c => c.Number == item);
                            if (CallDurationPerNumber != null)
                                if (CallDurationPerNumber.SendTo != SendToOptions.All)
                                    CallDurationPerNumber.SendTo = SendToOptions.Friends;
                        }
                        invoiceCalculationLine.FreindNumbersMinutes = ReturnSumDuration(list, SendToOptions.Friends);
                    }
                    if(package.MaxMinute!=null)
                    invoiceCalculationLine.Minutes = (double)package.MaxMinute;
                    invoiceCalculationLine.MinutesLeftInPackage = ReturnSumDuration(list, SendToOptions.General);
                    invoiceCalculationLine.LineNumber = line.Number;

                    DateTime dateStart = new DateTime(date.Year, date.Month, 1);
                    DateTime dateEnd = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
                    invoiceCalculationLine.SMS = dalInvoiceRead.GetListSMSByIdLine(line.Number, dateStart, dateEnd);
                    ClientType clientType = dalInvoiceRead.GetClientType(line.ClientId);
                    invoiceCalculationLine.SMSPrice = clientType.SMSPrice;
                    invoiceCalculationLine.MinutePrice = clientType.MinutePrice;
                    if (package.DiscountPercentage == null) package.DiscountPercentage = 0;

                    invoiceCalculationLine.FreindNumbersMinutePrice = ((100 - (double)package.DiscountPercentage) / 100) * invoiceCalculationLine.MinutePrice;
                    invoiceCalculationLine.FreindNumbersMinutePrice = Math.Round(invoiceCalculationLine.FreindNumbersMinutePrice, 2);

                    invoiceCalculationLine = CalcPrice(invoiceCalculationLine);
                    invoiceCalculationLine.TotalPrice = Math.Round(TotalPrice(invoiceCalculationLine), 2);

                }
                return invoiceCalculationLine;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        private double ReturnSumDuration(List<CallDurationPerNumber> list, SendToOptions sendTo)
        {
            return list.Where(c => c.SendTo == sendTo).Sum(c => c.SumDuration);
        }
        private double TotalPrice(LineInvoice invoiceCalculationLine)
        {
            double price = 0;
            price += invoiceCalculationLine.OutOfPackageTotalPrice;
            price += invoiceCalculationLine.PackagePrice;
            return price;
        }
        private LineInvoice CalcPrice(LineInvoice invoiceCalculationLine)
        {
            try
            {
                double MinutesInPackage = 0;
                double SumDuration = 0;
                SumDuration = invoiceCalculationLine.MinutesLeftInPackage - invoiceCalculationLine.Minutes;
                if (SumDuration > 0)
                {
                    invoiceCalculationLine.MinutesBeyondPackageLimit = Math.Round(SumDuration, 2);
                    invoiceCalculationLine.MinutesLeftInPackage = 0;
                    invoiceCalculationLine.PackagePercentUsage = 100;
                }
                else
                {
                    MinutesInPackage = Math.Round(invoiceCalculationLine.MinutesLeftInPackage, 2);
                   invoiceCalculationLine.MinutesLeftInPackage = Math.Round(invoiceCalculationLine.Minutes - invoiceCalculationLine.MinutesLeftInPackage,2);
                   invoiceCalculationLine.MinutesBeyondPackageLimit = 0;
                    if (invoiceCalculationLine.Minutes != 0 && invoiceCalculationLine.MinutesLeftInPackage != 0)
                        invoiceCalculationLine.PackagePercentUsage = Math.Round(MinutesInPackage / invoiceCalculationLine.Minutes * 100);
                    else invoiceCalculationLine.PackagePercentUsage = 100;

                }                
                invoiceCalculationLine.TotalMinutesPrice = Math.Round(invoiceCalculationLine.MinutesBeyondPackageLimit * invoiceCalculationLine.MinutePrice,2);
                invoiceCalculationLine.FreindNumbersTotalPrice = (invoiceCalculationLine.FreindNumbersMinutes * invoiceCalculationLine.FreindNumbersMinutePrice);
                invoiceCalculationLine.TotalSMSPrice = (invoiceCalculationLine.SMS * invoiceCalculationLine.SMSPrice);
                invoiceCalculationLine.TotalSMSPrice = Math.Round(invoiceCalculationLine.TotalSMSPrice, 2);
                invoiceCalculationLine.FreindNumbersTotalPrice = Math.Round(invoiceCalculationLine.FreindNumbersTotalPrice, 2);

                invoiceCalculationLine.OutOfPackageTotalPrice += invoiceCalculationLine.TotalSMSPrice;
                invoiceCalculationLine.OutOfPackageTotalPrice += invoiceCalculationLine.TotalMinutesPrice;
                invoiceCalculationLine.OutOfPackageTotalPrice += invoiceCalculationLine.FreindNumbersTotalPrice;
                invoiceCalculationLine.OutOfPackageTotalPrice = Math.Round(invoiceCalculationLine.OutOfPackageTotalPrice, 2);
                return invoiceCalculationLine;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        public ClientInvoice InvoiceCalculationClient(List<Line> list, DateTime date)
        {
            try
            {
                ClientInvoice invoiceCalculationClient = new ClientInvoice();
                if (list != null)
                {
                    invoiceCalculationClient.LineInvoices = new List<LineInvoice>();
                    foreach (var item in list)
                    {
                        var lineInvoice = CalculationLine(item, date);
                        invoiceCalculationClient.LineInvoices.Add(lineInvoice);
                    }
                    GetInvoiceCalculationClient(invoiceCalculationClient, date);
                    AddPayment(invoiceCalculationClient, date);
                }
                else invoiceCalculationClient = null;
                return invoiceCalculationClient;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        private void GetInvoiceCalculationClient(ClientInvoice invoiceCalculationClient, DateTime date)
        {
            invoiceCalculationClient.Month = date.Month;
            invoiceCalculationClient.Year = date.Year;
            try
            {
                invoiceCalculationClient.ClientName = dalInvoiceRead.GetNameClientByLine(invoiceCalculationClient.LineInvoices[0].LineNumber);
                invoiceCalculationClient.ClientId = dalInvoiceRead.GetIdClientByLine(invoiceCalculationClient.LineInvoices[0].LineNumber);
                foreach (var item in invoiceCalculationClient.LineInvoices)
                {
                    invoiceCalculationClient.TotalPrice += item.TotalPrice;
                }
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        private void AddPayment(ClientInvoice invoiceCalculationClient, DateTime date)
        {
            try
            {
                CRMManagerWrite cRMManagerEdit = new CRMManagerWrite();
                Payment payment = new Payment(0, invoiceCalculationClient.ClientId, null, date, invoiceCalculationClient.TotalPrice);
                int? paymentId = dalInvoiceRead.CheckIfThereIsPayment(invoiceCalculationClient.ClientId, date);
                if (paymentId != null)
                {
                    payment.Id = (int)paymentId;
                    cRMManagerEdit.EditPaymentById(payment);
                }

                else
                    cRMManagerEdit.AddPayment(payment);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }

        }
    }
}
