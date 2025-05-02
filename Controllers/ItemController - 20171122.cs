using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ResourceBorrowing.ResourceBorrowingData;
using wv.Encrypt;
using System.Data;

namespace ResourceBorrowing.Items
{
    public class ItemController
    {

        private ResourceBorrowingSqlDataProvider _dp;
        private EncColltroller _en;

        public ItemController()
        {
            _dp = new ResourceBorrowingSqlDataProvider();
            _en = new EncColltroller();
        }

        public DataSet GetTxnByItemID(int iItemID, string sStatus)
        //(string sDeptCode, string sEPONumber, String sSendTo, string sStaffInCharge)
        {
            try
            {
                return _dp.GetTxnByItemID(iItemID, sStatus);
                //(sDeptCode, sEPONumber, sSendTo, sStaffInCharge);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getItemLanguages()
        {
            try
            {
                return _dp.getItemLanguages(0, true);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getItemLanguages(int iID)
        {
            try
            {
                return _dp.getItemLanguages(iID, true);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getItemLanguages(int iID, bool bShowObsoleted)
        {
            try
            {
                return _dp.getItemLanguages(iID, bShowObsoleted);
            }
            catch
            {
                return null;
            }
        }


        public DataSet addItemLanguage(string sDesc, string sAddBy)
        {
            try
            {
                return _dp.addItemLanguage(sDesc, sAddBy);
            }
            catch
            {
                return null;
            }
        }

        public DataSet updateItemLanguage(int iID, string sDesc, string sEditBy, string dObsoleteDate)
        {
            try
            {
                return _dp.updateItemLanguage( iID,  sDesc,  sEditBy,  dObsoleteDate);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getItemObsoleteReasons()
        {
            try
            {
                return _dp.getItemObsoleteReasons(0, true);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getItemObsoleteReasons(int iID)
        {
            try
            {
                return _dp.getItemObsoleteReasons(iID, true);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getItemObsoleteReasons(int iID, bool bShowObsoleted)
        {
            try
            {
                return _dp.getItemObsoleteReasons(iID, bShowObsoleted);
            }
            catch
            {
                return null;
            }
        }


        public DataSet addItemObsoleteReason(string sDesc, string sAddBy)
        {
            try
            {
                return _dp.addItemObsoleteReason(sDesc, sAddBy);
            }
            catch
            {
                return null;
            }
        }

        public DataSet updateItemObsoleteReason(int iID, string sDesc, string sEditBy, string dObsoleteDate)
        {
            try
            {
                return _dp.updateItemObsoleteReason(iID, sDesc, sEditBy, dObsoleteDate);
            }
            catch
            {
                return null;
            }
        }

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

        public DataSet getItemTypes(int iID)
        {
            try
            {
                return _dp.getItemTypes(iID, true, true);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getItemTypes(int iID, bool bShowObsoleted, bool bShowInActive)
        {
            try
            {
                return _dp.getItemTypes(iID, bShowObsoleted, bShowInActive);
            }
            catch
            {
                return null;
            }
        }


        public DataSet addItemType(string sDesc, string sAddBy)
        {
            try
            {
                return _dp.addItemType(sDesc, sAddBy);
            }
            catch
            {
                return null;
            }
        }

        public DataSet updateItemType(int iID, string sDesc, bool bStatus, string sEditBy, string dObsoleteDate)
        {
            try
            {
                return _dp.updateItemType(iID, sDesc, bStatus, sEditBy, dObsoleteDate);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getItems(int iID)
        {
            try
            {
                //return _dp.getItems(iID, null,null, null, null, null, null, null, null, null, true, true);
                return _dp.getItems(iID, null, null, null, null, null, null, null, null, true, true);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getItems(int iID, string sStatus, string sItemCode, string sDescription, string sTypeID, string sDeptCode, string sLangID, string sNeedApproval, string sGlobalUse)
        {
            try
            {
                //return _dp.getItems(iID, sStatus, sItemCode, sItemCopiesID, sDescription, sTypeID, sDeptCode, sLangID, sNeedApproval, sGlobalUse, true, true);
                return _dp.getItems(iID, sStatus, sItemCode, sDescription, sTypeID, sDeptCode, sLangID, sNeedApproval, sGlobalUse, true, true);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getItems(int iID, string sStatus, string sItemCode, string sDescription, string sTypeID, string sDeptCode, string sLangID, string sNeedApproval, string sGlobalUse, bool bShowObsoleted, bool bShowInActive)
        {
            try
            {
                //return _dp.getItems(iID, sStatus, sItemCode, sItemCopiesID, sDescription, sTypeID, sDeptCode, sLangID, sNeedApproval, sGlobalUse, bShowObsoleted, bShowInActive);
                return _dp.getItems(iID, sStatus, sItemCode, sDescription, sTypeID, sDeptCode, sLangID, sNeedApproval, sGlobalUse, bShowObsoleted, bShowInActive);
            }
            catch
            {
                return null;
            }
        }


        public DataSet addItem(string sItemCode, int iNoOfCopy, string sDesc, int iTypeID, bool bStatus, string sDuration, int iLangID, int iQuantity, string sDeptCode, string sRemarks, bool bNeedApproval, bool bGlobalUse, string sAddBy)
        {
            try
            {
                return _dp.addItem(sItemCode, iNoOfCopy, sDesc, iTypeID, bStatus, sDuration, iLangID, iQuantity, sDeptCode, sRemarks, bNeedApproval, bGlobalUse, sAddBy);

                //return _dp.addItem("sItemCode", 10, "sDesc", 1, true, "sDuration", 1, 10, "IT", "sRemarks", false, false, sAddBy);
            }
            catch
            {
                return null;
            }
        }

        public DataSet updateItem(int iID, string sDesc, int iTypeID, bool bStatus, string sDuration, int iLangID, int iQuantity, string sDeptCode, string sRemarks, bool bNeedApproval, bool bGlobalUse, string sEditBy, string dObsoleteDate, int iObsoleteReasonID)
        {
            try
            {
                return _dp.updateItem( iID,  sDesc,  iTypeID,  bStatus, sDuration,  iLangID,  iQuantity,  sDeptCode,  sRemarks,  bNeedApproval,  bGlobalUse,  sEditBy,  dObsoleteDate, iObsoleteReasonID);
            }
            catch
            {
                return null;
            }
        }

        //Create Transaction - Item List
        //public DataSet getAvailableItems(int iID, string sStatus, string sItemCode, string sDescription, string sTypeID, string sDeptCode, string sLangID, string sNeedApproval, string sGlobalUse, string dRequestDateFrom, string dRequestDateTo, int iNoOfLagDay)      
        public DataSet getAvailableItems(int iID, string sItemCode, string sDescription, string sTypeID, string sDeptCode, string sLangID, string sNeedApproval, string sGlobalUse, string dRequestDateFrom, string dRequestDateTo, int iNoOfLagDay)
        {
            try
            {
                return _dp.getAvailableItems(iID, sItemCode, sDescription, sTypeID, sDeptCode, sLangID, sNeedApproval, sGlobalUse, dRequestDateFrom, dRequestDateTo, iNoOfLagDay, 0);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getAvailableItems(int iID, string dRequestDateFrom, string dRequestDateTo, int iNoOfLagDay, int iTranID)
        {
            try
            {
                return _dp.getAvailableItems(iID, null, null, null, null, null, null, null, dRequestDateFrom, dRequestDateTo, iNoOfLagDay, iTranID);
            }
            catch
            {
                return null;
            }
        }

    }

}