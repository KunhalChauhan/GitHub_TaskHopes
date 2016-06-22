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
    public class TaskassigntoGroupsController : ApiController
    {
         public class GroupDetails
    {
        public string usergroupname { get; set; }
        public string usergroupid { get; set; }
        public string clientid { get; set; }
        public string FormId { get; set; }
        public string Msg { get; set; }
    }
        // GET api/taskassigntogroups
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/taskassigntogroups/5
        public string Get(string id)
        {
            Hashtable hs = new Hashtable();
            hs.Add("@QType", "SelectGroupbyClient");
            hs.Add("@clientid", id);
            DataTable dt = new DataTable();
            dt = BindData.BindGridviewTable("ProcTaskOperation", hs);
            if (dt.Rows.Count > 0)
            {
                GroupDetails[] e = new GroupDetails[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    e[i] = new GroupDetails();
                    e[i].usergroupname = dt.Rows[i]["usergroupname"].ToString();
                    e[i].usergroupid = dt.Rows[i]["usergroupid"].ToString();
                    e[i].clientid = dt.Rows[i]["clientid"].ToString();
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

        // POST api/taskassigntogroups
        public string Post([FromBody]GroupDetails value)
        {
            try
            {
                string[] groups = value.usergroupid.Split(',');
                for (int i = 0; i < groups.Length; i++)
                {
                    Hashtable hs = new Hashtable();
                    hs.Add("@QType", "FormGroupMapping");
                    hs.Add("@FormId", value.FormId);
                    hs.Add("@usergroupid", groups[i]);
                    int a = BindData.ExecuteParaNonQuery("ProcTaskOperation", hs);
                    
                }
                GroupDetails[] e = new GroupDetails[1];
                e[0] = new GroupDetails();
                e[0].Msg = "1";
                return new JavaScriptSerializer().Serialize(e);
            }
            catch(Exception)
            {
                GroupDetails[] e = new GroupDetails[1];
                e[0] = new GroupDetails();
                e[0].Msg = "0";
                return new JavaScriptSerializer().Serialize(e);
            }           
           
        }

        // PUT api/taskassigntogroups/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/taskassigntogroups/5
        public void Delete(int id)
        {
        }
    }
}
