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
    public class GroupDetails
    {
        public string formid { get; set; }
        public string formname { get; set; }
        public string TotalAssigned { get; set; }
        public string TodayComp { get; set; }
        public string Total { get; set; }
        public string Msg { get; set; }
    }
    public class ReportsanalyticsController : ApiController
    {
        // GET api/reportsanalytics
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/reportsanalytics/5
        public string Get(string id)
        {
            Hashtable hs = new Hashtable();
            hs.Add("@QType", "ReportTab");
            hs.Add("@clientid", id);
            DataTable dt = new DataTable();
            dt = BindData.BindGridviewTable("ProcReportanalytics", hs);
            if (dt.Rows.Count > 0)
            {
                GroupDetails[] e = new GroupDetails[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    e[i] = new GroupDetails();
                    e[i].formid = dt.Rows[i]["formid"].ToString();
                    e[i].formname = dt.Rows[i]["formname"].ToString();
                    e[i].TotalAssigned = dt.Rows[i]["TotalAssigned"].ToString();
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

        // POST api/reportsanalytics
        public void Post([FromBody]string value)
        {
        }

        // PUT api/reportsanalytics/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/reportsanalytics/5
        public void Delete(int id)
        {
        }
    }
}
