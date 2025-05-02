using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using wv.Encrypt;

public partial class eKey : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //txtExistingKey.Text = ConfigurationSettings.AppSettings["EnKey"];    
    }

    protected void btnEnc_Click(object sender, EventArgs e)
    {
        txtOutput.Text = "";
        EncColltroller _dn;

        _dn = new EncColltroller();

        string output;



        output = _dn.Ec(txtInput.Text, txtNewKey.Text);

        txtOutput.Text = output;

    }

    protected void btnDnc_Click(object sender, EventArgs e)
    {
        txtOutput.Text = "";
        EncColltroller _dn;

        _dn = new EncColltroller();

        string output;

        output = _dn.Dc(txtInput.Text, txtNewKey.Text);

        txtOutput.Text = output;

    }
}