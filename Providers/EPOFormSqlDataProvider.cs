using System;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;

//using SystemDAO;
using System.Data.Common;

using wv.Encrypt;

namespace wv.EPOFormData
{
    public class EPOFormSqlDataProvider 
    {
        //public static readonly string connectionString = ConfigurationManager.ConnectionStrings["rsSqlServer"].ConnectionString.Trim();

        private EncColltroller _dn;

        #region Private Members
        private string _connectionString;
        #endregion

        #region Public Properties
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }
        #endregion

        #region Constructors
        public EPOFormSqlDataProvider()
        {
            //Get Connection string from web.config
            _dn = new EncColltroller();
            string sUrID;
            string sPwd;
            string sDBSvrName;
            string sDBName;

            sUrID = _dn.Dc(ConfigurationManager.AppSettings["UrID"], ConfigurationManager.AppSettings["EnKey"]);
            sPwd = _dn.Dc(ConfigurationManager.AppSettings["UrPwd"], ConfigurationManager.AppSettings["EnKey"]);
            sDBSvrName = _dn.Dc(ConfigurationManager.AppSettings["DBSvrName"], ConfigurationManager.AppSettings["EnKey"]);
            sDBName = _dn.Dc(ConfigurationManager.AppSettings["DBName"], ConfigurationManager.AppSettings["EnKey"]);

            _connectionString = ConfigurationManager.ConnectionStrings["rsSqlServer"].ConnectionString.Replace("[UrID]", sUrID).Replace("[UrPwd]", sPwd).Replace("[DBSvrName]", sDBSvrName).Replace("[DBName]", sDBName);
        }
        #endregion

        #region Public Methods

      
        //EPO
        public DataSet getPrintoutHashName(string sID, string sEPONo)
        {
            string sproc = "spEPOFormGetPrintoutHashName";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sID", sID),
                                                                                        new SqlParameter("sEPONo", sEPONo)
                                                                                        ); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getFormStatuses()
        {
            string sproc = "spEPOFormStatuses";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getCostCentreList(string sDeptCode)
        {
            string sproc = "spCostCentreList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sDeptCode", sDeptCode)
                                                                                        ); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /*
        public DataSet getCostCentre(string sDeptCode)
        {
            string sproc = "spCostCentre";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sDeptCode", sDeptCode)
                                                                                        ); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        */

        public DataSet getEPOForms(string iID, string sStatus, string sEPONumber, string sAddDateFrom, string sAddDateTo, string sEditDateFrom, string sEditDateTo, string sDeptCode, string sSendTo, string sStaffInCharge)
        {
            string sproc = "spEPOFormList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iID", iID),
                                                                                        new SqlParameter("sStatus", sStatus),
                                                                                        new SqlParameter("sEPONumber", sEPONumber),
                                                                                        new SqlParameter("sAddDateFrom", sAddDateFrom),
                                                                                        new SqlParameter("sAddDateTo", sAddDateTo),
                                                                                        new SqlParameter("sEditDateFrom", sEditDateFrom),
                                                                                        new SqlParameter("sEditDateTo", sEditDateTo),
                                                                                        new SqlParameter("sDeptCode", sDeptCode),
                                                                                        new SqlParameter("sSendTo", sSendTo),
                                                                                        new SqlParameter("sStaffInCharge", sStaffInCharge)
                                                                                        ); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getEPOFormItemDetails(string iEPOFormID)
        {
            string sproc = "spEPOFormItemDetailList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iEPOFormID", iEPOFormID));
            }
            catch (Exception ex)
            {

                throw ex;
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
            string sproc = "spEPOFormAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sSendTo", sSendTo),
                                                                                        new SqlParameter("sAttn", sAttn),
                                                                                        new SqlParameter("sRemarks", sRemarks),
                                                                                        new SqlParameter("sRemarks2", sRemarks2),
                                                                                        new SqlParameter("sRefNo", sRefNo),
                                                                                        new SqlParameter("sStaffInCharge", sStaffInCharge),
                                                                                        new SqlParameter("sCostCentre", sCostCentre),
                                                                                        new SqlParameter("iStatus", iStatus),
                                                                                        new SqlParameter("sSharedFolderPath", sSharedFolderPath),
                                                                                        new SqlParameter("sCurrencyCode", sCurrencyCode),
                                                                                        new SqlParameter("sAttachment1", sAttachment1),
                                                                                        new SqlParameter("sAttachment2", sAttachment2),
                                                                                        new SqlParameter("sAttachment3", sAttachment3),
                                                                                        new SqlParameter("sAttachment4", sAttachment4),
                                                                                        new SqlParameter("sAttachment5", sAttachment5),
                                                                                        new SqlParameter("sAttachment6", sAttachment6),
                                                                                        new SqlParameter("sAttachment7", sAttachment7),
                                                                                        new SqlParameter("sAttachment8", sAttachment8),
                                                                                        new SqlParameter("sAttachment9", sAttachment9),
                                                                                        new SqlParameter("sAttachment10", sAttachment10),
                                                                                        new SqlParameter("sAttachment1_Remarks", sAttachment1_Remarks),
                                                                                        new SqlParameter("sAttachment2_Remarks", sAttachment2_Remarks),
                                                                                        new SqlParameter("sAttachment3_Remarks", sAttachment3_Remarks),
                                                                                        new SqlParameter("sAttachment4_Remarks", sAttachment4_Remarks),
                                                                                        new SqlParameter("sAttachment5_Remarks", sAttachment5_Remarks),
                                                                                        new SqlParameter("sAttachment6_Remarks", sAttachment6_Remarks),
                                                                                        new SqlParameter("sAttachment7_Remarks", sAttachment7_Remarks),
                                                                                        new SqlParameter("sAttachment8_Remarks", sAttachment8_Remarks),
                                                                                        new SqlParameter("sAttachment9_Remarks", sAttachment9_Remarks),
                                                                                        new SqlParameter("sAttachment10_Remarks", sAttachment10_Remarks),
                                                                                         new SqlParameter("sAttachmentHashName", sAttachmentHashName),
                                                                                         new SqlParameter("sDeptInCharge1", sDeptInCharge1),
                                                                                        new SqlParameter("sDeptInCharge2", sDeptInCharge2),
                                                                                        new SqlParameter("sDeptInCharge3", sDeptInCharge3),
                                                                                        new SqlParameter("sDeptInCharge4", sDeptInCharge4),
                                                                                        new SqlParameter("sDeptInCharge5", sDeptInCharge5),
                                                                                        new SqlParameter("sDeptInCharge1_Contact", sDeptInCharge1_Contact),
                                                                                        new SqlParameter("sDeptInCharge2_Contact", sDeptInCharge2_Contact),
                                                                                        new SqlParameter("sDeptInCharge3_Contact", sDeptInCharge3_Contact),
                                                                                        new SqlParameter("sDeptInCharge4_Contact", sDeptInCharge4_Contact),
                                                                                        new SqlParameter("sDeptInCharge5_Contact", sDeptInCharge5_Contact),
                                                                                        new SqlParameter("fDiscount", fDiscount),
                                                                                        new SqlParameter("fTotalAmount", fTotalAmount),
                                                                                        new SqlParameter("sAddBy", sAddBy)
                                                                                                                                                                            
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet updateEPOForm(int iID, string sSendTo, string sAttn, string sRemarks, string sRemarks2, string sRefNo,
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
            float fDiscount, float fTotalAmount,int iUpdateDataEditVersion , string sEditBy)
        {
            string sproc = "spEPOFormUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iID", iID),
                                                                                        new SqlParameter("sSendTo", sSendTo),
                                                                                        new SqlParameter("sAttn", sAttn),
                                                                                        new SqlParameter("sRemarks", sRemarks),
                                                                                        new SqlParameter("sRemarks2", sRemarks2),
                                                                                        new SqlParameter("sRefNo", sRefNo),
                                                                                        new SqlParameter("sStaffInCharge", sStaffInCharge),
                                                                                        new SqlParameter("sCostCentre", sCostCentre),
                                                                                        new SqlParameter("iStatus", iStatus),
                                                                                        new SqlParameter("sSharedFolderPath", sSharedFolderPath),
                                                                                        new SqlParameter("sCurrencyCode", sCurrencyCode),
                                                                                        new SqlParameter("sAttachment1", sAttachment1),
                                                                                        new SqlParameter("sAttachment2", sAttachment2),
                                                                                        new SqlParameter("sAttachment3", sAttachment3),
                                                                                        new SqlParameter("sAttachment4", sAttachment4),
                                                                                        new SqlParameter("sAttachment5", sAttachment5),
                                                                                        new SqlParameter("sAttachment6", sAttachment6),
                                                                                        new SqlParameter("sAttachment7", sAttachment7),
                                                                                        new SqlParameter("sAttachment8", sAttachment8),
                                                                                        new SqlParameter("sAttachment9", sAttachment9),
                                                                                        new SqlParameter("sAttachment10", sAttachment10),
                                                                                        new SqlParameter("sAttachment1_Remarks", sAttachment1_Remarks),
                                                                                        new SqlParameter("sAttachment2_Remarks", sAttachment2_Remarks),
                                                                                        new SqlParameter("sAttachment3_Remarks", sAttachment3_Remarks),
                                                                                        new SqlParameter("sAttachment4_Remarks", sAttachment4_Remarks),
                                                                                        new SqlParameter("sAttachment5_Remarks", sAttachment5_Remarks),
                                                                                        new SqlParameter("sAttachment6_Remarks", sAttachment6_Remarks),
                                                                                        new SqlParameter("sAttachment7_Remarks", sAttachment7_Remarks),
                                                                                        new SqlParameter("sAttachment8_Remarks", sAttachment8_Remarks),
                                                                                        new SqlParameter("sAttachment9_Remarks", sAttachment9_Remarks),
                                                                                        new SqlParameter("sAttachment10_Remarks", sAttachment10_Remarks),
                                                                                        new SqlParameter("iUpdateAttachment1_LatestUploadFileDate", iUpdateAttachment1_LatestUploadFileDate),
                                                                                        new SqlParameter("iUpdateAttachment2_LatestUploadFileDate", iUpdateAttachment2_LatestUploadFileDate),
                                                                                        new SqlParameter("iUpdateAttachment3_LatestUploadFileDate", iUpdateAttachment3_LatestUploadFileDate),
                                                                                        new SqlParameter("iUpdateAttachment4_LatestUploadFileDate", iUpdateAttachment4_LatestUploadFileDate),
                                                                                        new SqlParameter("iUpdateAttachment5_LatestUploadFileDate", iUpdateAttachment5_LatestUploadFileDate),
                                                                                        new SqlParameter("iUpdateAttachment6_LatestUploadFileDate", iUpdateAttachment6_LatestUploadFileDate),
                                                                                        new SqlParameter("iUpdateAttachment7_LatestUploadFileDate", iUpdateAttachment7_LatestUploadFileDate),
                                                                                        new SqlParameter("iUpdateAttachment8_LatestUploadFileDate", iUpdateAttachment8_LatestUploadFileDate),
                                                                                        new SqlParameter("iUpdateAttachment9_LatestUploadFileDate", iUpdateAttachment9_LatestUploadFileDate),
                                                                                        new SqlParameter("iUpdateAttachment10_LatestUploadFileDate", iUpdateAttachment10_LatestUploadFileDate),
                                                                                        new SqlParameter("sAttachmentHashName", sAttachmentHashName),
                                                                                        new SqlParameter("sDeptInCharge1", sDeptInCharge1),
                                                                                        new SqlParameter("sDeptInCharge2", sDeptInCharge2),
                                                                                        new SqlParameter("sDeptInCharge3", sDeptInCharge3),
                                                                                        new SqlParameter("sDeptInCharge4", sDeptInCharge4),
                                                                                        new SqlParameter("sDeptInCharge5", sDeptInCharge5),
                                                                                        new SqlParameter("sDeptInCharge1_Contact", sDeptInCharge1_Contact),
                                                                                        new SqlParameter("sDeptInCharge2_Contact", sDeptInCharge2_Contact),
                                                                                        new SqlParameter("sDeptInCharge3_Contact", sDeptInCharge3_Contact),
                                                                                        new SqlParameter("sDeptInCharge4_Contact", sDeptInCharge4_Contact),
                                                                                        new SqlParameter("sDeptInCharge5_Contact", sDeptInCharge5_Contact),
                                                                                        new SqlParameter("fDiscount", fDiscount),
                                                                                        new SqlParameter("fTotalAmount", fTotalAmount),
                                                                                        new SqlParameter("iUpdateDataEditVersion", iUpdateDataEditVersion),
                                                                                        new SqlParameter("sEditBy", sEditBy)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet AddEPOFormItemDetail(string sEPONumber, int iItemNo, string sItemDescription, string sItemCurrencyCode, float fItemUnitPrice, string sItemQuantity, float fItemTotalAmount, string sAction)
        {
            string sproc = "spEPOFormItemDetailAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sEPONumber", sEPONumber),
                                                                                        new SqlParameter("iItemNo", iItemNo),
                                                                                        new SqlParameter("sItemDescription", sItemDescription),
                                                                                        new SqlParameter("sItemCurrencyCode", sItemCurrencyCode),
                                                                                        new SqlParameter("fItemUnitPrice", fItemUnitPrice),
                                                                                        new SqlParameter("sItemQuantity", sItemQuantity),
                                                                                        new SqlParameter("fItemTotalAmount", fItemTotalAmount),
                                                                                        new SqlParameter("sAction", sAction)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
    }
}
