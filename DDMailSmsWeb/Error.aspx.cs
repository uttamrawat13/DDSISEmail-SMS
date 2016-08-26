using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDMailSmsWeb
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["globalERROR"] != null)
            {
                RlblTempResult.Text = Convert.ToString(Session["globalERROR"]);
            }
        }
    }
}