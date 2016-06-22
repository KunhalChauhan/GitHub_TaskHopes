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
    public class createtaskController : ApiController
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
            public string val { get; set; }
            public string Msg { get; set; }
        }
        // GET api/createtask
        public string Get()
        {
            try
            {
                Hashtable hs = new Hashtable();
                hs.Add("@QType", "GetObjects");
                DataTable dt = new DataTable();
                dt = BindData.BindGridviewTable("ProcFormOperations", hs);
                if (dt.Rows.Count > 0)
                {
                    FormObject[] e = new FormObject[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        e[i] = new FormObject();
                        e[i].objecttypeid = dt.Rows[i]["objecttypeid"].ToString();
                        e[i].objecttypename = dt.Rows[i]["objecttypename"].ToString();
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
            catch(Exception)
            {
                FormObject[] e = new FormObject[1];
                e[0].Msg = "-1";
                return new JavaScriptSerializer().Serialize(e);
            }
           
        }

        // GET api/createtask/5
        public string Get(int id)
        {
            return "";
        }

        // POST api/createtask
        public string Post([FromBody]FormObject value)
        {
            try
            {
                DateTime d = DateTime.Now;
                var day = "01";
                var month = "01";
                if (d.Day.ToString().Length == 1)
                    day = "0" + d.Day;
                if (d.Month.ToString().Length == 1)
                    month = "0" + d.Month;
                var date = day + "-" + month + "-" + d.Year;
                Hashtable hs = new Hashtable();
                hs.Add("@QType", "Insert");
                hs.Add("@formName", value.formName);
                hs.Add("@clientId", value.clientId);
                hs.Add("@Date", date);
                DataTable dt = new DataTable();
                int a = BindData.ExecuteParaNonQuery("ProcFormOperations", hs);
                if (a > 0)
                {
                    Hashtable hss=new Hashtable();
                    hss.Add("@QType","SelectMax");
                    dt=BindData.BindGridviewTable("ProcFormOperations", hss);
                    if(dt.Rows.Count>0)
                    {
                        FormObject[] e = new FormObject[1];
                        e[0] = new FormObject();
                        e[0].formid = dt.Rows[0]["formid"].ToString();
                        e[0].Msg = "1";
                        return new JavaScriptSerializer().Serialize(e);
                    }
                    else
                    {
                        FormObject[] e = new FormObject[1];
                        e[0] = new FormObject();
                        e[0].Msg = "-2";
                        return new JavaScriptSerializer().Serialize(e);
                    }
                   
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

        // PUT api/createtask/5
        public string Put([FromBody]FormObject value)
        {
            try
            {
                DateTime d = DateTime.Now;
                var day = "01";
                var month = "01";
                if (d.Day.ToString().Length == 1)
                    day = "0" + d.Day;
                if (d.Month.ToString().Length == 1)
                    month = "0" + d.Month;
                var date = day + "-" + month + "-" + d.Year;
                Hashtable hs = new Hashtable();
                hs.Add("@QType", "InsertObjects");
                hs.Add("@label", value.label);
                hs.Add("@formid", value.formid);
                hs.Add("@formobjectid", value.formobjectid);
                hs.Add("@objecttypeid", value.objecttypeid);
                hs.Add("@validationId", value.validationId);
                hs.Add("@date", date);
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

        // DELETE api/createtask/5
        public string Delete([FromBody]FormObject value)
        {
            try
            {
                Hashtable hs = new Hashtable();
                hs.Add("@QType", "InsertSpinner");
                hs.Add("@formid", value.formid);
                hs.Add("@formobjectid", value.formobjectid);
                hs.Add("@val", value.val);
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
