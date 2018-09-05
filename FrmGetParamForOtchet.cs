using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DgvFilterPopup;
using System.Data.SqlClient;

namespace sprut
{
    public partial class FrmGetParamForOtchet : Form
    {
        public DateTime dt_beg;
        public DateTime dt_end;
        public bool cncl = true;
        public string stage;
        public string depts;

        public FrmGetParamForOtchet(string con)
        {
            InitializeComponent();
            //Create_list_project(con);
        }
        public void CreateListSotr(List<clDepartment> listSotr)
        {
            this.listBox1.DataSource = listSotr;
            listBox1.DisplayMember = "FullName";
            listBox1.ValueMember = "Id";
        }

        public void Create_list_project(string conn, List<clDepartment> listSotr)
        {
            SqlDataReader reader = null;
            
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[PL_GetAllStagesWithDeptsFromTdms]"))
                    {
                        string depts = "";
                        string w = "";
                        foreach (clDepartment item in listSotr)
                        {

                            depts = depts + w + item.Id.ToString();
                            w = ",";
                        }
                        cmd.CommandTimeout = 3000;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@depts", depts);
                        //cmd.Parameters.AddWithValue("@month", month_);
                        cmd.Connection = con;
                        con.Open();

                        reader = cmd.ExecuteReader();
                        DataTable drtable = new DataTable();

                        drtable.Load(reader);
                        reader.Close();
                        if (con != null)
                            con.Close();

                        if (drtable.Rows.Count > 0)
                        {
                            this.listBox2.DataSource = drtable;
                            this.listBox2.DisplayMember = "stage_name";
                            this.listBox2.ValueMember = "stage_guid";
                        }
                        else
                            //stage = "";
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        


        internal void Show()
        {
            if (!(this.listBox2.DataSource == null))
            {
                this.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dt_beg = new DateTime(dtpReportBeg.Value.Year, dtpReportBeg.Value.Month, 1, 0, 0, 0);
            this.dt_end = new DateTime(dtpReportEnd.Value.Year, dtpReportEnd.Value.Month, DateTime.DaysInMonth(dtpReportEnd.Value.Year, dtpReportEnd.Value.Month), 0, 0, 0);

            if ((this.dt_end == this.dt_beg) || (this.dt_end > this.dt_beg))
            {
                this.dt_end = this.dt_end.AddDays(1);
                this.cncl = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("Дата окончания не может быть меньше даты начала");
                return;
            }

            // выбранные отделы
            string w = "";
            depts = "";
            foreach (clDepartment item in listBox1.SelectedItems)
            {

                depts = depts + w + item.Id.ToString();
                w = ",";
            }
            if (depts == "")
            {
                foreach (clDepartment item in listBox1.Items)
                {

                    depts = depts + w + item.Id.ToString();
                    w = ",";
                }

            }



                
            //выбранные проекты
            w = "";
            stage = "";
            foreach (DataRowView row in listBox2.SelectedItems)
            {

                stage = stage + w + row.Row["stage_guid"];
                w = ",";
            }
            if (stage == "")
            {
                foreach (DataRowView row in listBox2.Items)
                {

                    stage = stage + w + row.Row["stage_guid"];
                    w = ",";
                }
            }
            this.cncl = false;
            this.Close();
        }

        private void btnReportBegDec_Click_1(object sender, EventArgs e)
        {
            dtpReportBeg.Value = dtpReportBeg.Value.AddMonths(-1);
        }

        private void btnReportBegInc_Click_1(object sender, EventArgs e)
        {
            dtpReportBeg.Value = dtpReportBeg.Value.AddMonths(1);
        }

        private void btnReportEndDec_Click_1(object sender, EventArgs e)
        {
            dtpReportEnd.Value = dtpReportEnd.Value.AddMonths(-1);
        }

        private void btnReportEndInc_Click_1(object sender, EventArgs e)
        {
            dtpReportEnd.Value = dtpReportEnd.Value.AddMonths(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.cncl = true;
            this.Close();
        }
    }
}
