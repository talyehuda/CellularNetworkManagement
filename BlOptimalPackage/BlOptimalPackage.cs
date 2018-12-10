using BlBaseClassesLib;
using BlCRM;
using BlCRM.Interface;
using BlInvoice;
using BlInvoice.Interface;
using BlOptimalPackage.Interface;
using Common.Model;
using Common.ModelToBlClient.Invoice;
using Common.ModelToBlClient.OptimalPackage;
using DalMain.Interface;
using DalMain.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlOptimalPackage
{
    public class BlOptimalPackage : BlBase, IBlOptimalPackage
    {
        IBlInvoiceCalculation blInvoiceCalculation;
        IDalOptimalPackage optimalPackageManagerRead;
        IDalCRMRead cRMManagerRead;
        IBlLineAndPackage blLineAndPackage;

        public BlOptimalPackage(IBlInvoiceCalculation blInvoiceCalculation
            , IDalOptimalPackage optimalPackageManagerRead, IDalCRMRead cRMManagerRead, IBlLineAndPackage blLineAndPackage)
        {
            this.blInvoiceCalculation = blInvoiceCalculation;
            this.optimalPackageManagerRead = optimalPackageManagerRead;
            this.cRMManagerRead = cRMManagerRead;
            this.blLineAndPackage = blLineAndPackage;
        }
        /// <summary>
        /// Retorn OptimalPackage By the previous month for line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public OptimalPackage GetOptimalPackage(int lineId)
        {
            try
            {
                Line line = cRMManagerRead.GetLineById(lineId);
                if (line == null) throw NewBLException(new StackTrace(true), "line does not exist");
                else
                {
                    DateTime dateTime = DateTime.Today.AddMonths(-1);
                    return GetOptimalPackage(line, new DateTime(dateTime.Year, dateTime.Month, 1));
                }
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Retorn OptimalPackage for line By date received
        /// </summary>
        /// <param name="line"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public OptimalPackage GetOptimalPackage(Line line, DateTime date)
        {
            try
            {
                OptimalPackage optimalPackage = TotalMinutes(line, date);
                List<Recommendation> lr = GetListRecommendation(line, date);
                if (lr != null)
                {
                    optimalPackage.Recommendation1 = lr.Count >= 1 ? lr[0] : null;
                    optimalPackage.Recommendation2 = lr.Count >= 2 ? lr[1] : null;
                    optimalPackage.Recommendation3 = lr.Count >= 3 ? lr[2] : null;
                }
                return optimalPackage;
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        /// <summary>
        /// Calculates the recommended package for the customer. 
        /// A list of recommended packages in descending order(ID = 1, the most successful package)
        /// </summary>
        /// <returns> A list of recommended packages in descending order(ID = 1, the most successful package)</returns>
        private List<Recommendation> GetListRecommendation(Line line, DateTime date)
        {
            try
            {
                LineInvoice invoiceCalculationLine;
                List<Package> list = new List<Package>();
                list = blLineAndPackage.GetListPackageOptions();
                List<Recommendation> listRecommendation = new List<Recommendation>();
                foreach (var item in list)
                {
                    invoiceCalculationLine = blInvoiceCalculation.CalculationLine(line, date, item);
                    Recommendation recommendation = new Recommendation(invoiceCalculationLine.PackageInfo, invoiceCalculationLine.PackagePrice, invoiceCalculationLine.TotalPrice);
                    listRecommendation.Add(recommendation);
                }
                listRecommendation = GetListRecommendationOrderByPrice(listRecommendation);
                return listRecommendation;
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        /// <summary>
        /// Arrange the list by recommended packages in descending order (ID = 1, the most successful package)
        /// </summary>
        /// <param name="listRecommendation"></param>
        /// <returns></returns>
        private List<Recommendation> GetListRecommendationOrderByPrice(List<Recommendation> listRecommendation)
        {
            listRecommendation = listRecommendation.OrderBy(c => c.PriceForLastMonth).ToList();
            int count = 0;
            foreach (var item in listRecommendation)
            {
                item.Id = count++;
            }
            return listRecommendation;
        }
        /// <summary>
        /// Enters the parameter "OptimalPackage"  the quantities the customer made last month
        /// </summary>
        /// <param name="line"></param>
        /// <param name="date"></param>
        /// <returns>OptimalPackage</returns>
        private OptimalPackage TotalMinutes(Line line, DateTime date)
        {
            try
            {
                OptimalPackage optimalPackage = new OptimalPackage();
            LineInvoice invoiceCalculationLine;
            Package package = new Package(1, "", 1, 1, 50, 1, null, true, true);
            invoiceCalculationLine = blInvoiceCalculation.CalculationLine(line, date, package);
            optimalPackage.TotalMinutesFamily = Math.Round(invoiceCalculationLine.FamilyNumbersMinutes,2);
            optimalPackage.TotalMinutesGeneral = invoiceCalculationLine.MinutesLeftInPackage + invoiceCalculationLine.MinutesBeyondPackageLimit;
            optimalPackage.TotalMinutesTop3Number = Math.Round(invoiceCalculationLine.FreindNumbersMinutes, 2);
            optimalPackage.TotalMinutesTopNumber =Math.Round( invoiceCalculationLine.MostCalledNumbersMinutes,2);
            optimalPackage.TotalMinutes = Math.Round(optimalPackage.TotalMinutesFamily + optimalPackage.TotalMinutesGeneral + optimalPackage.TotalMinutesTop3Number + optimalPackage.TotalMinutesTopNumber, 2);
            optimalPackage.TotalSMS = invoiceCalculationLine.SMS;
            optimalPackage.ClientValue = GatClientValue(line.ClientId);

            return optimalPackage;
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }
        /// <summary>
        /// Calculates the value of the customer
        /// </summary>
        /// <param name="line"></param>
        /// <returns>ClientValue</returns>
        private double GatClientValue(int idClient)
        {
            try
            {
                ClientValue clientValue = optimalPackageManagerRead.GetClientValueByClient(idClient);

                int amountLine = clientValue.NumberOfLines;
            double amountPrice3Month = clientValue.AmountOfInvoices;
            int amountCallsToCenter = clientValue.CallsToCenter;

            double calcAmountLine = 0.2;
            double calcAmountPrice3Month = 1000;
            double calcAmountCallsToCenter = -0.1;

            int MaxamountLine = 4;
            double MaxamountPrice3Month = 6;
            int MaxamountCallsToCenter = -3;

            double resultAmountLine = 0;
            double resultAmountPrice3Month = 0;
            double resultAmountCallsToCenter = 0;

            resultAmountLine = (amountLine * calcAmountLine >= MaxamountLine) ? MaxamountLine : amountLine * calcAmountLine;
                if (amountPrice3Month!=0&& calcAmountPrice3Month!=0)
                resultAmountPrice3Month = (amountPrice3Month / calcAmountPrice3Month >= MaxamountPrice3Month) ? MaxamountPrice3Month : amountPrice3Month / calcAmountPrice3Month;
            resultAmountCallsToCenter = (amountCallsToCenter * calcAmountCallsToCenter <= MaxamountCallsToCenter) ? MaxamountCallsToCenter : amountCallsToCenter * calcAmountCallsToCenter;

            double result = resultAmountCallsToCenter + resultAmountLine + resultAmountPrice3Month;
            if (result > 10) result = 10;
            else if (result < 0) result = 0;
            return (result*10);
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }




    }
}
