using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using System.Data.OleDb;

namespace sprut
{
    public class clSprut            
    {
        private double _default_day_work_time = 8;

        private string _connStringSprut = "Data Source=" + frmMain.TdmsSrv + ";Initial Catalog=" + frmMain.TdmsName + ";Persist Security Info=True;User ID=" + frmMain.UserSqlName + ";Password=" + frmMain.UserSqlPassword;
        private string _connStringPlatan = "Data Source=" + frmMain.SqlLink + ";Initial Catalog=Platan;Persist Security Info=True;User ID=" + frmMain.UserSqlName + ";Password=" + frmMain.UserSqlPassword;
        

        private clTepCalendar _tep_calendar = new clTepCalendar();
        

        private DataTable _dt_department_plan;
        public DataTable dtDepartmentPlan
        {
            get { return _dt_department_plan; }
        }

        private DataTable _dt_department_users;
        public DataTable dtDepartmentUsers
        {
            get { return _dt_department_users; }
        }

        //private DataTable _dt_departments;
        //public DataTable dtDepartments
        //{
        //    get { return _dt_departments; }
        //}

        private DataTable _dt_selected_plan;
        public DataTable dtSelectedPlan
        {
            get { return _dt_selected_plan; }
        }

        public DataTable dtIndividualPlan
        {
            get { return _current_user.GetIndividualPlan(); }
        }

        private DataTable _dt_agreement;
        public DataTable dtAgreement
        {
            get { return _dt_agreement; }
            set { _dt_agreement = value; }
        }

        //private DataTable _dt_production_plan_dept;
        //public DataTable dtProductionPlanDept
        //{
        //    get { return _dt_production_plan_dept; }
        //}

        //private DataTable _dt_production_plan;
        //public DataTable dtProductionPlan
        //{
        //    get { return _dt_production_plan; }
        //}

        private List<clUser> _listDepartmentUsers = null;
        public List<clUser> ListDepartmentUsers
        {
            get { return _listDepartmentUsers; }
        }

        private List<clSet> _listPlanSets = null;
        private List<clSet> _listPlanSets_source = null;

        private List<clTableRecord> _listTableRecords = null;
        private List<clTableRecord> _listTableRecords_source = null;

        private clCurrentUser _current_user = null;

        public DateTime _PlanDate;

        private List<clDepartment> _departments = null;
        public List<clDepartment> ListDepartments
        {
            get { return _departments; }
        }

        private List<string> _all_departments;
        public List<string> AllDepartments
        {
            get { return _all_departments; }
            set { _all_departments = value; }
        }

        private List<clRequest> _list_requests;
        public List<clRequest> ListRequests
        {
            get { return _list_requests; }
            set { _list_requests = value; }
        }

        public clSprut(DateTime dt)
        {
            //MessageBox.Show(_connStringPlatan);
            _PlanDate = dt;
            _departments = new List<clDepartment>();

            Load_AllDepartments();

            Create_dtDepartmentPlan();

            Create_dtDepartmentUsers();

            Create_dtSelectedPlan();

            Create_dtAgreement();

            Create_CurrentUser();

            //Create_dtProductionPlanDept(ref _dt_production_plan_dept); //EL_2

            //Create_dtProductionPlanDept(ref _dt_production_plan); //EL_2
           
            //Create_dtDepartments();

            Initialize_Sprut();
        }

        private void Load_AllDepartments()
        {
            /*try
            {
                using (SqlConnection conn = new SqlConnection(_connStringSprut))
                {
                    conn.Open();

                    using (SqlCommand comm = new SqlCommand("[sprut_test_p].[dbo].[GetAllDepartments]", conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = comm.ExecuteReader())
                        {
                            if(dr.HasRows)
                            {
                                if (_all_departments == null)
                                    _all_departments = new List<string>();
                                else
                                    _all_departments.Clear();

                                while(dr.Read())
                                {
                                    string dept_small_name = dr["dept_small_name"].ToString();
                                    _all_departments.Add(dept_small_name);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }*/
        }

        private void Create_dtAgreement()
        {
            if (_dt_agreement == null)
                _dt_agreement = new DataTable("dtAgreement");
            else
            {
                _dt_agreement.Rows.Clear();
                _dt_agreement.Columns.Clear();
            }

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(clRequest));
            if (properties == null)
            {
                return;
            }

            foreach (PropertyDescriptor prop in properties)
            {
                _dt_agreement.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
        }

        private void Initialize_Sprut()
        {
            if (_dt_department_plan == null)
                _dt_department_plan = new DataTable();
            else
                _dt_department_plan.Rows.Clear();

            if (_dt_department_users == null)
                _dt_department_users = new DataTable();
            else
                _dt_department_users.Rows.Clear();

            if (_dt_selected_plan == null)
                _dt_selected_plan = new DataTable();
            else
                _dt_selected_plan.Rows.Clear();


            if (_current_user == null)
            {
                Application.Exit();
                return;
            }
            switch (_current_user.Permission)
            {   
                case "executor":
                    {
                        Initialize_Executor();
                        break;
                    }
                case "planner":
                    {
                        Initialize_Planner();
                        break;
                    }
                case "viewer":
                    {
                        Initialize_Planner();
                        break;
                    }
                case "head":
                    {
                        Initialize_Head();
                        break;
                    }
                case "gip":
                    {
                        Initialize_Gip();
                        break;
                    }
                case "root":
                    {
                        Initialize_Root();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Неудалось определить права пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        break;
                    }
            }
        }

        private void Initialize_Executor()
        {
            Load_DepartmentUsers();
        }

        private void Initialize_Gip()
        {
            Load_DepartmentUsers();
            FillUsersTable();   
            Load_TableRecords();
            Load_PlanSets();
            CalculateAllUsersResource();
            Create_UserLinkSet();
            //Create_SetMonthPlanAttribute();
            Fill_SetsDataTable();
            Agreement_LoadRequests();
        }

        private void Initialize_Head()
        {
            Load_DepartmentUsers();
            FillUsersTable();
            Load_TableRecords();
            Load_PlanSets();      
            CalculateAllUsersResource();
            Create_UserLinkSet();
            //Create_SetMonthPlanAttribute();
            Fill_SetsDataTable();
        }

        private void Initialize_Root()
        {
            Load_DepartmentUsers();
            FillUsersTable();
            Load_TableRecords();
            Load_PlanSets();        
            CalculateAllUsersResource();
            Create_UserLinkSet();
            //Create_SetMonthPlanAttribute();
            Fill_SetsDataTable();
            Agreement_LoadRequests();
        }

        private void Initialize_Planner()
        {
            Load_DepartmentUsers(); //+
            FillUsersTable();  //+
            Load_TableRecords(); //+
            Load_PlanSets();     //+  
            CalculateAllUsersResource();
            Create_UserLinkSet();
            //Create_SetMonthPlanAttribute();
            Fill_SetsDataTable();
        }

        //private void Create_SetMonthPlanAttribute()
        //{
        //    if ((_listPlanSets == null))
        //        return;
  
        //    for(int i = 0 ; i < _listPlanSets.Count; i++)
        //    {
        //        _listPlanSets[i].IsMonthPlan = "-";
        //        var set = _listPlanSets[i];
        //        DepartmentPlan_IsSetInMonthPlan(_listPlanSets[i].StageGuid, _listPlanSets[i].SetGuid, _PlanDate);
        //    }
        //}

        private void Create_UserLinkSet()
        {
            if ((_listTableRecords == null) || (_listTableRecords.Count <= 0))
            {
                return;
            }

            if ((_listPlanSets == null) || (_listPlanSets.Count <= 0))
            {
                return;
            }

            if ((_listDepartmentUsers == null) || (_listDepartmentUsers.Count <= 0))
            {
                return;
            }

            foreach (var tr in _listTableRecords)
            {
                var set = _listPlanSets.Find(x => (x.SetGuid.Equals(tr.SetGuid)));
                if (set == null)
                {
                    continue;
                }
                if (set._list_executors == null)
                    set._list_executors = new List<clUser>();
                var user = _listDepartmentUsers.Find(x => (x.UserId.Equals(tr.ExecutorId)));
                if (user == null)
                {
                    continue;
                }
                int index = -1;

                index = set._list_executors.FindIndex(x => (x.UserId.ToLower().Equals(tr.ExecutorId)));
                if (index < 0)
                {
                    set._list_executors.Add(user);
                }
            }

        }

        private void CalculateAllUsersResource()
        {
            if ((_listDepartmentUsers == null) || (_listDepartmentUsers.Count <= 0))
                return;
            else
            {
                DateTime dt1 = new DateTime(_PlanDate.Year, _PlanDate.Month, 1, 0, 0, 0);
                DateTime dt2 = new DateTime(_PlanDate.Year, _PlanDate.Month, DateTime.DaysInMonth(_PlanDate.Year, _PlanDate.Month), 0, 0, 0);
                double month_resource = GetWorkTime(dt1, dt2);
                for (int i = 0; i < _listDepartmentUsers.Count; i++)
                {
                    _listDepartmentUsers[i].Resource = month_resource;
                    string executor_id = _listDepartmentUsers[i].UserId;
                    CalculateUserResource(executor_id, month_resource, dt1, dt2);
                    DataTableUsers_UpdateRow(executor_id);
                }
            }

        }

        private void DataTableUsers_UpdateRow(string executor_id)
        {
            if ((_dt_department_users == null) || (_listDepartmentUsers == null))
                return;

            for (int i = 0; i < _dt_department_users.Rows.Count; i++)
            {
                if (_dt_department_users.Rows[i]["UserId"].ToString().Equals(executor_id))
                {
                    clUser user = _listDepartmentUsers.Find(x => (x.UserId.Equals(executor_id)));
                    if (user != null)
                    {
                        _dt_department_users.Rows[i]["Resource"] = user.Resource;
                    }
                }
            }
        }     

        private void Load_TableRecords()
        {
            if (_listTableRecords == null)
                _listTableRecords = new List<clTableRecord>();
            else
                _listTableRecords.Clear();

            if (_listTableRecords_source == null)
                _listTableRecords_source = new List<clTableRecord>();
            else
                _listTableRecords_source.Clear();

            SqlDataReader dr = null;
            SqlConnection connection = new SqlConnection(_connStringSprut);
            try
            {
                SqlCommand command = new SqlCommand("[" + frmMain.SqlLink+"].[Platan].[dbo].[PL_GetTableRecords]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@year", SqlDbType.Int).Value = _PlanDate.Year;
                command.Parameters.Add("@month", SqlDbType.Int).Value = _PlanDate.Month;
                //command.Parameters.Add("@dept_id", SqlDbType.BigInt).Value = Convert.ToInt64(_current_user.DepartmentId); //IzmPl
                connection.Open();
                dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    DateTime dt;
                    double val = 0;

                    while (dr.Read())
                    {
                        clTableRecord tr = new clTableRecord();

                        tr.SetGuid = dr["set_guid"].ToString();
                        tr.StageGuid = dr["stage_guid"].ToString();
                        tr.ExecutorId = dr["executor_id"].ToString();
                        if (DateTime.TryParse(dr["work_date"].ToString(), out dt))
                            tr.WorkDate = dt;
                        else
                            tr.WorkDate = DateTime.Now;

                        if (Double.TryParse(dr["work_plan"].ToString(), out val))
                            tr.WorkPlan = val;
                        else
                            tr.WorkPlan = 0;

                        if (Double.TryParse(dr["work_fact"].ToString(), out val))
                            tr.WorkFact = val;
                        else
                            tr.WorkFact = 0;

                        if (Double.TryParse(dr["manually_input"].ToString(), out val))
                            tr.ManuallyInput = val;
                        else
                            tr.ManuallyInput = 0;

                        tr.DeptId = dr["dept_id"].ToString();
                        if (DateTime.TryParse(dr["modify_date"].ToString(), out dt))
                            tr.ModifyDate = dt;
                        else
                            tr.ModifyDate = DateTime.Now;
                        tr.ModifyUserId = dr["modify_user_id"].ToString();
                        if (DateTime.TryParse(dr["create_date"].ToString(), out dt))
                            tr.CreateDate = dt;
                        else
                            tr.CreateDate = DateTime.Now;
                        tr.CreateUserId = dr["create_user_id"].ToString();

                        _listTableRecords.Add(tr);
                    }


                    //Копируем загруженные табельные записи
                    if (_listTableRecords != null)
                    {
                        foreach (var item in _listTableRecords)
                        {
                            clTableRecord tr = new clTableRecord();

                            tr.CreateDate = item.CreateDate;
                            tr.CreateUserId = item.CreateUserId;
                            tr.DeptId = item.DeptId;
                            tr.ExecutorId = item.ExecutorId;
                            tr.ManuallyInput = item.ManuallyInput;
                            tr.ModifyDate = item.ModifyDate;
                            tr.ModifyUserId = item.ModifyUserId;
                            tr.SetGuid = item.SetGuid;
                            tr.StageGuid = item.StageGuid;
                            tr.WorkDate = item.WorkDate;
                            tr.WorkFact = item.WorkFact;
                            tr.WorkPlan = item.WorkPlan;

                            _listTableRecords_source.Add(tr);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при получении табельных записей.\n" + ex.Message + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (connection != null)
                    connection.Close();
            }
        }

        private void Fill_SetsDataTable()
        {
            if (_listPlanSets == null)
            {
                if (_dt_department_plan != null)
                {
                    _dt_department_plan.Rows.Clear();
                }
                return;
            }
                
            _dt_department_plan.Rows.Clear();

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(clSet));
            if (properties == null)
                return;

            foreach (var item in _listPlanSets)
            {

                DataRow row = _dt_department_plan.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                _dt_department_plan.Rows.Add(row);
            }

        }

        private void Load_PlanSets()
        {
            //Очищаем список комплектов
            if (_listPlanSets == null)
                _listPlanSets = new List<clSet>();
            else
                _listPlanSets.Clear();

            
            if (_listPlanSets_source == null)
                _listPlanSets_source = new List<clSet>();
            else
                _listPlanSets_source.Clear();


            int year = _PlanDate.Year;
            int month = _PlanDate.Month;
            string dept = _current_user.DepartmentId;

            SqlDataReader dr = null;
            SqlConnection connection = new SqlConnection(_connStringSprut);
            try
            {
                SqlCommand command = new SqlCommand("[dbo].[PL_GetDepartmentPlanSetsNewt]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                command.Parameters.Add("@dept_id", SqlDbType.BigInt).Value = Convert.ToInt32(dept);
                connection.Open();
                dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    DateTime dt;

                    DateTime month_start = new DateTime(_PlanDate.Year, _PlanDate.Month, 1, 0, 0, 0);
                    DateTime month_end = new DateTime(_PlanDate.Year, _PlanDate.Month, DateTime.DaysInMonth(_PlanDate.Year, _PlanDate.Month),0,0,0);

                    while (dr.Read())
                    {
                        clSet set = new clSet();
                        set.Contract = dr["contract"].ToString();
                        set.StageName = dr["stage_name"].ToString();
                        //set.Kks = dr["kks"].ToString();
                        set.Building = dr["building"].ToString();
                        set.SetCode = dr["set_code"].ToString();
                        set.SetName = dr["set_name"].ToString();
                        //set.Mark = dr["set_mark"].ToString();
                        set.SetGuid = dr["set_guid"].ToString();
                        set.StageGuid = dr["stage_guid"].ToString();
                        //set.StatusFromTdms = dr["status_from_tdms"].ToString();
                        set.AgreedStatus = dr["agreed_status"].ToString();
                        //set.CommentGip = dr["comment_gip"].ToString(); //EL_2
                        //set.StatusPP = ApprovedStatus(dr["status_pp"].ToString()); //EL_2
                        set.SetDept = dr["set_dept"].ToString(); //EL_2
                        //set.SetCpkDate = dr["set_cpkdate"].ToString(); //EL_2
                        //set.SetRevision = dr["set_revision"].ToString(); //EL_2
                        //if (set.Contract == "996" || set.Contract == "997" || set.Contract == "998" || set.Contract == "999" || set.Building == "Работы по проекту")
                        //{
                        //    set.PriceDog = "";
                        //    set.PriceMng = "";
                        //    set.PriceDop = "";
                        //    set.WorkHrFakt = "";
                        //    set.WorkHrMng = "";
                        //    set.WorkHrPlan = "";
                        //    set.WorkHrDelta = "";
                        //    set.DopRev = "";
                        //}
                        //else
                        //{
                        //    set.PriceDog = (dr["price_dog"].ToString() == "0" ? "" : String.Format("{0:### ### ###.00}", dr["price_dog"]));//EL
                        //    set.PriceMng = (dr["price_mng"].ToString() == "0" ? "" : String.Format("{0:### ### ###.00}", dr["price_mng"]));//EL
                        //    set.PriceDop = (dr["price_dop"].ToString() == "0" ? "" : String.Format("{0:### ### ###.00}", dr["price_dop"]));//EL
                        //    set.DopRev = (dr["price_dop"].ToString() == "0" ? "" : dr["dop_rev"].ToString());//EL
                        //    set.WorkHrFakt = (dr["WorkHr_fakt"].ToString() == "0" ? "" : String.Format("{0:### ##0.00}", dr["WorkHr_fakt"]));//EL
                        //    set.WorkHrMng = (dr["WorkHr_mng"].ToString() == "0" ? "" : String.Format("{0:### ##0.00}", dr["WorkHr_mng"]));//EL
                        //    set.WorkHrPlan = (dr["WorkHr_plan"].ToString() == "0" ? "" : String.Format("{0:### ##0.00}", dr["WorkHr_mng"]));
                        //    double s = 0;
                        //    Double.TryParse(dr["WorkHr_mng"].ToString(), out s);
                        //    s = s == 0? 0:(s - Convert.ToDouble(dr["WorkHr_fakt"]));
                        //    set.WorkHrDelta = (s.ToString() == "0" ? "" : String.Format("{0:### ##0.00}", s));
                        //}
                        
                        //set.WorkHrPlanTek = "";

                        
                        //Даты из ТДМС
                        if (DateTime.TryParse(dr["gip_start"].ToString(), out dt))
                        {
                            set.GipStart = dt;
                            set.SetStart = dt;
                        }
                        else
                        {
                            set.GipStart = DateTime.Now;
                            set.SetStart = DateTime.Now;
                        }

                        if (DateTime.TryParse(dr["gip_end"].ToString(), out dt))
                        {
                            set.GipEnd = dt;
                            set.SetEnd = dt;
                        }
                        else
                        {
                            set.GipEnd = DateTime.Now;
                            set.SetEnd = DateTime.Now;
                        }

                        //Согласованные даты
                        if (DateTime.TryParse(dr["agreed_start"].ToString(), out dt))
                        {
                            set.AgreedStart = dt;
                            set.SetStart = dt;
                        }

                        if (DateTime.TryParse(dr["agreed_end"].ToString(), out dt))
                        {
                            set.AgreedEnd = dt;
                            set.SetEnd = dt;
                        }

                        //if ((dr["Contract"].ToString() == "997") || (dr["building"].ToString() == "Работы по проекту"))    //EL_2 (даты "работы по проекту")
                        //{                
                        //    set.GipStart = month_start;
                        //    set.SetStart = month_start;
                        //    set.GipEnd = month_end;
                        //    set.SetEnd = month_end;
                        //}
                        //set.IsMonthPlan = "-";

                        //set.PercentComplete = dr["percent_complete"].ToString();

                        if (set._responsible_user == null)
                            set._responsible_user = new clResponsibleUser();
                        set._responsible_user.FullName = dr["responsible_fio"].ToString();
                        //set._responsible_user.Id = dr["responsible_id"].ToString();

                        _listPlanSets.Add(set);
 
                    }
                }

                //Копируем все загруженные комплекты, копия нужна для поиска изменений при сохранении
                if ((_listPlanSets != null) && (_listPlanSets.Count > 0))
                {
                    foreach(var item in _listPlanSets)
                    {
                        clSet set = new clSet();

                        set.SetGuid = item.SetGuid;
                        set.StageGuid = item.StageGuid;
                        set.Contract = item.Contract;

                        //set.PercentComplete = item.PercentComplete;
                        set._responsible_user.FullName = item._responsible_user.FullName;
                        //set._responsible_user.Id = item._responsible_user.Id;

                        _listPlanSets_source.Add(set);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка при загрузке комплектов.\n" + e.Message + "\n" + e.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (connection != null)
                    connection.Close();
            }
        }

        public void FillUsersTable()
        {
            if (_dt_department_users != null)
            {
                _dt_department_users.Rows.Clear();
            }
            if (_listDepartmentUsers == null)
            {
                
                return;
            }

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(clUser));
            if (properties == null)
                return;

            foreach (var item in _listDepartmentUsers)
            {
                DataRow row = _dt_department_users.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                _dt_department_users.Rows.Add(row);
            }
        }

        public void Load_DepartmentUsers()
        {
            if (_listDepartmentUsers == null)
                _listDepartmentUsers = new List<clUser>();
            else
                _listDepartmentUsers.Clear();

            DateTime start = new DateTime(_PlanDate.Year, _PlanDate.Month, 1, 0, 0, 0);
            DateTime end = new DateTime(_PlanDate.Year, _PlanDate.Month, DateTime.DaysInMonth(_PlanDate.Year, _PlanDate.Month), 0, 0, 0);
            double work_time_month = GetWorkTime(start, end);


            SqlDataReader dr = null;
            SqlConnection connection = new SqlConnection(_connStringSprut);
            try
            {
                SqlCommand command = new SqlCommand("[dbo].[PL_GetDepartmentUsers]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@department_id", SqlDbType.BigInt).Value = _current_user.DepartmentId;
                connection.Open();
                dr = command.ExecuteReader();

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        clUser newUser = new clUser();
                        newUser.FullName = dr["UserFullName"].ToString();
                        newUser.UserId = dr["UserID"].ToString();
                        newUser.DepartmentFull = dr["DepartmentFullName"].ToString();
                        newUser.DepartmentSmall = dr["DepartmentSmallName"].ToString();
                        newUser.Speciality = dr["DepartmentFullName"].ToString(); ; //площадка
                        newUser.Phone = dr["UserPhone"].ToString();
                        newUser.Position = dr["UserPosition"].ToString();
                        newUser.DepartmentId = dr["UserDepartmentID"].ToString();
                        newUser.Resource = work_time_month;
                        _listDepartmentUsers.Add(newUser);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при получении данных о сотрудниках отдела.\n" + ex.Message + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (connection != null)
                    connection.Close();
            }
        }

        

        #region CurrentUser
        
        private void Create_CurrentUser()
        {
            //string current_user_windows_login = GetCurrentUserWindowsLogin();
            //_current_user = CreateUser(current_user_windows_login);   
            _current_user = CreateUser();   
        
            if(_current_user == null)
            {
                MessageBox.Show("Неудалось определить пользователя!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private clCurrentUser CreateUser()
        {
            clCurrentUser result = null;

            SqlDataReader dr = null;
            SqlConnection connection = new SqlConnection(_connStringSprut);
            
            try
            {
                SqlCommand command = new SqlCommand("[dbo].[Pl_GetCurrentUser]", connection);
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.Add("@user_login", SqlDbType.NVarChar).Value = current_user_windows_login;
                command.Parameters.Add("@user_descr", SqlDbType.NVarChar).Value = frmMain.FioUser;
                connection.Open();
                dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    string user_full_name = dr["user_full_name"].ToString();
                    string user_id = dr["user_id"].ToString();
                    string department_full = dr["department_full"].ToString();
                    string department_small = dr["department_small"].ToString();
                    string user_speciality = dr["user_speciality"].ToString();
                    string user_phone = dr["user_phone"].ToString();
                    string user_position = dr["user_position"].ToString();
                    string department_id = dr["department_id"].ToString();
                    
                    //string dept_executor_id = dr["dept_executor_id"].ToString();
                    //string dept_executor_small_name = dr["dept_executor_small_name"].ToString();
                    //string dept_plan_id = dr["dept_plan_id"].ToString();
                    //string dept_plan_small_name = null;  //dr["dept_plan_small_name"].ToString(); добавляется список отделов для ГИП и рук-во
                    //string permission = dr["permission"].ToString();                  
                    string permission = frmMain.FioPrms;
                    


                    result = new clCurrentUser();
                    result.ConnStringSprut = _connStringSprut;
                    result.ConnStringPlatan = _connStringPlatan;
                    result.FullName = user_full_name;
                    result.DepartmentFull = department_full;
                    result.DepartmentId = department_id;
                    result.DepartmentSmall = department_small;
                    result.Phone = user_phone;
                    result.Position = user_position;
                    result.Speciality = user_speciality;
                    result.UserId = user_id;
                    result.SelectedUserID = user_id;
                    result.SelectedUserFirstName = result.FirstName;
                    result.SelectedUserLastName = result.LastName;
                    result.SelectedUserMiddleName = result.MiddleName;

                    if(!String.IsNullOrEmpty(permission))
                    {
                        result.Permission = permission; 
                    }

                    result.LoadIndividualPlan(_PlanDate);
                }

                // добавляем отделы, которые должен видеть CurrentUser
                dr.Close();
                command = new SqlCommand("[dbo].[PL_GetDeptsForCurrentUser]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@user_id", SqlDbType.Int).Value = result.UserId;
                command.Parameters.Add("@user_prms", SqlDbType.NVarChar).Value = frmMain.FioPrms;
                dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            clDepartment dept = new clDepartment();
                            dept.FullName = dr["dept_name"].ToString();
                            dept.Id = dr["dept_id"].ToString();
                            _departments.Add(dept);
                        }
                    }
                    
                }
                else
                {
                    clDepartment dept = new clDepartment();
                    dept.FullName = result.DepartmentSmall;
                    dept.Id = result.DepartmentId;
                    _departments.Add(dept);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (connection != null)
                    connection.Close();
            }

            return result;
        }
        
        private string GetCurrentUserWindowsLogin()
        {
            string result = "";

            try
            {
                WindowsIdentity identity = null;
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                WindowsPrincipal principial = (WindowsPrincipal)Thread.CurrentPrincipal;
                identity = (WindowsIdentity)principial.Identity;
                result = identity.Name;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
            return result;
        }
        
        #endregion

        private void Create_dtDepartmentPlan()
        {
            if (_dt_department_plan == null)
                _dt_department_plan = new DataTable("dtDepartmentPlan");
            else
            {
                _dt_department_plan.Rows.Clear();
                _dt_department_plan.Columns.Clear();
            }

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(clSet));
            if (properties == null)
            {
                return;
            }

            foreach (PropertyDescriptor prop in properties)
            {
                _dt_department_plan.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
        }

        private void Create_dtDepartmentUsers()
        {
            //_dt_department_users = new DataTable("dtDepartmentUsers");

            if (_dt_department_users == null)
                _dt_department_users = new DataTable("dtDepartmentUsers");
            else
            {
                _dt_department_users.Rows.Clear();
                _dt_department_users.Columns.Clear();
            }

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(clUser));
            if (properties == null)
            {
                return;
            }

            foreach (PropertyDescriptor prop in properties)
            {
                _dt_department_users.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
        }

        private void Create_dtSelectedPlan()
        {
            _dt_selected_plan = new DataTable();
        }

        public void IndividualPlan_Update(DateTime dateTime)
        {
            if(_current_user != null)
                _current_user.LoadIndividualPlan(dateTime);
        }

        public double GetWorkTime(DateTime dt)
        {
            double result = 0;
            result = _tep_calendar.WorkHours(dt);
            return result;
        }

        public double GetWorkTime(DateTime dt1, DateTime dt2)
        {
            double result = 0;
            result = _tep_calendar.CountWorkHours(dt1,dt2);
            return result;
        }

        public clTableRecord IndividualPlan_GetTableRecord(string set_guid, DateTime work_date)
        {
            clTableRecord result = null;

            result = _current_user.GetTableRecord(set_guid, work_date);
            
            return result;
        }

        public double IndividualPlan_GetDayFact(DateTime work_date)
        {
            double result = 0;

            result = _current_user.GetDayFact(work_date);

            return result;
        }

        public void IndividualPlan_CreateOrUpdateTableReord(string set_guid, string stage_guid, DateTime column_date, double val_double)
        {
            _current_user.CreateOrUpdateTableRecord(set_guid, stage_guid, column_date, val_double);
        }

        public void IndividualPlan_DeleteTableRecord(string set_guid, DateTime column_date)
        {
            _current_user.DeleteTableRecord(set_guid, column_date);
        }

        public string CurrentUser_GetDepartmentSmall()
        {
            string result = "";
            if(_current_user != null)
            {
                result = _current_user.DepartmentSmall;
            }

            return result;
        }

        public string CurrentUser_GetFio()
        {
            string result = "";
            if (_current_user != null)
            {
                result = _current_user.Fio;
            }

            return result;
        }

        public void FindChanges(ref List<clTableRecord> _original, ref List<clTableRecord> _modified, ref List<clTableRecord> _new, ref List<clTableRecord> _updated, ref List<clTableRecord> _deleted)
        {
            //Ищем новые табельные записи
            foreach(var item in _modified)
            {
                clTableRecord tr = _original.Find(x=> (x.SetGuid.Equals(item.SetGuid))
                                                    &&(x.ExecutorId.Equals(item.ExecutorId))
                                                    &&(x.WorkDate.ToShortDateString().Equals(item.WorkDate.ToShortDateString()))
                                                );
                if (tr == null)
                {
                    //Новая табельная запись, ее нет в исходном списке
                    _new.Add(item);
                }
                else
                {
                    //Проверить была ли изменена табельная запись
                    if ((tr.WorkPlan != item.WorkPlan) || (tr.ManuallyInput != item.ManuallyInput))
                    {
                        _updated.Add(item);
                    }
                }
            }

            //Ищем удаленные табельные записи
            foreach (var item in _original)
            {
                clTableRecord tr = _modified.Find(x=> (x.SetGuid.Equals(item.SetGuid))
                                                    &&(x.ExecutorId.Equals(item.ExecutorId))
                                                    &&(x.WorkDate.ToShortDateString().Equals(item.WorkDate.ToShortDateString()))
                                                );
                if (tr == null)
                {
                    //Табельная запись есть в исходном но отсутствует в модифицированном - была удалена
                    _deleted.Add(item);
                }
            }
        }

        public bool IsTableRecordsModified(ref List<clTableRecord> _original, ref List<clTableRecord> _modified)
        {
            bool result = false;

            //Ищем новые табельные записи
            foreach (var item in _modified)
            {
                clTableRecord tr = _original.Find(x => (x.SetGuid.Equals(item.SetGuid))
                                                    && (x.ExecutorId.Equals(item.ExecutorId))
                                                    && (x.WorkDate.ToShortDateString().Equals(item.WorkDate.ToShortDateString()))
                                                );
                if (tr == null)
                {
                    //Новая табельная запись, ее нет в исходном списке
                    result = true;
                    return result;
                }
                else
                {
                    //Проверить была ли изменена табельная запись
                    if ((tr.WorkPlan != item.WorkPlan) || (tr.ManuallyInput != item.ManuallyInput))
                    {
                        result = true;
                        return result;
                    }
                }
            }

            //Ищем удаленные табельные записи
            foreach (var item in _original)
            {
                clTableRecord tr = _modified.Find(x => (x.SetGuid.Equals(item.SetGuid))
                                                    && (x.ExecutorId.Equals(item.ExecutorId))
                                                    && (x.WorkDate.ToShortDateString().Equals(item.WorkDate.ToShortDateString()))
                                                );
                if (tr == null)
                {
                    //Табельная запись есть в исходном но отсутствует в модифицированном - была удалена
                    result = true;
                    return result;
                }
            }

            return result;
        }

        public void DepartmentPlan_AddExecutor(string set_guid, string executor_id)
        {
            clSet set = _listPlanSets.Find(x => (x.SetGuid.Equals(set_guid)));
            if (set == null)
            {
                MessageBox.Show("Неудалось определить комплект", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clUser user = _listDepartmentUsers.Find(x => (x.UserId.ToLower().Equals(executor_id.ToLower())));
            if (user == null)
            {
                MessageBox.Show("Неудалось определить сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (set._list_executors == null)
                set._list_executors = new List<clUser>();

            if (!set._list_executors.Contains(user)) 
            {
                set._list_executors.Add(user);
                DepartmentPlan_UpdateRow(ref set);
            }
            else
                MessageBox.Show("Сотрудник '" + user.Fio + "' уже является исполнителем комплекта '" + set.SetCode + "'", "Повторное добавление исполнителя", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void DepartmentPlan_AddSet(clSet newSet)
        {
            try
            {
                if ((_listPlanSets == null) || (_dt_department_plan == null))
                    return;
                DataRow row = _dt_department_plan.NewRow();

                row["Contract"] = newSet.Contract;
                row["StageName"] = newSet.StageName;
                //row["Kks"] = newSet.Kks;
                row["Building"] = newSet.Building;
                row["SetCode"] = newSet.SetCode;
                row["SetName"] = newSet.SetName;
                row["GipStart"] = newSet.GipStart.ToShortDateString();
                row["GipEnd"] = newSet.GipEnd.ToShortDateString();
                row["SetGuid"] = newSet.SetGuid;
                row["StageGuid"] = newSet.Contract;
                //row["StatusFromTdms"] = newSet.StatusFromTdms;
                row["Executors"] = newSet.Executors;
                //row["SetStart"] = newSet.SetStart;
                //row["SetEnd"] = newSet.SetEnd;

                _dt_department_plan.Rows.Add(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void DepartmentPlan_UpdateRow(ref clSet set)
        {
            if ((_dt_department_plan == null) || (_dt_department_plan.Columns.Count <= 0) || (_dt_department_plan.Rows.Count <= 0))
            {
                return;
            }
            else
            {
                for (int i = 0; i < _dt_department_plan.Rows.Count; i++)
                {
                    if (_dt_department_plan.Rows[i]["SetGuid"].Equals(set.SetGuid)) 
                    {
                        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(clSet));
                        if (properties == null)
                            return;
                        foreach (PropertyDescriptor prop in properties)
                        {
                            _dt_department_plan.Rows[i][prop.Name] = prop.GetValue(set) ?? DBNull.Value;
                        }
                        break;
                    }
                    
                }

            }
        }

        public void DepartmentPlan_CreatePlanTable(int p1, string p2)
        {
            if (p1 == 1)
            {
                string id = p2;
                PlanTable_CreateById(id);
            }

            if (p1 == 2)
            {
                string set_guid = "";
                string stage_guid = "";
                //string[] words = p2.Split(new string[] { "[*]" }, StringSplitOptions.RemoveEmptyEntries);
                set_guid = p2;
                //stage_guid = words[1];
                PlanTable_CreateBySet(set_guid);
            }
        }

        private void PlanTable_CreateBySet(string set_guid)
        {
            DateTime start = _PlanDate;
            DateTime end = _PlanDate;
            if (_listTableRecords != null)
            {
                var tempListTableRecords = _listTableRecords.FindAll(x => (x.SetGuid.Equals(set_guid)));
                if ((tempListTableRecords != null) && (tempListTableRecords.Count > 0))
                {
                    start = tempListTableRecords.Min(x => x.WorkDate);
                    end = tempListTableRecords.Max(x => x.WorkDate);
                    if (start > _PlanDate)
                        start = _PlanDate;
                }

                PlanTable_CreateColumns(start, end, "dtPlanSet");

                clSet set = _listPlanSets.Find(x => (x.SetGuid.Equals(set_guid)));
                if ((set == null) || (set._list_executors == null) || (set._list_executors.Count <= 0))
                    return;
                else
                {
                    PlanTable_CreateRows(ref set, ref tempListTableRecords);
                }

            }
        }

        private void PlanTable_CreateRows(ref clSet set, ref List<clTableRecord> ListTableRecords)
        {
            foreach (clUser user in set._list_executors)
            {
                DataRow row = _dt_selected_plan.NewRow();
                List<clTableRecord> tempListTableRecords = ListTableRecords.FindAll(x => (x.ExecutorId.Equals(user.UserId.ToLower())));
                PlanTable_CreateRow(ref row, set, user, ref tempListTableRecords);
                _dt_selected_plan.Rows.Add(row);
            }
        }

        private void PlanTable_CreateRow(ref DataRow row, clSet set, clUser user, ref List<clTableRecord> tempListTableRecords)
        {
            row["Building"] = set.Building;
            row["ExecutorFullName"] = user.FullName;
            row["ExecutorId"] = user.UserId;
            row["DeptId"] = user.DepartmentId;  //IzmPl
            row["GipEnd"] = set.GipEnd;
            row["GipStart"] = set.GipStart;
            //row["Kks"] = set.Kks;
            row["Contract"] = set.Contract;
            row["SetCode"] = set.SetCode;
            row["SetGuid"] = set.SetGuid;
            row["SetName"] = set.SetName;
            row["StageGuid"] = set.StageGuid;
            row["StageName"] = set.StageName;
            //row["StatusFromTdms"] = set.StatusFromTdms;
            if ((tempListTableRecords != null) && (tempListTableRecords.Count > 0))
            {
                foreach (var item in tempListTableRecords)
                {
                    row[item.WorkDate.ToShortDateString()] = item.WorkPlan;
                }

                List<clTableRecord> tListTableRecords = tempListTableRecords.FindAll(x => x.WorkPlan > 0);
                if ((tListTableRecords != null) && (tListTableRecords.Count > 0))
                {
                    DateTime start = tListTableRecords.Min(x => x.WorkDate);
                    DateTime end = tListTableRecords.Max(x => x.WorkDate);
                    row["PlanStart"] = start.ToShortDateString();
                    row["PlanEnd"] = end.ToShortDateString();
                }
            }
        }

        private void PlanTable_CreateColumns(DateTime start, DateTime end, string TableName)
        {
            if (_dt_selected_plan == null)
                _dt_selected_plan = new DataTable();
            else
            {
                _dt_selected_plan.Rows.Clear();
                _dt_selected_plan.Columns.Clear();
            }
            _dt_selected_plan.TableName = TableName;

            _dt_selected_plan.Columns.Add("Contract", typeof(string));
            _dt_selected_plan.Columns.Add("StageName", typeof(string));
            _dt_selected_plan.Columns.Add("Building", typeof(string));         
            //_dt_selected_plan.Columns.Add("Kks", typeof(string));         
            _dt_selected_plan.Columns.Add("SetCode", typeof(string));
            _dt_selected_plan.Columns.Add("SetName", typeof(string));
            _dt_selected_plan.Columns.Add("GipStart", typeof(string));
            _dt_selected_plan.Columns.Add("GipEnd", typeof(string));
            _dt_selected_plan.Columns.Add("SetGuid", typeof(string));
            _dt_selected_plan.Columns.Add("StageGuid", typeof(string));
            _dt_selected_plan.Columns.Add("StatusFromTdms", typeof(string));
            _dt_selected_plan.Columns.Add("ExecutorFullName", typeof(string));
            _dt_selected_plan.Columns.Add("ExecutorId", typeof(string));
            _dt_selected_plan.Columns.Add("DeptId", typeof(string));  //IzmPl
            _dt_selected_plan.Columns.Add("PlanStart", typeof(string));
            _dt_selected_plan.Columns.Add("PlanEnd", typeof(string));


            start = new DateTime(start.Year, start.Month, 1, 0, 0, 0);
            end = new DateTime(end.Year, end.Month, DateTime.DaysInMonth(end.Year, end.Month), 0, 0, 0);
            for (DateTime dt = start; dt <= end; dt = dt.AddDays(1))
            {
                _dt_selected_plan.Columns.Add(dt.ToShortDateString(), typeof(string));
            }
        }

        private void PlanTable_CreateById(string id)
        {
            DateTime start = _PlanDate;
            DateTime end = _PlanDate;

            clUser user = _listDepartmentUsers.Find(x => x.UserId.ToLower().Equals(id));
            if (user == null)
            {
                PlanTable_CreateColumns(start, end, "dtPlanUser");
                return;
            }

            var tempListSet = _listPlanSets.FindAll(x => (x._list_executors.Contains(user)));
            if ((tempListSet == null) || (tempListSet.Count == 0))
            {
                PlanTable_CreateColumns(start, end, "dtPlanUser");
                return;
            }

            var tempListTableRecords = _listTableRecords.FindAll(x => x.ExecutorId.Equals(id));
            if ((tempListTableRecords != null) && (tempListTableRecords.Count > 0))
            {
                start = tempListTableRecords.Min(x => x.WorkDate);
                end = tempListTableRecords.Max(x => x.WorkDate);
            }
            PlanTable_CreateColumns(start, end, "dtPlanUser");


            PlanTable_CreateRows(ref tempListSet, ref tempListTableRecords, ref user);
        }

        private void PlanTable_CreateRows(ref List<clSet> ListSets, ref List<clTableRecord> ListTableRecords, ref clUser user)
        {
            ListSets = ListSets.OrderBy(x => x.Contract).ThenBy(x => x.StageName).ThenBy(x => x.SetCode).ToList();
            foreach (var set in ListSets)
            {
                DataRow row = _dt_selected_plan.NewRow();
                List<clTableRecord> tempListTableRecords = ListTableRecords.FindAll(x => (x.SetGuid.Equals(set.SetGuid)));

                PlanTable_CreateRow(ref row, set, user, ref tempListTableRecords);
                _dt_selected_plan.Rows.Add(row);
            }
        }

        public clTableRecord GetTableRecord(string set_guid, string executor_id, string dt)
        {
            clTableRecord result = null;

            if ((_listTableRecords == null) || (_listTableRecords.Count <= 0))
            {
                result = null;
            }
            else
            {
                result = _listTableRecords.Find(x =>
                                                    (x.SetGuid.Equals(set_guid))
                                                    && (x.ExecutorId.ToLower().Equals(executor_id.ToLower()))
                                                    && (x.WorkDate.ToShortDateString().Equals(dt)));
            }

            return result;
        }

        public bool IsDayBusy(string executor_id, DateTime dt)
        {
            bool result = true;

            if (_listTableRecords == null)
                return result;

            double work_time = GetWorkTime(dt);
            List<clTableRecord> tempListTableRecords = _listTableRecords.FindAll(x => (x.ExecutorId.Equals(executor_id))
                                                                                   && (x.WorkDate.ToShortDateString().Equals(dt.ToShortDateString())));
            if (tempListTableRecords == null)
                return result;

            double real_busy = 0;
            real_busy = tempListTableRecords.Sum(x => x.WorkPlan);
            //real_busy = Math.Round(real_busy,2);

            if (Math.Round(real_busy, 2) < work_time)
                result = false;
            else
                result = true;

            return result;
        }

        public void DepartmentPlan_CreateTableRecord(string set_guid, string stage_guid, string executor_id, string dept_id,DateTime work_date, double work_time, int input_type)
        {
            //Можно ли проводить изменения табельной записи на указанную дату
            if (!CanChangeTableRecord(work_date))
                return;

            //Почему то дабл теряет точность, 8.24 -> 8.2399999999967801
            //work_time = Math.Round(work_time, 6);

            //Проверить существует ли такая табельная запись
            bool tr_exist = IsTableRecordExists(set_guid, stage_guid,executor_id, work_date);

            if (input_type > 0)
            {
                if (tr_exist)
                {
                    //Изменить существующую табельную запись с ручным временем
                    //ChangeManuallyTableRecord(set_guid, stage_guid, executor_id, work_date, work_time, input_type);
                    ManuallyTableRecord_Change(set_guid, stage_guid,executor_id, work_date, work_time, input_type);
                }
                else
                {
                    //Создать новую табельную запись с ручным временем
                    //CreateNewManuallyTableRecord(set_id, set_guid, stage_id, stage_guid, executor_login, executor_id, work_date, work_time, input_type);
                    ManuallyTableRecord_Create(set_guid, stage_guid,executor_id, dept_id,work_date, work_time, input_type);
                }
            }
            else
            {
                if (tr_exist)
                {
                    //ChangeAutoTableRecord(set_guid, stage_guid, executor_login, work_date, input_type);
                    AutoTableRecord_Change(set_guid, stage_guid, executor_id, work_date, input_type);
                }
                else
                {
                    //CreateNewAutoTableRecord(set_id, set_guid, stage_id, stage_guid, executor_login, executor_id, work_date, input_type);
                    AutoTableRecord_Create(set_guid,stage_guid, executor_id, dept_id,work_date, input_type);
                }
            }

            SeparateUserAutoTime(executor_id, work_date);
            CalculateUserResource(executor_id);
            DataTableUsers_UpdateRow(executor_id);       
        }

        private void CalculateUserResource(string executor_id)
        {
            DateTime start = new DateTime(_PlanDate.Year, _PlanDate.Month, 1, 0, 0, 0);
            DateTime end = new DateTime(_PlanDate.Year, _PlanDate.Month, DateTime.DaysInMonth(_PlanDate.Year, _PlanDate.Month), 0, 0, 0);
            double month_resource = GetWorkTime(start, end);
            double resource = month_resource;

            CalculateUserResource(executor_id, month_resource, start, end);
        }

        private void CalculateUserResource(string id, double month_resource, DateTime start, DateTime end)
        {
            int index = -1;
            index = _listDepartmentUsers.FindIndex(x => x.UserId.Equals(id));
            if (index < 0)
                return;

            if ((_listTableRecords == null) || (_listTableRecords.Count <= 0))
            {
                _listDepartmentUsers[index].Resource = month_resource;
            }
            else
            {

                double resource = month_resource;
                List<clTableRecord> _tempList = _listTableRecords.FindAll(x => (x.ExecutorId.Equals(id))
                                                                              && (x.WorkDate >= start)
                                                                              && (x.WorkDate <= end)
                                                                              && (GetWorkTime(x.WorkDate) > 0)
                                                                            );

                if ((_tempList == null) || (_tempList.Count <= 0))
                {
                    _listDepartmentUsers[index].Resource = month_resource;
                }
                else
                {
                    double busy_time = _tempList.Sum(x => x.WorkPlan);

                    /**/
                    busy_time = 0.0;
                    for (DateTime s = start; s <= end; s = s.AddDays(1))
                    {
                        double d = GetWorkTime(s);
                        double day_busy = 0;
                        if (d > 0)
                        {
                            List<clTableRecord> __tempList = _tempList.FindAll(x => x.WorkDate.ToShortDateString().Equals(s.ToShortDateString()));
                            if (__tempList != null)
                            {
                                day_busy = __tempList.Sum(x => x.WorkPlan);
                            }
                        }
                        if (day_busy > d)
                            day_busy = d;

                        busy_time += day_busy;
                    }
                    /**/

                    resource -= busy_time;
                    resource = Math.Round(resource, 4);
                    if (resource < 0.0001)
                        resource = 0;
                    _listDepartmentUsers[index].Resource = resource;
                }
            }
        }

        private void SeparateUserAutoTime(string executor_id, DateTime work_date)
        {
            if ((_listTableRecords == null) || (_listTableRecords.Count <= 0))
            {
                return;
            }
            else
            {
                List<clTableRecord> temp = _listTableRecords.FindAll(x => (x.ExecutorId.Equals(executor_id))
                                                                    && (x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString()))
                                                                    );

                if ((temp == null) || (temp.Count <= 0))
                {
                    return;
                }
                else
                {
                    int auto_tr = temp.Count(x => (x.ManuallyInput <= 0));
                    double free_time = GetUserFreeAutoTime(executor_id, work_date);

                    if (auto_tr <= 0)
                        auto_tr = 1;
                    //double work_time = Math.Round(free_time / auto_tr, 6);
                    double work_time = free_time / auto_tr;
                    //
                    for (int i = 0; i < _listTableRecords.Count; i++)
                    {
                        if (_listTableRecords[i].ExecutorId.ToLower().Equals(executor_id.ToLower()))
                            if (_listTableRecords[i].WorkDate.ToShortDateString().Equals(work_date.ToShortDateString()))
                                if ((_listTableRecords[i].ManuallyInput <= 0))
                                {
                                    _listTableRecords[i].WorkPlan = work_time;
                                    _listTableRecords[i].ModifyDate = DateTime.Now;
                                    _listTableRecords[i].ModifyUserId = _current_user.UserId;
                                }
                    }
                }
            }
        }

        private void AutoTableRecord_Create(string set_guid, string stage_guid, string executor_id, string dept_id,DateTime work_date, int input_type)
        {
            //Получаем свободный ресурс сотрудника на указанную дату
            double free_time = GetUserFreeAutoTime(executor_id, work_date);
            //Если ресурс сотрудника занят ручным вводом - табельную запись не создаем
            if (free_time <= 0)
                return;

            //free_time = Math.Round(free_time, 6);
            TableRecord_AddNew(set_guid, stage_guid, executor_id, dept_id,work_date, free_time, input_type);
        
        }

        private void AutoTableRecord_Change(string set_guid, string stage_guid, string executor_id, DateTime work_date, int input_type)
        {
            /*
             *
             *
             *
             *
             */
        }

        private void ManuallyTableRecord_Create(string set_guid, string stage_guid, string executor_id, string dept_id,DateTime work_date, double plan_work_time, int input_type)
        {
            double free_time = GetUserFreeAutoTime(executor_id, work_date);
            double work_time = GetWorkTime(work_date);
            if (work_time > 0)
            {
                //Рабочий день
                if (free_time <= 0)
                    return;
                if (free_time < plan_work_time)
                    plan_work_time = free_time;
            }
            else
            {
                //Выходной день
                if (plan_work_time > _default_day_work_time)
                    plan_work_time = _default_day_work_time;
            }

            TableRecord_AddNew(set_guid, stage_guid, executor_id, dept_id, work_date, plan_work_time, input_type);
        
        }

        private void TableRecord_AddNew(string set_guid, string stage_guid, string executor_id, string dept_id,DateTime work_date, double free_time, int input_type)
        {
            if (_listTableRecords == null)
                _listTableRecords = new List<clTableRecord>();

            clTableRecord tr = new clTableRecord();
            tr.CreateDate = DateTime.Now;
            tr.CreateUserId = _current_user.UserId;
            //tr.DeptId = _current_user.DepartmentId;
            tr.DeptId = dept_id; //IzmPl
            tr.ExecutorId = executor_id;
            tr.ManuallyInput = input_type;
            tr.ModifyDate = DateTime.Now;
            tr.ModifyUserId = _current_user.UserId;
            tr.SetGuid = set_guid;
            tr.StageGuid = stage_guid;
            tr.WorkDate = work_date;
            tr.WorkFact = 0;
            tr.WorkPlan = free_time;

            _listTableRecords.Add(tr);
        }

        private void ManuallyTableRecord_Change(string set_guid, string stage_guid, string executor_id, DateTime work_date, double plan_work_time, int input_type)
        {
            int index = -1;
            index = _listTableRecords.FindIndex(x => (x.SetGuid.Equals(set_guid)) && (x.StageGuid.Equals(stage_guid))
                                                  && (x.ExecutorId.ToLower().Equals(executor_id.ToLower()))
                                                  && (x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString()))
                                                );
            if (index < 0)
                return;

            double day_work_time = GetWorkTime(work_date);
            double free_time = GetUserFreeAutoTime(executor_id, work_date);

            if (day_work_time > 0)
            {
                //Рабочий день
                if (_listTableRecords[index].ManuallyInput > 0)
                    free_time += _listTableRecords[index].WorkPlan;

                if (free_time >= plan_work_time)
                {
                    _listTableRecords[index].WorkPlan = plan_work_time;
                }
                else
                {
                    _listTableRecords[index].WorkPlan = free_time;
                }
            }
            else
            {
                //Выходной день
                if (plan_work_time > _default_day_work_time)
                    plan_work_time = _default_day_work_time;
                _listTableRecords[index].WorkPlan = plan_work_time;
            }

            _listTableRecords[index].ManuallyInput = input_type;
            _listTableRecords[index].ModifyDate = DateTime.Now;
            _listTableRecords[index].ModifyUserId = _current_user.UserId;
        }

        private double GetUserFreeAutoTime(string executor_id, DateTime work_date)
        {
            double result = 0;

            double day_work_time = GetWorkTime(work_date);

            result = day_work_time;

            if (day_work_time <= 0)
                return result;

            if ((_listTableRecords == null) || (_listTableRecords.Count == 0))
                return day_work_time;

            List<clTableRecord> tempListManually = _listTableRecords.FindAll(x =>
                                                                        (x.ExecutorId.Equals(executor_id))
                                                                     && (x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString()))
                                                                     && (x.ManuallyInput > 0)
                                                                    );
            double time_to_separate = day_work_time;

            if (tempListManually != null)
            {
                for (int i = 0; i < tempListManually.Count; i++)
                {
                    time_to_separate -= tempListManually[i].WorkPlan;
                }
            }

            if (time_to_separate <= 0)
                result = 0;
            else
            {
                result = time_to_separate;
            }

            return result;
        }

        private bool IsTableRecordExists(string set_guid, string stage_guid, string executor_id, DateTime work_date)
        {
            bool result = false;
            if ((_listTableRecords == null) || (_listTableRecords.Count == 0))
                return result;

            clTableRecord tr = _listTableRecords.Find(x => (x.SetGuid.Equals(set_guid)) && (x.StageGuid.Equals(stage_guid))
                                                       && (x.ExecutorId.Equals(executor_id))
                                                       && (x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString()))
                                                     );
            if (tr != null)
                result = true;

            return result;
        }

        private bool CanChangeTableRecord(DateTime work_date)
        {
            bool result = false;
            DateTime CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime s = new DateTime(CurrentDate.Year, CurrentDate.Month, 1, 0, 0, 0);
            DateTime e = new DateTime(CurrentDate.Year, CurrentDate.Month, 7, 0, 0, 0);
            //DateTime e = new DateTime(CurrentDate.Year, CurrentDate.Month, 31, 0, 0, 0); //EL plan

            if ((work_date >= CurrentDate)&&((_current_user.Permission == "planner" )||( _current_user.Permission == "head" )))
                return true;

            if (((CurrentDate >= s) && (CurrentDate <= e))&&((_current_user.Permission == "planner" )||( _current_user.Permission == "head" )))
                return true;
                
            return result;
        }

        public void DepartmentPlan_TableRecords_Delete(string set_guid, string executor_id)
        {
            List<clTableRecord> tempListTableRecords = null;
            if ((_listTableRecords == null) || (_listTableRecords.Count == 0))
                tempListTableRecords = new List<clTableRecord>();
            else
                tempListTableRecords = _listTableRecords.FindAll(x => (x.SetGuid.Equals(set_guid))
                                                                    && (x.ExecutorId.ToLower().Equals(executor_id.ToLower()))
                                                                    );
            DeleteTableRecords(ref tempListTableRecords);
        }

        private void DeleteTableRecords(ref List<clTableRecord> tempListTableRecords)
        {
            if ((tempListTableRecords != null) && (tempListTableRecords.Count > 0))
            {
                for (int i = 0; i < tempListTableRecords.Count; i++)
                {
                    DepartmentPlan_TableRecord_Delete(tempListTableRecords[i].SetGuid, tempListTableRecords[i].ExecutorId, tempListTableRecords[i].WorkDate);
                }
            }
        }

        public void DepartmentPlan_TableRecord_Delete(string set_guid, string executor_id, DateTime work_date)
        {
            if ((_listTableRecords == null) || (_listTableRecords.Count == 0))
                return;
            if (CanChangeTableRecord(work_date))
            {
                int index = -1;
                index = _listTableRecords.FindIndex(x => (x.SetGuid.Equals(set_guid))
                                                      && (x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString()))
                                                      && (x.ExecutorId.ToLower().Equals(executor_id.ToLower()))
                                                      );
                if (index > -1)
                {
                    if (_listTableRecords[index].WorkFact <= 0)
                    {
                        _listTableRecords.RemoveAt(index);



                        SeparateUserAutoTime(executor_id, work_date);
                        CalculateUserResource(executor_id);
                        DataTableUsers_UpdateRow(executor_id);
                    }
                }
            }
        }

        public void DepartmentPlan_DeleteExecutor(string set_guid, string executor_id)
        {
            clUser user = _listDepartmentUsers.Find(x => x.UserId.Equals(executor_id));

            List<clTableRecord> tempListTableRecords = null;

            if ((_listTableRecords == null) || (_listTableRecords.Count == 0))
                tempListTableRecords = new List<clTableRecord>();
            else
                tempListTableRecords = _listTableRecords.FindAll(x => (x.SetGuid.Equals(set_guid))
                                                                    && (x.ExecutorId.Equals(executor_id))
                                                                    );
            clSet set = null;
            if ((_listPlanSets != null) || (_listPlanSets.Count != 0))
                set = _listPlanSets.Find(x => (x.SetGuid.Equals(set_guid)));
            if (set == null)
                return;

            DeleteTableRecords(ref tempListTableRecords);

            tempListTableRecords = null;
            if ((_listTableRecords == null) || (_listTableRecords.Count == 0))
                tempListTableRecords = new List<clTableRecord>();
            else
                tempListTableRecords = _listTableRecords.FindAll(x => (x.SetGuid.Equals(set_guid))
                                                                    && (x.ExecutorId.Equals(executor_id))
                                                                    );
            if (tempListTableRecords.Count == 0)
            {
                if ((set != null) && (user != null))
                {
                    set._list_executors.Remove(user);
                }
            }

            DepartmentPlan_UpdateRow(ref set);
        }

        public bool DepartmentPlan_IsSetSetExists(string set_guid,string stage_guid)
        {
            bool result = true;

            if(_listPlanSets != null)
            {
                clSet set = _listPlanSets.Find(x => (x.SetGuid.Equals(set_guid) && x.StageGuid.Equals(stage_guid)));
                if(set != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public string DepartmentPlan_AddSet(string set_guid, string stage_guid, string contract, string stage_name)
        {
            string result = ""; 

            try
            {
                using (SqlConnection con = new SqlConnection(_connStringSprut))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[PL_GetSetFromTdms]", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@set_guid", SqlDbType.NVarChar).Value = set_guid;
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                clSet newSet = new clSet();

                                newSet.Contract = contract;
                                newSet.StageName = stage_name;

                                //newSet.Kks = dr["kks"].ToString();
                                newSet.SetCode = dr["set_code"].ToString();
                                newSet.Building = dr["building"].ToString();
                                newSet.SetName = dr["set_name"].ToString();

                                DateTime dt;
                                if (DateTime.TryParse(dr["gip_start"].ToString(), out dt))
                                    newSet.GipStart = dt;
                                else
                                    newSet.GipStart = Convert.ToDateTime("1900-01-01");

                                if (DateTime.TryParse(dr["gip_end"].ToString(), out dt))
                                    newSet.GipEnd = dt;
                                else
                                    newSet.GipEnd = Convert.ToDateTime("1900-01-01");

                                if (DateTime.TryParse(dr["set_start"].ToString(), out dt))
                                    newSet.SetStart = dt;
                                else
                                    newSet.SetStart = Convert.ToDateTime("1900-01-01");

                                if (DateTime.TryParse(dr["set_end"].ToString(), out dt))
                                    newSet.SetEnd = dt;
                                else
                                    newSet.SetEnd = Convert.ToDateTime("1900-01-01");

                                if (DateTime.TryParse(dr["agreed_start"].ToString(), out dt))
                                    newSet.AgreedStart = dt;
                                else
                                    newSet.AgreedStart = Convert.ToDateTime("1900-01-01");

                                if (DateTime.TryParse(dr["agreed_end"].ToString(), out dt))
                                    newSet.AgreedEnd = dt;
                                else
                                    newSet.AgreedEnd = Convert.ToDateTime("1900-01-01");

                                //if ((newSet.Contract.ToString() == "997") || (newSet.Building.ToString() == "Работы по проекту"))    //EL_2 (даты "работы по проекту")
                                //{

                                //    DateTime month_start = new DateTime(_PlanDate.Year, _PlanDate.Month, 1, 0, 0, 0); 
                                //    DateTime month_end = new DateTime(_PlanDate.Year, _PlanDate.Month, DateTime.DaysInMonth(_PlanDate.Year, _PlanDate.Month), 0, 0, 0);
                                //    newSet.GipStart = month_start;
                                //    newSet.AgreedStart = month_start;
                                //    newSet.GipEnd = month_end;
                                //    newSet.SetEnd = month_end;
                                //}

                                newSet.SetGuid = dr["set_guid"].ToString();
                                newSet.StageGuid = stage_guid;
                                //newSet.StatusFromTdms = dr["status_from_tdms"].ToString();
                                //newSet.PercentComplete = dr["percent_complete"].ToString();
                                //newSet._responsible_user.Id = dr["responsible_user"].ToString();
                                newSet._responsible_user.FullName = dr["responsible_fio"].ToString();
                                //newSet.IsMonthPlan = "-";

                                //Добавить новый комплект
                                _listPlanSets.Add(newSet);
                                result = newSet.SetCode;

                                //Обновить таблицу комплектов
                                DepartmentPlan_AddSet(newSet);
                                DepartmentPlan_UpdateRow(ref newSet);
                            }
                            else
                            {
                                result = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                result = "";
            }

            return result;
        }

        public string DepartmentPlan_IsModified()
        {
            string result = "unknown";

            bool f1 = false;
            bool f2 = false;

            if(IsTableRecordsModified(ref _listTableRecords, ref _listTableRecords_source))
            {
                f1 = true;
            }

            if(IsSetsModified(ref _listPlanSets, ref _listPlanSets_source))
            {
                f2 = true;
            }

            if ((f1) && (!f2))
                result = "record";
            if ((!f1) && (f2))
                result = "set";
            if ((f1) && (f2))
                result = "record_set";

            return result;
        }

        private bool IsSetsModified(ref List<clSet> _modified, ref List<clSet> _source)
        {
            bool result = false;

            if ((_modified == null) || (_modified.Count <= 0) || (_source == null) || (_source.Count <= 0))
            {
                result = false;
            }
            else
            {
                foreach(var item in _source)
                {
                    var set = _modified.Find(x=> (x.SetGuid.Equals(item.SetGuid)));
                    if (set != null) 
                    {
                        //if((!set.PercentComplete.Equals(item.PercentComplete)) || (!set._responsible_user.Id.Equals(item._responsible_user.Id)))
                        if (!set._responsible_user.Fio.Equals(item._responsible_user.Fio))
                        {
                            //result = true;
                            return true;
                        }
                    }
                }

                foreach(var item in _modified)
                {
                    var set = _source.Find(x => (x.SetGuid.Equals(item.SetGuid)));
                    if(set == null)
                    {
                        //Новый комплект
                        return true;
                    }
                }
            }

            return result;
        }

        public void DepartmentPlan_Save()
        {
            DepartmentPlan_SaveTableRecords();
            DepartmentPlan_SaveSets();
        }

        private void DepartmentPlan_SaveSets()
        {
            /*
            List<clSet> UpdatedSets = new List<clSet>();

            FindChanges(ref _listPlanSets_source, ref _listPlanSets, ref UpdatedSets);

            
            if ((UpdatedSets != null) && (UpdatedSets.Count > 0))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(_connStringSprut))
                    {
                        conn.Open();

                        foreach (var set in UpdatedSets)
                        {
                            using (SqlCommand comm = new SqlCommand("UPDATE [dbo].SetExtended SET percent_complete = @pc, responsible_user = @ru WHERE guid_tdms = @gt", conn))
                            {
                                //comm.CommandText = ;
                                //comm.Parameters.AddWithValue("@pc", set.PercentComplete);
                                comm.Parameters.AddWithValue("@ru",set._responsible_user.Id);
                                comm.Parameters.AddWithValue("@gt", set.SetGuid);

                                comm.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
            }
             */
        }

        private void FindChanges(ref List<clSet> _source, ref List<clSet> _modified, ref List<clSet> UpdatedSets)
        {
            if ((_modified == null) || (_modified.Count <= 0))
                return;


            //Измененные аттрибуты комплекта
            IEnumerable<clSet> _new = _modified.Except<clSet>(_source, new clSetComparer());

            if(_new.ToList<clSet>().Count > 0)
            {
                if (UpdatedSets == null)
                    UpdatedSets = new List<clSet>();
                else if (UpdatedSets.Count > 0)
                    UpdatedSets.Clear();

                foreach(var item in _new)
                {
                    clSet set = new clSet();

                    set.SetGuid = item.SetGuid;
                    //set.StageGuid = item.StageGuid;
                    //set.PercentComplete = item.PercentComplete;
                    set._responsible_user.FullName = item._responsible_user.FullName;
                    //set._responsible_user.Id = item._responsible_user.Id;

                    UpdatedSets.Add(set);
                }
            }
        }

        private void DepartmentPlan_SaveTableRecords()
        {
            List<clTableRecord> NewTableRecords = new List<clTableRecord>();
            List<clTableRecord> UpdateTableRecords = new List<clTableRecord>();
            List<clTableRecord> DeleteTableRecords = new List<clTableRecord>();

            //DateTime sss = DateTime.Now;
            FindChanges(ref _listTableRecords_source, ref _listTableRecords, ref NewTableRecords, ref UpdateTableRecords, ref DeleteTableRecords);
            //DateTime eee = DateTime.Now;

            //MessageBox.Show("NewTableRecords count = " + NewTableRecords.Count.ToString() + "\n" + "UpdateTableRecords count = " + UpdateTableRecords.Count.ToString() + "\n" + "DeleteTableRecords count = " + DeleteTableRecords.Count.ToString() + "\nfind changes = " + (eee-sss).ToString());


            DataTable dt = new DataTable("dtMerge");
            dt.Columns.Add("set_guid", typeof(string));
            dt.Columns.Add("stage_guid", typeof(string));
            dt.Columns.Add("executor_id", typeof(Int64));
            dt.Columns.Add("work_date", typeof(DateTime));
            dt.Columns.Add("work_plan", typeof(double));
            dt.Columns.Add("work_fact", typeof(double));
            dt.Columns.Add("manually_input", typeof(double));
            dt.Columns.Add("dept_id", typeof(Int64));
            dt.Columns.Add("modify_date", typeof(DateTime));
            dt.Columns.Add("modify_user_id", typeof(Int64));
            dt.Columns.Add("create_date", typeof(DateTime));
            dt.Columns.Add("create_user_id", typeof(Int64));

            if (NewTableRecords.Count > 0)
            {
                foreach (var item in NewTableRecords)
                {
                    DataRow row = dt.NewRow();

                    row["set_guid"] = item.SetGuid;
                    row["stage_guid"] = item.StageGuid;
;
                    row["executor_id"] = Int64.Parse(item.ExecutorId);
                    row["work_date"] = item.WorkDate;
                    row["work_plan"] = item.WorkPlan;
                    row["work_fact"] = item.WorkFact;
                    row["manually_input"] = item.ManuallyInput;
                    row["dept_id"] = Int64.Parse(item.DeptId);
                    row["modify_date"] = item.ModifyDate;
                    row["modify_user_id"] = item.ModifyUserId;
                    row["create_date"] = item.CreateDate;
                    row["create_user_id"] = item.CreateUserId;

                    dt.Rows.Add(row);
                }
            }

            if (UpdateTableRecords.Count > 0)
            {
                foreach (var item in UpdateTableRecords)
                {
                    DataRow row = dt.NewRow();

                    row["set_guid"] = item.SetGuid;
                    row["stage_guid"] = item.StageGuid;
                    row["executor_id"] = Int64.Parse(item.ExecutorId);
                    row["work_date"] = item.WorkDate;
                    row["work_plan"] = item.WorkPlan;
                    row["work_fact"] = item.WorkFact;
                    row["manually_input"] = item.ManuallyInput;
                    row["dept_id"] = Int64.Parse(item.DeptId);
                    row["modify_date"] = item.ModifyDate;
                    row["modify_user_id"] = item.ModifyUserId;
                    row["create_date"] = item.CreateDate;
                    row["create_user_id"] = item.CreateUserId;

                    dt.Rows.Add(row);
                }
            }


            if (dt.Rows.Count > 0)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(_connStringPlatan))
                    //using (OleDbConnection con = new OleDbConnection(_connStringPlatan))

                    {
                        using (  SqlCommand cmd = new SqlCommand("[dbo].[PL_MergeTableRecords]"))
                        //using (OleDbCommand cmd = new OleDbCommand("[dbo].[PL_MergeTableRecords]"))
                        {
                            cmd.CommandTimeout = 3000;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@tblRecords", dt);
                            con.Open();

                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
            }
            dt.Rows.Clear();

            if (DeleteTableRecords.Count > 0)
            {
                foreach (var item in DeleteTableRecords)
                {
                    DataRow row = dt.NewRow();
                    row["set_guid"] = item.SetGuid;
                    row["stage_guid"] = item.StageGuid;
                    row["executor_id"] = Int64.Parse(item.ExecutorId);
                    row["work_date"] = item.WorkDate;
                    row["work_plan"] = item.WorkPlan;
                    row["work_fact"] = item.WorkFact;
                    row["manually_input"] = item.ManuallyInput;
                    row["dept_id"] = Int64.Parse(item.DeptId);
                    row["modify_date"] = item.ModifyDate;
                    row["modify_user_id"] = item.ModifyUserId;
                    row["create_date"] = item.CreateDate;
                    row["create_user_id"] = item.CreateUserId;
                    dt.Rows.Add(row);
                }


                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(_connStringPlatan))
                        {
                            using (SqlCommand cmd = new SqlCommand("[dbo].[PL_BulkDeleteTableRecords]"))
                            {
                                cmd.CommandTimeout = 3000;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@tblRecords", dt);
                                con.Open();

                                cmd.ExecuteNonQuery();
                                con.Close();
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

        public void DepartmentPlan_Update(DateTime dateTime)
        {
            _PlanDate = dateTime;
            Initialize_Sprut();
        }

        public bool IndividualPlan_IsSetExists(string set_guid)
        {
            bool result = true;

            if(_current_user != null)
                result = _current_user.IsSetExists(set_guid);

            return result;
        }

        public string IndividualPlan_AddSet(string set_guid, string stage_guid, string contract, string stage_name)
        {
            string result = "";

            result = _current_user.AddSet(set_guid, stage_guid, contract, stage_name);

            return result;
        }

        public void IndividualPlan_FillTableIndividualPlan(DateTime dateTime)
        {
            _current_user.FillTableIndividualPlan(dateTime);
        }

        public bool IndividualPlan_IsModified()
        {
            bool result = false;

            result = _current_user.IsModified();

            return result;
        }

        public void IndividualPlan_Save()
        {
            _current_user.Save();
        }

        public void IndividualPlan_ChangeUser(string selected_id, string selected_fio, string selected_lastname, string selected_firstname, string select_middle)
        {
            _current_user.SelectedUserID = selected_id;
            _current_user.SelectedUserFio = selected_fio;
            _current_user.SelectedUserLastName = selected_lastname;
            _current_user.SelectedUserFirstName = selected_firstname;
            _current_user.SelectedUserMiddleName = select_middle;
            
        }

        public string CurrentUser_GetPermission()
        {
            string result = "";

            if(_current_user != null)
            {
                result = _current_user.Permission;
            }

            return result;
        }

        public void CurrentUser_ChangeDepartment(string dept_id,string dept_name)
        {
            if(_current_user != null)
            {
                _current_user.DepartmentId = dept_id;
                _current_user.DepartmentSmall = dept_name;
                
            }
        }

        public string CurrentUser_GetDepartmentId()
        {
            string result = "";

            if(_current_user != null)
            {
                result = _current_user.DepartmentId;
            }

            return result;
        }

        public string GetSprutConnectionString()
        {
            return _connStringSprut;
        }

        public string GetPlatanConnectionString()
        {
            return _connStringPlatan;
        }

        public clUser CurrentUser_GetUser()
        {
            clUser result = null;

            if(_current_user != null)
            {
                result = _current_user;
            }

            return result;
        }

        public void Agreement_LoadRequests()
        {
            if (_list_requests == null)
                _list_requests = new List<clRequest>();
            else
                _list_requests.Clear();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStringSprut))
                {
                    conn.Open();
                    using(SqlCommand comm = new SqlCommand("[PL_GetRequests]",conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        //comm.Parameters.AddWithValue("@dept_small_name", dept_small_name);
                        comm.Parameters.AddWithValue("@User_ID", _current_user.UserId);
                        using(SqlDataReader r = comm.ExecuteReader())
                        {
                            if (r.HasRows)
                            {
                                DateTime dt;

                                while (r.Read())
                                {
                                    var req = new clRequest();

                                    req.Id = r["id"].ToString();
                                    req.CommentRequest = r["comment"].ToString();
                                    req.DepartmentId = r["dept_id"].ToString();

                                    if (DateTime.TryParse(r["end_date"].ToString(), out dt))
                                        req.EndDate = dt;
                                    else
                                        req.EndDate = DateTime.Now;

                                    req.Fio = r["fio"].ToString();
                                    req.Reason = r["reason"].ToString();


                                    if (DateTime.TryParse(r["request_date"].ToString(), out dt))
                                        req.RequestDate = dt;
                                    else
                                        req.RequestDate = DateTime.Now;

                                    req.SetGuid = r["set_guid"].ToString();
                                    req.StageGuid = r["stage_guid"].ToString();


                                    if (DateTime.TryParse(r["start_date"].ToString(), out dt))
                                        req.StartDate = dt;
                                    else
                                        req.StartDate = DateTime.Now;

                                    req.UserId = r["user_id"].ToString();

                                    req.Contract = r["contract"].ToString();
                                    req.SetCode = r["set_code"].ToString();
                                    req.StageName = r["stage_name"].ToString();
                                    req.DepartmentSmallName = r["department_small_name"].ToString();

                                    //req.KKS = r["kks"].ToString();
                                    req.Building = r["building"].ToString();
                                    req.SetName = r["set_name"].ToString();

                                    if (DateTime.TryParse(r["gip_start"].ToString(), out dt))
                                        req.GipStart = dt;
                                    else
                                        req.GipStart = DateTime.Now;

                                    if (DateTime.TryParse(r["gip_end"].ToString(), out dt))
                                        req.GipEnd = dt;
                                    else
                                        req.GipEnd = DateTime.Now;

                                    req.AgreedStatus = r["status"].ToString();

                                    req.TransferToPrj = Convert.ToInt32(r["transfer_to_prj"].ToString());

                                    _list_requests.Add(req);
                                }
                            }
                        }

                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace);
            }

            Fill_AgreementTable();
        }

        private void Fill_AgreementTable()
        {
            if (_list_requests == null)
                _list_requests = new List<clRequest>();

            if (_dt_agreement == null)
                _dt_agreement = new DataTable("dtAgreement");
            else
                _dt_agreement.Rows.Clear();
            
            /*Здесь бы Reflection забабахать бы...*/
            foreach(clRequest req in _list_requests)
            {
                DataRow dr = _dt_agreement.NewRow();

                dr["Id"] = req.Id;
                dr["CommentRequest"] = req.CommentRequest;
                dr["DepartmentId"] = req.DepartmentId;
                dr["EndDate"] = req.EndDate;
                dr["Fio"] = req.Fio;
                dr["Reason"] = req.Reason;
                dr["RequestDate"] = req.RequestDate;
                dr["SetGuid"] = req.SetGuid;
                dr["StageGuid"] = req.StageGuid;
                dr["StartDate"] = req.StartDate;
                dr["UserId"] = req.UserId;
                dr["StageName"] = req.StageName;
                dr["SetCode"] = req.SetCode;
                dr["Contract"] = req.Contract;
                dr["DepartmentSmallName"] = req.DepartmentSmallName;
                dr["GipStart"] = req.GipStart.ToShortDateString();
                dr["GipEnd"] = req.GipEnd.ToShortDateString();
                dr["AgreedStatus"] = req.AgreedStatus;
                //dr["KKS"] = req.KKS;
                dr["Building"] = req.Building;
                dr["SetName"] = req.SetName;
                dr["TransferToPrj"] = req.TransferToPrj;
                dr["BtnAgree"] = req.BtnAgree;
                dr["BtnDisAgree"] = req.BtnDisAgree;
                dr["BtnTransferToProject"] = req.BtnTransferToProject;
                dr["VisibleAgreedStatus"] = req.VisibleAgreedStatus;

                _dt_agreement.Rows.Add(dr);
            }
        }

        public string CurrentUser_GetUserId()
        {
            string result = "";

            if(_current_user != null)
            {
                result = _current_user.UserId;
            }

            return result;
        }

        internal void DepartmentPlan_Update_SetStatus(string set_guid)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(_connStringSprut))
                {
                    conn.Open();
                    using(SqlCommand comm = new SqlCommand("[" + frmMain.SqlLink + "].Platan.dbo.PL_GetAgreementStatus",conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("@set_guid", set_guid);

                        using(SqlDataReader dr = comm.ExecuteReader())
                        {
                            if(dr.HasRows)
                            {
                                dr.Read();
                                string status = dr["agreed_status"].ToString();
                                string agreed_start = dr["agreed_start"].ToString();
                                string agreed_end = dr["agreed_end"].ToString();
                                string comment_gip = dr["comment_gip"].ToString();  //EL

                                clSet set = _listPlanSets.Find(x => x.SetGuid.Equals(set_guid));

                                if(set != null)
                                {
                                    set.AgreedStatus = status;
                                    //set.CommentGip = comment_gip;
                                    DateTime dt;
                                    if (DateTime.TryParse(agreed_start, out dt))
                                    {
                                        set.AgreedStart = dt;
                                        set.SetStart = dt;
                                    }
                                    if (DateTime.TryParse(agreed_end, out dt))
                                    {
                                        set.AgreedEnd = dt;
                                        set.SetEnd = dt;
                                    }
                                    DepartmentPlan_UpdateRow(ref set);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void DepartmentPlan_IsSetInMonthPlan(string set_guid, DateTime dateTime)
        {
            /*if ((_listPlanSets == null) || (_listTableRecords == null))
                return;
            DateTime start = new DateTime(dateTime.Year, dateTime.Month,1,0,0,0);
            DateTime end = new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month),0,0,0);



            int index = _listPlanSets.FindIndex(x => (x.SetGuid.Equals(set_guid)) && (x.StageGuid.Equals(stage_guid)));
            var set = _listPlanSets[index];

            //if (!IsMonthLast(_PlanDate) && !set.Contract.Equals("996") && !set.Contract.Equals("997") && !set.Contract.Equals("998") && !set.Contract.Equals("999") && !set.Building.Equals("Работы по проекту"))
            //{
            //    double s = 0;
            //    Double.TryParse(set.WorkHrPlan, out s);
            //    s = s + GetPlanHrForSet(set_guid, stage_guid);
            //    set.WorkHrPlanTek = (s == 0 ? "" : String.Format("{0:### ##0.00}", s));//EL
            //    DepartmentPlan_UpdateRow(ref set);
            //}

            if ((set.SetEnd >= start) && (set.SetEnd <= end) && !set.Contract.Equals("996") && !set.Contract.Equals("997") && !set.Contract.Equals("998") && !set.Contract.Equals("999") && !set.Building.Equals("Работы по проекту"))
            {
                if (set.PercentComplete.Equals("")) 
                {
                    set.PercentComplete = "100";
                    DepartmentPlan_UpdateRow(ref set);
                }

                if (set.SetCode.Contains("r0") && set.StatusFromTdms.Equals("В работе")) 
                {
                    set.IsMonthPlan = "+";
                    DepartmentPlan_UpdateRow(ref set);
                    return;
                }

                if (!set.SetCode.Contains("r0") && (set.StatusFromTdms.Equals("В работе")) && (!String.IsNullOrEmpty(set.Executors.ToString()))) 
                {
                    set.IsMonthPlan = "+";
                    DepartmentPlan_UpdateRow(ref set);
                    return;
                }
            }
            //if ((_listPlanSets[index].AgreedStatus.Equals("Согласован")) && (_listPlanSets[index].StatusFromTdms.Equals("В работе")) && (_listPlanSets[index].SetEnd < start) )
            //{
            //    _listPlanSets[index].IsMonthPlan = "+";
            //    DepartmentPlan_UpdateRow(ref set);
            //    return;
            //}


            List<clTableRecord> temp = _listTableRecords.FindAll(x =>
                                                        (x.StageGuid.Equals(stage_guid))
                                                        && (x.SetGuid.Equals(set_guid))
                                                        && (x.WorkDate >= start)
                                                        && (x.WorkDate <= end)
                                                        && (x.WorkPlan > 0)
                                                    );

            if((temp != null) && (temp.Count > 0))
            {
                set.IsMonthPlan = "+";
                DepartmentPlan_UpdateRow(ref set);
            }
            else
            if ((temp == null) || (temp.Count <= 0))
            {  
                if(index > -1)
                {
                    set.IsMonthPlan = "-";
                    DepartmentPlan_UpdateRow(ref set);
                }
            }*/
            
        }

        //EL_2 согласовать все комплекты тек месяца, у которых есть исполнители
        public void AgreeSetsPlaned()
        {
            DateTime start = new DateTime(_PlanDate.Year, _PlanDate.Month,1,0,0,0);
            List<clSet> plan = null;
            IEnumerable<clSet> plan1 = from lst in _listPlanSets
                                       where lst.AgreedStatus.Equals("") && !lst.Executors.Equals("") && ! lst.Contract.Equals("997") && !lst.Building.Equals("Работы по проекту") && lst.SetEnd >= start
                                       select lst;
            plan = plan1.ToList<clSet>();
            DataTable agreed_table = new DataTable("agreed_table");
            agreed_table.Columns.Add("set_guid", typeof(string));
            //agreed_table.Columns.Add("stage_guid", typeof(string));
            agreed_table.Columns.Add("user_id", typeof(string));
            agreed_table.Columns.Add("start_date", typeof(string));
            agreed_table.Columns.Add("end_date", typeof(string));          
            agreed_table.Columns.Add("request_date", typeof(string));
            agreed_table.Columns.Add("reason", typeof(string));
            agreed_table.Columns.Add("comment", typeof(string));
            agreed_table.Columns.Add("dept_id", typeof(string));      
            agreed_table.Columns.Add("status", typeof(string));
            agreed_table.Columns.Add("old_start_date", typeof(string));
            agreed_table.Columns.Add("old_end_date", typeof(string));
            agreed_table.Columns.Add("transfer_to_prj", typeof(string));
            agreed_table.Columns.Add("transfer_to_prj_u_id", typeof(string));
            
            foreach (var set in plan)
            {
                DataRow row = agreed_table.NewRow();
                row["set_guid"] = set.SetGuid;
                //row["stage_guid"] = set.StageGuid;
                row["user_id"] = _current_user.UserId.ToString();
                row["start_date"] = set.SetStart.ToShortDateString();
                row["end_date"] = set.SetEnd.ToShortDateString();
                row["request_date"] =  DateTime.Now.ToString();
                row["reason"] = "";
                row["comment"] = "Согласен с предложенными датами";
                row["dept_id"] = _current_user.DepartmentId.ToString();
                row["status"] = "agree";
                row["old_start_date"] = set.SetStart.ToShortDateString();
                row["old_end_date"] = set.SetEnd.ToShortDateString();
                row["transfer_to_prj"] = "0";
                row["transfer_to_prj_u_id"] = "0";
                
                agreed_table.Rows.Add(row);
            }

            if (agreed_table.Rows.Count <= 0)
            {
                MessageBox.Show("Таблица утверждения пустая");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStringSprut))
                {
                    conn.Open();
                    using (SqlCommand comm = new SqlCommand("[dbo].InsertAgreeStatusSetPlaned", conn)) //EL TEST
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("@agreed_table", agreed_table);
                        comm.ExecuteNonQuery();

                        MessageBox.Show("Согласование комплектов успешно");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }

            foreach (var set in plan)
            {
                DepartmentPlan_Update_SetStatus(set.SetGuid);
            }
        }

        

        internal void DepartmentPlan_AddResponsible(string set_guid, string executor_id)
        {
            //var set_index = _listPlanSets.FindIndex(x=> (x.SetGuid.Equals(set_guid)));
            //if (set_index < 0)
            //    return;
            //var set = _listPlanSets[set_index];

            ////if ((set.Contract.ToString() == "997") || (set.Building.ToString() == "Работы по проекту")) //EL_2 нельзя назначить ответственного на комплект не из графика!
            ////{
            ////    return;
            ////}



            //if(_listPlanSets[set_index]._responsible_user == null)
            //    _listPlanSets[set_index]._responsible_user = new clResponsibleUser();

            //var user_index = _listDepartmentUsers.FindIndex(x=> (x.UserId.Equals(executor_id)));
            //if (user_index < 0)
            //    return;

            //_listPlanSets[set_index]._responsible_user.FullName = _listDepartmentUsers[user_index].FullName;
            ////_listPlanSets[set_index]._responsible_user.Id = _listDepartmentUsers[user_index].UserId;
            //DepartmentPlan_UpdateRow(ref set);
        }

        internal void DepartmentPlan_AddPercentComplete(string set_guid, string percent_comlete)
        {
            var set_index = _listPlanSets.FindIndex(x => (x.SetGuid.Equals(set_guid)));
            if (set_index < 0)
                return;
            var set = _listPlanSets[set_index];
           // _listPlanSets[set_index].PercentComplete = percent_comlete;
            DepartmentPlan_UpdateRow(ref set);
        }

        internal clSet DepartmentPlan_GetSet(string set_guid)
        {
            clSet result = null;

            result = _listPlanSets.Find(x=> (x.SetGuid.Equals(set_guid)));

            return result;
        }

        /// <summary>
        /// Обновить информацию об указанном запросе
        /// </summary>
        /// <param name="stage_guid"></param>
        /// <param name="set_guid"></param>
        /// <param name="request_id"></param>
        internal void Agreement_RefreshRequestInfo(string set_guid, string stage_guid, string request_id)
        {
            string agreed_status = "";
            string transfer_to_project = "";

            //Получаем данные с сервера, для только что сохраненного запроса, якобы это наиболее актуальные данные
            //можно было бы и без этого
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStringSprut))
                {
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = "SELECT TOP 1 * FROM [" + frmMain.SqlLink + "].Platan.dbo.Request WHERE Request.id = @request_id AND Request.set_guid = @set_guid";
                        comm.Parameters.AddWithValue("@request_id", request_id);
                        comm.Parameters.AddWithValue("@set_guid", set_guid);

                        conn.Open();

                        using(SqlDataReader dr = comm.ExecuteReader())
                        {
                            if(dr.HasRows)
                            {
                                dr.Read();
                                agreed_status = dr["status"].ToString();
                                transfer_to_project = dr["transfer_to_prj"].ToString();
                            }
                        }
                    }
                }

                //Если комплект был перенесен - удаляем его
                if(transfer_to_project == "1")
                {
                    Request_Delete(request_id, set_guid);
                    return;
                }

                //Комплект не перенесен, нужно обновить кнопки строки в таблице
                switch(agreed_status)
                {
                    case "agree" :
                        {
                            Request_SetStatus(request_id, set_guid, stage_guid, agreed_status);
                            break;
                        }
                    case "disagree" :
                        {   
                            //Удалить строку с запросом из таблицы; удалить из списка;
                            Request_Delete(request_id, set_guid);
                            break;
                        }
                    case "request" :
                        {
                            Request_SetStatus(request_id, set_guid, stage_guid, agreed_status);
                            break;
                        }
                    default :
                        {
                            break;
                        }
                }



            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void Request_SetStatus(string request_id, string set_guid, string stage_guid, string status)
        {
            int index = _list_requests.FindIndex(x => (x.Id.Equals(request_id)) && (x.StageGuid.Equals(stage_guid)) && (x.SetGuid.Equals(set_guid)));
            if (index > -1)
            {
                _list_requests[index].AgreedStatus = status;
                DataRow row = _dt_agreement.AsEnumerable().First(x => (x.Field<string>("Id").Equals(request_id) && x.Field<string>("SetGuid").Equals(set_guid) && x.Field<string>("StageGuid").Equals(stage_guid)));
                if (!(row == null))
                {
                    row["VisibleAgreedStatus"] = _list_requests[index].VisibleAgreedStatus;
                    row["AgreedStatus"] = status;
                }
                //dtAgreement.Rows[index]["VisibleAgreedStatus"] = _list_requests[index].VisibleAgreedStatus;
                //dtAgreement.Rows[index]["AgreedStatus"] = status; 
            }
        }

        private void Request_Delete(string request_id, string set_guid)
        {
            dtAgreement_DeleteRequest(request_id, set_guid);
            listRequests_DeleteRequest(request_id, set_guid);
        }

        private void listRequests_DeleteRequest(string request_id, string set_guid)
        {
            if (_list_requests == null)
                return;

            try
            {
                int index = _list_requests.FindIndex(x=> (x.Id.Equals(request_id)) && (x.SetGuid.Equals(set_guid)));
                if(index > -1)
                {
                    _list_requests.RemoveAt(index);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void dtAgreement_DeleteRequest(string request_id, string set_guid)
        {         
            try
            {
                if ((dtAgreement == null) || (dtAgreement.Rows == null) || (dtAgreement.Rows.Count <= 0))
                    return;

                var rows = from r in dtAgreement.AsEnumerable()
                           where r.Field<string>("ID") == request_id && r.Field<string>("SetGuid") == set_guid
                           select r;

                if (rows == null)
                    return;

                foreach(var r in rows.ToList())
                    r.Delete();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }            
        }
       

        public string ApprovedStatus(string Status)
        {
            string result = "";

            switch (Status)
            {
                case "agree":
                    {
                        result = "Утвержден";
                        break;
                    }
                case "disagree":
                    {
                        result = "Отклонен";
                        break;
                    }
                case "request":
                    {
                        result = "На утверждении";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return result;
        }

        

        public Boolean IsMonthLast(DateTime dt)
        {
            Boolean result = true;
            DateTime CurDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            DateTime EndDate = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 0, 0, 0);  //посл день месяца
            result = (EndDate < CurDate);
            return result;
        }

        public Boolean IsMonthLast(DateTime dt,DateTime dtt)
        {
            Boolean result = true;
            DateTime CurDate = new DateTime(dtt.Year, dtt.Month, 1, 0, 0, 0);
            result = (dt < CurDate);
            return result;
        }

        public String UserNoPlan()
        {
            String result = "";
            foreach (DataRow row in dtDepartmentUsers.Rows)
            {
                String id = row["UserId"].ToString();
                clUser user = _listDepartmentUsers.Find(x => x.UserId.ToLower().Equals(id));
                var tempListSet = _listPlanSets.FindAll(x => (x._list_executors.Contains(user)));
                if (!(tempListSet == null) && tempListSet.Count > 0)
                {
                       foreach (var set in tempListSet)
                       {
                           var tempListTableRecords = _listTableRecords.FindAll(x => (x.SetGuid.Equals(set.SetGuid) && x.ExecutorId.Equals(id)));
                           if ((tempListTableRecords == null) || (tempListTableRecords.Count == 0))
                           {
                               result = result + user.Fio + " (" + set.SetCode + ");\n";
                           }
                       }
                }
            }
            return result;
        }

    }
}
