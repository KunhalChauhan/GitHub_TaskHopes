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
    public class usersubmissionController : ApiController
    {
        public class GroupDetails
        {
            public string userid { get; set; }
            public string username { get; set; }
            public string Assignedform { get; set; }
            public string WORKINGON { get; set; }
            public string TodayComp { get; set; }
            public string Total { get; set; }
            public string Msg { get; set; }
        }
        // GET api/usersubmission
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/usersubmission/5
        public string Get(int id)
        {
            Hashtable hs = new Hashtable();
            hs.Add("@QType", "usersubmissiondetails");
            hs.Add("@userid", id);
            DataTable dt = new DataTable();
            dt = BindData.BindGridviewTable("ProcReportanalytics", hs);
            if (dt.Rows.Count > 0)
            {
                GroupDetails[] e = new GroupDetails[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    e[i] = new GroupDetails();
                    e[i].userid = dt.Rows[i]["userid"].ToString();
                    e[i].username = dt.Rows[i]["username"].ToString();
                    e[i].Assignedform = dt.Rows[i]["Assignedform"].ToString();
                    e[i].WORKINGON = dt.Rows[i]["WORKINGON"].ToString();
                    e[i].Total = dt.Rows[i]["Total"].ToString();
                    e[i].TodayComp = dt.Rows[i]["TodayComp"].ToString();
                    e[i].Msg = "1";
                }
                return new JavaScriptSerializer().Serialize(e);

            }
            else
            {
                GroupDetails[] e = new GroupDetails[1];
                e[0] = new GroupDetails();
                e[0].Msg = "1";
                return new JavaScriptSerializer().Serialize(e);
            }
        }

        // POST api/usersubmission
        public void Post([FromBody]string value)
        {
        }

        // PUT api/usersubmission/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/usersubmission/5
        public void Delete(int id)
        {
        }
    }
}
