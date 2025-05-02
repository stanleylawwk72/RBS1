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

namespace wv.UserData
{
    public class UserSqlDataProvider 
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
        public UserSqlDataProvider()
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

            _connectionString = ConfigurationManager.ConnectionStrings["UserAccountSqlServer"].ConnectionString.Replace("[UrID]", sUrID).Replace("[UrPwd]", sPwd).Replace("[DBSvrName]", sDBSvrName).Replace("[DBName]", sDBName);
        }
        #endregion

        #region Public Methods

        public DataSet checkLogin(bool bWindowsAuth, string sWindowsAuthUserID, string sUserID, string sPwd, string sClientMachineName, string sClientIPAddress, string iLockAccCounter)
        {

            string sproc = "spAppUserCheck";

            // return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, @"exec spCheckLogin", null);
            //return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, @"select * from eFormUsers", null);
            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc, new SqlParameter("bWindowsAuth", bWindowsAuth),
                                                                                      new SqlParameter("sUserID", sUserID),
                                                                                      new SqlParameter("sPwd", sPwd),
                                                                                      new SqlParameter("sWindowsAuthUserID", sWindowsAuthUserID),
                                                                                      new SqlParameter("sClientMachineName", sClientMachineName),
                                                                                      new SqlParameter("sClientIPAddress", sClientIPAddress),
                                                                                      new SqlParameter("iLockAccCounter", iLockAccCounter));
                /*
                    return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, sproc,
                                               new SqlParameter[]{
                                     new SqlParameter(){ ParameterName="@bWindowsAuth" ,SqlDbType=SqlDbType.BigInt,Value=bWindowsAuth},
                                     new SqlParameter(){ ParameterName="@sLoginName" ,SqlDbType=SqlDbType.NVarChar,Value=sLoginName},
                                     new SqlParameter(){ ParameterName="@sPwd" ,SqlDbType=SqlDbType.NVarChar,Value=sPwd},
                                     new SqlParameter(){ ParameterName="@sWindowsAuthUserID" ,SqlDbType=SqlDbType.NVarChar,Value=sWindowsAuthUserID}
                                    });
                */
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet requestResetPwd(string sAppUserSys_ID, string iExpiryMintue, string sRequestBy, string sMethod, string sEmailAddress)
        {
            string sproc = "spAppUserRequestResetPwd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                      new SqlParameter("sAppUserSys_ID", sAppUserSys_ID),
                                                                                      new SqlParameter("iExpiryMintue", iExpiryMintue),
                                                                                      new SqlParameter("sRequestBy", sRequestBy),
                                                                                      new SqlParameter("sEmailAddress", sEmailAddress),
                                                                                      new SqlParameter("@sMethod", @sMethod)
                                                                                      );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet resetPwd(string sAccessCode, string sNewPwd, string sClientMachineName, string sClientIPAddress)
        {
            string sproc = "spAppUserResetPwd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                      new SqlParameter("sAccessCode", sAccessCode),
                                                                                      new SqlParameter("sNewPwd", sNewPwd),
                                                                                      new SqlParameter("sClientMachineName", sClientMachineName),
                                                                                      new SqlParameter("sClientIPAddress", sClientIPAddress)
                                                                                      );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet changePwd(string sUserID, string sCurrentPwd, string sNewPwd)
        {
            string sproc = "spAppUserChangePwd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                      new SqlParameter("sUserID", sUserID),
                                                                                      new SqlParameter("sCurrentPwd", sCurrentPwd),
                                                                                      new SqlParameter("sNewPwd", sNewPwd)
                                                                                      );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getPermissions(string sUserID)
        {
            string sproc = "spUserPermissions";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                      new SqlParameter("sSys_ID", sUserID)
                                                                                      );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

//User
        public DataSet getUsers()
        {
            string sproc = "spAppUserList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getUsers(string sSys_ID)
        {
            string sproc = "spAppUserList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sSys_ID", sSys_ID)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getUsers(string sSys_ID, string sUserName, string sDeptCode, int iStatus)
        {
            string sproc = "spAppUserList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sSys_ID", sSys_ID),
                                                                                        new SqlParameter("sUserName", sUserName),
                                                                                        new SqlParameter("sDeptCode", sDeptCode),
                                                                                        new SqlParameter("iStatus", iStatus)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getUsers(string sSys_ID, string sUserName, string sDeptCode, int iStatus, string sRoleCode)
        {
            string sproc = "spAppUserList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sSys_ID", sSys_ID),
                                                                                        new SqlParameter("sUserName", sUserName),
                                                                                        new SqlParameter("sDeptCode", sDeptCode),
                                                                                        new SqlParameter("iStatus", iStatus),
                                                                                        new SqlParameter("sRoleCode", sRoleCode)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




        public DataSet updateUser(string sSys_ID, bool bStatus, string sEmailAddress, bool bChangePwd, string sPwd, string sEditBy, bool bLocked, string sWindowsAuthUserID, string sUserID, string sUserName, string sDeptCode, string sRoleCode, string dObsoleteDate)
        {
            string sproc = "spAppUserUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sSys_ID", sSys_ID),
                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                        new SqlParameter("bLocked", bLocked),
                                                                                        new SqlParameter("sUserName", sUserName),
                                                                                        new SqlParameter("sUserID", sUserID),
                                                                                        new SqlParameter("sWindowsAuthUserID", sWindowsAuthUserID),
                                                                                        new SqlParameter("sEmailAddress", sEmailAddress),
                                                                                        new SqlParameter("bChangePwd", bChangePwd),
                                                                                        new SqlParameter("sDeptCode", sDeptCode),
                                                                                        new SqlParameter("sRoleCode", sRoleCode),
                                                                                        new SqlParameter("sPwd", sPwd),
                                                                                        new SqlParameter("sEditBy", sEditBy),
                                                                                        new SqlParameter("dObsoleteDate", dObsoleteDate)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet addUser(bool bStatus,  string sEmailAddress, string sPwd, string sAddBy, string sWindowsAuthUserID, string sUserID, string sUserName, string sDeptCode, string sRoleCode)
        {
            string sproc = "spAppUserAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                        new SqlParameter("sUserName", sUserName),
                                                                                        new SqlParameter("sUserID", sUserID),
                                                                                        new SqlParameter("sWindowsAuthUserID", sWindowsAuthUserID),
                                                                                        new SqlParameter("sEmailAddress", sEmailAddress),
                                                                                        new SqlParameter("sPwd", sPwd),
                                                                                        new SqlParameter("sDeptCode", sDeptCode),
                                                                                        new SqlParameter("sRoleCode", sRoleCode),
                                                                                        new SqlParameter("sAddBy", sAddBy)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Dept
        public DataSet getDepartments(bool bShowObsoleted, bool bShowInActive)
        {
            string sproc = "spDepartmentList";

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

        public DataSet getDepartments(string sCode)
        {
            string sproc = "spDepartmentList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sCode", sCode)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public DataSet addDepartment(string sCode, string sDesc, string sAddBy, bool bStatus)
        {
            string sproc = "spDepartmentAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sCode", sCode),
                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                        new SqlParameter("sAddBy", sAddBy)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet updateDepartment(string sCode, string sDesc, string sEditBy, bool bStatus, string dObsoleteDate)
        {
            string sproc = "spDepartmentUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sCode", sCode),
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



        //Role
        public DataSet getRoles(bool bShowObsoleted, bool bShowInActive)
        {
            string sproc = "spRoleList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("bShowObsoleted", bShowObsoleted),
                                                                                        new SqlParameter("bShowInActive", bShowInActive));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getRoles(string sCode)
        {
            string sproc = "spRoleList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sCode", sCode)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet addRole(string sCode, string sDesc, string sAddBy, bool bStatus)
        {
            string sproc = "spRoleAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sCode", sCode),
                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                        new SqlParameter("sAddBy", sAddBy)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet updateRole(string sCode, string sDesc, string sEditBy, bool bStatus, string dObsoleteDate )
        {
            string sproc = "spRoleUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sCode", sCode),
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

        public DataSet getRoleDetails(string iID, string sRoleCode, bool bShowObsoleted, bool bShowInActive)
        {
            string sproc = "spRoleDetailList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iID", iID),
                                                                                        new SqlParameter("sRoleCode", sRoleCode),
                                                                                        new SqlParameter("bShowObsoleted", bShowObsoleted),
                                                                                        new SqlParameter("bShowInActive", bShowInActive));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet getRoleDetails(string iID, string sRoleCode)
        {
            string sproc = "spRoleDetailList";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                         new SqlParameter("sRoleCode", sRoleCode), 
                                                                                         new SqlParameter("iID", iID)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet updateRoleDetail(string iID, string sRoleCode, string sRoleDetailCode, string sDesc, bool bStatus, bool bPermission, string sEditBy, string dObsoleteDate)
        {
            string sproc = "spRoleDetailUpdate";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("iID", iID),
                                                                                        new SqlParameter("sRoleCode", sRoleCode),
                                                                                        new SqlParameter("sRoleDetailCode", sRoleDetailCode),
                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                        new SqlParameter("bPermission", bPermission),
                                                                                        new SqlParameter("sEditBy", sEditBy),
                                                                                        new SqlParameter("dObsoleteDate", dObsoleteDate)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet addRoleDetail(string sRoleCode, string sRoleDetailCode, string sDesc, bool bStatus, bool bPermission, string sAddBy)
        {
            string sproc = "spRoleDetailAdd";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sRoleCode", sRoleCode),
                                                                                        new SqlParameter("sRoleDetailCode", sRoleDetailCode),
                                                                                        new SqlParameter("sDesc", sDesc),
                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                        new SqlParameter("bPermission", bPermission),
                                                                                        new SqlParameter("sAddBy", sAddBy)
                                                                                        );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet synADUsers(string sUserID, string sUserName, string sDistinguishedName, string sAction, string sAddBy, string sRole, bool bStatus, string sUserList, string sADDN)
        {
            string sproc = "spAppUserAndDeptViaADLDAP";

            try
            {
                return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, sproc,
                                                                                        new SqlParameter("sUserID", sUserID),
                                                                                        new SqlParameter("sUserName", sUserName),
                                                                                        new SqlParameter("sDistinguishedName", sDistinguishedName),
                                                                                        new SqlParameter("sAction", sAction),
                                                                                        new SqlParameter("sAddBy", sAddBy),
                                                                                        new SqlParameter("sRole", sRole),
                                                                                        new SqlParameter("bStatus", bStatus),
                                                                                        new SqlParameter("sUserList", sUserList),
                                                                                        new SqlParameter("sADDN", sADDN)
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
