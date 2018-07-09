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
    public partial class frmOtherWorks : Form
    {
        DgvFilterManager SetsFilterManager = null;
        DgvFilterManager StagesFilterManager = null;
        DgvFilterManager OtherWorksFilterManager = null;

        string conn_string = "";//"Data Source=sqltep;Initial Catalog=Sprut;Persist Security Info=True;User ID=Pdmstotdms;Password=PdMsToTdMs";

        DataTable dtStages = new DataTable("dtStages");
        DataTable dtSets = new DataTable("dtSets");
        DataTable dtOtherWorks = new DataTable("dtOtherWorks");

        public string set_guid = "";
        public string stage_guid = "";
        public string contract = "";
        public string stage_name = "";

        frmMain.delegateIndividualPlanAddOtherWork owd;

        public frmOtherWorks(string p)
        {
            conn_string = p;

            InitializeComponent();

            CreateDtStages();
            CreateDgvStages();
            LoadStages();
            dgvStages.DataSource = dtStages;

            CreateDtSets();
            CreateDgvSets();
            dgvSets.DataSource = dtSets;

            CreateDtOtherWorks();
            CreateDgvOtherWorks();
            dgvOtherWorks.DataSource = dtOtherWorks;
        }

        public frmOtherWorks(string p, frmMain.delegateIndividualPlanAddOtherWork owd)
        {
            conn_string = p;
            this.owd = owd;

            InitializeComponent();

            CreateDtStages();
            CreateDgvStages();
            LoadStages();
            dgvStages.DataSource = dtStages;

            CreateDtSets();
            CreateDgvSets();
            dgvSets.DataSource = dtSets;

            CreateDtOtherWorks();
            CreateDgvOtherWorks();
            dgvOtherWorks.DataSource = dtOtherWorks;     
            
        }

        private void CreateDgvStages()
        {
            try
            {
                if (dgvStages == null)
                    return;

                dgvStages.AutoGenerateColumns = false;
                dgvStages.ColumnCount = 3;
                dgvStages.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dgvStages.Columns[0].Name = "Contract";
                dgvStages.Columns[0].HeaderText = "Шифр проекта";
                dgvStages.Columns[0].DataPropertyName = "Contract";
                dgvStages.Columns[0].ReadOnly = true;
                dgvStages.Columns[0].Visible = true;

                dgvStages.Columns[1].Name = "StageName";
                dgvStages.Columns[1].HeaderText = "Наименование проекта";
                dgvStages.Columns[1].DataPropertyName = "StageName";
                dgvStages.Columns[1].ReadOnly = true;
                dgvStages.Columns[1].Visible = true;

                dgvStages.Columns[2].Name = "StageGuid";
                dgvStages.Columns[2].DataPropertyName = "StageGuid";
                dgvStages.Columns[2].ReadOnly = true;
                dgvStages.Columns[2].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }

        }

        private void CreateDtStages()
        {
            try
            {
                if (dtStages == null)
                    dtStages = new DataTable("dtStages");
                else
                {
                    dtStages.Rows.Clear();
                    dtStages.Columns.Clear();
                }
                dtStages.Columns.Add("Contract", typeof(string));
                dtStages.Columns.Add("StageName", typeof(string));
                dtStages.Columns.Add("StageGuid", typeof(string));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void LoadStages()
        {
            if (dtStages == null)
                return;
            else
                dtStages.Rows.Clear();

            SqlConnection conn = new SqlConnection(conn_string);
            SqlDataReader dr = null;
            try
            {
                SqlCommand command = new SqlCommand("[dbo].[PL_GetAllStagesFromTdms]", conn);
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                dr = command.ExecuteReader();
                 DataRow row = dtStages.NewRow();
                row["StageGuid"] = "Без проекта";
                row["StageName"] = "Общепроизводственные задачи";
                row["Contract"] = "Без проекта";
                dtStages.Rows.Add(row);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        row = dtStages.NewRow();
                        row["StageGuid"] = dr["stage_guid"].ToString();
                        row["StageName"] = dr["stage_name"].ToString();
                        row["Contract"] = dr["contract"].ToString();
                        dtStages.Rows.Add(row);
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
                dgvStages.ClearSelection();
            }
        }

        private void CreateDgvSets()
        {
            try
            {
                if (dgvSets == null)
                    return;

                dgvSets.AutoGenerateColumns = false;
                dgvSets.ColumnCount = 6;
                dgvSets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvSets.Columns[0].Name = "Building";
                dgvSets.Columns[0].DataPropertyName = "Building";
                dgvSets.Columns[0].HeaderText = "Титул";
                dgvSets.Columns[0].ReadOnly = true;
                dgvSets.Columns[0].Visible = true;

                dgvSets.Columns[1].Name = "SetCode";
                dgvSets.Columns[1].DataPropertyName = "SetCode";
                dgvSets.Columns[1].HeaderText = "Задача";
                dgvSets.Columns[1].ReadOnly = true;
                dgvSets.Columns[1].Visible = true;

                dgvSets.Columns[2].Name = "SetName";
                dgvSets.Columns[2].DataPropertyName = "SetName";
                dgvSets.Columns[2].HeaderText = "Наименование задачи";
                dgvSets.Columns[2].ReadOnly = true;
                dgvSets.Columns[2].Visible = true;

                dgvSets.Columns[3].Name = "SetGuid";
                dgvSets.Columns[3].DataPropertyName = "SetGuid";
                dgvSets.Columns[3].ReadOnly = true;
                dgvSets.Columns[3].Visible = false;

                //dgvSets.Columns[3].Name = "Kks";
                //dgvSets.Columns[3].HeaderText = "KKS";
                //dgvSets.Columns[3].DataPropertyName = "Kks";
                //dgvSets.Columns[3].ReadOnly = true;
                //dgvSets.Columns[3].Visible = true;

                

                dgvSets.Columns[4].Name = "GipStart";
                dgvSets.Columns[4].DataPropertyName = "GipStart";
                dgvSets.Columns[4].HeaderText = "Начало";
                dgvSets.Columns[4].ReadOnly = true;
                dgvSets.Columns[4].Visible = true;

                dgvSets.Columns[5].Name = "GipEnd";
                dgvSets.Columns[5].DataPropertyName = "GipEnd";
                dgvSets.Columns[5].HeaderText = "Окончание";
                dgvSets.Columns[5].ReadOnly = true;
                dgvSets.Columns[5].Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void CreateDtSets()
        {
            try
            {
                if (dtSets == null)
                    dtSets = new DataTable();
                else
                {
                    dtSets.Rows.Clear();
                    dtSets.Columns.Clear();
                }

                dtSets.Columns.Add("SetCode", typeof(string));
                dtSets.Columns.Add("SetName", typeof(string));
                dtSets.Columns.Add("SetGuid", typeof(string));
                //dtSets.Columns.Add("Kks", typeof(string));
                dtSets.Columns.Add("Building", typeof(string));
                dtSets.Columns.Add("GipStart", typeof(DateTime));
                dtSets.Columns.Add("GipEnd", typeof(DateTime));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void CreateDtOtherWorks()
        {
            try
            {
                if (dtOtherWorks == null)
                    dtOtherWorks = new DataTable();
                else
                {
                    dtOtherWorks.Rows.Clear();
                    dtOtherWorks.Columns.Clear();
                }

                dtOtherWorks.Columns.Add("SetCode", typeof(string));
                dtOtherWorks.Columns.Add("SetName", typeof(string));
                dtOtherWorks.Columns.Add("SetGuid", typeof(string));
                //dtOtherWorks.Columns.Add("kks", typeof(string));
                dtOtherWorks.Columns.Add("Building", typeof(string));
                dtOtherWorks.Columns.Add("GipStart", typeof(DateTime));
                dtOtherWorks.Columns.Add("GipEnd", typeof(DateTime));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void CreateDgvOtherWorks()
        {
            try
            {
                if (dgvOtherWorks == null)
                    return;

                dgvOtherWorks.AutoGenerateColumns = false;
                dgvOtherWorks.ColumnCount = 6;
                dgvOtherWorks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvOtherWorks.Columns[0].Name = "SetCode";
                dgvOtherWorks.Columns[0].DataPropertyName = "SetCode";
                dgvOtherWorks.Columns[0].HeaderText = "Задача";
                dgvOtherWorks.Columns[0].ReadOnly = true;
                dgvOtherWorks.Columns[0].Visible = true;

                dgvOtherWorks.Columns[1].Name = "Building";
                dgvOtherWorks.Columns[1].DataPropertyName = "Building";
                dgvOtherWorks.Columns[1].HeaderText = "Титул";
                dgvOtherWorks.Columns[1].ReadOnly = true;
                dgvOtherWorks.Columns[1].Visible = false;

                dgvOtherWorks.Columns[3].Name = "SetName";
                dgvOtherWorks.Columns[3].DataPropertyName = "SetName";
                dgvOtherWorks.Columns[3].HeaderText = "Наименование задачи";
                dgvOtherWorks.Columns[3].ReadOnly = true;
                dgvOtherWorks.Columns[3].Visible = false;


                dgvOtherWorks.Columns[2].Name = "SetGuid";
                dgvOtherWorks.Columns[2].DataPropertyName = "SetGuid";
                dgvOtherWorks.Columns[2].ReadOnly = true;
                dgvOtherWorks.Columns[2].Visible = false;

                //dgvOtherWorks.Columns[3].Name = "Kks";
                //dgvOtherWorks.Columns[3].HeaderText = "KKS";
                //dgvOtherWorks.Columns[3].DataPropertyName = "Kks";
                //dgvOtherWorks.Columns[3].ReadOnly = true;
                //dgvOtherWorks.Columns[3].Visible = true;

                

                dgvOtherWorks.Columns[4].Name = "GipStart";
                dgvOtherWorks.Columns[4].DataPropertyName = "GipStart";
                dgvOtherWorks.Columns[4].HeaderText = "Дата начала работ по плану";
                dgvOtherWorks.Columns[4].ReadOnly = true;
                dgvOtherWorks.Columns[4].Visible = false;

                dgvOtherWorks.Columns[5].Name = "GipEnd";
                dgvOtherWorks.Columns[5].DataPropertyName = "GipEnd";
                dgvOtherWorks.Columns[5].HeaderText = "Дата окончания работ по плану";
                dgvOtherWorks.Columns[5].ReadOnly = true;
                dgvOtherWorks.Columns[5].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void LoadSets(ref DataTable dt, string param)
        {
            if (dt == null)
                return;
            try
            {
                dt.Rows.Clear();
                SqlConnection conn = new SqlConnection(conn_string);
                SqlDataReader dr = null;
                try
                {
                    SqlCommand command = new SqlCommand("[dbo].[PL_GetAllSetsByStageGuidFromTdms]", conn);
                    command.Parameters.Add("@stage_guid", SqlDbType.NVarChar).Value = param;
                    command.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DataRow row = dtSets.NewRow();
                            row["SetCode"] = dr["set_code"].ToString();
                            row["SetName"] = dr["set_name"].ToString();
                            row["SetGuid"] = dr["set_guid"].ToString();
                           // row["Kks"] = dr["kks"].ToString();
                            row["Building"] = dr["building"].ToString();

                            DateTime gip_start;
                            DateTime gip_end;

                            if (DateTime.TryParse(dr["gip_start"].ToString(), out gip_start))
                                row["GipStart"] = gip_start.ToShortDateString();
                            else
                                row["GipStart"] = "1900-01-01";

                            if (DateTime.TryParse(dr["gip_end"].ToString(), out gip_end))
                                row["GipEnd"] = gip_end.ToShortDateString();
                            else
                                row["GipEnd"] = "1900-01-01";

                            dtSets.Rows.Add(row);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void LoadOtherWorks(string param)
        {
            SqlConnection conn = new SqlConnection(conn_string);
            SqlDataReader dr = null;
            try
            {
                dtOtherWorks.Rows.Clear();
                SqlCommand command = new SqlCommand("[dbo].[PL_GetSetsOrOtherWorksFromTdms]", conn);
                command.Parameters.Add("@param", SqlDbType.NVarChar).Value = param;
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DataRow row = dtOtherWorks.NewRow();
                        row["SetGuid"] = dr["set_guid"].ToString();
                        row["SetName"] = dr["set_name"].ToString();
                        //row["Building"] = dr["building"].ToString();
                        row["SetCode"] = dr["set_code"].ToString();
                        //row["Kks"] = dr["kks"].ToString();
                        /*DateTime gip_start;
                        DateTime gip_end;

                        if (DateTime.TryParse(dr["gip_start"].ToString(), out gip_start))
                            row["GipStart"] = gip_start.ToShortDateString();
                        else
                            row["GipStart"] = "1900-01-01";

                        if (DateTime.TryParse(dr["gip_end"].ToString(), out gip_end))
                            row["GipEnd"] = gip_end.ToShortDateString();
                        else
                            row["GipEnd"] = "1900-01-01";*/
                        dtOtherWorks.Rows.Add(row);
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

        private void frmOtherWorks_Shown(object sender, EventArgs e)
        {
            StagesFilterManager = new DgvFilterManager(dgvStages, true);
            SetsFilterManager = new DgvFilterManager(dgvSets, true);
            OtherWorksFilterManager = new DgvFilterManager(dgvOtherWorks, true);

            dgvStages.ClearSelection();
        }

        private void dgvStages_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if ((row < 0) || (col < 0))
            {
                return;
            }

            dtSets.Rows.Clear();
            dtOtherWorks.Rows.Clear();

            string param = "";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                stage_guid = dgvStages.Rows[row].Cells["StageGuid"].Value.ToString();

                contract = dgvStages.Rows[row].Cells["Contract"].Value.ToString();
                stage_name = dgvStages.Rows[row].Cells["StageName"].Value.ToString();

                if (contract == "Без проекта")
                {
                    param = "1";
                    LoadOtherWorks(param);
                    tabControlSets.SelectedIndex = 1;
                }
                else
                {
                    param = "2";
                    LoadSets(ref dtSets, stage_guid);
                    LoadOtherWorks(param);
                    tabControlSets.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnAddOtherWork_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStages == null)
                    return;
                if ((dgvStages.SelectedRows == null) || (dgvStages.SelectedRows.Count <= 0))
                {
                    MessageBox.Show("Не выбрана стадия!");
                    return;
                }

                DataGridViewRow row_stage = dgvStages.SelectedRows[0];
                
                contract = row_stage.Cells["Contract"].Value.ToString();
                stage_name = row_stage.Cells["StageName"].Value.ToString();

                set_guid = "";
                stage_guid = row_stage.Cells["StageGuid"].Value.ToString();

                switch (tabControlSets.SelectedIndex)
                {
                    case 0:
                        {
                            //Из таблицы комплектов
                            if ((dgvSets.SelectedRows == null) || (dgvSets.SelectedRows.Count <= 0))
                            {
                                MessageBox.Show("Не выбрана задача!");
                                return;
                            }
                            DataGridViewRow row_set = dgvSets.SelectedRows[0];
                            set_guid = row_set.Cells["SetGuid"].Value.ToString();

                            break;
                        }
                    case 1:
                        {
                            //Из таблицы других работ
                            if ((dgvOtherWorks.SelectedRows == null) || (dgvOtherWorks.SelectedRows.Count <= 0))
                            {
                                MessageBox.Show("Не выбрана задача!");
                                return;
                            }
                            DataGridViewRow row_other_work = dgvOtherWorks.SelectedRows[0];
                            set_guid = row_other_work.Cells["SetGuid"].Value.ToString();
                            break;
                        }
                    default:
                        {
                            set_guid = "";
                            stage_guid = "";
                            contract = "";
                            stage_name = "";
                            break;
                        }
                }
                //если owd не Null то форма не должна закрываться, можно много комплектов добавлять, используется при добавлении работ в индивидуальном плане
                if (owd != null)
                    owd(stage_guid, set_guid, contract, stage_name);
                else
                //иначе добавляем комплект в планирование, при этом форма добавления комплектов закрывается
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
