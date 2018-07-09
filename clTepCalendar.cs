using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sprut
{
    //string conn_string = "";
    public class clTepCalendar
    {
        private string conn_string = "Data Source=" + frmMain.TdmsSrv + ";Initial Catalog=" + frmMain.TdmsName + ";Persist Security Info=True;User ID=" + frmMain.UserSqlName + ";Password=" + frmMain.UserSqlPassword;
         
        private Dictionary<string, double> calendar;
        
        public clTepCalendar()
        {
            calendar = new Dictionary<string, double>();
            SqlConnection conn = new SqlConnection(conn_string);
            SqlDataReader dr = null;
            try
            {
                SqlCommand command = new SqlCommand("select * from ["+frmMain.SqlLink + "].Platan.dbo.TCalendar", conn);
                conn.Open();
                dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        try
                        {
                            DateTime d = Convert.ToDateTime(dr["c_date"].ToString());
                            double t = Convert.ToDouble(dr["c_time"].ToString());
                            string key = d.ToShortDateString();
                            calendar.Add(key, t);
                        }
                        catch
                        {
                            //MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "   " + ex.StackTrace);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (conn != null)
                    conn.Close();
            }
        }
        public double WorkHours(DateTime dt)
        {
            double result = 0.0;

            if (calendar != null)
            {
                try
                {
                    string key = dt.ToShortDateString();
                    double t = 0;
                    if(calendar.TryGetValue(key, out t))
                        result = t;

                }
                catch
                {
                    //MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                    result = 0;
                }
            }
            return result;
        }

        public double CountWorkHours(DateTime start, DateTime end)
        {
            double result = 0.0;
            double temp = 0;
            if (calendar != null)
            {
                while (start <= end)
                {
                    string key = start.ToShortDateString();
                    double t = 0;
                    if(calendar.TryGetValue(key, out t))
                        temp += t;
                    
                    start = start.AddDays(1);
                }
                result = temp;
            }
            return result;
        }
    }
}
