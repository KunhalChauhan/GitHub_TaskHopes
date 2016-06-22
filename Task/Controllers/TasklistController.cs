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
    public class TasklistController : ApiController
    {
        public class Task
        {
            public string formid { get; set; }
            public string formname { get; set; }
            public string dateofcreation { get; set; }
            public string Msg { get; set; }
        }
        // GET api/tasklist
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/tasklist/5
        public string Get(string id)
        {
            try
            {
                Hashtable hs = new Hashtable();
                hs.Add("@QType", "SelectByClientId");
                hs.Add("@clientid", id);
                DataTable dt = new DataTable();
                dt = BindData.BindGridviewTable("ProcTaskOperation", hs);
               if (dt.Rows.Count > 0)
                {
                    Task[] e = new Task[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        e[i] = new Task();
                        e[i].formid = dt.Rows[i]["formid"].ToString();
                        e[i].formname = dt.Rows[i]["formname"].ToString();
                        e[i].dateofcreation = dt.Rows[i]["dateofcreation"].ToString();
                        e[i].Msg = "1";
                    }
                    return new JavaScriptSerializer().Serialize(e);
                }
                else
                {
                    Task[] e = new Task[1];
                    e[0] = new Task();
                    e[0].Msg = "0";
                    return new JavaScriptSerializer().Serialize(e);

                }
            }
            catch (Exception)
            {
                Task[] e = new Task[1];
                e[0] = new Task();
                e[0].Msg = "-1";
                return new JavaScriptSerializer().Serialize(e);
            }
        }

        // POST api/tasklist
        public void Post([FromBody]string value)
        {
        }

        // PUT api/tasklist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/tasklist/5
        public void Delete(int id)
        {
        }
    }
}
