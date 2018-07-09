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
    public partial class frmGetProject : Form
    {
        public string stage;
     
        public frmGetProject()
        {
            
        }

        public frmGetProject(string month, string year, string con)
        {
            InitializeComponent();
            Create_cmb_project(month,year,con);
        }

        internal void Create_cmb_project(string month_, string year_, string conn)
        {
            SqlDataReader reader = null;
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[PL_GetStageWithFact]"))
                    {
                        cmd.CommandTimeout = 3000;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@year", year_);
                        cmd.Parameters.AddWithValue("@month", month_);
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
                            this.cmbxProject.DataSource = drtable;
                            this.cmbxProject.DisplayMember = "project";
                            this.cmbxProject.ValueMember = "stage_guid";
                        }
                        else
                            stage = "";
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stage = this.cmbxProject.SelectedValue.ToString();
            this.Close();
        }

        internal void Show()
        {
            if (!(this.cmbxProject.DataSource == null))
            {
                stage = "-1";
                this.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stage = "-1";
            this.Close();
        }
    }
}
