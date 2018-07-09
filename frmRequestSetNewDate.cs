using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sprut
{
    public partial class frmRequestSetNewDate : Form
    {
        clUser user = null;
        List<string> lstReasons = new List<string>();

        string set_guid = "";
        string stage_guid = "";
        string conn = "";
        DateTime set_start;
        DateTime set_end;

        string agreed_start = "";
        string agreed_end = "";

        public frmRequestSetNewDate()
        {
            InitializeComponent();
            
            lstReasons.Add("");
            lstReasons.Add("Нет исходных данных");
            lstReasons.Add("Недостаточно ресурсов отдела");
            lstReasons.Add("Удалить комплект");
            lstReasons.Add("Другая причина");
            cmbxReasons.DataSource = lstReasons;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            string new_start = dtpNewStartDate.Value.ToShortDateString();
            string new_end = dtpNewEndDate.Value.ToShortDateString();

            string reason = cmbxReasons.Items[cmbxReasons.SelectedIndex].ToString();
            string comment = rtxtbComment.Text;
            string user_id = user.UserId;
            string dept_id = user.DepartmentId;

            string old_start = lblGipStart.Text;
            string old_end = lblGipEnd.Text;

            if(dtpNewStartDate.Value > dtpNewEndDate.Value)
            {
                MessageBox.Show("Дата начала не может быть позднее даты окончания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(string.IsNullOrEmpty(reason))
            {
                MessageBox.Show("Не выбрана причина переноса!","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if ((reason.Equals("Другая причина")) && ((string.IsNullOrEmpty(comment.Trim()))))
            {
                MessageBox.Show("Укажите причину в комментарии","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(IsRequestExist(set_guid, stage_guid))
            {
                DialogResult diag_res = MessageBox.Show("В настоящее время перенос дат комплекта уже запрощен.\nПовторить запрос?", "Повторный запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(diag_res == System.Windows.Forms.DialogResult.Yes)
                {
                    CreateRequest(set_guid, stage_guid, user.UserId, new_start, new_end, agreed_start, agreed_end, reason, comment, user.DepartmentId, "request");
                    //this.Hide();
                }
                else
                {
                }
            }
            else
            {
                CreateRequest(set_guid, stage_guid, user.UserId, new_start, new_end, agreed_start, agreed_end, reason, comment, user.DepartmentId, "request");
                //this.Hide();
            }

            this.Close();
        }

        private bool IsRequestExist(string set_guid, string stage_guid)
        {
            bool result = false;
            SqlDataReader reader = null;
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("select top 1 * from [" + frmMain.SqlLink + "].Platan.dbo.Request where Request.set_guid = '" + set_guid + "' and Request.stage_guid = '" + stage_guid + "' and Request.status = '" + "request" + "' order by request_date"))
                    {
                        cmd.CommandTimeout = 3000;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();

                        reader = cmd.ExecuteReader();

                        if(reader.HasRows)
                        {
                            reader.Read();
                            result = true;
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }

            return result;
        }

        public void CreateRequest(string set_guid, string stage_guid, string user_id, string new_start, string new_end, string old_start, string old_end, string reason, string comment, string dept_id, string request)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            try
            {
                con = new SqlConnection(conn);
                string query = "insert into [" + frmMain.SqlLink + "].Platan.dbo.Request (set_guid, stage_guid, user_id, start_date, end_date, request_date, reason, comment, dept_id, status, old_start_date, old_end_date) values (@set_guid, @stage_guid, @user_id, @start_date, @end_date, @request_date, @reason, @comment, @dept_id, @status, @old_start, @old_end)";
                con.Open();

                com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@set_guid", set_guid);
                com.Parameters.AddWithValue("@stage_guid", stage_guid);
                com.Parameters.AddWithValue("@user_id", user_id);
                com.Parameters.AddWithValue("@start_date", new_start);
                com.Parameters.AddWithValue("@end_date", new_end);
                com.Parameters.AddWithValue("@request_date", DateTime.Now.ToString());
                com.Parameters.AddWithValue("@reason", reason);
                com.Parameters.AddWithValue("@comment", comment);
                com.Parameters.AddWithValue("@dept_id", dept_id);
                com.Parameters.AddWithValue("@status", request);
                com.Parameters.AddWithValue("@old_start", old_start);
                com.Parameters.AddWithValue("@old_end", old_end);

                com.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                if(con != null)
                {
                    con = null;
                }
            }
        }

        internal void ShowDialog(string contract, string set_g, string stage_g, string stage_name, string set_code, string set_name, string gip_start, string gip_end, string connString, clUser current_user, string set_st, string set_en)
        {
            set_guid = set_g;
            stage_guid = stage_g;
            conn = connString;

            user = current_user;
            lblContract.Text =contract;
            lblStage.Text = stage_name;
            lblSetCode.Text =  set_code;

            DateTime dt;
            if (DateTime.TryParse(gip_start, out dt))
                gip_start = dt.ToShortDateString();
            if (DateTime.TryParse(gip_end, out dt))
                gip_end = dt.ToShortDateString();
        
            lblGipStart.Text = gip_start;
            lblGipEnd.Text =  gip_end;

            if (DateTime.TryParse(set_st, out set_start) && DateTime.TryParse(set_en, out set_end))
            {
                int days = (set_end - set_start).Days;
                set_start = set_start.AddMonths(1);
                DateTime new_set_start = new DateTime(set_start.Year, set_start.Month, 1, 0, 0, 0);
                dtpNewStartDate.Value = new_set_start;
                new_set_start = new_set_start.AddDays(days);
                dtpNewEndDate.Value = new_set_start;
            }


            this.ShowDialog();
        }

        internal void ShowDialog(ref clSet set, string connString, clUser current_user)
        {
            set_guid = set.SetGuid;
            stage_guid = set.StageGuid;
            conn = connString;

            agreed_start = set.SetStart.ToShortDateString();
            agreed_end = set.SetEnd.ToShortDateString();

            user = current_user;
            lblContract.Text = set.Contract;
            lblStage.Text = set.StageName;
            lblSetCode.Text = set.SetCode;

            /*DateTime dt;
            if (DateTime.TryParse(set.GipStart, out dt))
                gip_start = dt.ToShortDateString();
            if (DateTime.TryParse(gip_end, out dt))
                gip_end = dt.ToShortDateString();*/

            lblGipStart.Text = set.GipStart.ToShortDateString();
            lblGipEnd.Text = set.GipEnd.ToShortDateString();



            if (DateTime.TryParse(set.SetStart.ToShortDateString(), out set_start) && DateTime.TryParse(set.SetEnd.ToShortDateString(), out set_end))
            {
                int days = (set_end - set_start).Days;
                set_start = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 1, 0, 0, 0);              
                dtpNewStartDate.Value = set_start;
                set_start = set_start.AddDays(days);
                dtpNewEndDate.Value = set_start;
            }


            this.ShowDialog();
        }
    }
}
