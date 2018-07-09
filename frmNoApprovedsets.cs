using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace sprut
{
    public partial class frmNoApprovedsets : Form
    {
        DataTable _dtProductionPlan = new DataTable("dtProductionPlan");
        public frmNoApprovedsets()
        {

        }
        public frmNoApprovedsets(List<clSet> plan, string reason)
        {
            InitializeComponent();

            dgvProductionPlan_Create();
            _dtProductionPlan_Create();
            dgvProductionPlan.DataSource = _dtProductionPlan;
            Production_Plan_Load(plan);
            this.Text = "Неутвержденные задачи. " + reason;

        }
        private void dgvProductionPlan_Create()
        {
            
            dgvProductionPlan.AutoGenerateColumns = false;
            dgvProductionPlan.ColumnCount = 12;

            dgvProductionPlan.Columns[0].Name = "contract";
            dgvProductionPlan.Columns[0].DataPropertyName = "contract";
            dgvProductionPlan.Columns[0].HeaderText = "Договор";
            dgvProductionPlan.Columns[0].Visible = true;
            dgvProductionPlan.Columns[0].Width = 50;
            
            dgvProductionPlan.Columns[1].Name = "stage_name";
            dgvProductionPlan.Columns[1].DataPropertyName = "stage_name";
            dgvProductionPlan.Columns[1].HeaderText = "Стадия";
            dgvProductionPlan.Columns[1].Visible = true;

            dgvProductionPlan.Columns[2].Name = "kks";
            dgvProductionPlan.Columns[2].DataPropertyName = "kks";
            dgvProductionPlan.Columns[2].HeaderText = "KKS";
            dgvProductionPlan.Columns[2].Visible = true;
            dgvProductionPlan.Columns[2].Width = 50;

            dgvProductionPlan.Columns[3].Name = "building";
            dgvProductionPlan.Columns[3].DataPropertyName = "building";
            dgvProductionPlan.Columns[3].HeaderText = "Сооружение";
            dgvProductionPlan.Columns[3].Visible = true;

            dgvProductionPlan.Columns[4].Name = "set_code";
            dgvProductionPlan.Columns[4].DataPropertyName = "set_code";
            dgvProductionPlan.Columns[4].HeaderText = "Задача";
            dgvProductionPlan.Columns[4].Visible = true;
            dgvProductionPlan.Columns[4].Width = 50;

            dgvProductionPlan.Columns[5].Name = "set_name";
            dgvProductionPlan.Columns[5].DataPropertyName = "set_name";
            dgvProductionPlan.Columns[5].HeaderText = "Наименование задачи";
            dgvProductionPlan.Columns[5].Visible = true;

            dgvProductionPlan.Columns[6].Name = "set_start";
            dgvProductionPlan.Columns[6].DataPropertyName = "set_start";
            dgvProductionPlan.Columns[6].HeaderText = "Начало";
            dgvProductionPlan.Columns[6].Visible = true;

            dgvProductionPlan.Columns[7].Name = "set_end";
            dgvProductionPlan.Columns[7].DataPropertyName = "set_end";
            dgvProductionPlan.Columns[7].HeaderText = "Окончание";
            dgvProductionPlan.Columns[7].Visible = true;

            dgvProductionPlan.Columns[8].Name = "agreed_status";
            dgvProductionPlan.Columns[8].DataPropertyName = "agreed_status";
            dgvProductionPlan.Columns[8].HeaderText = "Статус согласования";
            dgvProductionPlan.Columns[8].Visible = true;

            dgvProductionPlan.Columns[9].Name = "executors";
            dgvProductionPlan.Columns[9].DataPropertyName = "executors";
            dgvProductionPlan.Columns[9].HeaderText = "Исполнители";
            dgvProductionPlan.Columns[9].Visible = true;

            dgvProductionPlan.Columns[10].Name = "percent_complete";
            dgvProductionPlan.Columns[10].DataPropertyName = "percent_complete";
            dgvProductionPlan.Columns[10].HeaderText = "Процент выполнения";
            dgvProductionPlan.Columns[10].Visible = true;
            dgvProductionPlan.Columns[10].Width = 50;

            dgvProductionPlan.Columns[11].Name = "comment_gip";
            dgvProductionPlan.Columns[11].DataPropertyName = "comment_gip";
            dgvProductionPlan.Columns[11].HeaderText = "Причина отклонения";
            dgvProductionPlan.Columns[11].Visible = true;
            dgvProductionPlan.Columns[11].Width = 150;

        }

        private void _dtProductionPlan_Create()
        {
            if (_dtProductionPlan == null)
                _dtProductionPlan = new DataTable("dtProductionPlan");
            else
                _dtProductionPlan.Rows.Clear();
            _dtProductionPlan.Columns.Add("contract", typeof(string));
            _dtProductionPlan.Columns.Add("stage_name", typeof(string));
            _dtProductionPlan.Columns.Add("kks", typeof(string));
            _dtProductionPlan.Columns.Add("building", typeof(string));
            _dtProductionPlan.Columns.Add("set_code", typeof(string));
            _dtProductionPlan.Columns.Add("set_name", typeof(string));
            _dtProductionPlan.Columns.Add("set_start", typeof(string));
            _dtProductionPlan.Columns.Add("set_end", typeof(string));
            _dtProductionPlan.Columns.Add("agreed_status", typeof(string));
            _dtProductionPlan.Columns.Add("executors", typeof(string));
            _dtProductionPlan.Columns.Add("percent_complete", typeof(string));
            _dtProductionPlan.Columns.Add("comment_gip", typeof(string));
        }
        private void Production_Plan_Load(List<clSet> _plan)
        {
            foreach (var dr in _plan)
            {
                DataRow row = _dtProductionPlan.NewRow();
                row["contract"] = dr.Contract.ToString();
                row["stage_name"] = dr.StageName.ToString();
                //row["kks"] = dr.Kks.ToString();
                row["building"] = dr.Building.ToString();
                row["set_code"] = dr.SetCode.ToString();
                row["set_name"] = dr.SetName.ToString();              
                row["set_start"] = dr.SetStart;
                row["set_end"] = dr.SetEnd; 
                row["executors"] = dr.Executors.ToString();
                row["agreed_status"] = dr.AgreedStatus.ToString();
                //row["percent_complete"] = dr.PercentComplete.ToString();
                //row["comment_gip"] = dr.CommentGip.ToString();
                _dtProductionPlan.Rows.Add(row);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveGridToExcell(dgvProductionPlan);
        }

        private void SaveGridToExcell(DataGridView grid)
        {
            if ((grid.Columns == null) || (grid.Columns.Count <= 0))
                return;

            this.Cursor = Cursors.WaitCursor;

            Excel.Application _excel = null;
            try
            {
                _excel = new Excel.Application();
                _excel.SheetsInNewWorkbook = 1;
                _excel.Workbooks.Add(Type.Missing);
                Excel.Workbook _workbook = _excel.Workbooks[1];
                Excel.Worksheet _sheet = (Excel.Worksheet)_workbook.Worksheets.get_Item(1);
                Excel.Range rng;
                _excel.Visible = false;
                _excel.ScreenUpdating = false;

                int col_index = 0;
                int row_index = 0;
                DateTime dt;

                foreach (DataGridViewColumn col in grid.Columns)
                {
                    row_index = 1;

                    if (col.Visible)
                    {
                        col_index++;
                        rng = (Excel.Range)_sheet.Cells[row_index, col_index];
                        _sheet.Cells[row_index, col_index] = col.HeaderText;

                        if (DateTime.TryParse(col.Name, out dt))
                        {
                            rng.ColumnWidth = 8;
                        }
                        else
                        {
                            rng.ColumnWidth = 16;
                        }


                        rng.WrapText = true;
                        rng.VerticalAlignment = Excel.Constants.xlTop; //EL_1

                        for (int r = 0; r < grid.RowCount; r++)
                        {
                            _sheet.Cells[r + 2, col_index] = grid.Rows[r].Cells[col.Name].Value.ToString();
                            rng = (Excel.Range)_sheet.Cells[r + 2, col_index];
                            rng.WrapText = true;
                            rng.VerticalAlignment = Excel.Constants.xlTop; //EL_1
                        }
                    }
                }
                _excel.ScreenUpdating = true;
                _excel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                if (_excel != null)
                {
                    _excel.ScreenUpdating = true;
                    _excel.Visible = true;
                    Marshal.ReleaseComObject(_excel);
                }
                GC.GetTotalMemory(true);
                this.Cursor = Cursors.Default;
            }
        }

    }
}
