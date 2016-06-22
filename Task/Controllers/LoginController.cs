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
    public class LoginController : ApiController
    {
        public class Login
        {
            public string adminuseremail { get; set; }
            public string adminuserpassword { get; set; }
            public string clientid { get; set; }
            public string Msg { get; set; }
        }
        // GET api/login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/login
        public string Post([FromBody]Login value)
        {
            try
            {

                Hashtable hs = new Hashtable();
                hs.Add("@QType", "Login");
                hs.Add("@ID", value.adminuseremail);
                hs.Add("@Pass", value.adminuserpassword);
                DataTable dt = new DataTable();
                dt = BindData.BindGridviewTable("ProcLogin", hs);
                if(dt.Rows.Count>0)
                {
                    Login[] e = new Login[1];
                    e[0]=new Login();
                    e[0].clientid = dt.Rows[0]["clientid"].ToString();
                    e[0].Msg = "1";
                    return new JavaScriptSerializer().Serialize(e);
                }
                else
                {
                    Login[] e = new Login[1];
                    e[0] = new Login();
                    e[0].Msg = "0";
                    return new JavaScriptSerializer().Serialize(e);
                }
            }
            catch (Exception)
            {
                Login[] e = new Login[1];
                e[0] = new Login();
                e[0].Msg = "-1";
                return new JavaScriptSerializer().Serialize(e);
            }
        }

        // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/login/5
        public void Delete(int id)
        {
        }
    }
}
