using System;
using System.Configuration;
using System.Collections;
using Microsoft.Reporting.WebForms;
using System.IO;

using wv.Encrypt;

using System.Net;
using System.Security.Principal;

namespace wv.Reports
{
    public class ReportController : System.Web.UI.Page
    {

        EncColltroller _dn = new EncColltroller();

        public partial class ReportViewerCredentials : IReportServerCredentials
        {
            private string _userName;
            private string _password;
            private string _domain;

            public ReportViewerCredentials(string userName, string password, string domain)
            {
                _userName = userName;
                _password = password;
                _domain = domain;
            }

            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    return null;
                }
            }

            public ICredentials NetworkCredentials
            {
                get
                {
                    return new NetworkCredential(_userName, _password, _domain);
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie,
                    out string userName, out string password,
                    out string authority)
            {
                authCookie = null;
                userName = _userName;
                password = _password;
                authority = _domain;

                // Not using form credentials
                return false;
            }
        }

        private ArrayList setReportPatam(string[] sParaField, string[] sParaValue)
        {
            ArrayList arrLstDefaultParam = new ArrayList();
            //Para
            for (int i = 0; i < sParaField.Length; i++)
            {
                arrLstDefaultParam.Add(new ReportParameter(sParaField[i], sParaValue[i]));
            }
            //arrLstDefaultParam.Add(CreateReportParameter("IDs", "1"));
            // arrLstDefaultParam.Add(CreateReportParameter("ReportSubTitle", "Sub Title of Report"));
            return arrLstDefaultParam;
        }

        public void getSsrsReportViewer(ReportViewer rv, string[] sParaField, string[] sParaValue, string sReportPath, string sExportFileName,string sFormat)
        {
            _dn = new EncColltroller();


            string sSsrsReportServer = ""; //Database server name to which report connected
            string sSsrsUsrID = ""; //Database name to which report connected
            string sSsrsUsrPwd = ""; //Database user name to which report connected
            string sSsrsUsrDN = ""; //Database user password to which report connected

            sSsrsReportServer = _dn.Dc(ConfigurationManager.AppSettings["SsrsReportServer"], ConfigurationManager.AppSettings["EnKey"]); //"http://hkvn02:8002/WVHKReportServer";
            sSsrsUsrID = _dn.Dc(ConfigurationManager.AppSettings["SsrsUsrID"], ConfigurationManager.AppSettings["EnKey"]);
            sSsrsUsrPwd = _dn.Dc(ConfigurationManager.AppSettings["SsrsUsrPwd"], ConfigurationManager.AppSettings["EnKey"]);
            sSsrsUsrDN = _dn.Dc(ConfigurationManager.AppSettings["SsrsUsrDN"], ConfigurationManager.AppSettings["EnKey"]);

            ArrayList reportParam = new ArrayList();

            //ReportViewer rv = new (); //Microsoft.Reporting.WebForms.ReportViewer();
            rv.ServerReport.ReportServerCredentials = new ReportViewerCredentials(sSsrsUsrID, sSsrsUsrPwd, sSsrsUsrDN);
            string urlReportServer = sSsrsReportServer;

            rv.ProcessingMode = ProcessingMode.Remote; // ProcessingMode will be Either Remote or Local
            rv.ServerReport.ReportServerUrl = new Uri(urlReportServer); //Set the ReportServer Url
            rv.ServerReport.ReportPath = sReportPath ; //Passing the Report Path  

            reportParam = setReportPatam(sParaField, sParaValue);

            ReportParameter[] param = new ReportParameter[reportParam.Count];
            for (int k = 0; k < reportParam.Count; k++)
            {
                param[k] = (ReportParameter)reportParam[k];
            }
            
            rv.ServerReport.SetParameters(param); //Set Report Parameters

            if (sExportFileName != "")
            {
                string format = sFormat,
                       devInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

                //out parameters

                string mimeType = "",
                    encoding = "",
                    fileNameExtn = "xls";
                string[] streams = null;
                Microsoft.Reporting.WebForms.Warning[] warnings = null;

                byte[] bytes = null;

                try
                {
                    //render report, it will returns bite array



                    bytes = rv.ServerReport.Render(format,
                        devInfo, out mimeType, out encoding,
                        out fileNameExtn, out streams, out warnings);

                    

                    //create file with require legth
                    //FileStream stream = File.Create(Server.MapPath("~/EPOForm/PrintOut/ch.PDF"), result.Length);

                    ////FileStream stream = File.Create(Server.MapPath("~/" + ConfigurationManager.AppSettings["PrintOutPath"].ToString() + sExportFileName + "." + sFormat), result.Length);

                    //FileStream stream = new FileStream(@"c:\output.xls", FileMode.Create);
                    
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + sExportFileName + "." + fileNameExtn);
                    Response.BinaryWrite(bytes); // create the file
                    Response.Flush(); // send it to the client to download
                    //Response.End();

                    //write file with rendered result

                    ////stream.Write(result, 0, result.Length);
                    ///stream.Flush();

                    //close stream

                    ////stream.Close();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {

                }

            }

            rv.ServerReport.Refresh();
        
            //return rv;
        }


       
    }
}