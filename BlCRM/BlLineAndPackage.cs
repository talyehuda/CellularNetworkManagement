using BlBaseClassesLib;
using BlCRM.Interface;
using BLExceptionLib;
using Common.Model;
using DalMain;
using DalMain.Interface;
using DalMain.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlCRM
{
    public class BlLineAndPackage : BlBase, IBlLineAndPackage
    {
        IDalCRMRead cRMManagerRead = null;
        IDalCRMWrite cRMManagerEdit = null;
        public BlLineAndPackage(IDalCRMRead cRMManagerRead, IDalCRMWrite cRMManagerEdit)
        {
            this.cRMManagerEdit = cRMManagerEdit;
            this.cRMManagerRead = cRMManagerRead;
        }
        /// <summary>
        /// Get List Line By Client
        /// </summary>
        /// <param name="ClientIdNumber"></param>
        /// <returns></returns>
        public List<Line> GetListLineByClient(int ClientIdNumber)
        {
            try
            {
                return cRMManagerRead.GetListLineByClientIdNumber(ClientIdNumber);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Get List Line By ClientId
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        public List<Line> GetListLineByClientId(int ClientId)
        {
            try
            {
                return cRMManagerRead.GetListLineByClient(ClientId);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Get List Package Options
        /// </summary>
        /// <returns></returns>
        public List<Package> GetListPackageOptions()
        {
            try
            {
                List<Package> listPackage = new List<Package>();
                List<PackageOptions> list = cRMManagerRead.GetListPackageOptions();
                foreach (var item in list)
                {
                    int? FavouriteNumbers = null;
                    if (item.FavouriteNumbers)
                        FavouriteNumbers = 1;
                    listPackage.Add(
                        new Package(item.Id, item.PackageName, item.MaxMinute, item.FixedPrice, item.DiscountPercentage, FavouriteNumbers, null, item.MostCalledNumber, item.InsideFamilyCalls)
                        );
                }
                return listPackage;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }

        }
        /// <summary>
        /// Get If Line Is Assigned
        /// </summary>
        /// <param name="Newnumber"></param>
        /// <returns></returns>
        public bool GetIfLineIsAssigned(string Newnumber)
        {
            try
            {
                return cRMManagerRead.GetIfLineIsAssigned(Newnumber);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Add Line and Return Id
        /// </summary>
        /// <param name="line"></param>
        /// <param name="idEmployee"></param>
        /// <returns></returns>
        public int? AddLineReturnId(Line line, int idEmployee)
        {
            if (line == null) throw NewBLException(new StackTrace(true), "line Sent empty");
            else
            {
                try
                {
                    if (!GetIfLineIsAssigned(line.Number))
                        return cRMManagerEdit.AddLineReturnId(line, idEmployee);
                    else return null;
                }
                catch (Exception e)
                {
                    throw HandleException(e);
                }
            }
        }
        /// <summary>
        /// Edit Line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public RequestStatus EditLine(Line line)
        {
            if (line == null) throw NewBLException(new StackTrace(true), "line Sent empty");
            else
            {
                try
                {
                    return cRMManagerEdit.EditLine(line);
                }
                catch (Exception e)
                {
                    throw HandleException(e);
                }
            }
        }
        /// <summary>
        /// Add Package Return Id
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public int? AddPackageReturnId(Package package)
        {
            if (package == null) throw NewBLException(new StackTrace(true), "package Sent empty");
            else
            {
                try
                {
                    return cRMManagerEdit.AddPackageReturnId(package);
                }
                catch (Exception e)
                {
                    throw HandleException(e);
                }
            }
        }
        /// <summary>
        /// Add Selected Numbers and ReturnId
        /// </summary>
        /// <param name="selectedNumbers"></param>
        /// <returns></returns>
        public int? AddSelectedNumbersReturnId(SelectedNumbers selectedNumbers)
        {
            if (selectedNumbers == null) throw NewBLException(new StackTrace(true), "SelectedNumbers Sent empty");
            try
            {
                return cRMManagerEdit.AddSelectedNumbersReturnId(selectedNumbers);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Edit Package
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public RequestStatus EditPackage(Package package)
        {
            if (package == null) throw NewBLException(new StackTrace(true), "package Sent empty");
            try
            {
                return cRMManagerEdit.EditPackage(package);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Return Total Fixed Price
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public double ReturnTotalFixedPrice(Package package)
        {
            try
            {
                double TotalPrice = 0;
                if (package.FixedPrice != null)
                    TotalPrice = (double)package.FixedPrice;
                if (package.DiscountPercentage != null) TotalPrice += 9.9;
                return TotalPrice;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Get Package By Id
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public Package GetPackageById(int lineId)
        {
            try
            {
                return cRMManagerRead.GetPackageForLine(lineId);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Remove Line
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RequestStatus RemoveLine(int id)
        {
            try
            {
                return cRMManagerEdit.RemoveLine(id);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
        /// <summary>
        /// Get Selected Numbers By Id
        /// </summary>
        /// <param name="SelectedNumbersId"></param>
        /// <returns></returns>
        public SelectedNumbers GetSelectedNumbersById(int SelectedNumbersId)
        {
            try
            {
                return cRMManagerRead.GetSelectedNumbersById(SelectedNumbersId);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }
    }
}
