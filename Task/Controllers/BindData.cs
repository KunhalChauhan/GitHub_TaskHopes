using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Configuration;
using System.Data.OleDb;

using System.Data.Common;

namespace ClsCs
{
    # region class To Connect the database
    public static class DBConnccection
    {

        private static string strconnect = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;


        public static SqlConnection GetConnection()
        {

            SqlConnection oConnection = new SqlConnection(strconnect);
            return oConnection;
        }



    }
    #endregion
    #region Bind The Gridview
    public static class BindData
    {




        public static DataTable BindGridviewQry(string pass)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlDataAdapter ada = new SqlDataAdapter(pass, strcon);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {

                throw new Exception("Error in Binding data" + ex.Message);

            }

        }
        public static void BindGridview(GridView gd, string pass)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlDataAdapter ada = new SqlDataAdapter(pass, strcon);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                gd.DataSource = dt;
                gd.DataBind();
                // return dt;
            }

            catch (Exception ex)
            {



                throw new Exception("Error in Binding data" + ex.Message);

            }

        }
        public static DataSet BindGridview(string storeproc)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlCommand cmd = new SqlCommand(storeproc, strcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                //  DataTable dt = new DataTable();
                // ada.SelectCommand;
                ada.Fill(ds);
                return ds;
            }

            catch (Exception ex)
            {

                throw new Exception("Error in Binding data" + ex.Message);

            }

        }
        public static DataTable BindGridviewTable(string storeproc)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlCommand cmd = new SqlCommand(storeproc, strcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {

                throw new Exception("Error in Binding data" + ex.Message);

            }

        }
        public static DataTable BindGridviewTable(string storeproc, Hashtable hs)
        {
            SqlConnection strcon = DBConnccection.GetConnection();
            try
            {
                strcon.Open();
                
                SqlCommand cmd = new SqlCommand(storeproc, strcon);
                cmd.CommandType = CommandType.StoredProcedure;
               
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                foreach (string parameter in hs.Keys)
                {
                    cmd.Parameters.Add(parameter, hs[parameter]);
                }
                DataTable dt = new DataTable();
                ada.Fill(dt);
                strcon.Close();
                return dt;
            }

            catch (Exception ex)
            {

                throw new Exception("Error in Binding data" + ex.Message);

            }
            finally
            {
                strcon.Close();

            }

        }
        public static DataTable BindDataGrid(DataGrid gd, string pass)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlDataAdapter ada = new SqlDataAdapter(pass, strcon);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                gd.DataSource = dt;
                gd.DataBind();
                return dt;
            }

            catch (Exception ex)
            {

                throw new Exception("Error in Binding data" + ex.Message);

            }

        }
        //METHOD TO EXECUTE PARAMETERIZED INSERT AND UPDATE QUERY
        public static void ExecuteParameterizedNonQuery(string SQLQuery, Hashtable hs)
        {
            SqlConnection strcon = DBConnccection.GetConnection();
            SqlCommand cmd = strcon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = SQLQuery;
            foreach (string parameter in hs.Keys)
            {
                //cmd.Parameters.AddWithValue(parameter, hs[parameter]);
                cmd.Parameters.Add(parameter, hs[parameter]);
            }
            strcon.Open();

            cmd.ExecuteNonQuery();

            strcon.Close();
        }
        public static SqlDataReader Bind_label_Text(string query)
        {
            SqlDataReader rdr = null;
            SqlConnection strcon = DBConnccection.GetConnection();
            SqlCommand cmd = new SqlCommand(query, strcon);

            try
            {

                cmd.Connection.Open();
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                rdr.Read();
                return rdr;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw;
            }
            finally
            {
            }
        }
        public static DataTable ExecuteQuery(string select)
        {

            DataTable dt = new DataTable();

            SqlDataAdapter adapter = null;
            SqlConnection conn = null;

            try
            {
                conn = DBConnccection.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //if ((conn != null) && (!conn.Database.Equals(Database)))
                //{
                //    conn.ChangeDatabase(Database);
                //}



                adapter = new SqlDataAdapter(select, conn);
                //adapter.SelectCommand.Transaction = tx;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                throw ex;

            }
            finally
            {

                if (conn.State == ConnectionState.Open) { conn.Close(); }
            }
            return dt;
        }
        public static int ExecuteQueryProc(string storeproc, Hashtable hs)
        {

            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                if (strcon.State == ConnectionState.Closed)
                {
                    strcon.Open();
                }
                SqlCommand cmd = new SqlCommand(storeproc, strcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                foreach (string parameter in hs.Keys)
                {
                    cmd.Parameters.Add(parameter, hs[parameter]);
                }
              
                int Value=cmd.ExecuteNonQuery();
              
                if (strcon.State == ConnectionState.Open)
                {
                    strcon.Close();
                }
                return Value;
            }

            catch (Exception ex)
            {

                throw ex;

            }
        }
        public static bool ExecuteNonQuery(string query)
        {
            bool result = false;
            int recordsAffected = 0;
            SqlConnection conn = DBConnccection.GetConnection();
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {

                cmd.Connection.Open();
                recordsAffected = cmd.ExecuteNonQuery();
                result = (recordsAffected > 0) ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();

            }
            return result;

        }

        public static void BindDays(DropDownList ddl)
        {
            int i;
            ArrayList AlDay = new ArrayList();

            for (i = 1; i <= 31; i++)
                AlDay.Add(i);
            ddl.DataSource = AlDay;
            ddl.DataBind();
        }
        public static void BindYear(DropDownList ddl)
        {
            int i;
            ArrayList AlYear = new ArrayList();

            for (i = 1950; i <= 2020; i++)
                AlYear.Add(i);
            ddl.DataSource = AlYear;
            ddl.DataBind();
        }
        public static void BindHour(DropDownList ddl)
        {
            int i;
            string hrs = "";
            ArrayList AlHrs = new ArrayList();

            for (i = 0; i <= 12; i++)
            {
                if (i.ToString().Length == 1)
                { hrs = "0" + i.ToString(); }
                else { hrs = i.ToString(); }
                AlHrs.Add(hrs);
            }
            ddl.DataSource = AlHrs;
            ddl.DataBind();
        }
        public static void BindMinut(DropDownList ddl)
        {
            int i;
            string min = "";
            ArrayList AlMin = new ArrayList();

            for (i = 0; i <= 59; i++)
            {
                if (i.ToString().Length == 1)
                { min = "0" + i.ToString(); }
                else { min = i.ToString(); }
                AlMin.Add(min);
            }
            ddl.DataSource = AlMin;
            ddl.DataBind();
        }

        public static void PopulateDDL(DropDownList drp, string qry, string colnameText, string colnamevalue)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlCommand cmd = new SqlCommand(qry, strcon);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DataView dv = new DataView(dt);
                //dv.Sort = colnameText;
                drp.DataSource = dv;
                drp.DataTextField = colnameText;
                drp.DataValueField = colnamevalue;
                drp.DataBind();
            }
            catch (Exception) { }
        }
        public static void PopulateDDLProc(DropDownList drp, string proc, string colnameText, string colnamevalue)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlCommand cmd = new SqlCommand(proc, strcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DataView dv = new DataView(dt);
                // dv.Sort = colnameText;
                drp.DataSource = dv;
                drp.DataTextField = colnameText;
                drp.DataValueField = colnamevalue;
                drp.DataBind();
            }
            catch (Exception) { }
        }
        public static void PopulateDDLProcVal(DropDownList drp, string proc, string colnameText, string colnamevalue, Hashtable hs)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlCommand cmd = new SqlCommand(proc, strcon);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (string parameter in hs.Keys)
                {
                    cmd.Parameters.Add(parameter, hs[parameter]);
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DataView dv = new DataView(dt);
                //dv.Sort = colnameText;
                drp.DataSource = dv;
                drp.DataTextField = colnameText;
                drp.DataValueField = colnamevalue;
                drp.DataBind();
            }
            catch (Exception) { }
        }
        public static void PopulateCHKProcVal(CheckBoxList chk, string proc, string colnameText, string colnamevalue, Hashtable hs)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlCommand cmd = new SqlCommand(proc, strcon);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (string parameter in hs.Keys)
                {
                    cmd.Parameters.Add(parameter, hs[parameter]);
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DataView dv = new DataView(dt);
                //dv.Sort = colnameText;
                chk.DataSource = dv;
                chk.DataTextField = colnameText;
                chk.DataValueField = colnamevalue;
                chk.DataBind();
            }
            catch (Exception) { }
        }
        public static int ExecuteParaNonQuery(string Sp_name, Hashtable hs)
        {
            int r = 0;
            SqlConnection strcon = DBConnccection.GetConnection();
            SqlCommand cmd = new SqlCommand(Sp_name, strcon);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (string parameter in hs.Keys)
                {
                    cmd.Parameters.Add(parameter, hs[parameter]);
                }
                strcon.Open();

                r = cmd.ExecuteNonQuery();
                strcon.Close(); cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strcon.Close();
                cmd.Connection.Close();
            }
            return r;
        }
        public static void BindFYear(DropDownList ddl)
        {
            int i;
            ArrayList AlYear = new ArrayList();

            for (i = 2010; i <= DateTime.Now.Year; i++)
                AlYear.Add(i + "-" + (i + 1));
            ddl.DataSource = AlYear;
            ddl.SelectedValue = DateTime.Now.Year + "-" + (DateTime.Now.Year + 1);
            ddl.DataBind();
        }
        public static void PopulateRadio(RadioButtonList rnd, string qry, string colnameText, string colnamevalue)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlCommand cmd = new SqlCommand(qry, strcon);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                rnd.DataSource = dt;
                rnd.DataTextField = colnameText;
                rnd.DataValueField = colnamevalue;
                rnd.DataBind();
            }
            catch (Exception) { }
        }
        public static void PopulateRadioProc(RadioButtonList rnd, string proc, string colnameText, string colnamevalue, Hashtable hs)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlCommand cmd = new SqlCommand(proc, strcon);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (string parameter in hs.Keys)
                {
                    cmd.Parameters.Add(parameter, hs[parameter]);
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                rnd.DataSource = dt;
                rnd.DataTextField = colnameText;
                rnd.DataValueField = colnamevalue;
                rnd.DataBind();
            }
            catch (Exception) { }
        }
        public static void PopulateChkbx(CheckBoxList rnd, string qry, string colnameText, string colnamevalue)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlCommand cmd = new SqlCommand(qry, strcon);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                rnd.DataSource = dt;
                rnd.DataTextField = colnameText;
                rnd.DataValueField = colnamevalue;
                rnd.DataBind();
            }
            catch (Exception) { }
        }
        //public static void GetCourseDura(DropDownList ddl)
        //{
        //for ( int i = 0; i < 10; i++)
        //{
        //    ddl.Items.Insert(i, (i+1).ToString());
        //    ddl.Items[i].Text = (i+1).ToString() + " Weeks";
        //    ddl.Items[i].Value = (i + 1).ToString();
        //}            
        //    ddl.DataBind();           
        //}       
        //public static void GetCourseLot(DropDownList ddl)
        //{
        //    int i;
        //    ArrayList Al = new ArrayList();
        //    for (i = 1; i <= 10; i++)
        //        Al.Add(i);
        //    ddl.DataSource = Al;
        //    ddl.DataBind();
        //}
    }
    #endregion



    #region code for bind gridview using storedprocedure parameter
     

    #endregion
    #region Change the Password of Current User
    public class ChangePwd
    {

        public bool CheckPassword(string pwd)
        {

            string qry = HttpContext.Current.Session["Id"].ToString();
            string pass = "select * from user_mst where usr_id='" + qry + "' and pwd=@pwd";
            SqlConnection strcon = DBConnccection.GetConnection();
            SqlCommand cmd = new SqlCommand(pass, strcon);

            string encpwd = EncryptPwd.EncryptText(pwd);
            cmd.Parameters.AddWithValue("@pwd", SqlDbType.NVarChar).Value = encpwd;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == true)
                {
                    strcon.Close();
                    return true;
                }
                else
                {
                    strcon.Close();
                    return false;
                }

            }

            catch (Exception er)
            {
                throw;
            }


        }

        public bool Resetpassword(string ConPwd)
        {

            string qry = HttpContext.Current.Session["Id"].ToString();
            string pass = " Update  user_mst set pwd=@pwd  where usr_id='" + qry + "'";
            SqlConnection strcon = DBConnccection.GetConnection();
            SqlCommand cmd = new SqlCommand(pass, strcon);
            string encpwd = EncryptPwd.EncryptText(ConPwd);
            cmd.Parameters.AddWithValue("@pwd", SqlDbType.NVarChar).Value = encpwd;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception er)
            {

                return false;
                throw;
            }
            finally
            {
                //cmd.Connection.Close();
            }
        }

    }
    #endregion
    public class ExportData
    {

        public void Export(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();

                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        private void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }

        #region
        //Import Excel Data in Reperater
        public DataTable ImportRepeater(string path, string strFileType,string query)
        {
            string connString = "";
            if (strFileType.Trim() == ".xls")
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            OleDbConnection conn = new OleDbConnection(connString);
            DataTable ds = new DataTable();
            try
            {               
                OleDbCommand cmd = new OleDbCommand(query, conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                //foreach (string parameter in hs.Keys)
                //{
                //    cmd.Parameters.Add(parameter, hs[parameter]);
                //}                
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {               
            }           
            return ds;
        }
        #endregion

    }
    #region Bind The Chart
    public static class ChartControl
    {

        public static SqlDataReader BindChart(string storeproc)
        {
            SqlDataReader rdr = null;
            SqlConnection strcon = DBConnccection.GetConnection();
            SqlCommand cmd = new SqlCommand(storeproc, strcon);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {

                cmd.Connection.Open();
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


            }



            catch (Exception ex)
            {

                cmd.Connection.Close();
                throw;


            }
            finally
            {

                //if (rdr != null) rdr.Close();
                //if (strcon != null) strcon.Close();


            }

            return rdr;




        }
        public static SqlDataReader BindChart(string storeproc, string projId)
        {
            SqlDataReader rdr = null;
            SqlConnection strcon = DBConnccection.GetConnection();
            SqlCommand cmd = new SqlCommand(storeproc, strcon);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@Proj_id", SqlDbType.VarChar).Value = projId;
                cmd.Connection.Open();

                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


            }



            catch (Exception ex)
            {

                cmd.Connection.Close();
                throw;


            }
            finally
            {

                //if (rdr != null) rdr.Close();
                //if (strcon != null) strcon.Close();


            }

            return rdr;




        }
        public static DataTable BindGridviewTable(string storeproc, string projId)
        {
            try
            {
                SqlConnection strcon = DBConnccection.GetConnection();
                SqlCommand cmd = new SqlCommand(storeproc, strcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Proj_id", SqlDbType.VarChar).Value = projId;
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {

                throw new Exception("Erro in Binding data" + ex.Message);

            }

        }


    }
    #endregion
    
    
}
