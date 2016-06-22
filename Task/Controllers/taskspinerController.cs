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
    public class taskspinerController : ApiController
    {
        public class FormObject
        {
            public string objecttypeid { get; set; }
            public string objecttypename { get; set; }
            public string formid { get; set; }
            public string formName { get; set; }
            public string clientId { get; set; }
            public string date { get; set; }
            public string label { get; set; }
            public string formobjectid { get; set; }
            public string validationId { get; set; }
            public string spinnerName { get; set; }
            public string idcolumn { get; set; }
            public string citylist { get; set; }
            public string dependentspinner { get; set; }
            public string val { get; set; }
            public string Msg { get; set; }
        }
        // GET api/taskspiner
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/taskspiner/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/taskspiner
        public string Post([FromBody]FormObject value)
        {
            try
            {
                int cnt = 0;
                string[] spnr = value.spinnerName.Split(',');
                for (int i = 0; i < spnr.Length; i++)
                {
                    Hashtable hs = new Hashtable();
                    hs.Add("@QType", "insertDynaFirstSpinner");
                    hs.Add("@formid", value.formid);
                    hs.Add("@formobjectid", value.formobjectid);
                    hs.Add("@spinnerName", spnr[i]);
                    DataTable dt = new DataTable();
                    cnt = BindData.ExecuteParaNonQuery("ProcFormOperations", hs);
                }
                if (cnt > 0)
                {

                    FormObject[] e = new FormObject[1];
                    e[0] = new FormObject();
                    e[0].Msg = "1";
                    return new JavaScriptSerializer().Serialize(e);

                }
                else
                {
                    FormObject[] e = new FormObject[1];
                    e[0] = new FormObject();
                    e[0].Msg = "0";
                    return new JavaScriptSerializer().Serialize(e);
                }
            }
            catch (Exception)
            {
                FormObject[] e = new FormObject[1];
                e[0] = new FormObject();
                e[0].Msg = "-1";
                return new JavaScriptSerializer().Serialize(e);
            }
        }

        // PUT api/taskspiner/5
        public string Put([FromBody]FormObject value)
        {
            try
            {
                Hashtable hs = new Hashtable();
                hs.Add("@QType", "SelectDynSpinn");
                hs.Add("@formid", value.formid);
                hs.Add("@objecttypeid", value.objecttypeid);
                DataTable dt = new DataTable();
                dt = BindData.BindGridviewTable("ProcFormOperations", hs);
                if (dt.Rows.Count > 0)
                {
                    FormObject[] e = new FormObject[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        e[i] = new FormObject();
                        e[i].idcolumn = dt.Rows[i]["idcolumn"].ToString();
                        e[i].dependentspinner = dt.Rows[i]["dependentspinner"].ToString();
                        e[i].Msg = "1";
                    }
                    return new JavaScriptSerializer().Serialize(e);
                }
                else
                {
                    FormObject[] e = new FormObject[1];
                    e[0].Msg = "0";
                    return new JavaScriptSerializer().Serialize(e);

                }
            }
            catch (Exception)
            {
                FormObject[] e = new FormObject[1];
                e[0].Msg = "-1";
                return new JavaScriptSerializer().Serialize(e);
            }
        }

        // DELETE api/taskspiner/5
        public string Delete([FromBody]FormObject value)
        {
            try
            {
                Hashtable hs = new Hashtable();
                hs.Add("@QType", "insertDynaSpinner");
                hs.Add("@citylist", value.citylist);
                hs.Add("@idcolumn", value.idcolumn);
                DataTable dt = new DataTable();
                int a = BindData.ExecuteParaNonQuery("ProcFormOperations", hs);
                if (a > 0)
                {

                    FormObject[] e = new FormObject[1];
                    e[0] = new FormObject();
                    e[0].Msg = "1";
                    return new JavaScriptSerializer().Serialize(e);

                }
                else
                {
                    FormObject[] e = new FormObject[1];
                    e[0] = new FormObject();
                    e[0].Msg = "0";
                    return new JavaScriptSerializer().Serialize(e);
                }
            }
            catch (Exception)
            {
                FormObject[] e = new FormObject[1];
                e[0] = new FormObject();
                e[0].Msg = "-1";
                return new JavaScriptSerializer().Serialize(e);
            }
        }
    }
}
