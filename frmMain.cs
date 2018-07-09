using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DgvFilterPopup;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace sprut
{
    public partial class frmMain : Form
    {
        public delegate void delegateIndividualPlanAddOtherWork(string stage_guid, string set_guid, string contract, string stage_name);

        clSprut sprut = null;

        private static string path_report_accounting = @"";
        public static string FioUser;
        public static string FioPrms;
        public static string TdmsName;
        public static string TdmsSrv;
        public static string UserSqlName;
        public static string UserSqlPassword;
        public static string SqlLink;

        DgvFilterManager FilterManagerSets = null;
        DgvFilterManager FilterManagerUsers = null;
        DgvFilterManager FilterManagerAgreement = null;

        DateTimePicker dgvSelectedPlan_DateTimePicker;

        Dictionary<string, string> dict_speciality = new Dictionary<string, string>();


        #region Цвета раскраски ячеек
        Color color_fact = Color.DarkOrange;
        Color color_plan = Color.LightBlue;
        Color color_plan_and_fact = Color.DeepSkyBlue;

        Color color_resource_free = Color.LightCoral;
        Color color_resource_busy = Color.SkyBlue;
        Color color_holiday = Color.FromArgb(244, 244, 140);//Color.FromArgb(180, 180, 180);
        Color color_holiday_header = Color.Yellow;
        Color color_current_day = Color.Green;
        Color color_current_day_header = Color.GreenYellow;
        Color color_all_day_fact = Color.FromArgb(170, 170, 200);
        Color color_work_day_header = Color.FromArgb(200, 200, 200);
        Color color_auto_work_day_header = Color.FromArgb(154, 205, 50);
        Color color_selected_cell = Color.Blue;
        Color color_skud = Color.LightGray;
        #endregion

        #region DragAndDrop
        Rectangle dragBoxFromMouseDownUserToSet;
        DataGridViewRow valueFromMouseDownUserToSet;
        #endregion

        public frmMain()
        {
            InitializeComponent();

            this.dgvSelectedPlan_DateTimePicker = new DateTimePicker();
            this.dgvSelectedPlan_DateTimePicker.CloseUp += new EventHandler(dgvSelectedPlan_DateTimePickerCloseUp);
            this.dgvSelectedPlan_DateTimePicker.Visible = false;
            this.dgvSelectedPlan.Controls.Add(dgvSelectedPlan_DateTimePicker);
        }

        private void dgvSelectedPlan_DateTimePickerCloseUp(object sender, EventArgs e)
        {
            try
            {
                dgvSelectedPlan_DateTimePicker.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                DateTime plan_start = new DateTime();
                DateTime plan_end = new DateTime();
                DateTime date_tek = dtpIndividualPlan.Value;
                DateTime date_tek_end = new DateTime(date_tek.Year, date_tek.Month, DateTime.DaysInMonth(date_tek.Year, date_tek.Month), 0, 0, 0);
                DateTime current_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                int row, col;

                row = dgvSelectedPlan.CurrentCell.RowIndex;
                col = dgvSelectedPlan.CurrentCell.ColumnIndex;

                if ((row < 0) || (col < 0))
                    return;

                string set_guid = "";
                string stage_guid = "";
                string executor_id = "";
                string dept_id = "";  //IzmPl

                DataGridViewCell dgv_cell = dgvSelectedPlan.CurrentCell;
                DataGridViewColumn dgv_column = dgv_cell.OwningColumn;
                DataGridViewRow dgv_row = dgv_cell.OwningRow;

                set_guid = dgvSelectedPlan.Rows[row].Cells["SetGuid"].Value.ToString();
                stage_guid = dgvSelectedPlan.Rows[row].Cells["StageGuid"].Value.ToString();
                executor_id = dgvSelectedPlan.Rows[row].Cells["Executorid"].Value.ToString();
                dept_id = dgvSelectedPlan.Rows[row].Cells["Deptid"].Value.ToString(); //IzmPl

                if (dgv_column.Name == "PlanStart")
                {
                    plan_start = new DateTime(dgvSelectedPlan_DateTimePicker.Value.Year, dgvSelectedPlan_DateTimePicker.Value.Month, dgvSelectedPlan_DateTimePicker.Value.Day, 0, 0, 0);
                    if (plan_start < current_date)
                    {
                        plan_start = current_date;
                    }
                    dgvSelectedPlan.CurrentCell.Value = plan_start;
                }
                if (dgv_column.Name == "PlanEnd")
                {
                    plan_end = new DateTime(dgvSelectedPlan_DateTimePicker.Value.Year, dgvSelectedPlan_DateTimePicker.Value.Month, dgvSelectedPlan_DateTimePicker.Value.Day, 0, 0, 0);
                    if (plan_end < current_date)
                    {
                        plan_end = current_date.AddDays(1);
                    }
                    else
                    {
                        if (plan_end > date_tek_end)
                        {
                            plan_end = date_tek_end;
                        }
                    }
                    dgvSelectedPlan.CurrentCell.Value = plan_end;
                }

                try
                {
                    plan_start = Convert.ToDateTime(dgvSelectedPlan.Rows[row].Cells["PlanStart"].Value.ToString());
                    plan_end = Convert.ToDateTime(dgvSelectedPlan.Rows[row].Cells["PlanEnd"].Value.ToString());

                    if (plan_end < plan_start)
                    {
                        plan_end = plan_start;
                        dgvSelectedPlan.Rows[row].Cells["PlanStart"].Value = plan_start.ToShortDateString();
                        dgvSelectedPlan.Rows[row].Cells["PlanEnd"].Value = plan_end.ToShortDateString();
                    }
                }
                catch
                {
                    //дата начала или окончания работ не установлена
                    return;
                }

                //Удалить все табельные записи левее даты начала планирования
                sprut.DepartmentPlan_TableRecords_Delete(set_guid, executor_id);


                for (DateTime dt = plan_start; dt <= plan_end; dt = dt.AddDays(1))
                {
                    sprut.DepartmentPlan_CreateTableRecord(set_guid, stage_guid,executor_id, dept_id,dt, 0, -9);
                }

                DateTime column_date;
                for (int j = 0; j < dgvSelectedPlan.Columns.Count; j++)
                {
                    if (DateTime.TryParse(dgvSelectedPlan.Columns[j].Name, out column_date))
                    {
                        dgvSelectedPlan_UpdateColumnCells(column_date.ToShortDateString());
                        dgvSelectedPlan_ChangeHeaderColor(column_date.ToShortDateString(), column_date);
                    }
                }
                sprut.DepartmentPlan_IsSetInMonthPlan(set_guid, dtpDepartmentPlan.Value);

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

        private void dgvSelectedPlan_UpdateColumnCells(string column_name)
        {
            int col = -1;
            col = dgvSelectedPlan.Columns[column_name].Index;
            dgvSelectedPlan_UpdateColumnCells(col);
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            sprut = new clSprut(dtpDepartmentPlan.Value);
            this.Text = "ПЛАТАН (1.2) " + sprut.CurrentUser_GetDepartmentSmall() + ". " + sprut.CurrentUser_GetFio();
            Create_Dictionary_UserSpeciality();
            Create_dgvDepartmentPlan();
            Create_DgvDepartmentUsers();
            Create_DgvSelectedPlan();
            Create_DgvIndividualPlan();
            Create_dgvAgreement();
            //Create_dgvProductionPlanDept();  //Platan произв план
            //Create_dgvProductionPlan();  //Platan произв план
            //ProductionPlanDept_UpDate(dtpDepartmentPlan.Value);  //Platan произв план
            Create_LstDepartments();
            //Create_LstAllDepartments();
            //Create_dgvDepartments();//platan

            //textPricePlan.Text = sprut.PricePlanpp(dtpDepartmentPlan.Value);//Platan произв план
            //textFotPlan.Text = sprut.PriceFot(dtpDepartmentPlan.Value);//Platan произв план
            
            string permission = sprut.CurrentUser_GetPermission();

            DisableUserControls(permission);

            FilterManagerSets = new DgvFilterManager(dgvDepartmentPlan);
            FilterManagerUsers = new DgvFilterManager(dgvDepartmentUsers);
            FilterManagerAgreement = new DgvFilterManager(dgvAgreement);
            
        }

        private void Create_Dictionary_UserSpeciality()
        {
            if (dict_speciality == null)
                dict_speciality = new Dictionary<string, string>();
            else
                dict_speciality.Clear();
            // добавить новые из структуры платана
            //dict_speciality.Add("GL", "Генплан");
            //dict_speciality.Add("GT", "Генплан");
            //dict_speciality.Add("FP", "ВиК");
            //dict_speciality.Add("WS", "ВиК");
            //dict_speciality.Add("HV", "ОиВ");
            //dict_speciality.Add("AC", "Архитектурный");
            //dict_speciality.Add("RC", "Строительный");
        }

        private void Create_LstAllDepartments()
        {
            //cmbxAgreement_Departments.DataSource = sprut.AllDepartments;
        }

        private void DisableUserControls(string permission)
        {
            switch(permission)
            {
                case "executor" :
                    {
                        panelDepartments.Visible = false;
                        tabPageDepartmentPlan.Parent = null;
                        labelSelectedUser.Enabled = false;
                        tabPageAgreement.Parent = null;
                        tabPageProductionPlan.Parent = null; 
                        tabPageProductionPlanDept.Parent = null; 
                        tsmi_Reports.Enabled = false;                    
                        tsmi_Reports.Visible = false;                       
                        break;
                    }
                case "planner" :
                    {
                        panelDepartments.Visible = true;
                        tabPageAgreement.Parent = null;
                        tabPageProductionPlan.Parent = null; 
                        tabPageProductionPlanDept.Parent = null; 
                        //btnDepartmentPlan_Approve.Enabled = false;
                        //btnDepartmentPlan_Approve.Visible = false;
                        break;
                    }

                case "viewer":
                    {
                        panelDepartments.Visible = true;
                        tabPageAgreement.Parent = null;
                        tabPageProductionPlan.Parent = null;
                        tabPageProductionPlanDept.Parent = null;
                        btnAgreeSetPlaned.Visible = false;
                        btnDepartmentPlan_AddSet.Visible = false;
                        btnDepartmentPlan_Save.Visible = false;
                        //btnDepartmentPlan_Approve.Enabled = false;
                        //btnDepartmentPlan_Approve.Visible = false;
                        break;
                    }


                case "head" :
                    {
                        panelDepartments.Visible = true;
                        tabPageAgreement.Parent = null;
                        tabPageProductionPlan.Parent = null;  
                        tabPageProductionPlanDept.Parent = null; 
                        break;
                    }
                case "gip" :
                    {
                        tabPageProductionPlan.Parent = null; 
                        tabPageProductionPlanDept.Parent = null; 
                        btnDepartmentPlan_Save.Visible = false;
                        btnAgreeSetPlaned.Visible = false;
                        btnDepartmentPlan_AddSet.Visible = false;
                        //btnProductionPlan_Approve.Enabled = false;
                        //btnProductionPlan_Approve.Visible = false;
                        //btnProductionPlan_Reject.Enabled = false;
                        //btnProductionPlan_Reject.Visible = false;
                        dtpApprovedPlan.SelectedIndex = 2;
                        break;
                    }
                case "root" :
                    {
                        tabPageProductionPlan.Parent = null;
                        tabPageProductionPlanDept.Parent = null; 
                        btnDepartmentPlan_Save.Visible = false;
                        tabPageProductionPlanDept.Parent = null; 
                        btnAgreeSetPlaned.Visible = false;
                        btnDepartmentPlan_AddSet.Visible = false;
                        //btnDepartmentPlan_Approve.Enabled = false;
                        //btnDepartmentPlan_Approve.Visible = false;
                        break;
                    }
                default :
                    {
                        //cmbxDepartmentPlan_Departments.Visible = false;
                        cmbxDepartmentPlan_Departments.Enabled = false;
                        tabPageDepartmentPlan.Parent = null;
                        labelSelectedUser.Enabled = false;
                        tabPageAgreement.Parent = null;
                        tabPageProductionPlan.Parent = null; //EL_2
                        tabPageProductionPlanDept.Parent = null; //EL_2
                        break;
                    }
            }
        }

        private void Create_LstDepartments()
        {
            cmbxDepartmentPlan_Departments.DataSource = sprut.ListDepartments;
            cmbxDepartmentPlan_Departments.DisplayMember = "FullName";
            cmbxDepartmentPlan_Departments.ValueMember = "Id";
                
        }

        private void Create_dgvAgreement()
        {
            if (dgvAgreement == null)
                return;

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(clRequest));
            if ((properties == null) || (dgvAgreement == null))
            {
                MessageBox.Show("Неудалось создать таблицу согласования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvAgreement.AutoGenerateColumns = false;
            dgvAgreement.ColumnCount = properties.Count;
            int col = 0;
            foreach (PropertyDescriptor prop in properties)
            {
                Create_dgvAgreement_Column(prop.Name, col);
                col++;
            }

           
            dgvAgreement.DataSource = sprut.dtAgreement;
        }

        private void Create_dgvAgreement_Column(string p, int col)
        {
            string ColumnName = p;
            string ColumnDataPropertyName = p;
            string ColumnHeaderText = p;
            bool ColumnVisible = true;
            bool ColumnReadOnly = true;
            int ColumnWidth = 40;

            switch (p)
            {

                case "Contract":
                    {
                        ColumnHeaderText = "Шифр проекта";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 58;
                        break;
                    }
                case "Id":
                    {
                        ColumnHeaderText = "ID";
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        ColumnWidth = 0;
                        break;
                    }
                case "Fio":
                    {
                        ColumnHeaderText = "Заявитель";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 60;
                        break;
                    }

                case "StartDate":
                    {
                        ColumnHeaderText = "Запрошенная дата начала";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 60;
                        break;
                    }
                case "EndDate":
                    {
                        ColumnHeaderText = "Запрошенная дата окончания";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 60;
                        break;
                    }
                case "RequestDate":
                    {
                        ColumnHeaderText = "Дата запроса";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 60;
                        break;
                    }
                case "Reason":
                    {
                        ColumnHeaderText = "Причина";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 110;
                        break;
                    }

                case "DepartmentId":
                    {
                        ColumnHeaderText = p;
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }

                case "CommentRequest":
                    {
                        ColumnHeaderText = "Комментарий исполнителя";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 130;
                        break;
                    }
                case "CommentResponse":
                    {
                        ColumnHeaderText = "Комментарий ГИПа";
                        ColumnVisible = true;
                        ColumnReadOnly = false;
                        ColumnWidth = 130;
                        break;
                    }

                case "UserId":
                    {
                        ColumnHeaderText = p;
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }

                case "StageName":
                    {
                        ColumnHeaderText = "Наименование проекта";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 100;
                        break;
                    }

                //case "KKS":
                //    {
                //        ColumnHeaderText = "KKS";
                //        ColumnVisible = false;
                //        ColumnReadOnly = true;
                //        ColumnWidth = 40;
                //        break;
                //    }

                case "Building":
                    {
                        ColumnHeaderText = "Титул";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 86;
                        break;
                    }
                case "SetCode":
                    {
                        ColumnHeaderText = "Задача";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 80;
                        break;
                    }
                case "SetName":
                    {
                        ColumnHeaderText = "Наименование задачи";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 68;
                        break;
                    }
                case "GipStart":
                    {
                        ColumnHeaderText = "Начало по графику";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 60;
                        break;
                    }
                case "GipEnd":
                    {
                        ColumnHeaderText = "Окончание по графику";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 60;
                        break;
                    }
                case "SetGuid":
                    {
                        ColumnHeaderText = p;
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "StageGuid":
                    {
                        ColumnHeaderText = p;
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "DepartmentSmallName":
                    {
                        ColumnHeaderText = "Отдел";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "AgreedStatus" :
                    {
                        ColumnHeaderText = "Статус";
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        ColumnWidth = 50;
                        break;
                    }
                case "TransferToPrj":
                    {
                        ColumnHeaderText = "Отметка о переносе";
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        ColumnWidth = 70;
                        break;
                    }
                case "BtnAgree":
                    {
                        ColumnHeaderText = "Согласовать";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 76;
                        break;
                    }
                case "BtnDisAgree":
                    {
                        ColumnHeaderText = "Отклонить";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 76;
                        break;
                    }
                case "BtnTransferToProject":
                    {
                        ColumnHeaderText = "Перенесено";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 76;
                        break;
                    }
                case "VisibleAgreedStatus":
                    {
                        ColumnHeaderText = "Статус";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        ColumnWidth = 76;
                        break;
                    }
                default:
                    {
                        ColumnHeaderText = p;
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        ColumnWidth = 40;
                        break;
                    }
            }

            dgvAgreement.Columns[col].Name = ColumnName;
            dgvAgreement.Columns[col].DataPropertyName = ColumnDataPropertyName;
            dgvAgreement.Columns[col].HeaderText = ColumnHeaderText;
            dgvAgreement.Columns[col].Visible = ColumnVisible;
            dgvAgreement.Columns[col].ReadOnly = ColumnReadOnly;
            dgvAgreement.Columns[col].Width = ColumnWidth;
        }

        private void Create_DgvIndividualPlan()
        {
            if (dgvIndividualPlan == null)
                return;
            dgvIndividualPlan.DataSource = sprut.dtIndividualPlan;
        }

        //EL_2
        //private void Create_dgvProductionPlanDept()
        //{
        //    if (dgvProductionPlanDept == null)
        //        return;
        //    dgvProductionPlanDept.DataSource = sprut.dtIndividualPlan;
        //}

        private void Create_dgvDepartmentPlan()
        {
            if (dgvDepartmentPlan == null)
                return;

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(clSet));
            if ((properties == null) || (dgvDepartmentPlan == null))
            {
                MessageBox.Show("Неудалось создать таблицу задач.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvDepartmentPlan.AutoGenerateColumns = false;
            dgvDepartmentPlan.ColumnCount = properties.Count;
             
            int col = 0;
            foreach (PropertyDescriptor prop in properties)
            {
                Create_dgvDepartmentPlan_Column(prop.Name, col);
                col++;
            }

            dgvDepartmentPlan.DataSource = sprut.dtDepartmentPlan;
        }

        private void Create_dgvDepartmentPlan_Column(string p, int col)
        {
            string ColumnName = p;
            string ColumnDataPropertyName = p;
            string ColumnHeaderText = p;
            bool ColumnVisible = true;
            bool ColumnReadOnly = true;
            int ColumnWidth = 0;
            DataGridViewContentAlignment ColumnAlignment = DataGridViewContentAlignment.MiddleLeft;

            switch (p)
            {
                case "IsMonthPlan":
                    {
                        ColumnHeaderText = "МП";
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        ColumnWidth = 15;
                        break;
                    }

                //case "IsAgreementNeed":
                //    {
                //        ColumnHeaderText = "ТС";
                //        ColumnVisible = true;
                //        ColumnReadOnly = true;
                //        break;
                //    }

                case "Contract":
                    {
                        ColumnHeaderText = "Шифр проекта";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "StageName":
                    {
                        ColumnHeaderText = "Наименование проекта";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                //case "Kks":
                //    {
                //        ColumnHeaderText = "KKS";
                //        ColumnVisible = false;
                //        ColumnReadOnly = true;
                //        break;
                //    }
                case "Building":
                    {
                        ColumnHeaderText = "Титул";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "SetCode":
                    {
                        ColumnHeaderText = "Задача";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "SetName":
                    {
                        ColumnHeaderText = "Наименование задачи";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "GipStart":
                    {
                        ColumnHeaderText = "Начало работ по плану";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "GipEnd":
                    {
                        ColumnHeaderText = "Окончание работ по плану";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "AgreedStart":
                    {
                        ColumnHeaderText = "Согласованное начало";
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "AgreedEnd":
                    {
                        ColumnHeaderText = "Согласованное окончание";
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "AgreedStatus":
                    {
                        ColumnHeaderText = "Статус согласования";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "SetStart":
                    {
                        ColumnHeaderText = "Ожидаемое начало работ";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "SetEnd":
                    {
                        ColumnHeaderText = "Ожидаемое окончание работ";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "SetGuid":
                    {
                        ColumnHeaderText = p;
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "StageGuid":
                    {
                        ColumnHeaderText = p;
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "StatusFromTdms":
                    {
                        ColumnHeaderText = "Статус задачи в ТДМС";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }

                case "Executors" :
                    {
                        ColumnHeaderText = "Исполнители";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                //case "Mark" :
                //    {
                //        ColumnHeaderText = p;
                //        ColumnVisible = false;
                //        ColumnReadOnly = true;
                //        break;
                //    }

                case "CommentGip":
                    {
                        ColumnHeaderText = "Комментарий ГИПа";
                        ColumnVisible = true; //EL_2
                        ColumnReadOnly = true;
                        break;
                    }

                
                case "ResponsibleUser" :
                    {
                        ColumnHeaderText = "Ответственный";    //"Ответственный";
                        ColumnVisible = true; //EL_2
                        ColumnReadOnly = true;
                        break;
                    }
                //case "PercentComplete":
                //    {
                //        ColumnHeaderText = "Процент выполнения";
                //        ColumnVisible = false;
                //        ColumnReadOnly = false;
                //        break;
                //    }
                //case "PriceMng":
                //    {
                //        ColumnHeaderText = "Управленческая стоимость (руб.)";
                //        ColumnVisible = true;
                //        ColumnReadOnly = true;
                //        ColumnAlignment = DataGridViewContentAlignment.MiddleRight;
                //        break;
                //    }
                //case "PriceDog":
                //    {
                //        ColumnHeaderText = "Договорная стоимость (руб.)";
                //        ColumnVisible = false;
                //        ColumnReadOnly = true;
                //        break;
                //    }
                //case "PriceDop":
                //    {
                //        ColumnHeaderText = "Доп.соглашение стоимость (руб.)";
                //        ColumnVisible = true;
                //        ColumnReadOnly = true;
                //        ColumnAlignment = DataGridViewContentAlignment.MiddleRight;
                //        break;
                //    }

                //case "DopRev":
                //    {
                //        ColumnHeaderText = "Ревизия доп";
                //        ColumnVisible = true;
                //        ColumnReadOnly = true;
                //        ColumnWidth = 15;
                //        break;
                //    }
                //case "WorkHrPlan":
                //    {
                //        ColumnHeaderText = "Плановые ТРЗ";
                //        ColumnVisible = false;
                //        ColumnReadOnly = true;
                //        ColumnAlignment = DataGridViewContentAlignment.MiddleRight;
                //        break;
                //    }

                //case "WorkHrPlanTek":
                //    {
                //        ColumnHeaderText = "Плановые ТРЗ - отдел (чел.час)";
                //        ColumnVisible = true;
                //        ColumnReadOnly = true;
                //        ColumnAlignment = DataGridViewContentAlignment.MiddleRight;
                //        break;
                //    }

                //case "WorkHrDelta":
                //    {
                //        ColumnHeaderText = "Остаток по ТРЗ на 1 число тек.мес (чел.час)";
                //        ColumnVisible = true;
                //        ColumnReadOnly = true;
                //        ColumnAlignment = DataGridViewContentAlignment.MiddleRight;
                //        break;
                //    }

                //case "WorkHrMng":
                //    {
                //        ColumnHeaderText = "Плановые ТРЗ - ГИП (чел.час)";
                //        ColumnVisible = true;
                //        ColumnReadOnly = true;
                //        ColumnAlignment = DataGridViewContentAlignment.MiddleRight;
                //        break;
                //    }

                //case "WorkHrFakt":
                //    {
                //        ColumnHeaderText = "Фактические ТРЗ на 1 число тек.мес (чел.час)";
                //        ColumnVisible = true;
                //        ColumnReadOnly = true;
                //        ColumnAlignment = DataGridViewContentAlignment.MiddleRight;
                //        break;
                //    }
                default:
                    {
                        ColumnHeaderText = p;
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
            }

            dgvDepartmentPlan.Columns[col].Name = ColumnName;
            dgvDepartmentPlan.Columns[col].DataPropertyName = ColumnDataPropertyName;
            dgvDepartmentPlan.Columns[col].HeaderText = ColumnHeaderText;
            dgvDepartmentPlan.Columns[col].Visible = ColumnVisible;
            dgvDepartmentPlan.Columns[col].ReadOnly = ColumnReadOnly;
            dgvDepartmentPlan.Columns[col].Width = ColumnWidth > 0 ? ColumnWidth : dgvDepartmentPlan.Columns[col].Width;
            dgvDepartmentPlan.Columns[col].DefaultCellStyle.Alignment = ColumnAlignment;
        }

        private void Create_DgvDepartmentUsers()
        {
            if (dgvDepartmentUsers == null)
                return;
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(clUser));
            if ((properties == null) || (dgvDepartmentUsers == null))
            {
                MessageBox.Show("Неудалось создать таблицу сотрудников.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvDepartmentUsers.AutoGenerateColumns = false;
            dgvDepartmentUsers.ColumnCount = properties.Count;
            int col = 0;
            foreach (PropertyDescriptor prop in properties)
            {
                Create_DgvUsersColumn(prop.Name, col);
                col++;
            }
            dgvDepartmentUsers.DataSource = sprut.dtDepartmentUsers;
        }

        private void Create_DgvUsersColumn(string p, int col)
        {
            string ColumnName = p;
            string ColumnDataPropertyName = p;
            string ColumnHeaderText = p;
            bool ColumnVisible = true;
            bool ColumnReadOnly = true;

            switch (p)
            {
                case "FullName":
                    {
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "Fio":
                    {
                        ColumnHeaderText = "ФИО";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "Resource":
                    {
                        ColumnHeaderText = "Ресурс";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "LastName":
                    {
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "FirstName":
                    {
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "MiddleName":
                    {
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "UserId":
                    {
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "DepartmentFull":
                    {
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "DepartmentSmall":
                    {
                        ColumnHeaderText = "Отдел";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "Position":
                    {
                        ColumnHeaderText = "Должность";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "Speciality":
                    {
                        ColumnHeaderText = "Площадка";
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
                case "Phone":
                    {
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "DepartmentId":
                    {
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                case "Permission":
                    {
                        ColumnVisible = false;
                        ColumnReadOnly = true;
                        break;
                    }
                default:
                    {
                        ColumnHeaderText = p;
                        ColumnVisible = true;
                        ColumnReadOnly = true;
                        break;
                    }
            }

            dgvDepartmentUsers.Columns[col].Name = ColumnName;
            dgvDepartmentUsers.Columns[col].DataPropertyName = ColumnDataPropertyName;
            dgvDepartmentUsers.Columns[col].HeaderText = ColumnHeaderText;
            dgvDepartmentUsers.Columns[col].Visible = ColumnVisible;
            dgvDepartmentUsers.Columns[col].ReadOnly = ColumnReadOnly;
        }
        
        private void Create_DgvSelectedPlan()
        {
            if (dgvSelectedPlan == null)
                return;

            dgvSelectedPlan.DataSource = sprut.dtSelectedPlan;
        }

        private void picbxRefreshDepartmentPlan_MouseEnter(object sender, EventArgs e)
        {
            picbxRefreshDepartmentPlan.BorderStyle = BorderStyle.FixedSingle;
        }

        private void picbxRefreshDepartmentPlan_MouseLeave(object sender, EventArgs e)
        {
            picbxRefreshDepartmentPlan.BorderStyle = BorderStyle.Fixed3D;
        }

        private void labelSelectedUser_MouseEnter(object sender, EventArgs e)
        {
            labelSelectedUser.ForeColor = Color.Blue;
        }

        private void labelSelectedUser_MouseLeave(object sender, EventArgs e)
        {
            labelSelectedUser.ForeColor = Color.Black;
        }

        private void picbxRefreshIndividualPLan_MouseEnter(object sender, EventArgs e)
        {
            picbxRefreshIndividualPlan.BorderStyle = BorderStyle.FixedSingle;
        }

        private void picbxRefreshIndividualPLan_MouseLeave(object sender, EventArgs e)
        {
            picbxRefreshIndividualPlan.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picbxRefreshIndividualPLan_Click(object sender, EventArgs e)
        {
            IndividualPlan_ChangeMonth();
        }

        private void IndividualPlan_ChangeMonth()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                dgvIndividualPlan.DataSource = null;
                sprut.IndividualPlan_Update(dtpIndividualPlan.Value);
                dgvIndividualPlan.DataSource = sprut.dtIndividualPlan;
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

        private void dgvIndividualPlan_DataSourceChanged(object sender, EventArgs e)
        {
            dgvUserPlan_HideColumns();
        }

        private void dgvUserPlan_HideColumns()
        {
            
            if ((dgvIndividualPlan == null) || (dgvIndividualPlan.DataSource == null))
                return;


            //Цикл по всем стобцам
            for (int j = 0; j < dgvIndividualPlan.ColumnCount; j++)
            {
                try
                {
                    dgvIndividualPlan.Rows[0].Frozen = true;
                    //dgvIndividualPlan.Rows[1].Frozen = true;
                    dgvIndividualPlan.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                    string column_name = dgvIndividualPlan.Columns[j].Name;
                    DateTime column_date;
                    if (DateTime.TryParse(column_name, out column_date))
                    {
                        dgvIndividualPlan.Columns[j].Width = 37;
                    }
                    else
                    {
                        switch (column_name)
                        {
                            case "param":
                                {
                                    dgvIndividualPlan.Columns[j].Visible = true;
                                    break;
                                };
                            case "Contract":
                                {
                                    dgvIndividualPlan.Columns[j].Width = 60;
                                    dgvIndividualPlan.Columns[j].HeaderText = "Шифр проекта";
                                    break;
                                };
                            case "StageName":
                                {
                                    dgvIndividualPlan.Columns[j].Width = 70;
                                    dgvIndividualPlan.Columns[j].HeaderText = "Проект";
                                    break;
                                };
                            case "StageGuid":
                                {
                                    dgvIndividualPlan.Columns[j].Visible = false;
                                    break;
                                };

                            //case "SetKks":
                            //    {
                            //        dgvIndividualPlan.Columns[j].Width = 70;
                            //        dgvIndividualPlan.Columns[j].HeaderText = "KKS сооружения";
                            //        dgvIndividualPlan.Columns[j].Visible = false;
                            //        break;
                            //    };
                            case "Building":
                                {
                                    dgvIndividualPlan.Columns[j].Width = 70;
                                    dgvIndividualPlan.Columns[j].HeaderText = "building";
                                    dgvIndividualPlan.Columns[j].Visible = false;
                                    break;
                                };
                            case "SetCode":
                                {
                                    dgvIndividualPlan.Columns[j].Width = 80;
                                    dgvIndividualPlan.Columns[j].HeaderText = "set_code";
                                    dgvIndividualPlan.Columns[j].Visible = false;
                                    break;
                                };
                            case "SetName":
                                {
                                    dgvIndividualPlan.Columns[j].Width = 90;
                                    dgvIndividualPlan.Columns[j].HeaderText = "Наименование задачи";
                                    dgvIndividualPlan.Columns[j].Visible = false;
                                    break;
                                };
                            case "SetGuid":
                                {
                                    dgvIndividualPlan.Columns[j].Visible = false;
                                    break;
                                };
                            case "executor_fio":
                                {
                                    dgvIndividualPlan.Columns[j].Width = 80;
                                    dgvIndividualPlan.Columns[j].HeaderText = "Исполнитель";
                                    break;
                                };
                            case "executor_login":
                                {
                                    dgvIndividualPlan.Columns[j].Visible = false;
                                    break;
                                };
                            case "executor_id":
                                {
                                    dgvIndividualPlan.Columns[j].Visible = false;
                                    break;
                                };
                            case "plan_start":
                                {
                                    dgvIndividualPlan.Columns[j].Width = 66;
                                    dgvIndividualPlan.Columns[j].HeaderText = "Начало";
                                    break;
                                };
                            case "plan_end":
                                {
                                    dgvIndividualPlan.Columns[j].Width = 66;
                                    dgvIndividualPlan.Columns[j].HeaderText = "Окончание";
                                    break;
                                };
                            case "GipStart":
                                {
                                    dgvIndividualPlan.Columns[j].Visible = false;
                                    dgvIndividualPlan.Columns[j].HeaderText = "GipStart";
                                    break;
                                };
                            case "GipEnd":
                                {
                                    dgvIndividualPlan.Columns[j].Visible = true;
                                    dgvIndividualPlan.Columns[j].HeaderText = "Окончание";
                                    dgvIndividualPlan.Columns[j].Width = 70;
                                    //dgvUserPlan.Columns[j].Frozen = true;
                                    break;
                                };
                            case "SetKksBuilding":
                                {
                                    dgvIndividualPlan.Columns[j].Visible = true;
                                    dgvIndividualPlan.Columns[j].HeaderText = "Сооружение";
                                    dgvIndividualPlan.Columns[j].Width = 60;
                                    //dgvUserPlan.Columns[j].Frozen = true;
                                    break;
                                };
                            case "SetCodeName":
                                {
                                    dgvIndividualPlan.Columns[j].Visible = true;
                                    dgvIndividualPlan.Columns[j].Width = 88;
                                    dgvIndividualPlan.Columns[j].HeaderText = "Задача";
                                    dgvIndividualPlan.Columns[j].Frozen = true;
                                    break;
                                };
                            default:
                                {
                                    break;
                                }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
            }

            dgvIndividualPlan_DrawColorAllCells();
        }

        private void dgvIndividualPlan_DrawColorAllCells()
        {
            dgvIndividualPlan.Rows[0].Frozen = true;
            //dgvIndividualPlan.Rows[1].Frozen = true;

            //Цикл по всем столбцам
            for (int j = 0; j < dgvIndividualPlan.ColumnCount; j++)
            {
                string column_name = dgvIndividualPlan.Columns[j].Name;
                dgvIndividualPlan_RefreshColumn(column_name);
            }

        }

        private void dgvIndividualPlan_RefreshColumn(string column_name)
        {
            if (dgvIndividualPlan.RowCount <= 0)
                return;


            dgvIndividualPlan.Columns["param"].Visible = false;
            
            DateTime work_date;
            for (int i = 0; i < dgvIndividualPlan.RowCount; i++)
            {
                string param = dgvIndividualPlan.Rows[i].Cells["param"].Value.ToString();
                
                if (DateTime.TryParse(column_name, out work_date))
                {
                    double day_work_time = sprut.GetWorkTime(work_date);

                    if (day_work_time <= 0)
                    {
                        dgvIndividualPlan.Columns[column_name].HeaderCell.Style.BackColor = color_holiday_header;
                    }
                    else
                    {
                        dgvIndividualPlan.Columns[column_name].HeaderCell.Style.BackColor = color_work_day_header;
                    }

                    if (work_date.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                    {
                        dgvIndividualPlan.Columns[column_name].HeaderCell.Style.BackColor = color_current_day_header;
                    }
                    
                    
                    switch (param)
                    {
                        //Строка с фактом
                        case "fact":
                            {
                                string set_guid = dgvIndividualPlan.Rows[i].Cells["SetGuid"].Value.ToString();
                                string stage_guid = dgvIndividualPlan.Rows[i].Cells["StageGuid"].Value.ToString();

                                clTableRecord tr = sprut.IndividualPlan_GetTableRecord(set_guid, work_date);

                                if (tr != null)
                                {
                                    //Табельная запись существует
                                    if ((tr.WorkPlan == 0) && (tr.WorkFact == 0))
                                    {
                                        dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Style.BackColor = Color.White;
                                        dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Value = tr.WorkFact;
                                        dgvIndividualPlan.Rows[i - 1].Cells[work_date.ToShortDateString()].Value = tr.WorkPlan;

                                    }
                                    else
                                        if ((tr.WorkPlan == 0) && (tr.WorkFact > 0))
                                        {
                                            dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Style.BackColor = color_fact;
                                            dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Value = tr.WorkFact;
                                            dgvIndividualPlan.Rows[i - 1].Cells[work_date.ToShortDateString()].Value = tr.WorkPlan;

                                        }
                                        else
                                            if ((tr.WorkPlan > 0) && (tr.WorkFact == 0))
                                            {
                                                dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Style.BackColor = color_plan;
                                                dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Value = tr.WorkFact;
                                                dgvIndividualPlan.Rows[i - 1].Cells[work_date.ToShortDateString()].Value = tr.WorkPlan;

                                            }
                                            else
                                                if ((tr.WorkPlan > 0) && (tr.WorkFact > 0))
                                                {
                                                    dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Style.BackColor = color_plan_and_fact;
                                                    dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Value = tr.WorkFact;
                                                    dgvIndividualPlan.Rows[i - 1].Cells[work_date.ToShortDateString()].Value = tr.WorkPlan;

                                                }
                                }
                                else
                                {
                                    //Такой табельной записи несуществует, возможно была удалена
                                    dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Style.BackColor = Color.White;
                                    dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Value = "";
                                    dgvIndividualPlan.Rows[i - 1].Cells[work_date.ToShortDateString()].Value = "";

                                    if (day_work_time <= 0)
                                    {
                                        dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Style.BackColor = color_holiday;
                                    }
                                }
                                break;
                            }
                        //Строка с планом
                        case "plan":
                            {
                                dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Style.BackColor = Color.LightGray;
                                break;
                            }

                        //Строка Итого
                        case "result":
                            {

                                double work_fact_day = 0;
                                work_fact_day = sprut.IndividualPlan_GetDayFact(work_date);
                                dgvIndividualPlan.Rows[i].Cells[work_date.ToShortDateString()].Value = work_fact_day;
                                dgvIndividualPlan.Rows[i].Cells[column_name].Style.BackColor = color_all_day_fact;
                                break;
                            }

                        //Строка с отработанным временем
                        //case "skud":
                        //    {
                        //        dgvIndividualPlan.Rows[i].Cells[column_name].Style.BackColor = color_skud;
                        //        break;
                        //    }
                    }
                }
                else
                {
                    if (param.Equals("fact"))
                    {
                        dgvIndividualPlan.Rows[i].Cells[column_name].Style.ForeColor = Color.Transparent;
                        dgvIndividualPlan.Rows[i].Cells[column_name].Style.SelectionForeColor = Color.Transparent;
                    }

                    if (param.Equals("plan"))
                    {
                        dgvIndividualPlan.Rows[i].Cells[column_name].Style.BackColor = Color.LightGray;
                    }

                    if (param.Equals("result"))
                    {
                        switch (column_name)
                        {
                            case "SetCodeName":
                                {
                                    dgvIndividualPlan.Rows[i].Cells[column_name].Style.BackColor = color_all_day_fact;
                                    break;
                                }
                            case "GipEnd":
                                {
                                    dgvIndividualPlan.Rows[i].Cells[column_name].Style.BackColor = color_all_day_fact;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }

                    //if (param.Equals("skud"))
                    //{
                        
                    //    switch(column_name)
                    //    {
                    //        case "SetCodeName" :
                    //            {
                    //                dgvIndividualPlan.Rows[i].Cells[column_name].Style.BackColor = color_skud;
                    //                break;
                    //            }
                    //        case "GipEnd" :
                    //            {
                    //                dgvIndividualPlan.Rows[i].Cells[column_name].Style.BackColor = color_skud;
                    //                break;
                    //            }
                    //        default :
                    //            {
                    //                break;
                    //            }
                    //    }
                    //}
                }
            }
        }

        private void dgvIndividualPlan_VisibleChanged(object sender, EventArgs e)
        {
            dgvIndividualPlan_DrawColorAllCells();
        }

        private void dgvIndividualPlan_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if ((row < 0) || (col < 0))
                return;

            string column_name = dgvIndividualPlan.Columns[col].Name;
            DateTime column_date;
            if (!DateTime.TryParse(column_name, out column_date))
                return;

            string set_guid = dgvIndividualPlan.Rows[row].Cells["SetGuid"].Value.ToString();
            string stage_guid = dgvIndividualPlan.Rows[row].Cells["StageGuid"].Value.ToString();

            string val_string = dgvIndividualPlan.Rows[row].Cells[col].Value.ToString();
            
            

            double val_double = 0.0;

            try
            {
                val_string = val_string.Replace('.', ',');
                val_double = Convert.ToDouble(val_string);
                if (val_double > 15)
                {
                    val_double = 15;
                    val_string = val_double.ToString();
                }

                if (val_double < 0)
                {
                    val_double = 0;
                    val_string = val_double.ToString();
                }
                sprut.IndividualPlan_CreateOrUpdateTableReord(set_guid, stage_guid, column_date, val_double);
            }
            catch(Exception ex1)
            {
                //dgvIndividualPlan.Rows[row].Cells[col].Value = "";
                try
                {
                    val_string = val_string.Replace(',', '.');
                    val_double = Convert.ToDouble(val_string);
                    if (val_double > 15)
                    {
                        val_double = 15;
                        val_string = val_double.ToString();
                    }
                    if (val_double < 0)
                    {
                        val_double = 0;
                        val_string = val_double.ToString();
                    }
                    sprut.IndividualPlan_CreateOrUpdateTableReord(set_guid, stage_guid, column_date, val_double);
                }
                catch (Exception ex2)
                {
                    //dgvIndividualPlan.Rows[row].Cells[col].Value = "";
                }
                finally
                {
                    dgvIndividualPlan.Rows[row].Cells[col].Value = "";
                }
            }



            /*if (Double.TryParse(val_string, out val_double))
            {
                if (val_double > 15)
                {
                    val_double = 15;
                    val_string = val_double.ToString();
                }
                sprut.IndividualPlan_CreateOrUpdateTableReord(set_guid, stage_guid, column_date, val_double);
            }
            else
            {
                dgvIndividualPlan.Rows[row].Cells[col].Value = "";
            }*/


            dgvIndividualPlan_RefreshColumn(column_date.ToShortDateString());
        }

        private void dgvIndividualPlan_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if ((row < 0) || (col < 0))
                return;

            string param = dgvIndividualPlan.Rows[row].Cells["param"].Value.ToString();
            string column_name = dgvIndividualPlan.Columns[col].Name;
            DateTime column_date;

            if (!param.Equals("fact"))
            {
                dgvIndividualPlan.Rows[row].Cells[col].ReadOnly = true;

            }
            else
            {
                if (!DateTime.TryParse(column_name, out column_date))
                {
                    dgvIndividualPlan.Rows[row].Cells[col].ReadOnly = true;
                }
                else
                {
                    if (column_date > DateTime.Now)
                        dgvIndividualPlan.Rows[row].Cells[col].ReadOnly = true;
                    else
                        dgvIndividualPlan.Rows[row].Cells[col].ReadOnly = false;

                }
            }
        }

        private void dgvIndividualPlan_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvIndividualPlan.Refresh();
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                if (!c.Selected)
                {
                    c.DataGridView.ClearSelection();
                    c.DataGridView.CurrentCell = c;
                    c.Selected = true;
                }
            }
        }

        private void dgvIndividualPlan_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Draw only grid content cells not ColumnHeader cells nor RowHeader cells
            if (e.ColumnIndex > -1 & e.RowIndex > -1)
            {
                //Pen for left and top borders
                using (var backGroundPen = new Pen(e.CellStyle.BackColor, 1))
                //Pen for bottom and right borders
                using (var gridlinePen = new Pen(dgvIndividualPlan.GridColor, 1))
                //Pen for selected cell borders
                using (var currentDayPen = new Pen(color_current_day, 1))
                {
                    var topLeftPoint = new Point(e.CellBounds.Left, e.CellBounds.Top);
                    var topRightPoint = new Point(e.CellBounds.Right - 1, e.CellBounds.Top);
                    var bottomRightPoint = new Point(e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    var bottomLeftPoint = new Point(e.CellBounds.Left, e.CellBounds.Bottom - 1);

                    var column_name = dgvIndividualPlan.Columns[e.ColumnIndex].Name;
                    var row_param = dgvIndividualPlan.Rows[e.RowIndex].Cells["param"].Value.ToString();

                    var selectedColumnPen = new Pen(color_selected_cell, 1);
                    var resultColumnPen = new Pen(Color.Black, 2);

                    //Draw selected cells here
                    if (this.dgvIndividualPlan[e.ColumnIndex, e.RowIndex].Selected)
                    {
                        //Paint all parts except borders.
                        e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                        //Draw selected cells border here
                        e.Graphics.DrawRectangle(selectedColumnPen, new Rectangle(e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Width - 1, e.CellBounds.Height - 1));

                        //Handled painting for this cell, Stop default rendering.
                        e.Handled = true;
                    }
                    else
                    if ((dgvIndividualPlan.SelectedCells.Count > 0) && (e.ColumnIndex == dgvIndividualPlan.SelectedCells[0].ColumnIndex))
                    {
                        //Paint all parts except borders.
                        e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                        //Top border of first row cells should be in background color
                        if (e.RowIndex == 0)
                            e.Graphics.DrawLine(backGroundPen, topLeftPoint, topRightPoint);

                        //Left border of first column cells should be in background color
                        if (e.ColumnIndex == 0)
                            e.Graphics.DrawLine(backGroundPen, topLeftPoint, bottomLeftPoint);

                        //Bottom border of last row cells should be in gridLine color
                        if (e.RowIndex == dgvIndividualPlan.RowCount - 1)
                            e.Graphics.DrawLine(gridlinePen, bottomRightPoint, bottomLeftPoint);
                        else  //Bottom border of non-last row cells should be in background color
                            e.Graphics.DrawLine(backGroundPen, bottomRightPoint, bottomLeftPoint);

                        //Right border of last column cells should be in gridLine color
                        if (e.ColumnIndex == dgvIndividualPlan.ColumnCount - 1)
                            e.Graphics.DrawLine(gridlinePen, bottomRightPoint, topRightPoint);
                        else //Right border of non-last column cells should be in background color
                            e.Graphics.DrawLine(backGroundPen, bottomRightPoint, topRightPoint);

                        //Top border of non-first row cells should be in gridLine color, and they should be drawn here after right border
                        if (e.RowIndex > 0)
                            e.Graphics.DrawLine(gridlinePen, topLeftPoint, topRightPoint);

                        //Left border of non-first column cells should be in gridLine color, and they should be drawn here after bottom border
                        if (e.ColumnIndex > 0)
                        {
                            e.Graphics.DrawLine(selectedColumnPen, topLeftPoint, bottomLeftPoint);
                            e.Graphics.DrawLine(selectedColumnPen, topRightPoint, bottomRightPoint);
                        }
                        e.Handled = true;
                    }
                    //Draw non-selected cells here
                    else
                    {
                        if (column_name.Equals(DateTime.Now.ToShortDateString()))
                        {
                            e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                            e.Graphics.DrawLine(gridlinePen, topLeftPoint, topRightPoint);
                            e.Graphics.DrawLine(gridlinePen, bottomLeftPoint, bottomRightPoint);
                            e.Graphics.DrawLine(currentDayPen, topLeftPoint, bottomLeftPoint);
                            e.Graphics.DrawLine(currentDayPen, topRightPoint, bottomRightPoint);

                        }
                        else
                        if (row_param.Equals("result"))
                        {
                            e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                            e.Graphics.DrawLine(gridlinePen, topLeftPoint, topRightPoint);
                            e.Graphics.DrawLine(resultColumnPen, bottomLeftPoint, bottomRightPoint);
                            e.Graphics.DrawLine(gridlinePen, topLeftPoint, bottomLeftPoint);
                            e.Graphics.DrawLine(gridlinePen, topRightPoint, bottomRightPoint);
                            
                        }
                        else
                        {
                            //Paint all parts except borders.
                            e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                            //Top border of first row cells should be in background color
                            if (e.RowIndex == 0)
                                e.Graphics.DrawLine(backGroundPen, topLeftPoint, topRightPoint);

                            //Left border of first column cells should be in background color
                            if (e.ColumnIndex == 0)
                                e.Graphics.DrawLine(backGroundPen, topLeftPoint, bottomLeftPoint);

                            //Bottom border of last row cells should be in gridLine color
                            if (e.RowIndex == dgvIndividualPlan.RowCount - 1)
                                e.Graphics.DrawLine(gridlinePen, bottomRightPoint, bottomLeftPoint);
                            else  //Bottom border of non-last row cells should be in background color
                                e.Graphics.DrawLine(backGroundPen, bottomRightPoint, bottomLeftPoint);

                            //Right border of last column cells should be in gridLine color
                            if (e.ColumnIndex == dgvIndividualPlan.ColumnCount - 1)
                                e.Graphics.DrawLine(gridlinePen, bottomRightPoint, topRightPoint);
                            else //Right border of non-last column cells should be in background color
                                e.Graphics.DrawLine(backGroundPen, bottomRightPoint, topRightPoint);

                            //Top border of non-first row cells should be in gridLine color, and they should be drawn here after right border
                            if (e.RowIndex > 0)
                                e.Graphics.DrawLine(gridlinePen, topLeftPoint, topRightPoint);

                            //Left border of non-first column cells should be in gridLine color, and they should be drawn here after bottom border
                            if (e.ColumnIndex > 0)
                                e.Graphics.DrawLine(gridlinePen, topLeftPoint, bottomLeftPoint);
                        }

                        //We handled painting for this cell, Stop default rendering.
                        e.Handled = true;
                    }
                }
            }
        }

        private void dgvIndividualPlan_MouseClick(object sender, MouseEventArgs e)
        {
            var hittestInfo = dgvIndividualPlan.HitTest(e.X, e.Y);
            int row = hittestInfo.RowIndex;
            int col = hittestInfo.ColumnIndex;

            if ((row < 0) || (col < 0))
                return;

            string column_name = dgvIndividualPlan.Columns[col].Name;

            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    DateTime temp_dt;
                    string IsPlan = dgvIndividualPlan.Rows[row].Cells["param"].Value.ToString();
                    if ((DateTime.TryParse(column_name, out temp_dt)) && (IsPlan.Equals("fact")))
                    {
                        dgvIndividualPlan_Menu.Items["ToolStripMenuItem_IndividualPlan_DeleteTableRecord"].Visible = true;
                        dgvIndividualPlan_Menu.Show(dgvIndividualPlan, new Point(e.X, e.Y));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
            }
        }

        private void ToolStripMenuItem_IndividualPlan_DeleteTableRecord_Click(object sender, EventArgs e)
        {
            if (dgvIndividualPlan.CurrentCell == null)
                return;

            int row = dgvIndividualPlan.CurrentCell.RowIndex;
            int col = dgvIndividualPlan.CurrentCell.ColumnIndex;
            if ((row < 0) || (col < 0))
                return;

            string column_name = dgvIndividualPlan.CurrentCell.OwningColumn.Name;
            DateTime column_date;
            if (!DateTime.TryParse(column_name, out column_date))
                return;

            string set_guid = dgvIndividualPlan.Rows[row].Cells["SetGuid"].Value.ToString();
            string stage_guid = dgvIndividualPlan.Rows[row].Cells["StageGuid"].Value.ToString();

            sprut.IndividualPlan_DeleteTableRecord(set_guid, column_date);

            dgvIndividualPlan_RefreshColumn(column_date.ToShortDateString());
        }

        private void dgvDepartmentPlan_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    Point clientPoint = dgvDepartmentPlan.PointToClient(new Point(e.X, e.Y));
                    DataGridViewRow dgv_row_user = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;//строка из таблицы dgvUsers
                    if ((dgv_row_user == null))
                        return;
                    if (dgv_row_user.DataGridView == null)
                        return;

                    //Если перетащили комплект-на-комплект
                    if (dgv_row_user.DataGridView.Name == (sender as DataGridView).Name)
                        return;

                    //получаем координаты строки в которую перетащили сотрудника
                    var hittest = dgvDepartmentPlan.HitTest(clientPoint.X, clientPoint.Y);
                    int row_set = hittest.RowIndex;
                    int col_set = hittest.ColumnIndex;
                    if ((row_set < 0) || (col_set < 0))
                        return;

                    DataGridViewRow dgv_row_set = dgvDepartmentPlan.Rows[row_set];
                    string set_guid = dgv_row_set.Cells["SetGuid"].Value.ToString();
                    string stage_guid = dgv_row_set.Cells["StageGuid"].Value.ToString();
                    string executor_id = dgv_row_user.Cells["UserId"].Value.ToString();


                    if (dgvDepartmentPlan.Columns[col_set].Name.Equals("Executors"))
                    {
                        if (sprut.IsMonthLast(dtpDepartmentPlan.Value)) //EL
                        {
                            return;
                        }

                    if (!((sprut.CurrentUser_GetPermission() == "planner")||(sprut.CurrentUser_GetPermission() == "head")))
                        {
                            return;
                        }
                         sprut.DepartmentPlan_AddExecutor(set_guid, executor_id);
                        //MessageBox.Show("set = " + dgv_row_set.Cells["SetCode"].Value.ToString() + "  user = " + executor_id);
                        //
                         //this.Cursor = Cursors.WaitCursor;
                         //int row = e.RowIndex;
                         //int col = e.ColumnIndex;

                         //if ((row < 0) || (col < 0))
                           //  return;

                         //string set_guid = dgvDepartmentPlan.Rows[row_set].Cells["SetGuid"].Value.ToString();
                         //string stage_guid = dgvDepartmentPlan.Rows[row_set].Cells["StageGuid"].Value.ToString();
                         

                         


                        //
                        if (dgvDepartmentUsers.SelectedRows.Count > 0)
                        {
                            foreach (DataGridViewRow item in dgvDepartmentUsers.SelectedRows)
                            {
                                try
                                {
                                    if (!executor_id.Equals(item.Cells["UserId"].Value.ToString()))
                                    {
                                        executor_id = item.Cells["UserId"].Value.ToString();
                                        sprut.DepartmentPlan_AddExecutor(set_guid, executor_id);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                                }
                                //textPricePlan.Text = sprut.PricePlanpp(dtpDepartmentPlan.Value).ToString(); //platan
                            }
                        }
                        //MessageBox.Show(row_set.ToString());
                        dgvDepartmentPlan.Rows[row_set].Selected = true;
                        int param = 2;
                        dgvSelectedPlan.DataSource = null;
                        sprut.DepartmentPlan_CreatePlanTable(param, set_guid);
                        dgvSelectedPlan.DataSource = sprut.dtSelectedPlan;

                        dgvDepartmentUsers_HideByMark(row_set, col_set);
                    }

                    if (dgvDepartmentPlan.Columns[col_set].Name.Equals("ResponsibleUser"))
                    {
                        //sprut.DepartmentPlan_AddResponsible(set_guid, executor_id);
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
        }

        private void dgvDepartmentPlan_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dgvDepartmentPlan_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dgvDepartmentUsers_MouseDown(object sender, MouseEventArgs e)
        {
            //dgvPlan_DateTimePicker.Visible = false;
            var hittestInfo = dgvDepartmentUsers.HitTest(e.X, e.Y);

            if (hittestInfo.RowIndex != -1 && hittestInfo.ColumnIndex != -1)
            {
                valueFromMouseDownUserToSet = dgvDepartmentUsers.Rows[hittestInfo.RowIndex];
                if (valueFromMouseDownUserToSet != null)
                {
                    Size dragSize = SystemInformation.DragSize;
                    dragBoxFromMouseDownUserToSet = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
                }
            }
            else
                dragBoxFromMouseDownUserToSet = Rectangle.Empty;
        }

        private void dgvDepartmentUsers_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dragBoxFromMouseDownUserToSet != Rectangle.Empty && !dragBoxFromMouseDownUserToSet.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffect = dgvDepartmentUsers.DoDragDrop(valueFromMouseDownUserToSet, DragDropEffects.Copy);
                }
            }
        }

        private void dgvDepartmentUsers_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dragBoxFromMouseDownUserToSet != Rectangle.Empty && !dragBoxFromMouseDownUserToSet.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffectUserToSet = dgvDepartmentUsers.DoDragDrop(valueFromMouseDownUserToSet, DragDropEffects.Copy);
                }
            }
        }

        private void dgvDepartmentPlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int row = e.RowIndex;
                int col = e.ColumnIndex;

                if ((row < 0) || (col < 0))
                    return;

                string set_guid = dgvDepartmentPlan.Rows[row].Cells["SetGuid"].Value.ToString();
                string stage_guid = dgvDepartmentPlan.Rows[row].Cells["StageGuid"].Value.ToString();
                int param = 2;

                dgvSelectedPlan.DataSource = null;
                sprut.DepartmentPlan_CreatePlanTable(param, set_guid);
                dgvSelectedPlan.DataSource = sprut.dtSelectedPlan;

                dgvDepartmentUsers_HideByMark(row, col);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Неудалось определить задачут\n" + ex.Message + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dgvDepartmentUsers_HideByMark(int row, int col)
        {
            //if ((row < 0) || (col < 0))
            //    return;

            //string set_code = dgvDepartmentPlan.Rows[row].Cells["Mark"].Value.ToString();
            //string user_speciality = "";
            //dict_speciality.TryGetValue(set_code, out user_speciality);
            
            //if(!String.IsNullOrEmpty(set_code) && (!String.IsNullOrEmpty(user_speciality)) && (chkBoxFilterUserTableByMark.Checked))
            //{
            //    DataView dv = new DataView(sprut.dtDepartmentUsers, "", "Speciality", DataViewRowState.CurrentRows);
            //    dv.RowFilter = "Speciality = " + "'" + user_speciality + "'";
            //    dgvDepartmentUsers.DataSource = dv;
            //}
            //else
            //{
            //    dgvDepartmentUsers.DataSource = sprut.dtDepartmentUsers;
            //}
        }

        private void dgvDepartmentUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int row = e.RowIndex;
                int col = e.ColumnIndex;

                if ((row < 0) || (col < 0))
                    return;

                string executor_id = dgvDepartmentUsers.Rows[row].Cells["UserId"].Value.ToString();
                string dept_id = dgvDepartmentUsers.Rows[row].Cells["DepartmentId"].Value.ToString();  //IzmPl
                int param = 1;

                dgvSelectedPlan.DataSource = null;
                sprut.DepartmentPlan_CreatePlanTable(param, executor_id);
                dgvSelectedPlan.DataSource = sprut.dtSelectedPlan;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неудалось определить сотрудника\n" + ex.Message + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dgvSelectedPlan_DataSourceChanged(object sender, EventArgs e)
        {
            if ((dgvSelectedPlan.DataSource == null) || (dgvSelectedPlan.ColumnCount <= 0))
            {
                return;
            }

            for (int j = 0; j < dgvSelectedPlan.ColumnCount; j++)
            {
                string column_name = dgvSelectedPlan.Columns[j].Name;
                dgvSelectedPlan_ChangeColumnProperties(column_name, j);
            }
        }

        private void dgvSelectedPlan_ChangeColumnProperties(string column_name, int col)
        {
            DateTime dt;
            dgvSelectedPlan.Columns[column_name].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvSelectedPlan.Columns["ExecutorFullName"].Frozen = true;

            if (DateTime.TryParse(column_name, out dt))
            {
                dgvSelectedPlan.Columns[column_name].Width = 36;
                dgvSelectedPlan.Columns[column_name].Visible = true;
                dgvSelectedPlan_ChangeHeaderColor(column_name, dt);
                dgvSelectedPlan_UpdateColumnCells(col);

            }
            else
            {
                #region Columns Properties
                switch (column_name)
                {
                    case "Contract":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "Шифр проекта";
                            dgvSelectedPlan.Columns[column_name].Width = 60;
                            dgvSelectedPlan.Columns[column_name].Visible = true;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }
                    case "StageName":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "Наименование проекта";
                            dgvSelectedPlan.Columns[column_name].Width = 120;
                            dgvSelectedPlan.Columns[column_name].Visible = true;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }
                    case "Kks":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "KKS";
                            dgvSelectedPlan.Columns[column_name].Width = 50;
                            dgvSelectedPlan.Columns[column_name].Visible = false;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }
                    case "Building":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "Титул";
                            dgvSelectedPlan.Columns[column_name].Width = 100;
                            dgvSelectedPlan.Columns[column_name].Visible = true;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }
                    case "SetCode":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "Задача";
                            dgvSelectedPlan.Columns[column_name].Width = 80;
                            dgvSelectedPlan.Columns[column_name].Visible = true;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }
                    case "SetName":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "Наименование задачи";
                            dgvSelectedPlan.Columns[column_name].Width = 80;
                            dgvSelectedPlan.Columns[column_name].Visible = false;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }
                    case "GipStart":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "GipStart";
                            dgvSelectedPlan.Columns[column_name].Width = 40;
                            dgvSelectedPlan.Columns[column_name].Visible = false;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }
                    case "GipEnd":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "GipEnd";
                            dgvSelectedPlan.Columns[column_name].Width = 40;
                            dgvSelectedPlan.Columns[column_name].Visible = false;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }
                    case "SetGuid":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = column_name;
                            dgvSelectedPlan.Columns[column_name].Width = 40;
                            dgvSelectedPlan.Columns[column_name].Visible = false;
                            break;
                        }

                    case "StageGuid":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = column_name;
                            dgvSelectedPlan.Columns[column_name].Width = 40;
                            dgvSelectedPlan.Columns[column_name].Visible = false;
                            break;
                        }

                    case "StatusFromTdms":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "Статус";
                            dgvSelectedPlan.Columns[column_name].Width = 60;
                            dgvSelectedPlan.Columns[column_name].Visible = false;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }

                    case "ExecutorFullName":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "Исполнитель";
                            dgvSelectedPlan.Columns[column_name].Width = 80;
                            dgvSelectedPlan.Columns[column_name].Visible = true;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }

                    case "PlanStart":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "Начало";
                            dgvSelectedPlan.Columns[column_name].Width = 70;
                            dgvSelectedPlan.Columns[column_name].Visible = true;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }
                    case "PlanEnd":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = "Окончание";
                            dgvSelectedPlan.Columns[column_name].Width = 70;
                            dgvSelectedPlan.Columns[column_name].Visible = true;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }
                    case "ExecutorId":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = column_name;
                            dgvSelectedPlan.Columns[column_name].Width = 70;
                            dgvSelectedPlan.Columns[column_name].Visible = false;
                            break;
                        }
                    case "DeptId":
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = column_name;
                            dgvSelectedPlan.Columns[column_name].Width = 40;
                            dgvSelectedPlan.Columns[column_name].Visible = false;
                            break;
                        }
                    default:
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderText = column_name;
                            dgvSelectedPlan.Columns[column_name].Width = 40;
                            dgvSelectedPlan.Columns[column_name].Visible = true;
                            dgvSelectedPlan.Columns[column_name].ReadOnly = true;
                            break;
                        }

                }
                #endregion
            }
        }

        private void dgvSelectedPlan_ChangeHeaderColor(string column_name, DateTime dt)
        {
            double day_work_time = sprut.GetWorkTime(dt);
            if (day_work_time <= 0)
            {
                //Выходной день
                dgvSelectedPlan.Columns[column_name].HeaderCell.Style.BackColor = color_holiday_header;
            }
            else
                if ((dgvSelectedPlan.DataSource as DataTable).TableName.Equals("dtPlanUser"))
                {
                    //for(int i=0; i < dgvPlan.RowCount; i++)
                    if (dgvSelectedPlan.RowCount > 0)
                    {
                        string executor_id= dgvSelectedPlan.Rows[0].Cells["ExecutorId"].Value.ToString();
                        if (sprut.IsDayBusy(executor_id, dt))
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderCell.Style.BackColor = color_work_day_header;
                        }
                        else
                        {
                            dgvSelectedPlan.Columns[column_name].HeaderCell.Style.BackColor = color_resource_free;
                        }
                    }
                }
                else
                    dgvSelectedPlan.Columns[column_name].HeaderCell.Style.BackColor = color_work_day_header;
           
        }

        private void dgvSelectedPlan_UpdateColumnCells(int col)
        {
            for (int i = 0; i < dgvSelectedPlan.RowCount; i++)
            {
                string set_guid = dgvSelectedPlan.Rows[i].Cells["SetGuid"].Value.ToString();
                string stage_guid = dgvSelectedPlan.Rows[i].Cells["StageGuid"].Value.ToString();
                string executor_id = dgvSelectedPlan.Rows[i].Cells["ExecutorId"].Value.ToString();
                string column_name = dgvSelectedPlan.Columns[col].Name;
                DateTime column_date;
                double day_work_time = 0;

                if (DateTime.TryParse(column_name, out column_date))
                {
                    day_work_time = sprut.GetWorkTime(column_date);
                }

                clTableRecord tr = sprut.GetTableRecord(set_guid, executor_id, column_name);

                if (tr == null)
                {
                    dgvSelectedPlan.Rows[i].Cells[column_name].Value = "";
                    dgvSelectedPlan.Rows[i].Cells[column_name].Style.BackColor = Color.White;
                    dgvSelectedPlan.Rows[i].Cells[column_name].Style.ForeColor = Color.Black;
                    if (day_work_time <= 0)
                    {
                        dgvSelectedPlan.Rows[i].Cells[column_name].Style.BackColor = color_holiday;
                        dgvSelectedPlan.Rows[i].Cells[column_name].Style.ForeColor = Color.Black;
                    }

                    continue;
                }
                else
                {
                    dgvSelectedPlan.Rows[i].Cells[column_name].Value = tr.WorkPlan.ToString();

                    if (tr.WorkPlan > 0)
                        dgvSelectedPlan.Rows[i].Cells[column_name].Style.BackColor = color_plan;
                    if (tr.WorkFact > 0)
                        dgvSelectedPlan.Rows[i].Cells[column_name].Style.BackColor = color_fact;

                    if ((tr.WorkPlan > 0) && (tr.WorkFact > 0))  
                        dgvSelectedPlan.Rows[i].Cells[column_name].Style.BackColor = color_plan_and_fact;

                    if (tr.ManuallyInput > 0)
                    {
                        dgvSelectedPlan.Rows[i].Cells[column_name].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        dgvSelectedPlan.Rows[i].Cells[column_name].Style.ForeColor = Color.Black; //Андрей
                        if (tr.WorkFact > 0)
                        {
                            dgvSelectedPlan.Rows[i].Cells[column_name].Style.BackColor = color_plan_and_fact;
                        }
                        else
                        {
                            dgvSelectedPlan.Rows[i].Cells[column_name].Style.BackColor = color_auto_work_day_header; //Андрей
                        }
                    }

                    if (tr.WorkPlan == 0)
                        dgvSelectedPlan.Rows[i].Cells[column_name].Style.ForeColor = Color.Black;
                }

            }

        }

        private void dgvSelectedPlan_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if ((row < 0) || (col < 0))
                return;

            dgvSelectedPlan.Rows[row].Cells[col].Tag = dgvSelectedPlan.Rows[row].Cells[col].Value.ToString();
            dgvSelectedPlan.Rows[row].Cells[col].Style.ForeColor = Color.Black;
        }

        private void dgvSelectedPlan_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if ((row < 0) || (col < 0))
                return;

            string new_value = dgvSelectedPlan.Rows[row].Cells[col].Value.ToString();
            string old_value = dgvSelectedPlan.Rows[row].Cells[col].Tag.ToString();
            new_value = new_value.Replace('.', ',');
            double val = 0;
            if (!double.TryParse(new_value, out val))
            {
                dgvSelectedPlan.Rows[row].Cells[col].Value = dgvSelectedPlan.Rows[row].Cells[col].Tag.ToString();
            }
            else
            {
                string column_name = dgvSelectedPlan.Columns[col].Name;
                DateTime column_date;
                if (DateTime.TryParse(column_name, out column_date))
                {
                    string set_guid = dgvSelectedPlan.Rows[row].Cells["SetGuid"].Value.ToString();
                    string stage_guid = dgvSelectedPlan.Rows[row].Cells["StageGuid"].Value.ToString();
                    string executor_id = dgvSelectedPlan.Rows[row].Cells["ExecutorId"].Value.ToString();
                    string dept_id = dgvSelectedPlan.Rows[row].Cells["DeptId"].Value.ToString();  //IzmPl
                    sprut.DepartmentPlan_CreateTableRecord(set_guid, stage_guid,executor_id, dept_id,column_date, val, 9);
                    sprut.DepartmentPlan_IsSetInMonthPlan(set_guid, dtpDepartmentPlan.Value);
                    dgvSelectedPlan.Rows[row].Cells[col].Tag = "";
                    dgvSelectedPlan_UpdateColumnCells(col);
                    dgvSelectedPlan_ChangeHeaderColor(column_name, column_date);
                }
            }
        }

        private void dgvSelectedPlan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int row = e.RowIndex;
                int col = e.ColumnIndex;

                if ((row < 0) || (col < 0))
                    return;

                string column_name = dgvSelectedPlan.Columns[col].Name;

                if (column_name.Equals("ExecutorFullName"))
                {
                    string login = dgvSelectedPlan.Rows[row].Cells["ExecutorId"].Value.ToString();
                    int param = 1;

                    dgvSelectedPlan.DataSource = null;
                    sprut.DepartmentPlan_CreatePlanTable(param, login);
                    dgvSelectedPlan.DataSource = sprut.dtSelectedPlan;
                }

                if (column_name.Equals("SetCode"))
                {
                    string set_stage_id = dgvSelectedPlan.Rows[row].Cells["SetGuid"].Value.ToString();
                    //set_stage_id += "[*]" + dgvSelectedPlan.Rows[row].Cells["StageGuid"].Value.ToString();
                    int param = 2;

                    dgvSelectedPlan.DataSource = null;
                    sprut.DepartmentPlan_CreatePlanTable(param, set_stage_id);
                    dgvSelectedPlan.DataSource = sprut.dtSelectedPlan;
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

        private void dgvSelectedPlan_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                if (!c.Selected)
                {
                    c.DataGridView.ClearSelection();
                    c.DataGridView.CurrentCell = c;
                    c.Selected = true;
                }
            }
        }

        private void dgvSelectedPlan_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int col = e.ColumnIndex;
                if ((row < 0) || (col < 0))
                    return;

                string set_guid = dgvSelectedPlan.Rows[row].Cells["SetGuid"].Value.ToString();
                string stage_guid = dgvSelectedPlan.Rows[row].Cells["StageGuid"].Value.ToString();
                string executor_id = dgvSelectedPlan.Rows[row].Cells["ExecutorId"].Value.ToString();
                string column_name = dgvSelectedPlan.Columns[col].Name;

                DateTime dt;

                if (DateTime.TryParse(column_name, out dt))
                {
                    clTableRecord tr = sprut.GetTableRecord(set_guid, executor_id, column_name);
                    if (tr == null)
                    {
                        dgvSelectedPlan.Rows[row].Cells[col].ToolTipText = "";
                    }
                    else
                    {
                        dgvSelectedPlan.Rows[row].Cells[col].ToolTipText = "План = " + tr.WorkPlan + "\n" + "Факт = " + tr.WorkFact;
                    }
                }
            }
            catch
            {

            }
        }

        private void dgvSelectedPlan_MouseClick(object sender, MouseEventArgs e)
        {
            var hittestInfo = dgvSelectedPlan.HitTest(e.X, e.Y);
            int row = hittestInfo.RowIndex;
            int col = hittestInfo.ColumnIndex;

            if ((row < 0) || (col < 0))
                return;
            string column_name = dgvSelectedPlan.Columns[col].Name;

            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    dgvSelectedPlan_DateTimePicker.Visible = false;
                    DateTime temp_dt;

                    if ((column_name == "ExecutorFullName"))
                    {
                        dgvSelectedPlan_Menu.Items["dgvSelectedPlan_Menu_AutoPlan"].Visible = false;
                        dgvSelectedPlan_Menu.Items["dgvSelectedPlan_Menu_ManuallyPlan"].Visible = false;
                        dgvSelectedPlan_Menu.Items["dgvSelectedPlan_Menu_DeleteSelected"].Visible = false;
                        dgvSelectedPlan_Menu.Items["dgvSelectedPlan_Menu_DeleteExecutor"].Visible = true;
                        dgvSelectedPlan_Menu.Show(dgvSelectedPlan, new Point(e.X, e.Y));
                    }
                    else
                        if (DateTime.TryParse(column_name, out temp_dt))
                        {
                            dgvSelectedPlan_Menu.Items["dgvSelectedPlan_Menu_AutoPlan"].Visible = true;
                            dgvSelectedPlan_Menu.Items["dgvSelectedPlan_Menu_ManuallyPlan"].Visible = true;
                            dgvSelectedPlan_Menu.Items["dgvSelectedPlan_Menu_DeleteSelected"].Visible = true;
                            dgvSelectedPlan_Menu.Items["dgvSelectedPlan_Menu_DeleteExecutor"].Visible = false;
                            dgvSelectedPlan_Menu.Show(dgvSelectedPlan, new Point(e.X, e.Y));
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
            }
            else
                if (e.Button == MouseButtons.Left)
                {
                    if ((column_name.Equals("PlanStart")) || (column_name.Equals("PlanEnd")))
                    {
                        Rectangle tempRect = dgvSelectedPlan.GetCellDisplayRectangle(col, row, false);
                        dgvSelectedPlan_DateTimePicker.Location = tempRect.Location;
                        dgvSelectedPlan_DateTimePicker.Width = tempRect.Width;
                        dgvSelectedPlan_DateTimePicker.Value = DateTime.Now;
                        dgvSelectedPlan_DateTimePicker.Visible = true;
                    }
                    else
                    {
                        dgvSelectedPlan_DateTimePicker.Visible = false;
                    }
                }

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.dgvSelectedPlan_DateTimePicker.CloseUp -= new EventHandler(dgvSelectedPlan_DateTimePickerCloseUp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void dgvSelectedPlan_Menu_AutoPlan_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if ((dgvSelectedPlan.SelectedCells == null) || (dgvSelectedPlan.SelectedCells.Count <= 0))
                    return;
                foreach (DataGridViewCell cell in dgvSelectedPlan.SelectedCells)
                {
                    int row = cell.RowIndex;
                    int col = cell.ColumnIndex;
                    if ((row < 0) || (col < 0))
                        continue;
                    string column_name = dgvSelectedPlan.Columns[col].Name;
                    DateTime column_date;
                    if (!DateTime.TryParse(column_name, out column_date))
                        continue;
                    string set_guid = dgvSelectedPlan.Rows[row].Cells["SetGuid"].Value.ToString();
                    string stage_guid = dgvSelectedPlan.Rows[row].Cells["StageGuid"].Value.ToString();
                    string executor_id = dgvSelectedPlan.Rows[row].Cells["ExecutorId"].Value.ToString();
                    string dept_id = dgvSelectedPlan.Rows[row].Cells["DeptId"].Value.ToString(); //IzmPl
                    sprut.DepartmentPlan_CreateTableRecord(set_guid, stage_guid,executor_id, dept_id,column_date, 0, -9);

                    sprut.DepartmentPlan_IsSetInMonthPlan(set_guid, dtpDepartmentPlan.Value);

                    dgvSelectedPlan_UpdateColumnCells(col);
                    dgvSelectedPlan_ChangeHeaderColor(column_name, column_date);
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

        private void dgvSelectedPlan_Menu_DeleteSelected_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if ((dgvSelectedPlan.SelectedCells != null) || (dgvSelectedPlan.SelectedCells.Count != 0))
                {
                    foreach (DataGridViewCell cell in dgvSelectedPlan.SelectedCells)
                    {
                        int row = cell.RowIndex;
                        int col = cell.ColumnIndex;

                        if ((row < 0) || (col < 0))
                            continue;

                        string column_name = dgvSelectedPlan.Columns[col].Name;
                        DateTime column_date;

                        if (!DateTime.TryParse(column_name, out column_date))
                            continue;

                        string executor_id = dgvSelectedPlan.Rows[row].Cells["ExecutorId"].Value.ToString();
                        string set_guid = dgvSelectedPlan.Rows[row].Cells["SetGuid"].Value.ToString();
                        string stage_guid = dgvSelectedPlan.Rows[row].Cells["StageGuid"].Value.ToString();

                        sprut.DepartmentPlan_TableRecord_Delete(set_guid, executor_id, column_date);

                        sprut.DepartmentPlan_IsSetInMonthPlan(set_guid, dtpDepartmentPlan.Value);

                        dgvSelectedPlan_UpdateColumnCells(col);
                        dgvSelectedPlan_ChangeHeaderColor(column_name, column_date);
                    }
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

        private void dgvSelectedPlan_Menu_DeleteExecutor_Click(object sender, EventArgs e)
        {
            DataGridViewCell dgv_cell = dgvSelectedPlan.CurrentCell;
            DataGridViewColumn dgv_column = dgv_cell.OwningColumn;
            DataGridViewRow dgv_row = dgv_cell.OwningRow;

            string executor_fio = "";
            string executor_id = "";
            string set_guid = "";
            string stage_guid = "";
            string set_code = "";
            this.Cursor = Cursors.WaitCursor;
            try
            {
                executor_fio = dgv_row.Cells["ExecutorFullName"].Value.ToString();
                executor_id = dgv_row.Cells["ExecutorId"].Value.ToString();
                set_guid = dgv_row.Cells["SetGuid"].Value.ToString();
                stage_guid = dgv_row.Cells["StageGuid"].Value.ToString();
                set_code = dgv_row.Cells["SetCode"].Value.ToString();

                DialogResult dialog_result = MessageBox.Show("Удалить все плановые работы сотрудника \n      '" + executor_fio + "'\nнад задачей \n      " + set_code + "?", "Удаление плана", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog_result == DialogResult.Yes)
                {
                    sprut.DepartmentPlan_DeleteExecutor(set_guid, executor_id);
                    sprut.DepartmentPlan_IsSetInMonthPlan(set_guid, dtpDepartmentPlan.Value);
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

        private void dgvSelectedPlan_Menu_ManuallyPlan_Click(object sender, EventArgs e)
        {
            frmInputManuallyTime mipt = null;
            try
            {
                mipt = new frmInputManuallyTime();
                mipt.Owner = this;
                mipt.ShowDialog();

                double val = 0;
                if (Double.TryParse(mipt.InputManualPlanTime, out val))
                {
                    this.Cursor = Cursors.WaitCursor;
                    foreach (DataGridViewCell cell in dgvSelectedPlan.SelectedCells)
                    {
                        try
                        {
                            string s = cell.OwningColumn.Name.ToString();
                            DateTime dt;
                            if (DateTime.TryParse(s, out dt))
                            {
                                if (sprut.GetWorkTime(dt) > 0)
                                {
                                    //Выходные дни не планируем
                                    int row = cell.RowIndex;
                                    int col = cell.ColumnIndex;
                                    string set_guid = dgvSelectedPlan.Rows[row].Cells["SetGuid"].Value.ToString();
                                    string stage_guid = dgvSelectedPlan.Rows[row].Cells["StageGuid"].Value.ToString();
                                    string executor_id = dgvSelectedPlan.Rows[row].Cells["ExecutorId"].Value.ToString();
                                    string dept_id = dgvSelectedPlan.Rows[row].Cells["DeptId"].Value.ToString();

                                    sprut.DepartmentPlan_CreateTableRecord(set_guid, stage_guid,executor_id, dept_id,dt, val, 9);
                                    dgvSelectedPlan_UpdateColumnCells(col);
                                    dgvSelectedPlan_ChangeHeaderColor(s, dt);
                                    sprut.DepartmentPlan_IsSetInMonthPlan(set_guid, dtpDepartmentPlan.Value);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (mipt != null)
                {
                    mipt.Dispose();
                    mipt = null;
                }
            }
        }

        private void dgvDepartmentUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if ((row < 0) || (col < 0))
                return;
            else
            {
                string column_name = dgvDepartmentUsers.Columns[col].Name;
                if (column_name.Equals("Resource"))
                {
                    double resource = 0;
                    if (double.TryParse(dgvDepartmentUsers.Rows[row].Cells[col].Value.ToString(), out resource))
                    {
                        if (resource > 0)
                            dgvDepartmentUsers.Rows[row].Cells[col].Style.BackColor = color_resource_free;
                        else
                            dgvDepartmentUsers.Rows[row].Cells[col].Style.BackColor = color_resource_busy;
                    }
                }
            }
        }



        private void btnDepartmentPlanSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            int indexsel;
            int index = dgvDepartmentPlan.FirstDisplayedScrollingRowIndex;
            indexsel = (dgvDepartmentPlan.SelectedRows.Count > 0) ? dgvDepartmentPlan.SelectedRows[0].Index : -1; 

            try
            {
                //проверка на исполнителей без плана

                string mes = sprut.UserNoPlan();
                if (!(mes == ""))
                {
                   DialogResult diag_res =  MessageBox.Show("Следующие сотрудники, назначенные исполнителями на задачи, не запланированы. \n"+mes+"Назначение не будет сохранено. Продолжить? ", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (diag_res == DialogResult.No)
                    {
                        return;
                    }
                }
                
                string save_mode = sprut.DepartmentPlan_IsModified();

                if(!save_mode.Equals("unknown"))
                {
                    DialogResult diag_res = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (diag_res == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        switch (save_mode)
                        {
                            case "record":
                                {
                                    sprut.DepartmentPlan_Save();
                                    sprut.DepartmentPlan_Update(dtpDepartmentPlan.Value);
                                    MessageBox.Show("Сохранение завершено!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }
                            case "set":
                                {
                                    sprut.DepartmentPlan_Save();
                                    sprut.DepartmentPlan_Update(dtpDepartmentPlan.Value);
                                    MessageBox.Show("Сохранение завершено!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }
                            case "record_set":
                                {
                                    sprut.DepartmentPlan_Save();
                                    sprut.DepartmentPlan_Update(dtpDepartmentPlan.Value);
                                    MessageBox.Show("Сохранение завершено!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        this.Cursor = Cursors.Default;
                        if (indexsel >= 0)
                        {
                            //dgvDepartmentPlan.Rows[indexsel].Selected = true;
                            //dgvDepartmentPlan.FirstDisplayedScrollingRowIndex = index;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нет изменений для сохранения", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                /*
                if (true)
                {
                    DialogResult diag_res = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (diag_res == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        sprut.DepartmentPlan_Save();
                        sprut.DepartmentPlan_Update(dtpDepartmentPlan.Value);
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Сохранение завершено!" , "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Нет изменений для сохранения", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                */

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnDepartmentPlanInc_Click(object sender, EventArgs e)
        {
            DialogResult diag_res;
            string mes = sprut.UserNoPlan();
            if (!(mes == ""))
            {
                diag_res = MessageBox.Show("Следующие сотрудники, назначенные исполнителями на задачи, не запланированы. \n" + mes + "Назначение не будет сохранено. Продолжить? ", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (diag_res == DialogResult.No)
                {
                    return;
                }
            }

            string save_mode = sprut.DepartmentPlan_IsModified();
            if (!save_mode.Equals("unknown"))
            {
                diag_res = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (diag_res == DialogResult.Yes)
                {
                    sprut.DepartmentPlan_Save();
                }
            }
            dtpDepartmentPlan.Value = dtpDepartmentPlan.Value.AddMonths(1);
            DepartmentPlan_ChangeMonth();
        }

        private void btnDepartmentPlanDec_Click(object sender, EventArgs e)
        {
            DialogResult diag_res;
            string mes = sprut.UserNoPlan();
            if (!(mes == ""))
            {
                diag_res = MessageBox.Show("Следующие сотрудники, назначенные исполнителями на задачи, не запланированы. \n" + mes + "Назначение не будет сохранено. Продолжить? ", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (diag_res == DialogResult.No)
                {
                    return;
                }
            }

            string save_mode = sprut.DepartmentPlan_IsModified();
            if (!save_mode.Equals("unknown"))
            { 
                diag_res = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (diag_res == DialogResult.Yes)
                {
                    sprut.DepartmentPlan_Save();
                }
            }
            dtpDepartmentPlan.Value = dtpDepartmentPlan.Value.AddMonths(-1);
            DepartmentPlan_ChangeMonth();
        }

        private void DepartmentPlan_ChangeMonth()
        {
            this.Cursor = Cursors.WaitCursor;
            if (sprut.IsMonthLast(dtpDepartmentPlan.Value))  //EL - прошлый месяц 
            {
                //btnDepartmentPlan_Approve.Enabled = false;
                btnAgreeSetPlaned.Enabled = false;
                btnDepartmentPlan_AddSet.Enabled = false;
                btnDepartmentPlan_Save.Enabled = false;
                dgvSelectedPlan.ReadOnly = true;
                dgvDepartmentPlan.DefaultCellStyle.BackColor = Color.FromArgb(230,230,230);
                dgvDepartmentUsers.DefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
                dgvSelectedPlan.DefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
            }
            else
            {
                //btnDepartmentPlan_Approve.Enabled = true;
                btnAgreeSetPlaned.Enabled = true;
                btnDepartmentPlan_AddSet.Enabled = true;
                btnDepartmentPlan_Save.Enabled = true;
                dgvSelectedPlan.ReadOnly = false;
                dgvDepartmentPlan.DefaultCellStyle.BackColor = Color.White;
                dgvDepartmentUsers.DefaultCellStyle.BackColor = Color.White;
                dgvSelectedPlan.DefaultCellStyle.BackColor = Color.White;
            }
            try
            {
                sprut.DepartmentPlan_Update(dtpDepartmentPlan.Value);
                //textPricePlan.Text = sprut.PricePlanpp(dtpDepartmentPlan.Value);
                //textFotPlan.Text = sprut.PriceFot(dtpDepartmentPlan.Value);  
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void picbxRefreshDepartmentPlan_Click(object sender, EventArgs e)
        {
            DepartmentPlan_ChangeMonth();
        }

        private void btnDepartmentPlanAddSet_Click(object sender, EventArgs e)  //EL_2
        {
            delegateIndividualPlanAddOtherWork owd = new delegateIndividualPlanAddOtherWork(DepartmentPlanAddSet);
            frmOtherWorks ow = new frmOtherWorks(sprut.GetSprutConnectionString(),owd);
            ow.Owner = this;
            ow.ShowDialog();

            //string set_guid = ow.set_guid;
            //string stage_guid = ow.stage_guid;
            //string contract = ow.contract;
            //string stage_name = ow.stage_name;

            //ow.Dispose();

            //if ((String.IsNullOrEmpty(set_guid)) || (String.IsNullOrEmpty(stage_guid)))
            //{
            //    //MessageBox.Show("Неудалось определить комплект","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else
            //{
            //    if (sprut.DepartmentPlan_IsSetSetExists(set_guid, stage_guid))
            //    {
            //        MessageBox.Show("Выбранный Вами комплект был добавлен ранее.\nДля поиска комплекта используйте фильтры по столбцам.\nДля вызова фильтра нажмите правой кнопкой мыши на заголовок столбца", "Повторное добавление комплекта", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        string res = sprut.DepartmentPlan_AddSet(set_guid, stage_guid, contract, stage_name);

            //        if (!String.IsNullOrEmpty(res))
            //        {
            //            MessageBox.Show("Комплект '" + res + "' добавлен", "Добавление комплекта", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Неудалось добавить комплект", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
           //}
        }
        private void DepartmentPlanAddSet(string stage_guid, string set_guid, string contract, string stage_name)  //EL_2
        {
            //frmOtherWorks ow = new frmOtherWorks(sprut.GetSprutConnectionString());
            //ow.Owner = this;
            //ow.ShowDialog();

            //string set_guid = ow.set_guid;
            //string stage_guid = ow.stage_guid;
            //string contract = ow.contract;
            //string stage_name = ow.stage_name;

            //ow.Dispose();

            if ((String.IsNullOrEmpty(set_guid)) || (String.IsNullOrEmpty(stage_guid)))
            {
                //MessageBox.Show("Неудалось определить комплект","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (sprut.DepartmentPlan_IsSetSetExists(set_guid, stage_guid))  //есть другие работы по объектам
                {
                    MessageBox.Show("Выбранная Вами задача был добавлена ранее.\nДля поиска задачи используйте фильтры по столбцам.\nДля вызова фильтра нажмите правой кнопкой мыши на заголовок столбца", "Повторное добавление задачи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string res = sprut.DepartmentPlan_AddSet(set_guid, stage_guid, contract, stage_name);

                    if (!String.IsNullOrEmpty(res))
                    {
                        MessageBox.Show("Задача '" + res + "' добавлена", "Добавление задачи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Неудалось добавить задачу", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnIndividualPlanAddSet_Click(object sender, EventArgs e)
        {

            delegateIndividualPlanAddOtherWork owd = new delegateIndividualPlanAddOtherWork(IndividualPlanAddSet);
            frmOtherWorks ow = new frmOtherWorks(sprut.GetSprutConnectionString(), owd);
            ow.Owner = this;
            ow.ShowDialog();
        }

        private void IndividualPlanAddSet(string stage_guid, string set_guid, string contract, string stage_name) 
        {
            //frmOtherWorks ow = new frmOtherWorks(sprut.GetSprutConnectionString());
            //ow.Owner = this;
            //ow.ShowDialog();

            //string set_guid = ow.set_guid;
            //string stage_guid = ow.stage_guid;
            //string contract = ow.contract;
            //string stage_name = ow.stage_name;
            //ow.Dispose();

            if ((String.IsNullOrEmpty(set_guid)) || (String.IsNullOrEmpty(stage_guid)))
            {
                //MessageBox.Show("Неудалось определить комплект","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (sprut.IndividualPlan_IsSetExists(set_guid))
                {
                    MessageBox.Show("Выбранная Вами задача была добавлена ранее.\nДля поиска задачи используйте фильтры по столбцам.\nДля вызова фильтра нажмите правой кнопкой мыши на заголовок столбца", "Повторное добавление задачи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string res = sprut.IndividualPlan_AddSet(set_guid,stage_guid, contract, stage_name);
                    if (!String.IsNullOrEmpty(res))
                    {
                        MessageBox.Show("Задача '" + res + "' добавлена", "Добавление задачи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvIndividualPlan.DataSource = null;
                        sprut.IndividualPlan_FillTableIndividualPlan(dtpIndividualPlan.Value);
                        dgvIndividualPlan.DataSource = sprut.dtIndividualPlan;
                    }
                    else
                    {
                        MessageBox.Show("Неудалось добавить задачу", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void btnIndividualPlanSave_Click(object sender, EventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if(sprut.IndividualPlan_IsModified())
                {
                    DialogResult diag_res = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (diag_res == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        sprut.IndividualPlan_Save();
                        IndividualPlan_ChangeMonth();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Сохранение завершено!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Нет изменений для сохранения", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnIndividualPlanDec_Click(object sender, EventArgs e)
        {
            dtpIndividualPlan.Value = dtpIndividualPlan.Value.AddMonths(-1);
            IndividualPlan_ChangeMonth();
        }

        private void btnIndividualPlanInc_Click(object sender, EventArgs e)
        {
            dtpIndividualPlan.Value = dtpIndividualPlan.Value.AddMonths(1);
            IndividualPlan_ChangeMonth();
        }

        private void labelSelectedUser_Click(object sender, EventArgs e)
        {
            frmNoApproveSets frmChUser = new frmNoApproveSets(sprut.ListDepartmentUsers);
            frmChUser.ShowDialog();

            string selected_id = frmChUser.SelectedUserID;
            string selected_fio = frmChUser.SelectedUserFIO;
            string selected_lastname = frmChUser.SelectedUserLastName;
            string selected_firstname = frmChUser.SelectedUserFirstName;
            string select_middle = frmChUser.SelectedUserMiddleName;

            if (!String.IsNullOrEmpty(selected_fio) && !String.IsNullOrEmpty(selected_id))
            {
                labelSelectedUser.Text = selected_fio;

                sprut.IndividualPlan_ChangeUser(selected_id, selected_fio, selected_lastname, selected_firstname, select_middle);
                IndividualPlan_ChangeMonth();
            }
        }

        private void cmbxDepartments_SelectedValueChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (cmbxDepartmentPlan_Departments.Items.Count == 0)
                    return;
                string selected_dept_small = cmbxDepartmentPlan_Departments.SelectedValue.ToString();

                var item = cmbxDepartmentPlan_Departments.SelectedItem as sprut.clDepartment;

                if (!item.Id.Equals(sprut.CurrentUser_GetDepartmentId()))
                {
                    string perm = sprut.CurrentUser_GetPermission();
                    
                    //if (!(perm == "head")) //все задачи  
                    //{
                        sprut.CurrentUser_ChangeDepartment(item.Id, item.FullName);
                        sprut.DepartmentPlan_Update(dtpDepartmentPlan.Value);
                    //}
                    //else
                    //{
                    //    sprut.Load_DepartmentUsers(item.Id);
                    //    sprut.FillUsersTable();
                    //}
                }
                dgvDepartmentPlan.Focus();

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

        private void отчетДляБухгалтерииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                DateTime report_date = dtpDepartmentPlan.Value;
                string monthtek = report_date.Month.ToString();
                string yeartek = report_date.Year.ToString();
                //string stageguid = @"{9FBF4174-1A67-447D-8C01-7BECC0D0468D}";
                string template = @"c:\platan\report2_shb.xlsx";
                string tdmsName = frmMain.TdmsName;
                string tdmsSrv = frmMain.TdmsSrv;
                string userName = frmMain.UserSqlName;
                string userPassword = frmMain.UserSqlPassword;
                string constr = sprut.GetSprutConnectionString();

                frmGetProject frmPrj = new frmGetProject(monthtek, yeartek, constr);
                frmPrj.Show();
                if (frmPrj.stage == "")
                    MessageBox.Show("Отсутствуют данные для формирования отчета");
                else
                {
                    string stageguid = frmPrj.stage;
                    Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.WorkingDirectory = @"C:\Platan";
                    proc.StartInfo.FileName = "Report2.vbs";
                    proc.StartInfo.Arguments = monthtek + " " + yeartek + " " + stageguid + " " + template + " " + tdmsName + " " + tdmsSrv + " " + userName + " " + userPassword;
                    proc.Start();
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

        private void отчетПоПланированиюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                DateTime report_date = dtpDepartmentPlan.Value;
                string monthtek = report_date.Month.ToString();
                string yeartek = report_date.Year.ToString();
                //string stageguid = @"{9FBF4174-1A67-447D-8C01-7BECC0D0468D}";
                string template = @"c:\platan\report1_shb.xlsx";
                string tdmsName = frmMain.TdmsName;
                string tdmsSrv = frmMain.TdmsSrv;
                string userName = frmMain.UserSqlName;
                string userPassword = frmMain.UserSqlPassword;
                string constr = sprut.GetSprutConnectionString();
                string srvlnk = frmMain.SqlLink;
                frmGetProject frmPrj = new frmGetProject(monthtek, yeartek,constr);
                // frmPrj.stage = "";
                frmPrj.Show();
                if (frmPrj.stage == "")
                    MessageBox.Show("Отсутствуют данные для формирования отчета");
                else
                    if (!(frmPrj.stage == "-1"))
                {
                    string stageguid = frmPrj.stage;   
                    Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.WorkingDirectory = @"c:\Platan";
                    proc.StartInfo.FileName = "Report1.vbs";
                    proc.StartInfo.Arguments = monthtek + " " + yeartek + " " + stageguid + " " + template + " " + tdmsName + " " + tdmsSrv + " " + userName + " " + userPassword + " " + srvlnk;
                    proc.Start();
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

        private void отчетПоПроизводственномуПлануToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                DateTime report_date = dtpDepartmentPlan.Value;
                string monthtek = report_date.Month.ToString();
                string yeartek = report_date.Year.ToString();
                string template = @"c:\platan\report3_shb.xlsx";
                string tdmsName = frmMain.TdmsName;
                string tdmsSrv = frmMain.TdmsSrv;
                string userName = frmMain.UserSqlName;
                string userPassword = frmMain.UserSqlPassword;
                string constr = sprut.GetSprutConnectionString();
                Process proc = new System.Diagnostics.Process();
                proc.StartInfo.WorkingDirectory = @"c:\Platan";
                proc.StartInfo.FileName = "Report3.vbs";
                proc.StartInfo.Arguments = monthtek + " " + yeartek + " " + template + " " + tdmsName + " " + tdmsSrv + " " + userName + " " + userPassword;
                proc.Start();
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

        private void dgvDepartmentPlan_Menu_ShowSetHistory_Click(object sender, EventArgs e)
        {
            string set_guid = "";
            string stage_guid = "";

            string stage_name = "";
            string set_name = "";
            string set_code = "";
            string contract = "";
            string gip_start = "";
            string gip_end = "";
            
            try
            {
                set_guid = dgvDepartmentPlan.SelectedRows[0].Cells["SetGuid"].Value.ToString();
                stage_guid = dgvDepartmentPlan.SelectedRows[0].Cells["StageGuid"].Value.ToString();
                stage_name = dgvDepartmentPlan.SelectedRows[0].Cells["StageName"].Value.ToString();
                set_name = dgvDepartmentPlan.SelectedRows[0].Cells["SetName"].Value.ToString();
                set_code = dgvDepartmentPlan.SelectedRows[0].Cells["SetCode"].Value.ToString();
                contract = dgvDepartmentPlan.SelectedRows[0].Cells["Contract"].Value.ToString();
                gip_start = dgvDepartmentPlan.SelectedRows[0].Cells["GipStart"].Value.ToString();
                gip_end = dgvDepartmentPlan.SelectedRows[0].Cells["GipEnd"].Value.ToString();


                frmSetHistory frmSetHis = new frmSetHistory();
                frmSetHis.Owner = this;
                frmSetHis.CreateSetHistory(contract, set_guid, stage_guid, stage_name, set_code, set_name, gip_start, gip_end, sprut.GetSprutConnectionString());
                frmSetHis.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void dgvDepartmentPlan_MouseClick(object sender, MouseEventArgs e)
        {
            var hittestInfo = dgvDepartmentPlan.HitTest(e.X, e.Y);
            int row = hittestInfo.RowIndex;
            int col = hittestInfo.ColumnIndex;

            if ((row < 0) || (col < 0))
                return;

            string contract_code = dgvDepartmentPlan.Rows[row].Cells["Contract"].Value.ToString();
            string agreed_status = dgvDepartmentPlan.Rows[row].Cells["AgreedStatus"].Value.ToString();
            string building = dgvDepartmentPlan.Rows[row].Cells["Building"].Value.ToString(); //EL2 другие работы

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

            }
            else
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (
                            (sprut.IsMonthLast(dtpDepartmentPlan.Value)  //EL
                            ||contract_code.Equals("996"))
                            || (contract_code.Equals("997"))
                            || (contract_code.Equals("998"))
                            || (contract_code.Equals("999"))
                            || (building.Equals("Работы по проекту"))  //EL_2
                       )
                    {
                        dgvDepartmentPlan_Menu_Agree.Visible = false;
                        dgvDepartmentPlan_Menu_Request.Visible = false;
                    }
                    else
                    if(agreed_status.Equals("На согласовании"))
                    {
                        dgvDepartmentPlan_Menu_Agree.Visible = false;
                        dgvDepartmentPlan_Menu_Request.Visible = false;
                        dgvDepartmentPlan_Menu_DeleteRequest.Visible = true;
                    }
                    else
                    {
                        dgvDepartmentPlan_Menu_Agree.Visible = true;
                        dgvDepartmentPlan_Menu_Request.Visible = true;
                        dgvDepartmentPlan_Menu_DeleteRequest.Visible = false;
                    }

                    dgvDepartmentPlan.ClearSelection();
                    dgvDepartmentPlan.Rows[row].Selected = true;
                    dgvDepartmentPlan_Menu.Show(dgvDepartmentPlan, new Point(e.X, e.Y));
                }
        }

        private void dgvDepartmentPlan_Menu_Request_Click(object sender, EventArgs e)
        {
            string set_guid = "";
            string stage_guid = "";

            string stage_name = "";
            string set_name = "";
            string set_code = "";
            string contract = "";
            string gip_start = "";
            string gip_end = "";

            string set_start;
            string set_end;

            try
            {
                set_guid = dgvDepartmentPlan.SelectedRows[0].Cells["SetGuid"].Value.ToString();
                stage_guid = dgvDepartmentPlan.SelectedRows[0].Cells["StageGuid"].Value.ToString();
                stage_name = dgvDepartmentPlan.SelectedRows[0].Cells["StageName"].Value.ToString();
                set_name = dgvDepartmentPlan.SelectedRows[0].Cells["SetName"].Value.ToString();
                set_code = dgvDepartmentPlan.SelectedRows[0].Cells["SetCode"].Value.ToString();
                contract = dgvDepartmentPlan.SelectedRows[0].Cells["Contract"].Value.ToString();
                gip_start = dgvDepartmentPlan.SelectedRows[0].Cells["GipStart"].Value.ToString();
                gip_end = dgvDepartmentPlan.SelectedRows[0].Cells["GipEnd"].Value.ToString();
                set_start = dgvDepartmentPlan.SelectedRows[0].Cells["SetStart"].Value.ToString();
                set_end = dgvDepartmentPlan.SelectedRows[0].Cells["SetEnd"].Value.ToString();

                clSet set = sprut.DepartmentPlan_GetSet(set_guid);
                if (set != null)
                {
                    frmRequestSetNewDate frmSetRequest = new frmRequestSetNewDate();
                    frmSetRequest.Owner = this;
                    frmSetRequest.ShowDialog(ref set, sprut.GetSprutConnectionString(), sprut.CurrentUser_GetUser());
                    sprut.DepartmentPlan_Update_SetStatus(set_guid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source);
            }
        }

        private void dgvDepartmentPlan_Manu_SaveToExcel_Click(object sender, EventArgs e)
        {
                SaveGridToExcell(dgvDepartmentPlan);
        }

        private void SaveGridToExcell(DataGridView grid)
        {
            if((grid.Columns == null) || (grid.Columns.Count <= 0))
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
                            _sheet.Cells[r+2, col_index] = grid.Rows[r].Cells[col.Name].Value.ToString();
                            rng = (Excel.Range)_sheet.Cells[r + 2, col_index];
                            rng.WrapText = true;
                            rng.VerticalAlignment = Excel.Constants.xlTop; //EL_1
                        }
                    }
                }
                _excel.ScreenUpdating = true;
                _excel.Visible = true;
            }
            catch(Exception ex)
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

        private void dgvIndividualPlan_Menu_SaveToExcel_Click(object sender, EventArgs e)
        {
            SaveGridIndividualPlanToExcell(dgvIndividualPlan);
        }

        private void SaveGridIndividualPlanToExcell(DataGridView grid)
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
                        

                        if (DateTime.TryParse(col.Name, out dt))
                        {
                            rng.ColumnWidth = 8;
                            _sheet.Cells[row_index, col_index] = dt.Day.ToString() + "." + dt.Month.ToString();
                        }
                        else
                        {
                            rng.ColumnWidth = 16;
                            _sheet.Cells[row_index, col_index] = col.HeaderText;
                        }



                        rng.WrapText = true;

                        for (int r = 0; r < grid.RowCount; r++)
                        {
                            if (grid.Rows[r].Cells[col.Name].Style.ForeColor != Color.Transparent)
                            {
                                _sheet.Cells[r + 2, col_index] = grid.Rows[r].Cells[col.Name].Value.ToString();
                                rng = (Excel.Range)_sheet.Cells[r + 2, col_index];
                                rng.WrapText = true;
                            }
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

        private void dgvSelectedPlan_Menu_SaveToExcel_Click(object sender, EventArgs e)
        {
            SaveGridToExcell(dgvSelectedPlan);
        }

        private void dgvDepartmentUsers_Menu_SaveToExcel_Click(object sender, EventArgs e)
        {
            SaveGridToExcell(dgvDepartmentUsers);
        }

        private void dgvDepartmentUsers_MouseClick(object sender, MouseEventArgs e)
        {
            var hittestInfo = dgvDepartmentUsers.HitTest(e.X, e.Y);
            int row = hittestInfo.RowIndex;
            int col = hittestInfo.ColumnIndex;

            if ((row < 0) || (col < 0))
                return;


            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

            }
            else
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    dgvDepartmentUsers.ClearSelection();
                    dgvDepartmentUsers.Rows[row].Selected = true;
                    dgvDepartmentUsers_Menu.Show(dgvDepartmentUsers, new Point(e.X, e.Y));
                }
        }

        private void dgvAgreement_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {           
            if ((e.ColumnIndex < 0) || (e.RowIndex < 0))
                return;

            var senderGrid = (DataGridView)sender;
            if (true)
            {
                int row = e.RowIndex;
                int col = e.ColumnIndex;

                string button_name = senderGrid.Columns[e.ColumnIndex].Name.ToString();

                string request_id = dgvAgreement.Rows[row].Cells["Id"].Value.ToString();
                string stage_guid = dgvAgreement.Rows[row].Cells["StageGuid"].Value.ToString();
                string set_guid = dgvAgreement.Rows[row].Cells["SetGuid"].Value.ToString();
                string user_id = sprut.CurrentUser_GetUserId();
                string comment = dgvAgreement.Rows[row].Cells["CommentResponse"].Value.ToString();
                string status = dgvAgreement.Rows[row].Cells["AgreedStatus"].Value.ToString();
                int trans_to_prj = 0;

                switch(button_name)
                {
                    case "BtnAgree" :
                        {
                            //Согласуем даты
                            if (status.Equals("request"))
                            {
                                status = "agree";
                                trans_to_prj = 0;
                                Response_Create(request_id, stage_guid, set_guid, user_id, status, comment);
                            }
                            break;
                        }
                    case "BtnDisAgree" : 
                        {
                            if (status.Equals("request"))
                            {
                                //Отклоняем запрос
                                status = "disagree";
                                trans_to_prj = 0;

                                //Обязательно должна быть указана причина отказа
                                if (String.IsNullOrEmpty(comment))
                                {
                                    MessageBox.Show("Необходимо указать комментарий!", "Нет комментария", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else
                                {
                                    Response_Create(request_id, stage_guid, set_guid, user_id, status, comment);
                                }
                            }
                            break;
                        }
                    case "BtnTransferToProject" : 
                        {
                            if (status.Equals("agree"))
                            {
                                //Сохраняем отметку о переносе согласованной даты в Project
                                status = "agree";
                                trans_to_prj = 1;
                                Request_TransferToProject(request_id, stage_guid, set_guid, user_id, trans_to_prj);
                            }
                            break;
                        }
                    default :
                        {
                            status = "request";
                            trans_to_prj = 0;
                            return;
                        }
                }

                //Обновляем информацию о только что обработанном запросе
                sprut.Agreement_RefreshRequestInfo(set_guid, stage_guid, request_id);

                //dgvAgreement_RefreshRowButtons(stage_guid, set_guid, request_id);

            }
        }

        
        private void dgvAgreement_RefreshRowButtons(string stage_guid, string set_guid, string request_id)
        {
            if ((dgvAgreement == null) || (dgvAgreement.Rows == null))
                return;

            for (int i = 0; i < dgvAgreement.Rows.Count;i++ )
            {
                if ((dgvAgreement.Rows[i].Cells["StageGuid"].Value.ToString()==stage_guid)
                    && (dgvAgreement.Rows[i].Cells["SetGuid"].Value.ToString() == set_guid)
                    && (dgvAgreement.Rows[i].Cells["Id"].Value.ToString() == request_id))
                {
                    string status = dgvAgreement.Rows[i].Cells["AgreedStatus"].Value.ToString();
                    dgvAgreement_UpdateButtons(i, status);
                }
            }
        }
        
        /// <summary>
        /// Сохраняет в БД отметку о переносе дат в Project
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="stage_guid"></param>
        /// <param name="set_guid"></param>
        /// <param name="user_id"></param>
        /// <param name="trans_to_prj"></param>
        private void Request_TransferToProject(string request_id, string stage_guid, string set_guid, string user_id, int trans_to_prj)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(sprut.GetSprutConnectionString()))
                {
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = "UPDATE [" + frmMain.SqlLink + "].Platan.dbo.Request SET transfer_to_prj = @trans_to_prj, transfer_to_prj_u_id = @user_id WHERE id = @request_id AND stage_guid = @stage_guid AND set_guid = @set_guid";
                        comm.Parameters.AddWithValue("@trans_to_prj", trans_to_prj);
                        comm.Parameters.AddWithValue("@user_id",user_id);
                        comm.Parameters.AddWithValue("@request_id", request_id);
                        comm.Parameters.AddWithValue("@stage_guid", stage_guid);
                        comm.Parameters.AddWithValue("@set_guid", set_guid);

                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Сохраняет в БД ответ на запрос
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="stage_guid"></param>
        /// <param name="set_guid"></param>
        /// <param name="user_id"></param>
        /// <param name="status"></param>
        /// <param name="comment"></param>
        private void Response_Create(string request_id, string stage_guid, string set_guid, string user_id, string status, string comment)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(sprut.GetSprutConnectionString()))
                {
                    conn.Open();
                    string query = "";
                    query = "insert into [" + frmMain.SqlLink + "].Platan.dbo.Response (request_id, user_id, response_date, comment, status) values (@request_id, @user_id, @response_date,  @comment, @status)";
                    using(SqlCommand comm = new SqlCommand(query,conn))
                    {
                        comm.Parameters.AddWithValue("@request_id", request_id);
                        comm.Parameters.AddWithValue("@user_id", user_id);
                        comm.Parameters.AddWithValue("@response_date", DateTime.Now.ToString());
                        comm.Parameters.AddWithValue("@comment", comment);
                        comm.Parameters.AddWithValue("@status", status);
                        comm.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void cmbxAgreement_Departments_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //string dept_small_name = cmbxAgreement_Departments.SelectedValue.ToString();

            //if(!String.IsNullOrEmpty(dept_small_name))
            //{
                //sprut.Agreement_LoadRequests();
            //}
        }

        private void picbxRefreshAgreement_Click(object sender, EventArgs e)
        {
            Request_LoadAll();
        }

        /// <summary>
        /// Загрузка из БД всех запросов на перенос
        /// </summary>
        private void Request_LoadAll()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //string dept_small_name = cmbxAgreement_Departments.SelectedValue.ToString();

                //if (!String.IsNullOrEmpty(dept_small_name))
                //{
                    sprut.Agreement_LoadRequests();
                //}
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

        private void picbxRefreshAgreement_MouseEnter(object sender, EventArgs e)
        {
            picbxRefreshAgreement.BorderStyle = BorderStyle.FixedSingle;
        }

        private void picbxRefreshAgreement_MouseLeave(object sender, EventArgs e)
        {
            picbxRefreshAgreement.BorderStyle = BorderStyle.Fixed3D;
        }

        private void dgvDepartmentPlan_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if((e.RowIndex == -1) )
            {
                string col_name = dgvDepartmentPlan.Columns[e.ColumnIndex].Name.ToString();

                switch(col_name)
                {
                    case "IsMonthPlan" : 
                        {
                            dgvDepartmentPlan.Columns[e.ColumnIndex].HeaderCell.ToolTipText = "'+' - задача попадает в производственный план на месяц\n'-' - иначе";
                            break;
                        }
                    case "SetStart":
                        {
                            dgvDepartmentPlan.Columns[e.ColumnIndex].HeaderCell.ToolTipText = "Согласованное начало работ";
                            break;
                        }
                    case "SetEnd":
                        {
                            dgvDepartmentPlan.Columns[e.ColumnIndex].HeaderCell.ToolTipText = "Согласованное окончание работ";
                            break;
                        }
                    default :
                        {
                            break;
                        }
                }

            }
        }

        private void dgvDepartmentPlan_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0))
                return;

            string col_name = dgvDepartmentPlan.Columns[e.ColumnIndex].Name;
            string set_end_s = dgvDepartmentPlan.Rows[e.RowIndex].Cells["SetEnd"].Value.ToString();
            string gip_end_s = dgvDepartmentPlan.Rows[e.RowIndex].Cells["GipEnd"].Value.ToString();
            DateTime set_end;
            DateTime gip_end;
            //if(col_name.Equals("SetEnd"))
            if (col_name.Equals("GipEnd")) //EL_2
            {
                

                if( (DateTime.TryParse(set_end_s, out set_end)) && (DateTime.TryParse(gip_end_s, out gip_end)) )
                {
                    if(set_end < gip_end)
                    {
                        dgvDepartmentPlan.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Pink;  //EL_2
                    }
                    
                }
            }
            if (col_name.Equals("AgreedStatus")) //EL3
            {
                if ((DateTime.TryParse(set_end_s, out set_end)) && (DateTime.TryParse(gip_end_s, out gip_end)))
                {
                    DateTime dt = dtpDepartmentPlan.Value;
                    dt = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
                    if (set_end < dt && !String.IsNullOrEmpty(dgvDepartmentPlan.Rows[e.RowIndex].Cells["AgreedStatus"].Value.ToString()))
                    {
                        dgvDepartmentPlan.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Pink;
                    }
                }
            }

        }

        private void chkBoxFilterUserTableByMark_CheckedChanged(object sender, EventArgs e)
        {
            //if(!chkBoxFilterUserTableByMark.Checked)
            //{
            //    dgvDepartmentUsers.DataSource = sprut.dtDepartmentUsers;
            //}
        }

        //private void btnHeadApprove_Click(object sender, EventArgs e) //Утверждение
        //{
        //    sprut.DepartmentPlan_Save();
        //    sprut.DepartmentPlan_Update(dtpDepartmentPlan.Value);
        //    this.Cursor = Cursors.WaitCursor;
        //    try
        //    {
        //        sprut.DepartmentPlan_TrySendToApprove();
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
        //    }
        //    finally
        //    {
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        private void dgvDepartmentPlan_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if ((row < 0) || (col < 0))
                return;

            string col_name = dgvDepartmentPlan.Columns[col].Name;

            if (!col_name.Equals("PercentComplete"))
                return;

            string percent_comlete = dgvDepartmentPlan.Rows[row].Cells[col].Value.ToString();
            string set_guid = dgvDepartmentPlan.Rows[row].Cells["SetGuid"].Value.ToString();
            string stage_guid = dgvDepartmentPlan.Rows[row].Cells["StageGuid"].Value.ToString();

            if (!string.IsNullOrEmpty(percent_comlete))
            {
                sprut.DepartmentPlan_AddPercentComplete(set_guid, percent_comlete);
            }
        }

        private void dgvDepartmentPlan_Menu_Agree_Click(object sender, EventArgs e)
        {
            try
            {
                string set_guid = dgvDepartmentPlan.SelectedRows[0].Cells["SetGuid"].Value.ToString();
                string stage_guid = dgvDepartmentPlan.SelectedRows[0].Cells["StageGuid"].Value.ToString();
                string stage_name = dgvDepartmentPlan.SelectedRows[0].Cells["StageName"].Value.ToString();
                string set_name = dgvDepartmentPlan.SelectedRows[0].Cells["SetName"].Value.ToString();
                string set_code = dgvDepartmentPlan.SelectedRows[0].Cells["SetCode"].Value.ToString();
                string contract = dgvDepartmentPlan.SelectedRows[0].Cells["Contract"].Value.ToString();
                string gip_start = dgvDepartmentPlan.SelectedRows[0].Cells["GipStart"].Value.ToString();
                string gip_end = dgvDepartmentPlan.SelectedRows[0].Cells["GipEnd"].Value.ToString();
                string set_start = dgvDepartmentPlan.SelectedRows[0].Cells["SetStart"].Value.ToString();
                string set_end = dgvDepartmentPlan.SelectedRows[0].Cells["SetEnd"].Value.ToString();
                string old_start = set_start;
                string old_end = set_end;
                DateTime gipend = Convert.ToDateTime(gip_end);
                DateTime gipstart = Convert.ToDateTime(gip_start);
                DateTime setend = Convert.ToDateTime(set_end);
                if ((gipend > setend)&&!sprut.IsMonthLast(gipend,dtpDepartmentPlan.Value))
                {
                    DialogResult result = MessageBox.Show("Согласовать сроки графика :\nНачало - " + gipstart.ToShortDateString() + " \nОкончание - " + gipend.ToShortDateString() + "?", "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        set_start = gip_start;
                        set_end = gip_end;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (sprut.IsMonthLast(setend,dtpDepartmentPlan.Value))
                    {
                        MessageBox.Show("Невозможно согласовать сроки выполнения работ прошлых периодов,\nвозможен только перенос сроков выполнения! ");
                        return;
                    }
                }
                DateTime dt;
                if (DateTime.TryParse(set_start, out dt))
                    set_start = dt.ToShortDateString();
                if (DateTime.TryParse(set_end, out dt))
                    set_end = dt.ToShortDateString();
                if (DateTime.TryParse(old_start, out dt))
                    old_start = dt.ToShortDateString();
                if (DateTime.TryParse(old_end, out dt))
                    old_end = dt.ToShortDateString();


                clSet set = sprut.DepartmentPlan_GetSet(set_guid);
                if (set != null)
                {
                    SqlConnection con = null;
                    SqlCommand com = null;
                    try
                    {
                        con = new SqlConnection(sprut.GetSprutConnectionString());
                        string query = "insert into [" + frmMain.SqlLink + "].Platan.dbo.Request (set_guid, stage_guid, user_id, start_date, end_date, request_date, reason, comment, dept_id, status, old_start_date, old_end_date) values (@set_guid, @stage_guid, @user_id, @start_date, @end_date, @request_date, @reason, @comment, @dept_id, @status, @old_start, @old_end)";
                        con.Open();

                        com = new SqlCommand(query, con);
                        com.Parameters.AddWithValue("@set_guid", set_guid);
                        com.Parameters.AddWithValue("@stage_guid", stage_guid);
                        com.Parameters.AddWithValue("@user_id", sprut.CurrentUser_GetUserId());
                        com.Parameters.AddWithValue("@start_date", set_start);
                        com.Parameters.AddWithValue("@end_date", set_end);
                        com.Parameters.AddWithValue("@request_date", DateTime.Now.ToString());
                        com.Parameters.AddWithValue("@reason", "");
                        com.Parameters.AddWithValue("@comment", "Согласен с предложенными датами");
                        com.Parameters.AddWithValue("@dept_id", sprut.CurrentUser_GetDepartmentId());
                        com.Parameters.AddWithValue("@status", "agree");
                        com.Parameters.AddWithValue("@old_start", old_start);
                        com.Parameters.AddWithValue("@old_end", old_end); 

                        com.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                    }
                    finally
                    {
                        if (con != null)
                        {
                            con = null;
                        }
                    }

                    sprut.DepartmentPlan_Update_SetStatus(set_guid);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source);
            }

        }

        private void dgvIndividualPlan_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0))
                return;

            string param = dgvIndividualPlan.Rows[e.RowIndex].Cells["param"].Value.ToString();
        }

        private void dgvIndividualPlan_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex != -1) || (e.ColumnIndex < 0))
                return;

            string col_name = dgvIndividualPlan.Columns[e.ColumnIndex].Name;

            if (!col_name.Equals("SetCodeName"))
                return;
        }

        private void dgvIndividualPlan_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //int col = this.dgvIndividualPlan.CurrentCell.ColumnIndex;
            if (this.dgvIndividualPlan.CurrentRow == null)
                return;
            if (e.RowIndex == this.dgvIndividualPlan.CurrentRow.Index)
            {
                using (Pen pen = new Pen(Color.Blue))
                {
                    int penWidth = 2;

                    pen.Width = penWidth;

                    int x = e.RowBounds.Left + (penWidth / 2);
                    int y = e.RowBounds.Top + (penWidth / 2);
                    int width = e.RowBounds.Width - penWidth;
                    int height = e.RowBounds.Height - penWidth;

                    e.Graphics.DrawRectangle(pen, x, y, width, height);
                }
            }
        }

        private void dgvIndividualPlan_SelectionChanged(object sender, EventArgs e)
        {
            dgvIndividualPlan.Refresh();
        }

        private void dgvIndividualPlan_Scroll(object sender, ScrollEventArgs e)
        {
            if(e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                dgvIndividualPlan.Refresh();
        }

        private void dgvSelectedPlan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if ((e.RowIndex < 0) || (e.ColumnIndex < 0))
                    return;

                int row = e.RowIndex;
                int col = e.ColumnIndex;

                string set_name = dgvSelectedPlan.Rows[row].Cells["SetName"].Value.ToString();

                dgvSelectedPlan.Rows[row].Cells["SetCode"].ToolTipText = set_name;
            }
            catch(Exception ex)
            {
                
            }
        }

        private void dgvAgreement_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvAgreement.IsCurrentCellDirty)
            {
                dgvAgreement.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvAgreement_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string AgreedStatus = dgvAgreement.Rows[e.RowIndex].Cells["AgreedStatus"].Value.ToString();
            dgvAgreement_UpdateButtons(e.RowIndex, AgreedStatus);

        }

        private void dgvAgreement_UpdateButtons(int row, string AgreedStatus)
        {
            /*DataGridViewDisableButtonCell buttonCell;
            switch (AgreedStatus)
            {
                case "request":
                    {
                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnAgree"];
                        buttonCell.Enabled = true;

                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnDisAgree"];
                        buttonCell.Enabled = true;

                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnTransferToProject"];
                        buttonCell.Enabled = false;

                        //dgvAgreement.InvalidateCell(buttonCell.ColumnIndex, buttonCell.RowIndex);
                        break;
                    }
                case "agree":
                    {
                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnAgree"];
                        buttonCell.Enabled = false;

                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnDisAgree"];
                        buttonCell.Enabled = false;

                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnTransferToProject"];
                        buttonCell.Enabled = true;

                        //dgvAgreement.InvalidateCell(buttonCell.ColumnIndex, buttonCell.RowIndex);
                        break;
                    }
                case "disagree":
                    {
                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnAgree"];
                        buttonCell.Enabled = false;

                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnDisAgree"];
                        buttonCell.Enabled = false;

                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnTransferToProject"];
                        buttonCell.Enabled = false;

                        //dgvAgreement.InvalidateCell(buttonCell.ColumnIndex, buttonCell.RowIndex);
                        break;
                    }
                default:
                    {
                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnAgree"];
                        buttonCell.Enabled = false;

                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnDisAgree"];
                        buttonCell.Enabled = false;

                        buttonCell = (DataGridViewDisableButtonCell)dgvAgreement.Rows[row].Cells["btnTransferToProject"];
                        buttonCell.Enabled = false;

                        //dgvAgreement.InvalidateCell(buttonCell.ColumnIndex, buttonCell.RowIndex);
                        break;
                    }
            }*/

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dgvAgreement_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex > -1 & e.RowIndex > -1)
            {
                string agreed_status = dgvAgreement.Rows[e.RowIndex].Cells["AgreedStatus"].Value.ToString();
                string trans_to_prj = dgvAgreement.Rows[e.RowIndex].Cells["TransferToPrj"].Value.ToString();

                //Pen for left and top borders
                var backGroundPen = new Pen(e.CellStyle.BackColor, 1);

                //Pen for bottom and right borders
                var gridlinePen = new Pen(dgvAgreement.GridColor, 1);

                var column_name = dgvAgreement.Columns[e.ColumnIndex].Name;

                var topLeftPoint = new Point(e.CellBounds.Left, e.CellBounds.Top);
                var topRightPoint = new Point(e.CellBounds.Right - 1, e.CellBounds.Top);
                var bottomRightPoint = new Point(e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                var bottomLeftPoint = new Point(e.CellBounds.Left, e.CellBounds.Bottom - 1);

                switch (column_name)
                {
                    case "BtnAgree":
                        {
                            gridlinePen = new Pen(dgvAgreement.GridColor, 2);
                            if (agreed_status.Equals("request"))
                            {
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(215, 255, 215);
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = Color.Black;
                            }
                            else
                            {
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightGray;
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = Color.Transparent;
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.LightGray;
                            }

                            
                            break;
                        }
                    case "BtnDisAgree":
                        {
                            gridlinePen = new Pen(dgvAgreement.GridColor, 2);
                            if (agreed_status.Equals("request"))
                            {
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(255, 215, 215);
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = Color.Black;
                            }
                            else
                            {
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightGray;
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = Color.Transparent;
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.LightGray;
                            }
                            break;
                        }
                    case "BtnTransferToProject":
                        {
                            gridlinePen = new Pen(dgvAgreement.GridColor, 2);
                            
                            if (agreed_status.Equals("request"))
                            {
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightGray;
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = Color.Transparent;
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.LightGray;
                            }
                            else
                            {
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(110, 140, 240);
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
                                dgvAgreement.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = Color.Black;
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                //Paint all parts except borders.
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                //Top border of first row cells should be in background color
                if (e.RowIndex == 0)
                    e.Graphics.DrawLine(backGroundPen, topLeftPoint, topRightPoint);

                //Left border of first column cells should be in background color
                if (e.ColumnIndex == 0)
                    e.Graphics.DrawLine(backGroundPen, topLeftPoint, bottomLeftPoint);

                //Bottom border of last row cells should be in gridLine color
                if (e.RowIndex == dgvAgreement.RowCount - 1)
                    e.Graphics.DrawLine(gridlinePen, bottomRightPoint, bottomLeftPoint);
                else  //Bottom border of non-last row cells should be in background color
                    e.Graphics.DrawLine(backGroundPen, bottomRightPoint, bottomLeftPoint);

                //Right border of last column cells should be in gridLine color
                if (e.ColumnIndex == dgvAgreement.ColumnCount - 1)
                    e.Graphics.DrawLine(gridlinePen, bottomRightPoint, topRightPoint);
                else //Right border of non-last column cells should be in background color
                    e.Graphics.DrawLine(backGroundPen, bottomRightPoint, topRightPoint);

                //Top border of non-first row cells should be in gridLine color, and they should be drawn here after right border
                if (e.RowIndex > 0)
                    e.Graphics.DrawLine(gridlinePen, topLeftPoint, topRightPoint);

                //Left border of non-first column cells should be in gridLine color, and they should be drawn here after bottom border
                if (e.ColumnIndex > 0)
                    e.Graphics.DrawLine(gridlinePen, topLeftPoint, bottomLeftPoint);

                e.Handled = true;

            }
           
        }

       

       

        //EL_1
        private void button2_Click(object sender, EventArgs e)
        {
            SaveGridToExcell(dgvAgreement);
        }

        //EL_2
        private void button1_Click_1(object sender, EventArgs e)
        {
            sprut.AgreeSetsPlaned();
        }

        //EL_2
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    dtpProductionPlanDept.Value = dtpProductionPlanDept.Value.AddMonths(1);  //EL_2
        //    ProductionPlanDept_UpDate(dtpProductionPlanDept.Value);
        //}

        //EL_2
        //private void btnProductionPlanDeptIncDec_Click(object sender, EventArgs e)
        //{
        //    dtpProductionPlanDept.Value = dtpProductionPlanDept.Value.AddMonths(-1);  //EL_2
        //    ProductionPlanDept_UpDate(dtpProductionPlanDept.Value);
        //}

        //EL_2
        //private void ProductionPlanDept_UpDate(DateTime dt)
        //{
        //    this.Cursor = Cursors.WaitCursor;
            
        //    try
        //    {
        //        this.label5.Text = "";
        //        this.richTextBox2.Text = "";
        //        dgvProductionPlanDept.DataSource = null;
        //        sprut.ProductionPlanRequest_Load(dt);
        //        dgvProductionPlanDept.DataSource = sprut.dtProductionPlanDept;
        //        if (sprut.dtProductionPlanDept.Rows.Count > 0)
        //        {
        //            string statusPl = sprut.dtProductionPlanDept.Rows[0]["pp_status"].ToString();
        //            this.label5.Text = statusPl;
        //            this.label5.BackColor = (statusPl == "Отклонен" ? Color.Pink : statusPl == "Утвержден"? Color.FromArgb(190,250,240):Color.LightYellow);
        //            this.richTextBox2.Text = sprut.dtProductionPlanDept.Rows[0]["ceo_comment"].ToString();
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
        //    }
        //    finally
        //    {
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        //EL_2
        //private void Create_dgvProductionPlanDept()
        //{
        //    dgvProductionPlanDept.AutoGenerateColumns = false;
        //    dgvProductionPlanDept.ColumnCount = 19;

        //    dgvProductionPlanDept.Columns[0].Name = "set_guid";
        //    dgvProductionPlanDept.Columns[0].DataPropertyName = "set_guid";
        //    dgvProductionPlanDept.Columns[0].HeaderText = "set_guid";
        //    dgvProductionPlanDept.Columns[0].Visible = false;

        //    dgvProductionPlanDept.Columns[1].Name = "stage_guid";
        //    dgvProductionPlanDept.Columns[1].DataPropertyName = "stage_guid";
        //    dgvProductionPlanDept.Columns[1].HeaderText = "stage_guid";
        //    dgvProductionPlanDept.Columns[1].Visible = false;

        //    dgvProductionPlanDept.Columns[2].Name = "contract";
        //    dgvProductionPlanDept.Columns[2].DataPropertyName = "contract";
        //    dgvProductionPlanDept.Columns[2].HeaderText = "Договор";
        //    dgvProductionPlanDept.Columns[2].Visible = true;

        //    dgvProductionPlanDept.Columns[3].Name = "stage_name";
        //    dgvProductionPlanDept.Columns[3].DataPropertyName = "stage_name";
        //    dgvProductionPlanDept.Columns[3].HeaderText = "Стадия";
        //    dgvProductionPlanDept.Columns[3].Visible = true;

        //    dgvProductionPlanDept.Columns[4].Name = "kks";
        //    dgvProductionPlanDept.Columns[4].DataPropertyName = "kks";
        //    dgvProductionPlanDept.Columns[4].HeaderText = "KKS";
        //    dgvProductionPlanDept.Columns[4].Visible = true;

        //    dgvProductionPlanDept.Columns[5].Name = "building";
        //    dgvProductionPlanDept.Columns[5].DataPropertyName = "building";
        //    dgvProductionPlanDept.Columns[5].HeaderText = "Сооружение";
        //    dgvProductionPlanDept.Columns[5].Visible = true;

        //    dgvProductionPlanDept.Columns[6].Name = "set_code";
        //    dgvProductionPlanDept.Columns[6].DataPropertyName = "set_code";
        //    dgvProductionPlanDept.Columns[6].HeaderText = "Комплект";
        //    dgvProductionPlanDept.Columns[6].Visible = true;

        //    dgvProductionPlanDept.Columns[7].Name = "set_name";
        //    dgvProductionPlanDept.Columns[7].DataPropertyName = "set_name";
        //    dgvProductionPlanDept.Columns[7].HeaderText = "Наименование комплекта";
        //    dgvProductionPlanDept.Columns[7].Visible = true;

        //    dgvProductionPlanDept.Columns[8].Name = "set_start";
        //    dgvProductionPlanDept.Columns[8].DataPropertyName = "set_start";
        //    dgvProductionPlanDept.Columns[8].HeaderText = "Начало";
        //    dgvProductionPlanDept.Columns[8].Visible = true;

        //    dgvProductionPlanDept.Columns[9].Name = "set_end";
        //    dgvProductionPlanDept.Columns[9].DataPropertyName = "set_end";
        //    dgvProductionPlanDept.Columns[9].HeaderText = "Окончание";
        //    dgvProductionPlanDept.Columns[9].Visible = true;

        //    dgvProductionPlanDept.Columns[10].Name = "set_start_old";
        //    dgvProductionPlanDept.Columns[10].DataPropertyName = "set_start_old";
        //    dgvProductionPlanDept.Columns[10].HeaderText = "Предыдущая дата начала";
        //    dgvProductionPlanDept.Columns[10].Visible = true;

        //    dgvProductionPlanDept.Columns[11].Name = "set_end_old";
        //    dgvProductionPlanDept.Columns[11].DataPropertyName = "set_end_old";
        //    dgvProductionPlanDept.Columns[11].HeaderText = "Предыдущая дата окончания";
        //    dgvProductionPlanDept.Columns[11].Visible = true;

        //    dgvProductionPlanDept.Columns[12].Name = "reason";
        //    dgvProductionPlanDept.Columns[12].DataPropertyName = "reason";
        //    dgvProductionPlanDept.Columns[12].HeaderText = "Причина переноса";
        //    dgvProductionPlanDept.Columns[12].Visible = true;     
            
        //    dgvProductionPlanDept.Columns[13].Name = "agreed_status";
        //    dgvProductionPlanDept.Columns[13].DataPropertyName = "agreed_status";
        //    dgvProductionPlanDept.Columns[13].HeaderText = "Статус согласования";
        //    dgvProductionPlanDept.Columns[13].Visible = true;

        //    dgvProductionPlanDept.Columns[14].Name = "comment_gip";
        //    dgvProductionPlanDept.Columns[14].DataPropertyName = "comment_gip";
        //    dgvProductionPlanDept.Columns[14].HeaderText = "Комментарий ГИПа";
        //    dgvProductionPlanDept.Columns[14].Visible = true;

        //    dgvProductionPlanDept.Columns[15].Name = "executors";
        //    dgvProductionPlanDept.Columns[15].DataPropertyName = "executors";
        //    dgvProductionPlanDept.Columns[15].HeaderText = "Исполнители";
        //    dgvProductionPlanDept.Columns[15].Visible = true;

        //    dgvProductionPlanDept.Columns[16].Name = "percent_complete";
        //    dgvProductionPlanDept.Columns[16].DataPropertyName = "percent_complete";
        //    dgvProductionPlanDept.Columns[16].HeaderText = "Процент выполнения";
        //    dgvProductionPlanDept.Columns[16].Visible = true;

        //    dgvProductionPlanDept.Columns[17].Name = "price_mng";
        //    dgvProductionPlanDept.Columns[17].DataPropertyName = "price_mng";
        //    dgvProductionPlanDept.Columns[17].HeaderText = "Управленческая стоимость";
        //    dgvProductionPlanDept.Columns[17].Visible = true;

        //    dgvProductionPlanDept.Columns[18].Name = "price_dop";
        //    dgvProductionPlanDept.Columns[18].DataPropertyName = "price_dop";
        //    dgvProductionPlanDept.Columns[18].HeaderText = "стоимость по доп.соглашению";
        //    dgvProductionPlanDept.Columns[18].Visible = true;

        //    //dgvProductionPlanDept.Columns[16].Name = "comment_request";
        //    //dgvProductionPlanDept.Columns[16].DataPropertyName = "comment_request";
        //    //dgvProductionPlanDept.Columns[16].HeaderText = "Комментарий отправителя";
        //    //dgvProductionPlanDept.Columns[16].Visible = false;
        //    dgvProductionPlanDept.DataSource = sprut.dtProductionPlanDept;
            
        //}

        //EL_2
        //private void Create_dgvProductionPlan()
        //{
        //    dgvProductionPlan.AutoGenerateColumns = false;
        //    dgvProductionPlan.ColumnCount = 19;

        //    dgvProductionPlan.Columns[0].Name = "set_guid";
        //    dgvProductionPlan.Columns[0].DataPropertyName = "set_guid";
        //    dgvProductionPlan.Columns[0].HeaderText = "set_guid";
        //    dgvProductionPlan.Columns[0].Visible = false;

        //    dgvProductionPlan.Columns[1].Name = "stage_guid";
        //    dgvProductionPlan.Columns[1].DataPropertyName = "stage_guid";
        //    dgvProductionPlan.Columns[1].HeaderText = "stage_guid";
        //    dgvProductionPlan.Columns[1].Visible = false;

        //    dgvProductionPlan.Columns[2].Name = "contract";
        //    dgvProductionPlan.Columns[2].DataPropertyName = "contract";
        //    dgvProductionPlan.Columns[2].HeaderText = "Договор";
        //    dgvProductionPlan.Columns[2].Visible = true;

        //    dgvProductionPlan.Columns[3].Name = "stage_name";
        //    dgvProductionPlan.Columns[3].DataPropertyName = "stage_name";
        //    dgvProductionPlan.Columns[3].HeaderText = "Стадия";
        //    dgvProductionPlan.Columns[3].Visible = true;

        //    dgvProductionPlan.Columns[4].Name = "kks";
        //    dgvProductionPlan.Columns[4].DataPropertyName = "kks";
        //    dgvProductionPlan.Columns[4].HeaderText = "KKS";
        //    dgvProductionPlan.Columns[4].Visible = true;

        //    dgvProductionPlan.Columns[5].Name = "building";
        //    dgvProductionPlan.Columns[5].DataPropertyName = "building";
        //    dgvProductionPlan.Columns[5].HeaderText = "Сооружение";
        //    dgvProductionPlan.Columns[5].Visible = true;

        //    dgvProductionPlan.Columns[6].Name = "set_code";
        //    dgvProductionPlan.Columns[6].DataPropertyName = "set_code";
        //    dgvProductionPlan.Columns[6].HeaderText = "Комплект";
        //    dgvProductionPlan.Columns[6].Visible = true;

        //    dgvProductionPlan.Columns[7].Name = "set_name";
        //    dgvProductionPlan.Columns[7].DataPropertyName = "set_name";
        //    dgvProductionPlan.Columns[7].HeaderText = "Наименование комплекта";
        //    dgvProductionPlan.Columns[7].Visible = true;

        //    dgvProductionPlan.Columns[8].Name = "set_start";
        //    dgvProductionPlan.Columns[8].DataPropertyName = "set_start";
        //    dgvProductionPlan.Columns[8].HeaderText = "Начало";
        //    dgvProductionPlan.Columns[8].Visible = true;

        //    dgvProductionPlan.Columns[9].Name = "set_end";
        //    dgvProductionPlan.Columns[9].DataPropertyName = "set_end";
        //    dgvProductionPlan.Columns[9].HeaderText = "Окончание";
        //    dgvProductionPlan.Columns[9].Visible = true;

        //    dgvProductionPlan.Columns[10].Name = "set_start_old";
        //    dgvProductionPlan.Columns[10].DataPropertyName = "set_start_old";
        //    dgvProductionPlan.Columns[10].HeaderText = "Предыдущая дата начала";
        //    dgvProductionPlan.Columns[10].Visible = true;

        //    dgvProductionPlan.Columns[11].Name = "set_end_old";
        //    dgvProductionPlan.Columns[11].DataPropertyName = "set_end_old";
        //    dgvProductionPlan.Columns[11].HeaderText = "Предыдущая дата окончания";
        //    dgvProductionPlan.Columns[11].Visible = true;

        //    dgvProductionPlan.Columns[12].Name = "reason";
        //    dgvProductionPlan.Columns[12].DataPropertyName = "reason";
        //    dgvProductionPlan.Columns[12].HeaderText = "Причина переноса";
        //    dgvProductionPlan.Columns[12].Visible = true;

        //    dgvProductionPlan.Columns[13].Name = "agreed_status";
        //    dgvProductionPlan.Columns[13].DataPropertyName = "agreed_status";
        //    dgvProductionPlan.Columns[13].HeaderText = "Статус согласования";
        //    dgvProductionPlan.Columns[13].Visible = true;

        //    dgvProductionPlan.Columns[14].Name = "comment_gip";
        //    dgvProductionPlan.Columns[14].DataPropertyName = "comment_gip";
        //    dgvProductionPlan.Columns[14].HeaderText = "Комментарий ГИПа";
        //    dgvProductionPlan.Columns[14].Visible = true;

        //    //dgvProductionPlan.Columns[15].Name = "status_pp";
        //    //dgvProductionPlan.Columns[15].DataPropertyName = "status_pp";
        //    //dgvProductionPlan.Columns[15].HeaderText = "Статус комплектов в ПП";
        //    //dgvProductionPlan.Columns[15].Visible = true;

        //    dgvProductionPlan.Columns[15].Name = "executors";
        //    dgvProductionPlan.Columns[15].DataPropertyName = "executors";
        //    dgvProductionPlan.Columns[15].HeaderText = "Исполнители";
        //    dgvProductionPlan.Columns[15].Visible = true;

        //    dgvProductionPlan.Columns[16].Name = "percent_complete";
        //    dgvProductionPlan.Columns[16].DataPropertyName = "percent_complete";
        //    dgvProductionPlan.Columns[16].HeaderText = "Процент выполнения";
        //    dgvProductionPlan.Columns[16].Visible = true;
        //    dgvProductionPlan.DataSource = sprut.dtProductionPlanDept;

        //    dgvProductionPlan.Columns[17].Name = "price_mng";
        //    dgvProductionPlan.Columns[17].DataPropertyName = "price_mng";
        //    dgvProductionPlan.Columns[17].HeaderText = "Управленческая стоимость";
        //    dgvProductionPlan.Columns[17].Visible = true;

        //    dgvProductionPlan.Columns[18].Name = "price_dop";
        //    dgvProductionPlan.Columns[18].DataPropertyName = "price_dop";
        //    dgvProductionPlan.Columns[18].HeaderText = "стоимость по доп.соглашению";
        //    dgvProductionPlan.Columns[18].Visible = true;
        //}

        //private void Create_dgvDepartments()
        //{
        //    dgvDepartments.AutoGenerateColumns = false;
        //    dgvDepartments.ColumnCount = 9;

        //    dgvDepartments.Columns[0].Name = "department_small_name";
        //    dgvDepartments.Columns[0].DataPropertyName = "department_small_name";
        //    dgvDepartments.Columns[0].HeaderText = "Отдел";
        //    dgvDepartments.Columns[0].Visible = true;

        //    dgvDepartments.Columns[1].Name = "pp_status";
        //    dgvDepartments.Columns[1].DataPropertyName = "pp_status";
        //    dgvDepartments.Columns[1].HeaderText = "Статус";
        //    dgvDepartments.Columns[1].Visible = true;

        //    dgvDepartments.Columns[2].Name = "send_date";
        //    dgvDepartments.Columns[2].DataPropertyName = "send_date";
        //    dgvDepartments.Columns[2].HeaderText = "Дата отправки";
        //    dgvDepartments.Columns[2].Visible = true;

        //    dgvDepartments.Columns[3].Name = "send_fio";
        //    dgvDepartments.Columns[3].DataPropertyName = "send_fio";
        //    dgvDepartments.Columns[3].HeaderText = "Отправитель";
        //    dgvDepartments.Columns[3].Visible = true;

        //    dgvDepartments.Columns[4].Name = "department_id";
        //    dgvDepartments.Columns[4].DataPropertyName = "department_id";
        //    dgvDepartments.Columns[4].HeaderText = "department_id";
        //    dgvDepartments.Columns[4].Visible = false;

        //    dgvDepartments.Columns[5].Name = "plan_id";
        //    dgvDepartments.Columns[5].DataPropertyName = "plan_id";
        //    dgvDepartments.Columns[5].HeaderText = "plan_id";
        //    dgvDepartments.Columns[5].Visible = false;
            
        //    dgvDepartments.Columns[6].Name = "price_fot";
        //    dgvDepartments.Columns[6].DataPropertyName = "price_fot";
        //    dgvDepartments.Columns[6].HeaderText = "Выручка мин.";
        //    dgvDepartments.Columns[6].Visible = true;
            
        //    dgvDepartments.Columns[7].Name = "price_fakt";
        //    dgvDepartments.Columns[7].DataPropertyName = "price_fakt";
        //    dgvDepartments.Columns[7].HeaderText = "Выручка факт";
        //    dgvDepartments.Columns[7].Visible = true;
            
        //    dgvDepartments.Columns[8].Name = "price_plan";
        //    dgvDepartments.Columns[8].DataPropertyName = "price_plan";
        //    dgvDepartments.Columns[8].HeaderText = "Выручка план";
        //    dgvDepartments.Columns[8].Visible = true;
        //    dgvDepartments.DataSource = sprut.dtDepartments;
        //}

        //EL_2
        //private void button1_Click_2(object sender, EventArgs e)
        //{
        //    SaveGridToExcell(dgvProductionPlanDept);
        //}

        //private void btnProductionPlanMonthInc_Click(object sender, EventArgs e)
        //{
        //    dtpProductionPlan.Value = dtpProductionPlan.Value.AddMonths(1);  //EL_2
        //    int row = dgvDepartments.SelectedRows[0].Index;
        //    sprut.dtDepartments.Clear();
        //    sprut.AllProductionPlanRequest_Load(dtpProductionPlan.Value);
        //    dgvDepartments.Rows[row].Selected = true;
        //}

        //private void btnProductionPlanMonthDec_Click(object sender, EventArgs e)
        //{
        //    dtpProductionPlan.Value = dtpProductionPlan.Value.AddMonths(-1);  //EL_2
        //    int row = dgvDepartments.SelectedRows[0].Index;
        //    sprut.dtDepartments.Clear();
        //    sprut.AllProductionPlanRequest_Load(dtpProductionPlan.Value);
        //    dgvDepartments.Rows[row].Selected = true;
        //}

        //private void dgvDepartments_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (dgvDepartments.SelectedRows.Count > 0)
        //    {
        //        int RowIndex = dgvDepartments.SelectedRows[0].Index;
        //        if (RowIndex < 0)
        //            return;
        //        string plan_id = dgvDepartments.Rows[RowIndex].Cells["plan_id"].Value.ToString();
        //        string statusPP = dgvDepartments.Rows[RowIndex].Cells["pp_status"].Value.ToString();
        //        string comment = sprut.dtDepartments.Rows[RowIndex]["ceo_comment"].ToString();
        //        richTextBox1.Text = comment;
        //        richTextBox1.Enabled = (statusPP == "На утверждении" ? true : false);
        //        DateTime dt = dtpProductionPlan.Value;
        //        dgvProductionPlan.DataSource = null;
        //        sprut.ProductionPlan_Load(plan_id);
        //        dgvProductionPlan.DataSource = sprut.dtProductionPlan;
        //    }
        //}

        //private void btnProductionPlan_Approve_Click(object sender, EventArgs e)
        //{
        //    ChangeStatusPP("agree");
        //}       

        //private void btnProductionPlan_Reject_Click(object sender, EventArgs e)
        //{
        //    if (String.IsNullOrEmpty(this.richTextBox1.Text))
        //        MessageBox.Show("Необходимо добавить в комментарий причину отклонения утверждения производственного плана отдела!");
        //    else
        //       //ChangeStatusPP("disagree");
        //}

        //private void ChangeStatusPP(string st)
        //{
        //    int RowIndex = dgvDepartments.SelectedRows[0].Index;
        //    if (RowIndex < 0)
        //        return;
        //    string status = dgvDepartments.Rows[RowIndex].Cells["pp_status"].Value.ToString();
        //    string comment = richTextBox1.Text;
        //    if (status == "На утверждении")
        //    {
        //        string plan_id = dgvDepartments.Rows[RowIndex].Cells["plan_id"].Value.ToString();
        //        //sprut.UpdateStatusProductionPlan(plan_id, st, comment, RowIndex);
        //        richTextBox1.Enabled = false;
        //        sprut.dtDepartments.Rows[RowIndex]["pp_status"] = sprut.ApprovedStatus(st);
        //        sprut.dtDepartments.Rows[RowIndex]["ceo_comment"] = comment;

        //        //перечитать из базы
        //        //sprut.dtDepartments.Clear();
        //        //sprut.AllProductionPlanRequest_Load(dtpProductionPlan.Value);
        //        //dgvDepartments.Rows[RowIndex].Selected = true;
        //    }
        //    else
        //    {
        //        MessageBox.Show(status =="" ? "Производственный план не передан на утверждение" :"Статус производственного плана - " + status + ".");
        //    }
        //}

        private void dgvDepartmentPlan_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView grid = sender as DataGridView ;
            if (e.RowIndex > -1 && e.RowIndex < dgvDepartmentPlan.RowCount)
            {
                //string status = dgvDepartmentPlan.Rows[e.RowIndex].Cells["StatusPP"].Value.ToString();
                //Color cc = grid.Rows[e.RowIndex].DefaultCellStyle.BackColor;
                //cc = (status == "Отклонен" ? Color.Pink : status == "Утвержден" ? Color.FromArgb(190, 250, 240) : status == "На утверждении" ? Color.LightYellow : cc);
                //grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = cc;
            }
        }

        private void dgvDepartmentPlan_Menu_DeleteRequest_Click(object sender, EventArgs e)
        {
            
            string set_guid = dgvDepartmentPlan.SelectedRows[0].Cells["SetGuid"].Value.ToString();
            SqlConnection con = null;
            SqlCommand com = null;
            try
            {
                con = new SqlConnection(sprut.GetSprutConnectionString());
                
                con.Open();

                com = new SqlCommand("[dbo].[PL_DeleteRequestSet]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@set_guid",set_guid);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                if (con != null)
                {
                    con = null;
                }
            }

            sprut.DepartmentPlan_Update_SetStatus(set_guid);
            
             
        }

        private void butPrice_Click(object sender, EventArgs e)
        {

        }

        private void cmbxDepartmentPlan_Departments_MouseHover(object sender, EventArgs e)
        {
            

    ToolTip buttonToolTip = new ToolTip();
    buttonToolTip.ToolTipTitle = "";
    buttonToolTip.UseFading = true;
    buttonToolTip.UseAnimation = true;
    buttonToolTip.IsBalloon = true;
    buttonToolTip.ShowAlways = true;
    buttonToolTip.AutoPopDelay = 5000;
    buttonToolTip.InitialDelay = 1000;
    buttonToolTip.ReshowDelay = 0;

    buttonToolTip.SetToolTip(cmbxDepartmentPlan_Departments, cmbxDepartmentPlan_Departments.Text);

        }

       
        private void dtpDepartmentPlan_CloseUp(object sender, EventArgs e)
        {
            if ((sprut._PlanDate.Year == dtpDepartmentPlan.Value.Year) && (sprut._PlanDate.Month == dtpDepartmentPlan.Value.Month))
            {
                return;
            }
            else
            {
               DepartmentPlan_ChangeMonth();
            }
            
        }



        private void dgvDepartmentPlan_Menu_ShowSet_Click(object sender, EventArgs e)
        {
            string stage_name = "";
            string set_name = "";
            string set_code = "";
            string contract = "";
            string gip_start = "";
            string gip_end = "";
            string set_start = "";
            string set_end = "";
            string building = "";
            string agreed_status = "";
            string executors = "";
            string responsibleUser = "";
            try
            {
                building = dgvDepartmentPlan.SelectedRows[0].Cells["Building"].Value.ToString();
                stage_name = dgvDepartmentPlan.SelectedRows[0].Cells["StageName"].Value.ToString();
                set_name = dgvDepartmentPlan.SelectedRows[0].Cells["SetName"].Value.ToString();
                set_code = dgvDepartmentPlan.SelectedRows[0].Cells["SetCode"].Value.ToString();
                contract = dgvDepartmentPlan.SelectedRows[0].Cells["Contract"].Value.ToString();
                gip_start = dgvDepartmentPlan.SelectedRows[0].Cells["GipStart"].Value.ToString();
                gip_end = dgvDepartmentPlan.SelectedRows[0].Cells["GipEnd"].Value.ToString();
                set_start = dgvDepartmentPlan.SelectedRows[0].Cells["SetStart"].Value.ToString();
                set_end = dgvDepartmentPlan.SelectedRows[0].Cells["SetEnd"].Value.ToString();
                executors = dgvDepartmentPlan.SelectedRows[0].Cells["Executors"].Value.ToString();
                responsibleUser = dgvDepartmentPlan.SelectedRows[0].Cells["ResponsibleUser"].Value.ToString();

                FrmShowPlTask frmSet = new FrmShowPlTask();
                frmSet.Owner = this;
                frmSet.CreateSet(contract, stage_name, building, set_code, set_name, gip_start, gip_end, set_start, set_end, agreed_status, executors, responsibleUser);
                frmSet.Show();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        

        //private void button3_Click_1(object sender, EventArgs e)
        //{
        //    this.Cursor = Cursors.WaitCursor;
        //    try
        //    {
        //        sprut.DepartmentPlan_TrySendToApprove(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
        //    }
        //    finally
        //    {
        //        this.Cursor = Cursors.Default;
        //    }
        //}

       

        //private void butPrice_Click(object sender, EventArgs e)
        //{
        //    string pp_month = dtpProductionPlan.Text;
        //    int RowIndex = dgvDepartments.SelectedRows[0].Index;
        //    string pp_id = dgvDepartments.Rows[RowIndex].Cells["plan_id"].Value.ToString();
        //    string pp_dept = dgvDepartments.Rows[RowIndex].Cells["department_small_name"].Value.ToString();
        //    string price_fot =  dgvDepartments.Rows[RowIndex].Cells["price_fot"].Value.ToString();
        //    string price_plan = dgvDepartments.Rows[RowIndex].Cells["price_plan"].Value.ToString();
        //    string price_fakt = dgvDepartments.Rows[RowIndex].Cells["price_fakt"].Value.ToString();
        //    string pp_title = pp_dept + "  " + pp_month;
        //    frmPriceEdit frmApr = new frmPriceEdit(pp_title);
        //    frmApr.pr_fakt = price_fakt;
        //    frmApr.pr_plan = price_plan;
        //    frmApr.pr_fot = price_fot;
        //    frmApr.Show();
        //    if (!(frmApr.pr_fakt == price_fakt && frmApr.pr_plan == price_plan && frmApr.pr_fot == price_fot))
        //    {
        //        sprut.UpdatePriceProductionPlan(pp_id,frmApr.pr_fot,frmApr.pr_plan,frmApr.pr_fakt);
        //        dgvDepartments.Rows[RowIndex].Cells["price_fot"].Value =  frmApr.pr_fot  == "0" ? "" : frmApr.pr_fot;
        //        dgvDepartments.Rows[RowIndex].Cells["price_plan"].Value = frmApr.pr_plan == "0" ? "" : frmApr.pr_plan;
        //        dgvDepartments.Rows[RowIndex].Cells["price_fakt"].Value = frmApr.pr_fakt == "0" ? "" : frmApr.pr_fakt;
        //    }
        //}

        
        
    }
}
