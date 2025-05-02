using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using wv.UserData;
using wv.Encrypt;
using System.Configuration;

namespace wv.Users
{
    /// <summary>
    /// Summary description for eFormController
    /// </summary>
    public class UserController : System.Web.UI.Page
    {
        private UserSqlDataProvider _dp;
        private EncColltroller _en;

        public UserController()
        {
            _dp = new UserSqlDataProvider();
            _en = new EncColltroller();
        }

        public void resetSession()
        {
            Session["UID"] = null;
            Session["UserName"] = null;
            Session["IsAuthenticated"] = null;
            Session["LoginDateTime"] = null;
            Session["EmailAddress"] = null;

            Session["Dept"] = null;
            Session["Role"] = null;
            Session["RoleCode"] = null;

            Session["NeedChgPwdImmediately"] = null;
            Session["ChangePwdWhenLogin"] = null;
            Session["LatestChangePwdDate"] = null;

            Session["LoginByWinAuthID"] = null;
            Session["IsAdmin"] = null;
            Session["DeptAdmin"] = null;
        }

        public DataSet requestResetPwd(string sAppUserSys_ID, string iExpiryMintue, string sRequestBy, string sMethod, string sEmailAddress)
        {
            try
            {
                return _dp.requestResetPwd(sAppUserSys_ID, iExpiryMintue, sRequestBy, sMethod, sEmailAddress);
            }
            catch
            {
                return null;
            }
        }

        public DataSet resetPwd(string sAccessCode, string sNewPwd, string sClientMachineName, string sClientIPAddressy,string sKey)
        {
            try
            {
                return _dp.resetPwd(sAccessCode, _en.Ec(sNewPwd.Trim(), sKey), sClientMachineName, sClientIPAddressy);
            }
            catch
            {
                return null;
            }
        }


        public DataSet checkLogin(bool bWindowsAuth, string sWindowsAuthUserID, string sUserID, string sPwd, string sClientMachineName, string sClientIPAddress, string iLockAccCounter, string sKey)
        {
            try
            {
                return _dp.checkLogin(bWindowsAuth, sWindowsAuthUserID, sUserID, _en.Ec(sPwd.Trim(), sKey), sClientMachineName, sClientIPAddress, iLockAccCounter);
            }
            catch
            {
                return null;
            }
        }

        public DataSet changePwd(string sUserID, string sCurrentPwd, string sNewPwd, string sKey)
        {
            try
            {
                return _dp.changePwd(sUserID, _en.Ec(sCurrentPwd.Trim(), sKey), _en.Ec(sNewPwd.Trim(), sKey));
            }
            catch
            {
                return null;
            }
        }

        public DataSet getPermissions(string sUserID)
        {
            try
            {
                return _dp.getPermissions(sUserID);
            }
            catch
            {
                return null;
            }
        }

        public DataSet updateUser(string sSys_ID, bool bStatus, string sEmailAddress, bool bChangePwd, string sPwd, string sEditBy, bool bLocked, string sWindowsAuthUserID, string sUserID, string sUserName, string sDeptCode, string sRoleCode,string dObsoleteDate,string sKey)
        {
            try
            {
                return _dp.updateUser(sSys_ID, bStatus, sEmailAddress, bChangePwd, _en.Ec(sPwd.Trim(), sKey), sEditBy, bLocked, sWindowsAuthUserID, sUserID, sUserName, sDeptCode, sRoleCode, dObsoleteDate);
            }
            catch
            {
                return null;
            }
        }

        public DataSet addUser(bool bStatus, string sEmailAddress, string sPwd, string sAddBy, string sWindowsAuthUserID, string sUserID, string sUserName, string sDeptCode, string sRoleCode, string sKey)
        {
            try
            {
                return _dp.addUser(bStatus, sEmailAddress, _en.Ec(sPwd.Trim(), sKey), sAddBy, sWindowsAuthUserID, sUserID, sUserName, sDeptCode, sRoleCode);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getUsers(string hfID, string sUserName, string sDeptCode, int iStatus, string sRole)
        {
            try
            {
                return _dp.getUsers(hfID, sUserName, sDeptCode, iStatus, sRole);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getUsers(string hfID, string sUserName, string sDeptCode, int iStatus)
        {
            try
            {
                return _dp.getUsers(hfID, sUserName, sDeptCode, iStatus);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getUsers(string hfID)
        {
            try
            {
                return _dp.getUsers(hfID);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getUsers()
        {
            try
            {
                return _dp.getUsers();
            }
            catch
            {
                return null;
            }
        }

      

        //Dept
        public DataSet getDepartments()
        {
            try
            {
                return _dp.getDepartments(true,true);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getDepartments(bool bShowObsoleted,bool bShowInActive)
        {
            try
            {
                return _dp.getDepartments(bShowObsoleted,bShowInActive);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getDepartments(string sCode)
        {
            try
            {
                return _dp.getDepartments(sCode);
            }
            catch
            {
                return null;
            }
        }

        public DataSet addDepartment(string sCode, string sDesc, string sAddBy, bool bStatus)
        {
            try
            {
                return _dp.addDepartment(sCode, sDesc, sAddBy, bStatus);
            }
            catch
            {
                return null;
            }
        }


        public DataSet updateDepartment(string sCode, string sDesc, string sEditBy, bool bStatus, string dObsoleteDate)
        {
            try
            {
                return _dp.updateDepartment(sCode, sDesc, sEditBy, bStatus, dObsoleteDate);
            }
            catch
            {
                return null;
            }
        }



        //Role

        public DataSet getRoles()
        {
            try
            {
                return _dp.getRoles(true,true);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getRoles(bool bShowObsoleted, bool bShowInActive)
        {
            try
            {
                return _dp.getRoles(bShowObsoleted, bShowInActive);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getRoles(string sCode)
        {
            try
            {
                return _dp.getRoles(sCode);
            }
            catch
            {
                return null;
            }
        }

        public DataSet addRole(string sCode, string sDesc, string sAddBy, bool bStatus)
        {
            try
            {
                return _dp.addRole(sCode, sDesc, sAddBy, bStatus);
            }
            catch
            {
                return null;
            }
        }


        public DataSet updateRole(string sCode, string sDesc, string sEditBy, bool bStatus, string dObsoleteDate)
        {
            try
            {
                return _dp.updateRole(sCode, sDesc, sEditBy, bStatus, dObsoleteDate);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getRoleDetails(string iID, string sRoleCode, bool bShowObsoleted, bool bShowInActive)
        {
            try
            {
                //Response.Write(iID);
                return _dp.getRoleDetails(iID, sRoleCode, bShowObsoleted, bShowInActive);
            }
            catch
            {
                return null;
            }
        }

        public DataSet updateRoleDetail(string iID, string sRoleCode, string sRoleDetailCode, string sDesc, bool bStatus, bool bPermission, string sEditBy, string dObsoleteDate)
        {
            try
            {
                return _dp.updateRoleDetail(iID, sRoleCode, sRoleDetailCode, sDesc, bStatus, bPermission, sEditBy, dObsoleteDate);
            }
            catch
            {
                return null;
            }
        }

        public DataSet addRoleDetail(string sRoleCode, string sRoleDetailCode, string sDesc, bool bStatus, bool bPermission, string sAddBy)
        {
            try
            {
                return _dp.addRoleDetail(sRoleCode, sRoleDetailCode, sDesc, bStatus, bPermission, sAddBy);
            }
            catch
            {
                return null;
            }
        }

        public DataSet synADUsers(string sUserID, string sUserName, string sDistinguishedName, string sAction, string sAddBy, string sRole, bool bStatus, string sUserList, string sADDN)
        {
            try
            {
                return _dp.synADUsers(sUserID, sUserName, sDistinguishedName, sAction, sAddBy, sRole, bStatus, sUserList, sADDN);
            }
            catch
            {
                return null;
            }
        }
        /*
        public DataSet getRoleDetails()
        {
            try
            {
                return _dp.getRoleDetails(string.Empty, string.Empty, true, true);
            }
            catch
            {
                return null;
            }
        }
    */
    }
}