using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CUS.ICS.HoldsInfo
{
    public partial class Holds_View : Jenzabar.Portal.Framework.Web.UI.PortletViewBase
    {
        protected String strUserID = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UserID"] != null)
                strUserID = HttpContext.Current.Session["UserID"].ToString();
            else
            {
                ParentPortlet.ShowFeedbackGlobalized(Jenzabar.Portal.Framework.Web.UI.FeedbackType.Error, "MSG_NO_HOST_ID");
            }
          
            //**************************************************
            // if the logged in user has an ID, check for Holds
            //**************************************************
            if (strUserID != String.Empty)
            {
                /*REALLY-REALLY-UGLY, but needed in case a HOLD_DESC was not found in CUS_DeptPhonesEmail*/
                System.Data.SqlClient.SqlCommand sqlcmdSelectHolds = new System.Data.SqlClient.SqlCommand(
                    "SELECT HOLD_DESC, Building, Phone FROM STUDENT_MASTER, HOLDS_DEF, CUS_DeptPhonesEmail"
                    + " WHERE ID_NUM LIKE " + strUserID
                    + " AND HOLD_CDE IN (HOLD_1_CDE,HOLD_2_CDE,HOLD_3_CDE,HOLD_4_CDE,HOLD_5_CDE,HOLD_6_CDE)"
                    + " AND DEPT = HOLD_CDE"
                    + " UNION"
                    + " SELECT HOLD_DESC, ' ' as Building, ' ' as Phone FROM STUDENT_MASTER, HOLDS_DEF"
                    + " WHERE ID_NUM LIKE " + strUserID
                    + " AND HOLD_CDE IN (HOLD_1_CDE,HOLD_2_CDE,HOLD_3_CDE,HOLD_4_CDE,HOLD_5_CDE,HOLD_6_CDE)"
                    + " AND HOLD_CDE not IN (SELECT HOLD_CDE FROM STUDENT_MASTER, HOLDS_DEF, CUS_DeptPhonesEmail"
                    + " WHERE ID_NUM LIKE " + strUserID
                    + " AND HOLD_CDE IN (HOLD_1_CDE,HOLD_2_CDE,HOLD_3_CDE,HOLD_4_CDE,HOLD_5_CDE,HOLD_6_CDE)"
                    + " AND DEPT = HOLD_CDE)", 
                    new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["JenzabarConnectionString"].ConnectionString));

                try
                {
                    sqlcmdSelectHolds.Connection.Open();
                    sqlcmdSelectHolds.StatementCompleted += new StatementCompletedEventHandler(SqlDataSourceHoldsStatusEventHandler);
                    dataListHolds.DataSource = sqlcmdSelectHolds.ExecuteReader();
                    dataListHolds.DataBind();
                }
                catch (Exception critical)
                {
                    lblError.ErrorMessage = "Database Error: <BR />" + critical.GetBaseException().Message;
                    lblError.Visible = true;
                }
                finally
                {
                    if (sqlcmdSelectHolds.Connection != null && sqlcmdSelectHolds.Connection.State == ConnectionState.Open)
                    {
                        sqlcmdSelectHolds.Connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SqlDataSourceHoldsStatusEventHandler(Object source, StatementCompletedEventArgs e)
        {
            if (e.RecordCount == 0)
            {
                lblMsg.Text = "<b>" + Jenzabar.Portal.Framework.PortalUser.Current.NameDetails.DisplayName + "</b>, you have no holds!";
            }
            else
            {
                lblMsg.Text = "<b>" + Jenzabar.Portal.Framework.PortalUser.Current.NameDetails.DisplayName + "</b>, you have holds in the following offices that will prevent you from registering for your classes:";
            }
        }

    }
}