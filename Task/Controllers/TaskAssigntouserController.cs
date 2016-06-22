using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClsCs;
using System.Collections;
using System.Data;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace Task.Controllers
{
    public class TaskAssigntouserController : ApiController
    {
        public class User
        {
            public string userid { get; set; }
            public string FormId { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string userrole { get; set; }
            public string Checked { get; set; }
            public string Msg { get; set; }
        }
        // GET api/taskassigntouser
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/taskassigntouser/5
        public string Get(string id)
        {
            Hashtable hs = new Hashtable();
            hs.Add("@QType", "SelectUsersbyClient");
            hs.Add("@clientid", id);
            DataTable dt = new DataTable();
            dt = BindData.BindGridviewTable("ProcTaskOperation", hs);
            if (dt.Rows.Count > 0)
            {
                User[] e = new User[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    e[i] = new User();
                    e[i].userid = dt.Rows[i]["userid"].ToString();
                    e[i].username = dt.Rows[i]["username"].ToString();
                    e[i].email = dt.Rows[i]["email"].ToString();
                    e[i].phone = dt.Rows[i]["phone"].ToString();
                    e[i].userrole = dt.Rows[i]["userrole"].ToString();
                    e[i].Checked = dt.Rows[i]["Checked"].ToString();
                    e[i].Msg = "1";
                }
                return new JavaScriptSerializer().Serialize(e);

            }
            else
            {
                User[] e = new User[1];
                e[0] = new User();
                e[0].Msg = "1";
                return new JavaScriptSerializer().Serialize(e);
            }
        }

        // POST api/taskassigntouser
        public string Post([FromBody]User value)
        {
            try
            {
                string[] userid = value.userid.Split(',');
                for (int i = 0; i < userid.Length; i++)
                {
                    Hashtable hs = new Hashtable();
                    hs.Add("@QType", "FormUserMapping");
                    hs.Add("@FormId", value.FormId);
                    hs.Add("@userId", userid[i]);
                    int a = BindData.ExecuteParaNonQuery("ProcTaskOperation", hs);

                }
                User[] e = new User[1];
                e[0] = new User();
                e[0].Msg = "1";
                return new JavaScriptSerializer().Serialize(e);
            }
            catch (Exception)
            {
                User[] e = new User[1];
                e[0] = new User();
                e[0].Msg = "0";
                return new JavaScriptSerializer().Serialize(e);
            }

        }

        // PUT api/taskassigntouser/5
        public string Put([FromBody]User value)
        {
            try
            {
                int cnt = 0;
                DateTime dt = DateTime.Now;
                var date = dt.Day + "-" + dt.Month + "-" + dt.Year;
                string[] userid = value.userid.Split(',');
                for (int i = 0; i < userid.Length; i++)
                {
                    Hashtable hs = new Hashtable();
                    hs.Add("@QType", "FormSavedForm");
                    hs.Add("@FormId", value.FormId);
                    hs.Add("@userId", userid[i]);
                    hs.Add("@Date", date);
                    int a = BindData.ExecuteParaNonQuery("ProcTaskOperation", hs);

                }
                User[] e = new User[1];
                e[0] = new User();
                e[0].Msg = "1";
                return new JavaScriptSerializer().Serialize(e);
            }
            catch (Exception)
            {
                User[] e = new User[1];
                e[0] = new User();
                e[0].Msg = "0";
                return new JavaScriptSerializer().Serialize(e);
            }
        }

        // DELETE api/taskassigntouser/5
        public void Delete(int id)
        {
        }
    }
}
