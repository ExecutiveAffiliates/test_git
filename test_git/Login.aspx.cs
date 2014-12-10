using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Login : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected override void SavePageStateToPersistenceMedium(object state)
    {
        Session["viewstate"] = state;
    }
    protected override object LoadPageStateFromPersistenceMedium()
    {
        return Session["viewstate"];
    }
    public void btn_Login_OnClick(object sender, EventArgs e)
    {
    }
}
