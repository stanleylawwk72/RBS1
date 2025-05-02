using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ResourceBorrowing.ResourceBorrowingData;
using wv.Encrypt;
using System.Data;

using System.Configuration;

namespace ResourceBorrowing.Transaction
{
    public class TransactionController
    {

        private ResourceBorrowingSqlDataProvider _dp;
        private EncColltroller _en;

        public DataSet getItemTypes()
        {
            try
            {
                return _dp.getItemTypes(0, true, true);
            }
            catch
            {
                return null;
            }
        }


        public TransactionController()
        {
            _dp = new ResourceBorrowingSqlDataProvider();
            _en = new EncColltroller();
        }

        public DataSet getTransaction() //J20180605
        {
            try
            {
                //return _dp.getTransaction(iID, sRefNo, sStatus, sOverdue, sToBorrowDateFrom, sToBorrowDateTo, sToReturnDateFrom, sToReturnDateTo, sInternalUserDept, sInternalUser, sClientName, sContactPersonName, sPartnerID);
                return _dp.getTransaction(0, "", "", "", "", "", "", "", "", "", "", "", "", "");
                //return _dp.getTransaction_Temp();

            }
            catch
            {
                return null;
            }
        }

        public DataSet getTransaction(int iID)  //J20180605
        {
            try
            {
                return _dp.getTransaction(iID, "", "", "", "", "", "", "", "", "", "", "", "","");
            }
            catch
            {
                return null;
            }
        }

        //public DataSet getTransaction(int iID, string sRefNo, string sStatus, string sOverdue, string sToBorrowDateFrom, string sToReturnDateFrom, string sInternalDept, string sInternalUser, string sClientName, string sContactPersonName, string sItemCode, string sDescription, string sPartnerID)
        //{
        //    try
        //    {
        //        //return _dp.getTransaction(iID, sRefNo, sStatus, sOverdue, sToBorrowDateFrom, sToBorrowDateTo, sToReturnDateFrom, sToReturnDateTo, sInternalDept, sInternalUser, sClientName, sContactPersonName);
        //        return _dp.getTransaction(iID, sRefNo, sStatus, sOverdue, sToBorrowDateFrom, sToReturnDateFrom, sInternalDept, sInternalUser, sClientName, sContactPersonName, sItemCode, sDescription, sPartnerID, "");
        //        //return _dp.getTransaction(0, "", "", "", "", "", "", "", "", "", "", "");

        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}


        //public DataSet getTransaction(int iID, string sRefNo, string sStatus, string sOverdue, string sToBorrowDateFrom, string sToReturnDateFrom, string sInternalDept, string sInternalUser, string sClientName, string sContactPersonName, string sItemCode, string sDescription, string sPartnerID,string sWindowsAuthUserID)   //J20180605
        public DataSet getTransaction(int iID, string sRefNo, string sStatus, string sOverdue, string sToBorrowDateFrom, string sToReturnDateFrom, string sInternalDept, string sInternalUser, string sClientName, string sContactPersonName, string sPartnerID, string sItemCode, string sDescription,string sWindowsAuthUserID)   //PE19003
        {
            try
            {
                //return _dp.getTransaction(iID, sRefNo, sStatus, sOverdue, sToBorrowDateFrom, sToBorrowDateTo, sToReturnDateFrom, sToReturnDateTo, sInternalDept, sInternalUser, sClientName, sContactPersonName);
                return _dp.getTransaction(iID, sRefNo, sStatus, sOverdue, sToBorrowDateFrom, sToReturnDateFrom, sInternalDept, sInternalUser, sClientName, sContactPersonName, sPartnerID, sItemCode, sDescription, sWindowsAuthUserID);
                //return _dp.getTransaction(0, "", "", "", "", "", "", "", "", "", "", "");

            }
            catch
            {
                return null;
            }
        }

        public DataSet getTransactionStatuses()
        {
            try
            {
                return _dp.getTransactionStatuses();
            }
            catch
            {
                return null;
            }
        }

        public DataSet addTransaction(int iID, int iContactPersonID, int iInternalUserID, string sAddUser, string sRemarks, string sTranAction)
        {
            try
            {
                return _dp.addTransaction(iID, iContactPersonID, iInternalUserID, sAddUser, sRemarks, sTranAction);
            }
            catch
            {
                return null;
            }
        }



        //public DataSet addTransactionDetail(int iTransactionID, int iItemID, int iAction, int iBorrowingQty, bool bNeedApproval, bool bApproved, string dToBorrowDate, string dToReturnDate, string sCurrentUser, string sTranAction)   //J20180605_20190603
        public DataSet addTransactionDetail(int iTransactionID, int iItemID, int iAction, int iBorrowingQty, bool bNeedApproval, bool bApproved, string dToBorrowDate, string dToReturnDate, string sCurrentUser, string sTranAction, int iNoOfLagDays)   
        {
            try
            {
                return _dp.addTransactionDetail(iTransactionID, iItemID, iAction, iBorrowingQty, bNeedApproval, bApproved, dToBorrowDate, dToReturnDate, sCurrentUser, sTranAction, iNoOfLagDays);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getTransactionDetails(int iID, int iTranID)
        {
            try
            {
                return _dp.getTransactionDetails(iID, iTranID);
            }
            catch
            {
                return null;
            }
        }

        //public DataSet updateTransactionDetail(int iID, int iTransactionID, int iItemID, int iAction, int iBorrowingQty, bool bNeedApproval, bool bApproved, string dToBorrowDate, string dToReturnDate, string sCurrentUser, string sTranAction) --J20180605_20190603
        public DataSet updateTransactionDetail(int iID, int iTransactionID, int iItemID, int iAction, int iBorrowingQty, bool bNeedApproval, bool bApproved, string dToBorrowDate, string dToReturnDate, string sCurrentUser, string sTranAction, int iNoOfLagDays)
        {
            try
            {
                //return _dp.updateTransactionDetail(iID, iTransactionID, iItemID, iAction, iBorrowingQty, bNeedApproval, bApproved, dToBorrowDate, dToReturnDate, sCurrentUser, sTranAction);  --J20180605_20190603
                return _dp.updateTransactionDetail(iID, iTransactionID, iItemID, iAction, iBorrowingQty, bNeedApproval, bApproved, dToBorrowDate, dToReturnDate, sCurrentUser, sTranAction, iNoOfLagDays);
            }
            catch
            {
                return null;
            }
        }

        //Client

        public DataSet getClientName(int iID)
        {
            try
            {
                return _dp.getClientName(iID);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getContactPerson(int iClientID)
        {
            try
            {
                return _dp.getContactPerson(iClientID, 0);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getContactPerson(int iClientID, int iID)
        {
            try
            {
                return _dp.getContactPerson(iClientID, iID);
            }
            catch
            {
                return null;
            }
        }

        //User

        public DataSet getUsers(int iID, string sDeptCode)
        {
            try
            {
                return _dp.getUsers(iID, sDeptCode);
            }
            catch
            {
                return null;
            }
        }

        //Print Confirmation Note


    }

}