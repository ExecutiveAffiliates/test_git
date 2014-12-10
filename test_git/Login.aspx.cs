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
    private SqlConnection SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EAConnectionString"].ConnectionString.ToString());
    private SqlConnection SqlConnection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["EAConnectionString"].ConnectionString.ToString());
    private SqlConnection SqlConnection3 = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnectionString"].ConnectionString.ToString());
    private SqlCommand SqlCommand;
    private SqlDataReader SqlData;
    private string spname;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsSecureConnection)
        {
            //Response.Redirect("Https://www.executiveaffiliates.net/login.aspx", true);
        }
        if (!IsPostBack)
        {
            Session.Abandon();
            Session.Clear();
            this.SetFocus(txt_Username);
        }
        pnl_login.Visible = true;
        pnl_sitedown.Visible = false;
        lbl_sitedown.Text = "";
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
        spname = "sp_LoginUser";
        SqlCommand = new SqlCommand(spname, SqlConnection);
        SqlConnection.Open();
        SqlCommand.CommandType = CommandType.StoredProcedure;
        SqlCommand.CommandTimeout = 60;

        SqlCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = txt_Username.Text.Trim();
        SqlCommand.Parameters.Add("@password", SqlDbType.VarChar).Value = txt_Password.Text.Trim();

        SqlData = SqlCommand.ExecuteReader();

        while (SqlData.Read())
        {
            string test = SqlData.GetString(0);
            if (SqlData.GetString(0).ToLower() == txt_Username.Text.ToString().ToLower())
            {
                Session["loginfail"] = 0;
                Session["username"] = SqlData.GetString(0).ToLower();
                Session["fullname"] = SqlData.GetString(1);
                Session["seclevel"] = SqlData.GetString(2);
                Session["msg"] = SqlData.GetInt32(3);
                Session["defaultloc"] = SqlData.GetString(4);
                Session["newprop"] = SqlData.GetString(4);
                Session["newpropname"] = SqlData.GetString(5);
                Session["locname"] = SqlData.GetString(5);
                Session["whoison"] = SqlData.GetString(6);
                Session["lockout"] = SqlData.GetString(7);
                Session["display"] = SqlData.GetString(8);
                Session["osasid"] = SqlData.GetString(9);
                Session["budactive"] = SqlData.GetBoolean(10);
                Session["budlevel"] = SqlData.GetInt32(11);

                Session["btntext"] = SqlData.GetString(12);
                Session["punchtype"] = SqlData.GetInt32(13);
                Session["reminderok"] = SqlData.GetBoolean(15);
                Session["forcepassword"] = SqlData.GetBoolean(16);
                Session["nomsgpop"] = SqlData.GetBoolean(17);
                Session["font"] = SqlData.GetString(18);
                Session["zdate"] = DateTime.Today.ToShortDateString();
                Session["zzdate"] = DateTime.Today.Month.ToString() + "/1/" + DateTime.Today.Year.ToString();
                Session["zzzdate"] = DateTime.Today.Year;
                Session["custom"] = SetCustom(SqlData);
                Session["refreshpage"] = false;

                if (Session["seclevel"].ToString() == "0")
                {
                    Session.Timeout = 360;
                }

                spname = "sp_AddLoggedUsers";
                SqlCommand SqlCommand2 = new SqlCommand(spname, SqlConnection2);
                SqlCommand2.CommandType = CommandType.StoredProcedure;

                SqlCommand2.Parameters.Add("@logUser", SqlDbType.VarChar).Value = SqlData.GetString(0).ToLower();
                SqlCommand2.Parameters.Add("@logPassword", SqlDbType.VarChar).Value = txt_Password.Text.ToString();
                SqlCommand2.Parameters.Add("@logUserId", SqlDbType.VarChar).Value = "0";
                SqlCommand2.Parameters.Add("@logSessionId", SqlDbType.VarChar).Value = Session.SessionID;
                SqlCommand2.Parameters.Add("@userid", SqlDbType.VarChar).Value = SqlData.GetString(1);

                SqlConnection2.Open();
                SqlCommand2.ExecuteNonQuery();
                SqlConnection2.Close();


                spname = "sp_AddLogIP";
                SqlCommand SqlCommand3 = new SqlCommand(spname, SqlConnection3);
                SqlCommand3.CommandType = CommandType.StoredProcedure;

                SqlCommand3.Parameters.Add("@proploc", SqlDbType.VarChar).Value = Session["defaultloc"].ToString();
                SqlCommand3.Parameters.Add("@username", SqlDbType.VarChar).Value = Session["username"].ToString();
                SqlCommand3.Parameters.Add("@ipaddress", SqlDbType.VarChar).Value = Request.ServerVariables["remote_addr"];
                SqlCommand3.Parameters.Add("@browseragent", SqlDbType.VarChar).Value = Request.ServerVariables["http_user_agent"];

                SqlConnection3.Open();
                SqlCommand3.ExecuteNonQuery();
                SqlConnection3.Close();

                Response.Redirect("http://testing.executiveaffiliates.net/home.aspx", false);
            }
        }

        if (Session["loginfail"] == null)
        {
            Session["loginfail"] = 1;
        }
        else
        {
            Session["loginfail"] = Int32.Parse(Session["loginfail"].ToString()) + 1;
        }
        lbl_LoginFail.Text = @"Your Username or Password is incorrect. <br \>You have " + (3 - Int32.Parse(Session["loginfail"].ToString())) + " attempts remaining";
        if (Int32.Parse(Session["loginfail"].ToString()) >= 3)
        {
            Response.Redirect("http://www.google.com/", true);
        }



        SqlData.Close();
        SqlConnection.Close();
        this.SetFocus(txt_Username); 
    }
    private DataTable SetCustom(SqlDataReader sqlData)
    {
        DataTable dt = new DataTable();
        int customcount = sqlData.FieldCount;
        int i = 19;

        while(i < customcount)
        {
            if (!SqlData.IsDBNull(i))
            {
                dt.Columns.Add(SqlData.GetString(i));
            }
            i = i + 2;
        }
        i = 19;

        DataRow dr = dt.NewRow();
        while (i < customcount)
        {
            if (!SqlData.IsDBNull(i))
            {
                dr[SqlData.GetString(i)] = SqlData.GetString(i + 1);
            }
            i = i + 2;
        }

        dt.Rows.Add(dr);

        return dt;
    }
}
