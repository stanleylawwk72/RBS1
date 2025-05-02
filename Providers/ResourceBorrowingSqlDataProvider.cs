using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using wv.Encrypt;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;


namespace ResourceBorrowing.ResourceBorrowingData
{
    public class ResourceBorrowingSqlDataProvider
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
        public ResourceBorrowingSqlDataProvider()
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

        //Item
        public DataSet getItemLanguages(int iID, bool bShowObsoleted)
        {
            string sproc = "spItemLanguageList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("bShowObsoleted", bShowObsoleted));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet addItemLanguage(string sDesc, string sAddBy)
        {
            string sproc = "spItemLanguageAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("sDesc", sDesc),
                                                                                                        new SqlParameter("sAddBy", sAddBy));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet updateItemLanguage(int iID, string sDesc, string sEditBy, string dObsoleteDate)
        {
            string sproc = "spItemLanguageUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                                        new SqlParameter("sEditBy", sEditBy),
                                                                                                        new SqlParameter("dObsoleteDate", dObsoleteDate)
                    );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public DataSet getItemObsoleteReasons(int iID, bool bShowObsoleted)
        {
            string sproc = "spItemObsoleteReasonList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("bShowObsoleted", bShowObsoleted));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet addItemObsoleteReason(string sDesc, string sAddBy)
        {
            string sproc = "spItemObsoleteReasonAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("sDesc", sDesc),
                                                                                                        new SqlParameter("sAddBy", sAddBy));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet updateItemObsoleteReason(int iID, string sDesc, string sEditBy, string dObsoleteDate)
        {
            string sproc = "spItemObsoleteReasonUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                                        new SqlParameter("sEditBy", sEditBy),
                                                                                                        new SqlParameter("dObsoleteDate", dObsoleteDate)
                    );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public DataSet getItemTypes(int iID, bool bShowObsoleted, bool bShowInActive)
        {
            string sproc = "spItemTypeList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("bShowObsoleted", bShowObsoleted),
                                                                                                        new SqlParameter("bShowInActive", bShowInActive));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet addItemType(string sDesc, string sAddBy)
        {
            string sproc = "spItemTypeAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("sDesc", sDesc),
                                                                                                        new SqlParameter("sAddBy", sAddBy));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet updateItemType(int iID, string sDesc, bool bStatus, string sEditBy, string dObsoleteDate)
        {
            string sproc = "spItemTypeUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                                        new SqlParameter("sEditBy", sEditBy),
                                                                                                        new SqlParameter("dObsoleteDate", dObsoleteDate)
                    );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //20171122 Item Unit
        public DataSet getItemUnits(int iID, bool bShowObsoleted, bool bShowInActive)
        {
            string sproc = "spItemUnitList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("bShowObsoleted", bShowObsoleted),
                                                                                                        new SqlParameter("bShowInActive", bShowInActive));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet addItemUnit(string sDesc, string sAddBy)
        {
            string sproc = "spItemUnitAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("sDesc", sDesc),
                                                                                                        new SqlParameter("sAddBy", sAddBy));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet updateItemUnit(int iID, string sDesc, bool bStatus, string sEditBy, string dObsoleteDate)
        {
            string sproc = "spItemUnitUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                                        new SqlParameter("sEditBy", sEditBy),
                                                                                                        new SqlParameter("dObsoleteDate", dObsoleteDate)
                    );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public DataSet getItems(int iID, string sStatus, string sItemCode, string sDescription, string sTypeID, string sDeptCode, string sLangID, string sNeedApproval, string sGlobalUse, bool bShowObsoleted, bool bShowInActive, string sWindowsAuthUserID) //J20180605
        {
            // add by stanley
            string sproc = "spItemList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,  new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("sStatus", sStatus),
                                                                                                        new SqlParameter("sItemCode", sItemCode),
                                                                                                        //new SqlParameter("sItemCopiesID", sItemCopiesID),
                                                                                                        new SqlParameter("sDescription", sDescription),
                                                                                                        new SqlParameter("sTypeID", sTypeID),
                                                                                                        new SqlParameter("sDeptCode", sDeptCode),
                                                                                                        new SqlParameter("sLangID", sLangID),
                                                                                                        new SqlParameter("sNeedApproval", sNeedApproval),
                                                                                                        new SqlParameter("sGlobalUse", sGlobalUse),
                                                                                                        new SqlParameter("bShowObsoleted", bShowObsoleted),
                                                                                                        new SqlParameter("bShowInActive", bShowInActive),
                                                                                                        new SqlParameter("sWindowsAuthUserID", sWindowsAuthUserID)
                                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //Create Transaction - Item List
        public DataSet getAvailableItems(int iID, string sItemCode, string sDescription, string sTypeID, string sDeptCode, string sLangID, string sNeedApproval, string sGlobalUse, string dRequestDateFrom, string dRequestDateTo, int iNoOfLagDay, int iTranID, string sWindowsAuthUserID)  //J20180605
        {
            string sproc = "spItemListAvailable";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iID", iID),
                                                                                                        //new SqlParameter("sStatus", sStatus),
                                                                                                        new SqlParameter("sItemCode", sItemCode),
                                                                                                        //new SqlParameter("sItemCopiesID", sItemCopiesID),
                                                                                                        new SqlParameter("sDescription", sDescription),
                                                                                                        new SqlParameter("sTypeID", sTypeID),
                                                                                                        new SqlParameter("sDeptCode", sDeptCode),
                                                                                                        new SqlParameter("sLangID", sLangID),
                                                                                                        new SqlParameter("sNeedApproval", sNeedApproval),
                                                                                                        new SqlParameter("sGlobalUse", sGlobalUse),
                                                                                                        //new SqlParameter("bShowObsoleted", bShowObsoleted),
                                                                                                        //new SqlParameter("bShowInActive", bShowInActive),
                                                                                                        new SqlParameter("dRequestDateFrom", dRequestDateFrom),
                                                                                                        new SqlParameter("dRequestDateTo", dRequestDateTo),
                                                                                                        new SqlParameter("iNoOfLagDay", iNoOfLagDay),
                                                                                                        new SqlParameter("iTranID", iTranID),
                                                                                                        new SqlParameter("sWindowsAuthUserID", sWindowsAuthUserID)
                                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

  


        public DataSet addItem(string sItemCode, int iNoOfCopy, string sDesc,  int iTypeID, bool bStatus, string sDuration, int iLangID, int iQuantity, string sDeptCode, string sRemarks, bool bNeedApproval, bool bGlobalUse, string sAddBy, int iUnit)
        {
            string sproc = "spItemAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                                        new SqlParameter("sItemCode", sItemCode),
                                                                                                        new SqlParameter("iNoOfCopy", iNoOfCopy),
                                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                                        new SqlParameter("iTypeID", iTypeID),
                                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                                        new SqlParameter("sDuration", sDuration),
                                                                                                        new SqlParameter("iLangID", iLangID),
                                                                                                        new SqlParameter("iQuantity", iQuantity),
                                                                                                        new SqlParameter("sDeptCode", sDeptCode),
                                                                                                        new SqlParameter("sRemarks", sRemarks),
                                                                                                        new SqlParameter("bNeedApproval", bNeedApproval),
                                                                                                        new SqlParameter("bGlobalUse", bGlobalUse),
                                                                                                        new SqlParameter("sAddBy", sAddBy),
                                                                                                        new SqlParameter("iUnit", iUnit));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet updateItem(int iID, string sDesc, int iTypeID, bool bStatus, string sDuration, int iLangID, int iQuantity, string sDeptCode, string sRemarks, bool bNeedApproval, bool bGlobalUse, string sEditBy, string dObsoleteDate, int iObsoleteReasonID, int iUnit)
        {
            string sproc = "spItemUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,  
                                                                                                        new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                                        new SqlParameter("iTypeID", iTypeID),
                                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                                        new SqlParameter("sDuration", sDuration),
                                                                                                        new SqlParameter("iLangID", iLangID),
                                                                                                        new SqlParameter("iQuantity", iQuantity),
                                                                                                        new SqlParameter("sDeptCode", sDeptCode),
                                                                                                        new SqlParameter("sRemarks", sRemarks),
                                                                                                        new SqlParameter("bNeedApproval", bNeedApproval),
                                                                                                        new SqlParameter("bGlobalUse", bGlobalUse),
                                                                                                        new SqlParameter("dObsoleteDate", dObsoleteDate),
                                                                                                        new SqlParameter("iObsoleteReasonID", iObsoleteReasonID),
                                                                                                        new SqlParameter("sEditBy", sEditBy),
                                                                                                        new SqlParameter("iUnit", iUnit));

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //Transaction


/*
        public DataSet getTransaction_Temp(int iID, string sRefNo, string sStatus, string sOverdue, string sToBorrowDateFrom, string sToBorrowDateTo, string sToReturnDateFrom, string sToReturnDateTo, string sInternalUserDept, string sInternalUser, string sClientName, string sContactPersonName)
        {
            string sproc = "spTransactionList_Temp";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                                        
                                                                                                                                                                                            new SqlParameter("iID", iID),
                                                                                                                                                                                            new SqlParameter("sRefNo", sRefNo),
                                                                                                                                                                                            new SqlParameter("sStatus", sStatus),
                                                                                                                                                                                            new SqlParameter("sOverdue", sOverdue),
                                                                                                                                                                                            new SqlParameter("sToBorrowDateFrom", sToBorrowDateFrom),
                                                                                                                                                                                            new SqlParameter("sToBorrowDateTo", sToBorrowDateTo),
                                                                                                                                                                                            new SqlParameter("sToReturnDateFrom", sToReturnDateFrom),
                                                                                                                                                                                            new SqlParameter("sToReturnDateTo", sToReturnDateTo),
                                                                                                                                                                                            new SqlParameter("sInternalUserDept", sInternalUserDept),
                                                                                                                                                                                            new SqlParameter("sInternalUser", sInternalUser),
                                                                                                                                                                                            new SqlParameter("sClientName", sClientName),
                                                                                                                                                                                            new SqlParameter("sContactPersonName", sContactPersonName));
                                                                                                                                                                                          
                                                                                                        new SqlParameter("@iID", 0),
                                                                                                        new SqlParameter("@sRefNo", ""),
                                                                                                        new SqlParameter("@sStatus", ""),
                                                                                                        new SqlParameter("@sOverdue", ""),
                                                                                                        new SqlParameter("@sToBorrowDateFrom", ""),
                                                                                                        new SqlParameter("@sToBorrowDateTo", ""),
                                                                                                        new SqlParameter("@sToReturnDateFrom", ""),
                                                                                                        new SqlParameter("@sToReturnDateTo", ""),
                                                                                                        new SqlParameter("@sInternalUserDept", ""),
                                                                                                        new SqlParameter("@sInternalUser", ""),
                                                                                                        new SqlParameter("@sClientName", ""),
                                                                                                        new SqlParameter("@sContactPersonName", "")
                                                                                                        
                                                                                                       

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
*/
        public DataSet getTransaction(int iID, string sRefNo, string sStatus, string sOverdue, string sToBorrowDateFrom, string sToReturnDateFrom, string sInternalDept, string sInternalUser, string sClientName, string sContactPersonName, string sPartnerID, string sItemCode, string sDescription, string sWindowsAuthUserID)  //J20180605
        {
            string sproc = "spTransactionList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                                        
                                                                                                                                                                                            new SqlParameter("iID", iID),
                                                                                                                                                                                            new SqlParameter("sRefNo", sRefNo),
                                                                                                                                                                                            new SqlParameter("sStatus", sStatus),
                                                                                                                                                                                            new SqlParameter("sOverdue", sOverdue),
                                                                                                                                                                                            new SqlParameter("sToBorrowDate", sToBorrowDateFrom),
                                                                                                                                                                                            //new SqlParameter("sToBorrowDateTo", sToBorrowDateTo),
                                                                                                                                                                                            new SqlParameter("sToReturnDate", sToReturnDateFrom),
                                                                                                                                                                                            //new SqlParameter("sToReturnDateTo", sToReturnDateTo),
                                                                                                                                                                                            new SqlParameter("sInternalDept", sInternalDept),
                                                                                                                                                                                            new SqlParameter("sInternalUser", sInternalUser),
                                                                                                                                                                                            new SqlParameter("sClientName", sClientName),
                                                                                                                                                                                            new SqlParameter("sContactPersonName", sContactPersonName),
                                                                                                                                                                                            new SqlParameter("sPartnerID", sPartnerID),
                                                                                                                                                                                            new SqlParameter("sItemCode", sItemCode),
                                                                                                                                                                                            //new SqlParameter("sItemCopiesID", sItemCopiesID),
                                                                                                                                                                                            new SqlParameter("sDescription", sDescription),
                                                                                                                                                                                            // new SqlParameter("sPartnerID", sPartnerID),
                                                                                                                                                                                            //new SqlParameter("sWindowsAuthUserID", sWindowsAuthUserID)
                                                                                                                                                                                            new SqlParameter("sWindowsAuthUserID", sWindowsAuthUserID)
                                                                                                                                                                                            );

                /*
                new SqlParameter("iID", 0),
                new SqlParameter("sRefNo", ""),
                new SqlParameter("sStatus", ""),
                new SqlParameter("sOverdue", ""),
                new SqlParameter("sToBorrowDateFrom", ""),
                new SqlParameter("sToBorrowDateTo", ""),
                new SqlParameter("sToReturnDateFrom", ""),
                new SqlParameter("sToReturnDateTo", ""),
                new SqlParameter("sInternalDept", ""),
                new SqlParameter("sInternalUser", ""),
                new SqlParameter("sClientName", ""),
                new SqlParameter("sContactPersonName", ""));
                 */
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getTransactionStatuses()
        {
            string sproc = "spTransactionStatuses";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getTransactionDetails(int iID, int iTranID)
        {
            string sproc = "spTransactionDetailList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("iID", iID),
                                                                                                        new SqlParameter("iTransactionID", iTranID)
                    );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet addTransaction(int iID, int iContactPersonID, int iInternalUserID, string sAddUser, string sRemarks, string sTranAction)
        {
            string sproc = "spTransactionUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                                                                                                                            new SqlParameter("iID", iID),
                                                                                                                                                                                            //new SqlParameter("sRefNo", sRefNo),
                                                                                                                                                                                            new SqlParameter("iContactPersonID", iContactPersonID),
                                                                                                                                                                                            new SqlParameter("iInternalUserID", iInternalUserID),
                                                                                                                                                                                            new SqlParameter("sCurrentUser", sAddUser),
                                                                                                                                                                                            new SqlParameter("sRemarks", sRemarks),
                                                                                                                                                                                            new SqlParameter("sTranAction", sTranAction));


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        //public DataSet addTransactionDetail(int iTransactionID, int iItemID, int iAction, int iBorrowingQty, bool bNeedApproval, bool bApproved, string dToBorrowDate, string dToReturnDate, string sCurrentUser, string sTranAction) //J20180605_20190603
        public DataSet addTransactionDetail(int iTransactionID, int iItemID, int iAction, int iBorrowingQty, bool bNeedApproval, bool bApproved, string dToBorrowDate, string dToReturnDate, string sCurrentUser, string sTranAction, int iNoOfLagDays)
        {
            string sproc = "spTransactionDetailUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                                                                                                                            //new SqlParameter("iID", iID),
                                                                                                                                                                                            new SqlParameter("iTransactionID", iTransactionID),
                                                                                                                                                                                            new SqlParameter("iItemID", iItemID),
                                                                                                                                                                                            new SqlParameter("iAction", iAction),
                                                                                                                                                                                            new SqlParameter("iBorrowingQty", iBorrowingQty),
                                                                                                                                                                                            new SqlParameter("bNeedApproval", bNeedApproval),
                                                                                                                                                                                            new SqlParameter("bApproved", bApproved),
                                                                                                                                                                                            new SqlParameter("dToBorrowDate", dToBorrowDate),
                                                                                                                                                                                            new SqlParameter("dToReturnDate", dToReturnDate),
                                                                                                                                                                                            new SqlParameter("sCurrentUser", sCurrentUser),
                                                                                                                                                                                            new SqlParameter("sTranAction", sTranAction)//);
                                                                                                                                                                                            ,new SqlParameter("iNoOfLagDays", iNoOfLagDays));  //J20180605_20190603


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //public DataSet updateTransactionDetail(int iID, int iTransactionID, int iItemID, int iAction, int iBorrowingQty, bool bNeedApproval, bool bApproved, string dToBorrowDate, string dToReturnDate, string sCurrentUser, string sTranAction) //J20180605_20190603     
        public DataSet updateTransactionDetail(int iID, int iTransactionID, int iItemID, int iAction, int iBorrowingQty, bool bNeedApproval, bool bApproved, string dToBorrowDate, string dToReturnDate, string sCurrentUser, string sTranAction, int iNoOfLagDays)
        {
            string sproc = "spTransactionDetailUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                                                                                                                            new SqlParameter("iID", iID),
                                                                                                                                                                                            new SqlParameter("iTransactionID", iTransactionID),
                                                                                                                                                                                            new SqlParameter("iItemID", iItemID),
                                                                                                                                                                                            new SqlParameter("iAction", iAction),
                                                                                                                                                                                            new SqlParameter("iBorrowingQty", iBorrowingQty),
                                                                                                                                                                                            new SqlParameter("bNeedApproval", bNeedApproval),
                                                                                                                                                                                            new SqlParameter("bApproved", bApproved),
                                                                                                                                                                                            new SqlParameter("dToBorrowDate", dToBorrowDate),
                                                                                                                                                                                            new SqlParameter("dToReturnDate", dToReturnDate),
                                                                                                                                                                                            new SqlParameter("sCurrentUser", sCurrentUser),
                                                                                                                                                                                            new SqlParameter("sTranAction", sTranAction)//);
                                                                                                                                                                                            ,new SqlParameter("iNoOfLagDays", iNoOfLagDays));  //J20180605_20190603



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        //Client
        public DataSet getClientName(int iID)
        {
            string sproc = "spClientList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                                        new SqlParameter("iClientID", iID));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getContactPerson(int iClientID, int iID)
        {
            string sproc = "spContactPersonList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                                        new SqlParameter("iClientID", iClientID),
                                                                                                        new SqlParameter("iContactPersonID", iID)
                                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //User
        public DataSet getUsers(int iID, string sDeptCode)
        {
            string sproc = "spAppUserList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iID", iID),
                                                                                        new SqlParameter("sDeptCode", sDeptCode)
                                                                                        //,new SqlParameter("iStatus", 1)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public DataSet GetTxnByItemID(int iItemID, string sStatus)
        {
            string sproc = "spTransactionList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iItemID", iItemID),
                                                                                        new SqlParameter("sStatus", sStatus)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetClientType(bool bShowObsoleted, bool bShowInActive)
        {
            string sproc = "spClientTypeList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("bShowObsoleted", bShowObsoleted),
                                                                                                       new SqlParameter("bShowInActive", bShowInActive));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetClientType(string sID)
        {
            string sproc = "spClientTypeList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sID", sID)
                                                                                        );
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet AddClientType(string sDesc, bool bStatus, string sAddUser)
        {
            string sproc = "spClientTypeAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                        new SqlParameter("sAddUser", sAddUser)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet UpdateClientType(string sID, string sDesc, string sEditUser, bool bStatus, String dObsoleteDate)
        {
            string sproc = "spClientTypeUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sID", sID),
                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                        new SqlParameter("sEditUser", sEditUser),
                                                                                        new SqlParameter("dObsoleteDate", dObsoleteDate)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /*
        public DataSet GetClient(string sClientTypeID, string sClientName, string sContactPersonName, string sClientPhoneNo, string sContactPersonPhoneNo)
        {
            string sproc = "spClientList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sClientTypeID", sClientTypeID),
                                                                                        new SqlParameter("sClientName", sClientName),
                                                                                        new SqlParameter("sContactPersonName", sContactPersonName),
                                                                                        new SqlParameter("sClientPhoneNo", sClientPhoneNo),
                                                                                        new SqlParameter("sContactPersonPhoneNo", sContactPersonPhoneNo)
                                                                                        ); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        */

        public DataSet GetClient(string sClientTypeID, string sClientName, string sContactPersonName, string sClientPhoneNo, string sContactPersonPhoneNo, string sClientEmail, string sContactPersonEmail, string sClientID, string sWindowsAuthUserID)  //J20180605
        {
            string sproc = "spClientList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sClientTypeID", sClientTypeID),
                                                                                        new SqlParameter("sClientName", sClientName),
                                                                                        new SqlParameter("sContactPersonName", sContactPersonName),
                                                                                        new SqlParameter("sClientPhoneNo", sClientPhoneNo),
                                                                                        new SqlParameter("sContactPersonPhoneNo", sContactPersonPhoneNo),
                                                                                        new SqlParameter("sClientEmail", sClientEmail),
                                                                                        new SqlParameter("sContactPersonEmail", sContactPersonEmail),
                                                                                        new SqlParameter("iClientID", Convert.ToInt32(sClientID))
                                                                                        ,new SqlParameter("sWindowsAuthUserID", sWindowsAuthUserID)  //J20180605
                                                                                        ); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetClient(int iClientID)
        {
            string sproc = "spClientList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iClientID", iClientID)
                                                                                        ); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet GetClient(string sPartnerID, string sClientTypeID, string sClientName, string sClientPhoneNo, string sClientFax, string sWindowsAuthUserID)  //J20180605
        {
            string sproc = "spClientList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sPartnerID", sPartnerID),
                                                                                        new SqlParameter("sClientTypeID", sClientTypeID),
                                                                                        new SqlParameter("sClientName", sClientName),
                                                                                        new SqlParameter("sClientPhoneNo", sClientPhoneNo),
                                                                                        new SqlParameter("sClientFax", sClientFax)
                                                                                        ,new SqlParameter("sWindowsAuthUserID", sWindowsAuthUserID)  //J20180605
                                                                                        ); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public DataSet GetContactPerson(string sClientID, String sContactPersonID)
        {
            string sproc = "spContactPersonList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iClientID", Convert.ToInt32(sClientID)),
                                                                                        new SqlParameter("iContactPersonID", Convert.ToInt32(sContactPersonID))
                                                                                        ); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //public DataSet GetTxn(int iClientID)
        //{
        //    string sproc = "spTransactionList";

        //    try
        //    {
        //        return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
        //                                                                                new SqlParameter("iClientID", iClientID)
        //                                                                                ); ;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        //public DataSet GetTxn(string sClientID, string sRefNo, string sContactPersonName, string sStatus, string sOverdue,
        //                      string sAddDateFrom, string sAddDateTo, string sToBorrowDateFrom, string sToBorrowDateTo, string sToReturnDateFrom, string sToReturnDateTo)
        public DataSet GetTxn(string sClientID, string sRefNo, string sContactPersonName, string sStatus, string sOverdue,
                      string sAddDate, string sToBorrowDate, string sToReturnDate)

        {
            string sproc = "spTransactionList";
            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                             new SqlParameter("iClientID", Convert.ToInt32(sClientID)),
                                             new SqlParameter("sRefNo", sRefNo),
                                             new SqlParameter("sContactPersonName", sContactPersonName),
                                             new SqlParameter("sStatus", sStatus),
                                             new SqlParameter("sOverdue", sOverdue),
                                             new SqlParameter("sAddDate", sAddDate),
                                             new SqlParameter("sToBorrowDate", sToBorrowDate),
                                             new SqlParameter("sToReturnDate", sToReturnDate)
                                             //new SqlParameter("sAddDateFrom", sAddDateFrom),
                                             //new SqlParameter("sAddDateTo", sAddDateTo)
                                             //, new SqlParameter("sToBorrowDateFrom", sToBorrowDateFrom)
                                             //, new SqlParameter("sToBorrowDateTo", sToBorrowDateTo)
                                             //, new SqlParameter("sToReturnDateFrom", sToReturnDateFrom)
                                             //, new SqlParameter("sToReturnDateTo", sToReturnDateTo)

                                             ); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet UpdateContactPerson(int iContactPersonID, int iContactPersonCode, int iClientID, string sName, string sSex, string sDept, string sTitle, string sPhone, string sExt, string sEmailAddress, string sRemarks, string sEditUser)
        {
            string sproc = "spContactPersonUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iID", iContactPersonID),
                                                                                        new SqlParameter("iClientID", iClientID),
                                                                                        new SqlParameter("iContactPersonCode", iContactPersonCode),
                                                                                        new SqlParameter("sName", sName),
                                                                                        new SqlParameter("sSex", sSex),
                                                                                        new SqlParameter("sDept", sDept),
                                                                                        new SqlParameter("sTitle", sTitle),
                                                                                        new SqlParameter("sPhone", sPhone),
                                                                                        new SqlParameter("sExt", sExt),
                                                                                        new SqlParameter("sEmailAddress", sEmailAddress),
                                                                                        new SqlParameter("sRemarks", sRemarks),
                                                                                        new SqlParameter("sEditUser", sEditUser)
                                                                                        ); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet AddClient(string sPartnerID, string sName, int iTypeID, string sPhone, string sExt, string sFax, string sEmailAddress, string sAddress, string sRemarks, string sAddUser)
        {
            string sproc = "spClientAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sPartnerID", sPartnerID),
                                                                                        new SqlParameter("sName", sName),
                                                                                        new SqlParameter("iTypeID", iTypeID),
                                                                                        new SqlParameter("sPhone", sPhone),
                                                                                        new SqlParameter("sExt", sExt),
                                                                                        new SqlParameter("sFax", sFax),
                                                                                        new SqlParameter("sEmailAddress", sEmailAddress),
                                                                                        new SqlParameter("sAddress", sAddress),
                                                                                        new SqlParameter("sRemarks", sRemarks),
                                                                                        new SqlParameter("sAddUser", sAddUser)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet UpdateClient(int iID, string sPartnerID, string sName, int iClientTypeID, string sPhone, string sExt, string sFax, string sEmailAddress, string sAddress, string sRemarks, string sEditUser)
        {
            string sproc = "spClientUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sPartnerID", sPartnerID),
                                                                                        new SqlParameter("iID", iID),
                                                                                        new SqlParameter("sName", sName),
                                                                                        new SqlParameter("iClientTypeID", iClientTypeID),
                                                                                        new SqlParameter("sPhone", sPhone),
                                                                                        new SqlParameter("sExt", sExt),
                                                                                        new SqlParameter("sFax", sFax),
                                                                                        new SqlParameter("sEmailAddress", sEmailAddress),
                                                                                        new SqlParameter("sAddress", sAddress),
                                                                                        new SqlParameter("sRemarks", sRemarks),
                                                                                        new SqlParameter("sEditUser", sEditUser)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // need change
        public DataSet AddContactPerson(int iClientID, string sName, string sSex, string sDept, string sTitle, string sPhone, string sExt, string sEmailAddress, string sRemarks, string sAddUser)
        {
            string sproc = "spContactPersonAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iClientID", iClientID),
                                                                                        new SqlParameter("sName", sName),
                                                                                        new SqlParameter("sSex", sSex),
                                                                                        new SqlParameter("sDept", sDept),
                                                                                        new SqlParameter("sTitle", sTitle),
                                                                                        new SqlParameter("sPhone", sPhone),
                                                                                        new SqlParameter("sExt", sExt),
                                                                                        new SqlParameter("sEmailAddress", sEmailAddress),
                                                                                        new SqlParameter("sRemarks", sRemarks),
                                                                                        new SqlParameter("sAddUser", sAddUser)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //need modification
        public DataSet UpdateContactPerson(int iID, string sName, int iClientTypeID, string sPhone, string sExt, string sFax, string sEmailAddress, string sAddress, string sRemarks, string sEditUser)
        {
            string sproc = "spClientUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iID", iID),
                                                                                        new SqlParameter("sName", sName),
                                                                                        new SqlParameter("iClientTypeID", iClientTypeID),
                                                                                        new SqlParameter("sPhone", sPhone),
                                                                                        new SqlParameter("sExt", sExt),
                                                                                        new SqlParameter("sFax", sFax),
                                                                                        new SqlParameter("sEmailAddress", sEmailAddress),
                                                                                        new SqlParameter("sAddress", sAddress),
                                                                                        new SqlParameter("sRemarks", sRemarks),
                                                                                        new SqlParameter("sEditUser", sEditUser)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetActionStatus()
        {
            string sproc = "spTransactionStatuses";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //20190307
        public DataSet getAccessRight(string sWindowsAuthUserID)
        {
            string sproc = "spIVRBSAccessRight";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sWindowsAuthUserID", sWindowsAuthUserID));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




    }
}