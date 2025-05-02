using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using wv.EPOFormData;
using wv.Encrypt;
using System.Configuration;

namespace wv.EPOForms
{
    /// <summary>
    /// Summary description for eFormController
    /// </summary>
    public class EPOFormController : System.Web.UI.Page
    {
        private EPOFormSqlDataProvider _dp;
        private EncColltroller _en;

        public EPOFormController()
        {
            _dp = new EPOFormSqlDataProvider();
            _en = new EncColltroller();
        }



        //Dept
        /*
        public DataSet getCostCentre(string sDeptCode)
        {
            try
            {
                return _dp.getCostCentre(sDeptCode);
            }
            catch
            {
                return null;
            }
        }
        */
        public DataSet getPrintoutHashName(string sID,string sEPONo)
        {
            try
            {
                return _dp.getPrintoutHashName(sID, sEPONo);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getFormStatuses()
        {
            try
            {
                return _dp.getFormStatuses();
            }
            catch
            {
                return null;
            }
        }

        public DataSet getCostCentreList(string sDeptCode)
        {
            try
            {
                return _dp.getCostCentreList(sDeptCode);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getEPOForms()
        {
            try
            {
                return _dp.getEPOForms("0", "0", "", "", "", "", "", "", "", "");
            }
            catch
            {
                return null;
            }
        }

        public DataSet getEPOForms(string iID)
        {
            try
            {
                return _dp.getEPOForms(iID, "0", "", "", "", "", "", "", "", "");
            }
            catch
            {
                return null;
            }
        }

        public DataSet getEPOForms(string iID, string sStatus, string sEPONumber, string sAddDateFrom, string sAddDateTo, string sEditDateFrom, string sEditDateTo, string sDeptCode, string sSendTo, string sStaffInCharge)
        {
            try
            {
                return _dp.getEPOForms(iID, sStatus, sEPONumber, sAddDateFrom, sAddDateTo, sEditDateFrom, sEditDateTo, sDeptCode, sSendTo, sStaffInCharge);
            }
            catch
            {
                return null;
            }
        }


        public DataSet getEPOFormItemDetails(string iEPOFormID)
        {
            try
            {
                return _dp.getEPOFormItemDetails(iEPOFormID);
            }
            catch
            {
                return null;
            }
        }


        public DataSet addEPOForm(string sSendTo, string sAttn, string sRemarks, string sRemarks2, string sRefNo,
            string sStaffInCharge, string sCostCentre, int iStatus, string sSharedFolderPath, string sCurrencyCode,
            string sAttachment1, string sAttachment2, string sAttachment3, string sAttachment4, string sAttachment5,
             string sAttachment6, string sAttachment7, string sAttachment8, string sAttachment9, string sAttachment10,
            string sAttachment1_Remarks, string sAttachment2_Remarks, string sAttachment3_Remarks, string sAttachment4_Remarks, string sAttachment5_Remarks,
            string sAttachment6_Remarks, string sAttachment7_Remarks, string sAttachment8_Remarks, string sAttachment9_Remarks, string sAttachment10_Remarks,
            string sAttachmentHashName,
            string sDeptInCharge1, string sDeptInCharge2, string sDeptInCharge3, string sDeptInCharge4, string sDeptInCharge5,
            string sDeptInCharge1_Contact, string sDeptInCharge2_Contact, string sDeptInCharge3_Contact, string sDeptInCharge4_Contact, string sDeptInCharge5_Contact,
            float fDiscount, float fTotalAmount, string sAddBy)
        {
            try
            {
                return _dp.addEPOForm(sSendTo,sAttn, sRemarks,sRemarks2, sRefNo, sStaffInCharge, sCostCentre, iStatus, sSharedFolderPath, sCurrencyCode,
                    sAttachment1, sAttachment2, sAttachment3, sAttachment4, sAttachment5,
                    sAttachment6, sAttachment7, sAttachment8, sAttachment9, sAttachment10,
                    sAttachment1_Remarks, sAttachment2_Remarks, sAttachment3_Remarks, sAttachment4_Remarks, sAttachment5_Remarks,
                    sAttachment6_Remarks, sAttachment7_Remarks, sAttachment8_Remarks, sAttachment9_Remarks, sAttachment10_Remarks,
                    sAttachmentHashName,
                    sDeptInCharge1, sDeptInCharge2, sDeptInCharge3, sDeptInCharge4, sDeptInCharge5,
                    sDeptInCharge1_Contact, sDeptInCharge2_Contact, sDeptInCharge3_Contact, sDeptInCharge4_Contact, sDeptInCharge5_Contact,
                    fDiscount, fTotalAmount, sAddBy);
            }
            catch
            {
                return null;
            }
        }

        public DataSet updateEPOForm(int iID, string sSendTo, string sAttn, string sRemarks,string sRemarks2, string sRefNo,
            string sStaffInCharge, string sCostCentre, int iStatus, string sSharedFolderPath, string sCurrencyCode,
            string sAttachment1, string sAttachment2, string sAttachment3, string sAttachment4, string sAttachment5,
             string sAttachment6, string sAttachment7, string sAttachment8, string sAttachment9, string sAttachment10,
            string sAttachment1_Remarks, string sAttachment2_Remarks, string sAttachment3_Remarks, string sAttachment4_Remarks, string sAttachment5_Remarks,
            string sAttachment6_Remarks, string sAttachment7_Remarks, string sAttachment8_Remarks, string sAttachment9_Remarks, string sAttachment10_Remarks,
            int iUpdateAttachment1_LatestUploadFileDate, int iUpdateAttachment2_LatestUploadFileDate, int iUpdateAttachment3_LatestUploadFileDate, int iUpdateAttachment4_LatestUploadFileDate, int iUpdateAttachment5_LatestUploadFileDate,
            int iUpdateAttachment6_LatestUploadFileDate, int iUpdateAttachment7_LatestUploadFileDate, int iUpdateAttachment8_LatestUploadFileDate, int iUpdateAttachment9_LatestUploadFileDate, int iUpdateAttachment10_LatestUploadFileDate,
            string sAttachmentHashName,
            string sDeptInCharge1, string sDeptInCharge2, string sDeptInCharge3, string sDeptInCharge4, string sDeptInCharge5,
            string sDeptInCharge1_Contact, string sDeptInCharge2_Contact, string sDeptInCharge3_Contact, string sDeptInCharge4_Contact, string sDeptInCharge5_Contact,
            float fDiscount, float fTotalAmount,int iUpdateDataEditVersion, string sEditBy)
        {
            try
            {
                return _dp.updateEPOForm(iID, sSendTo,sAttn, sRemarks,sRemarks2, sRefNo, sStaffInCharge, sCostCentre, iStatus, sSharedFolderPath, sCurrencyCode,
                    sAttachment1, sAttachment2, sAttachment3, sAttachment4, sAttachment5,
                    sAttachment6, sAttachment7, sAttachment8, sAttachment9, sAttachment10,
                    sAttachment1_Remarks, sAttachment2_Remarks, sAttachment3_Remarks, sAttachment4_Remarks, sAttachment5_Remarks,
                    sAttachment6_Remarks, sAttachment7_Remarks, sAttachment8_Remarks, sAttachment9_Remarks, sAttachment10_Remarks,
                    iUpdateAttachment1_LatestUploadFileDate, iUpdateAttachment2_LatestUploadFileDate, iUpdateAttachment3_LatestUploadFileDate, iUpdateAttachment4_LatestUploadFileDate, iUpdateAttachment5_LatestUploadFileDate,
                    iUpdateAttachment6_LatestUploadFileDate, iUpdateAttachment7_LatestUploadFileDate, iUpdateAttachment8_LatestUploadFileDate, iUpdateAttachment9_LatestUploadFileDate, iUpdateAttachment10_LatestUploadFileDate,
                    sAttachmentHashName, 
                    sDeptInCharge1, sDeptInCharge2, sDeptInCharge3, sDeptInCharge4, sDeptInCharge5,
                    sDeptInCharge1_Contact, sDeptInCharge2_Contact, sDeptInCharge3_Contact, sDeptInCharge4_Contact, sDeptInCharge5_Contact,
                    fDiscount, fTotalAmount, iUpdateDataEditVersion, sEditBy);
            }
            catch
            {
                return null;
            }
        }

        public DataSet AddEPOFormItemDetail(string sEPONumber, int iItemNo, string sItemDescription, string sItemCurrencyCode, float fItemUnitPrice, string sItemQuantity, float fItemTotalAmount, string sAction)
        {
            try
            {
                return _dp.AddEPOFormItemDetail(sEPONumber, iItemNo, sItemDescription, sItemCurrencyCode, fItemUnitPrice, sItemQuantity, fItemTotalAmount, sAction);
            }
            catch
            {
                return null;
            }
        }

    }
}