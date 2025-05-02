using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using wv.Encrypt;
using Microsoft.ApplicationBlocks.Data;

//using ResourceBorrowing.ClientDataAccess;
using ResourceBorrowing.ResourceBorrowingData;

namespace ResourceBorrowing.Clients
//namespace wv.ClientData
{
    /// <summary>
    /// Summary description for eFormController
    /// </summary>
    public class ClientController : System.Web.UI.Page
    {
        private ResourceBorrowingSqlDataProvider _dp;
        private EncColltroller _en;

        public ClientController()
        {
            _dp = new ResourceBorrowingSqlDataProvider();
            _en = new EncColltroller();
        }
              
        /*
        public DataSet GetClient(string sClientTypeID, string sClientName, string sContactPersonName, string sClientPhoneNo, string sContactPersonPhoneNo)
        //(string sDeptCode, string sEPONumber, String sSendTo, string sStaffInCharge)
        {
            try
            {
                return _dp.GetClient(sClientTypeID, sClientName, sContactPersonName, sClientPhoneNo, sContactPersonPhoneNo);
                    //(sDeptCode, sEPONumber, sSendTo, sStaffInCharge);
            }
            catch
            {
                return null;
            }
        }
        */

        public DataSet GetClient(string sClientTypeID, string sClientName, string sContactPersonName, string sClientPhoneNo, string sContactPersonPhoneNo, string sClientEmail, string sContactPersonEmail, string sClientID, string sWindowsAuthUserID)   //J20180605
        //(string sDeptCode, string sEPONumber, String sSendTo, string sStaffInCharge)
        {
            try
            {
                return _dp.GetClient(sClientTypeID, sClientName, sContactPersonName, sClientPhoneNo, sContactPersonPhoneNo, sClientEmail, sContactPersonEmail, sClientID, sWindowsAuthUserID);  //J20180605
                //(sDeptCode, sEPONumber, sSendTo, sStaffInCharge);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetClient(string sPartnerID, string sClientTypeID, string sClientName, string sClientPhoneNo, string sClientFax, string sWindowsAuthUserID)   //J20180605
        //(string sDeptCode, string sEPONumber, String sSendTo, string sStaffInCharge)
        {
            try
            {
                return _dp.GetClient(sPartnerID, sClientTypeID, sClientName, sClientPhoneNo, sClientFax, sWindowsAuthUserID);   //J20180605
                //(sDeptCode, sEPONumber, sSendTo, sStaffInCharge);
            }
            catch
            {
                return null;
            }
        }

        //Client Type
        public DataSet GetClientType(){
            try
            {
                return _dp.GetClientType(true, true);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetClientType(bool bShowobsoleted, bool bShowInActive)
        {
            try
            {
                return _dp.GetClientType(bShowobsoleted, bShowInActive);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetClientType(string sID)
        {
            try
            {
                return _dp.GetClientType(sID);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetClient(int iClientID)
        //(string sDeptCode, string sEPONumber, String sSendTo, string sStaffInCharge)
        {
            try
            {
                return _dp.GetClient(iClientID);
                //(sDeptCode, sEPONumber, sSendTo, sStaffInCharge);
            }
            catch
            {
                return null;
            }
        }
        public DataSet GetContactPerson(String sClientID, String sContactPersonID)
        //(string sDeptCode, string sEPONumber, String sSendTo, string sStaffInCharge)
        {
            try
            {
                return _dp.GetContactPerson(sClientID, sContactPersonID);
                //(sDeptCode, sEPONumber, sSendTo, sStaffInCharge);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetTxn(string sClientID)
        {
            try
            {
                //return _dp.GetTxn(iClientID);
                return _dp.GetTxn(sClientID, null, null, null, null, null, null, null);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetTxn(string sClientID, string sRefNo, string sContactPersonName, string sStatus, string sOverdue,
                      string sAddDate, string sToBorrowDate, string sToReturnDate)
        {
            try
            {
                return _dp.GetTxn(sClientID, sRefNo, sContactPersonName, sStatus, sOverdue,
                                  sAddDate, sToBorrowDate, sToReturnDate);
            }
            catch
            {
                return null;
            }
        }
        //public DataSet GetTxn(string sClientID, string sRefNo, string sContactPersonName, string sStatus, string sOverdue,
        //                      string sAddDateFrom, string sAddDateTo, string sToBorrowDateFrom, string sToBorrowDateTo, string sToReturnDateFrom, string sToReturnDateTo)
        //{
        //    try
        //    {
        //        return _dp.GetTxn(sClientID,  sRefNo,  sContactPersonName,  sStatus,  sOverdue,
        //                          sAddDateFrom,  sAddDateTo, sToBorrowDateFrom, sToBorrowDateTo, sToReturnDateFrom, sToReturnDateTo);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public DataSet GetActionStatus()
        {
            try
            {
                return _dp.GetActionStatus();
            }
            catch
            {
                return null;
            }
        }

        public DataSet AddClientType(string sDesc, bool bStatus, string sAddBy )
        {
            try
            {
                return _dp.AddClientType(sDesc, bStatus, sAddBy);
            }
            catch
            {
                return null;
            }

        }

        public DataSet UpdateClientType(string sID, string sDesc, string sEditBy, bool bStatus, String dObsoleteDate)
        {
            try
            {
                return _dp.UpdateClientType(sID, sDesc, sEditBy, bStatus, dObsoleteDate);
            }
            catch
            {
                return null;
            }

        }
        
        public DataSet AddClient(string sPartnerID, string sName, int iClientTypeID, string sPhone, string sExt, string sFax, string sEmailAddress, string sAddress, string sRemarks, string sAddUser)
        {
            try
            {
                return _dp.AddClient(sPartnerID, sName, iClientTypeID, sPhone, sExt, sFax, sEmailAddress, sAddress, sRemarks, sAddUser);
            }
            catch
            {
                return null;
            }

        }

        public DataSet UpdateClient(int iID, string sPartnerID, string sName, int iClientTypeID, string sPhone, string sExt, string sFax, string sEmailAddress, string sAddress, string sRemarks, string sEditUser)
        {
            try
            {
                return _dp.UpdateClient(iID, sPartnerID, sName, iClientTypeID, sPhone, sExt, sFax, sEmailAddress, sAddress, sRemarks, sEditUser);
            }
            catch
            {
                return null;
            }

        }
               
        public DataSet AddContactPerson(int iClientID, string sName, string sSex, string sDept, string sTitle, string sPhone, string sExt, string sEmailAddress, string sRemarks, string sAddUser)
        {
            try
            {
                return _dp.AddContactPerson(iClientID, sName, sSex, sDept, sTitle, sPhone, sExt, sEmailAddress, sRemarks, sAddUser);
            }
            catch
            {
                return null;
            }

        }

        public DataSet UpdateContactPerson(int iContactPersonID, int iContactPersonCode, int iClientID, string sName, string sSex, string sDept, string sTitle, string sPhone, string sExt, string sEmailAddress, string sRemarks, string sEditUser)
        {
            try
            {
                return _dp.UpdateContactPerson(iContactPersonID, iContactPersonCode, iClientID, sName, sSex, sDept, sTitle, sPhone, sExt, sEmailAddress, sRemarks, sEditUser);
            }
            catch
            {
                return null;
            }

        }

        //20190307
        //public DataSet getAccessRight(string sWindowsAuthUserID)  
        public DataSet getAccessRight(string sWindowsAuthUserID)
        {
            try
            {
                return _dp.getAccessRight(sWindowsAuthUserID);
            }
            catch
            {
                return null;
            }
        }

    }
}