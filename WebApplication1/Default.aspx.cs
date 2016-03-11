using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Application.Lock();
            //Application["PagerequestCount"] = ((int)Application["PagerequestCount"]) + 1;
            //Application.UnLock();
            //sm1.RegisterAsyncPostBackControl(btn);
           
        }

        //protected void btn_Click(object sender, EventArgs e)
        //{
        //    int counter = 0;
        //    Application.Lock();
        //    Application["PagerequestCount"] = ((int)Application["PagerequestCount"]) + 1;
        //    Application.UnLock();
        //    up1.Update();
        //}
    }
}