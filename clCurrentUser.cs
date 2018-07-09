using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using skud;

namespace sprut
{
    class clCurrentUser : clUser
    {
        //skud.skud skud = new skud.skud();
        
        private List<clTableRecord> _listUserTableRecords = null;
        private List<clTableRecord> _listUserTableRecords_Original = null;
        private List<clSet> _listUserSets = null;

        private DataTable _dt_individual_plan;
        public  DataTable GetIndividualPlan()
        {
            return _dt_individual_plan;
        }

        private string _conn_string_sprut;
        private string _conn_string_platan;

        private string _selected_user_id;
        public string SelectedUserID
        {
            get { return _selected_user_id; }
            set { _selected_user_id = value; }
        }

        private string _selected_user_fio;
        public string SelectedUserFio
        {
            get { return _selected_user_fio; }
            set { _selected_user_fio = value; }
        }

        private string _selected_user_lastname;
        public string SelectedUserLastName
        {
            get { return _selected_user_lastname; }
            set { _selected_user_lastname = value; }
        }

        private string _selected_user_firstname;
        public string SelectedUserFirstName
        {
            get { return _selected_user_firstname; }
            set { _selected_user_firstname = value; }
        }

        private string _selected_user_middlename;
        public string SelectedUserMiddleName
        {
            get { return _selected_user_middlename; }
            set { _selected_user_middlename = value; }
        }
        

        public string ConnStringSprut
        {
            get { return _conn_string_sprut; }
            set { _conn_string_sprut = value; }
        }

        public string ConnStringPlatan
        {
            get { return _conn_string_platan; }
            set { _conn_string_platan = value; }
        }

        public clCurrentUser( ) : base()
        {
            base.Permission = "executor";
            _dt_individual_plan = new DataTable("IndividualPlan");

            //this.SelectedUserFirstName = base.FirstName;
            //this.SelectedUserLastName = base.LastName;
            //this.SelectedUserMiddleName = base.MiddleName;
        }

        public void LoadIndividualPlan(DateTime PlanDate)
        {
            DateTime from = new DateTime(PlanDate.Year, PlanDate.Month, 1, 0 ,0, 0);
            DateTime to = new DateTime(PlanDate.Year, PlanDate.Month, DateTime.DaysInMonth(PlanDate.Year, PlanDate.Month),0,0,0);

            //skud.LoadData(from.ToShortDateString(), to.ToShortDateString());

            if (_listUserTableRecords == null)
                _listUserTableRecords = new List<clTableRecord>();
            else
                _listUserTableRecords.Clear();

            if (_listUserSets == null)
                _listUserSets = new List<clSet>();
            else
                _listUserSets.Clear();

            SqlConnection connection = null;
            SqlDataReader dr = null;

            try
            {
                connection = new SqlConnection(this.ConnStringSprut);
                SqlCommand command = new SqlCommand("[dbo].[PL_GetUserPlan]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@user_id", SqlDbType.BigInt).Value = this._selected_user_id;
                command.Parameters.Add("@year", SqlDbType.Int).Value = PlanDate.Year;
                command.Parameters.Add("@month", SqlDbType.Int).Value = PlanDate.Month;
                
                connection.Open();
                dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DateTime dt;
                        double val = 0;

        #region Считываем табельную запись
                        clTableRecord tr = new clTableRecord();
                        tr.SetGuid = dr["set_guid"].ToString();
                        tr.StageGuid = dr["stage_guid"].ToString();
                        tr.ExecutorId = dr["executor_id"].ToString();
                        if(DateTime.TryParse(dr["work_date"].ToString(), out dt))
                            tr.WorkDate = dt;
                        else
                            tr.WorkDate = DateTime.Now;

                        if(Double.TryParse(dr["work_plan"].ToString(), out val))
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
                        _listUserTableRecords.Add(tr);
        #endregion


        #region Считываем комплект
                        clSet set = new clSet();
                        set.Contract = dr["contract"].ToString();
                        set.SetCode = dr["set_code"].ToString();
                        set.SetName = dr["set_name"].ToString();
                        set.StageName = dr["stage_name"].ToString();
                        set.StageGuid = dr["stage_guid"].ToString();
                        //set.Kks = dr["set_kks"].ToString();
                        set.Building = dr["building"].ToString();

                        if (DateTime.TryParse(dr["gip_start"].ToString(), out dt))
                            set.GipStart = dt;
                        else
                            set.GipStart = DateTime.Now;

                        if (DateTime.TryParse(dr["gip_end"].ToString(), out dt))
                            set.GipEnd = dt;
                        else
                            set.GipEnd = DateTime.Now;

                        set.SetGuid = dr["set_guid"].ToString();

                        var temp = _listUserSets.Find(x=> (x.SetGuid.Equals(set.SetGuid)) );
                        if (temp == null)
                            _listUserSets.Add(set);
                        
        #endregion
                    }
                }

                if (_listUserTableRecords_Original == null)
                    _listUserTableRecords_Original = new List<clTableRecord>();
                else
                    _listUserTableRecords_Original.Clear();

                foreach(var item in _listUserTableRecords)
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

                    _listUserTableRecords_Original.Add(tr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при получении индивидуального плана сотрудника.\n" + ex.Message + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (connection != null)
                    connection.Close();
            }

            FillTableIndividualPlan(PlanDate);
        }

        public void FillTableIndividualPlan(DateTime plan_date)
        {
            if (_dt_individual_plan == null)
                _dt_individual_plan = new DataTable("dtIndividualPlan");
            else
            {
                _dt_individual_plan.Rows.Clear();
                _dt_individual_plan.Columns.Clear();
            }

            _dt_individual_plan.Columns.Add("param", typeof(string));
            _dt_individual_plan.Columns.Add("Contract", typeof(string));
            _dt_individual_plan.Columns.Add("StageName", typeof(string));
            _dt_individual_plan.Columns.Add("StageGuid", typeof(string));
            //_dt_individual_plan.Columns.Add("SetKks", typeof(string));
            //_dt_individual_plan.Columns.Add("SetKksBuilding", typeof(string));
            _dt_individual_plan.Columns.Add("SetCode", typeof(string));
            _dt_individual_plan.Columns.Add("SetName", typeof(string));
            _dt_individual_plan.Columns.Add("SetGuid", typeof(string));
            _dt_individual_plan.Columns.Add("SetCodeName", typeof(string));
            _dt_individual_plan.Columns.Add("Building", typeof(string));
            _dt_individual_plan.Columns.Add("GipStart", typeof(string));
            _dt_individual_plan.Columns.Add("GipEnd", typeof(string));

            DateTime start = new DateTime(plan_date.Year, plan_date.Month, 1, 0, 0, 0);
            DateTime end = new DateTime(plan_date.Year, plan_date.Month, DateTime.DaysInMonth(plan_date.Year, plan_date.Month), 0, 0, 0);


            for (DateTime dt = start; dt <= end; dt = dt.AddDays(1))
            {
                _dt_individual_plan.Columns.Add(dt.ToShortDateString(), typeof(string));
            }

            if (_listUserSets == null)
                _listUserSets = new List<clSet>();

            _listUserSets = _listUserSets.OrderBy(x => x.Contract).ToList<clSet>();

            DataRow row = null;


            //Формируем строку с рабочим временем
            //row = _dt_individual_plan.NewRow();
            //row["param"] = "skud";
            //row["Contract"] = "";
            //row["StageName"] = "";
            //row["StageGuid"] = "";
            //row["SetGuid"] = "";
            //row["SetCode"] = "";
            //row["SetName"] = "";
            //row["SetKks"] = "";
            //row["Building"] = "";
            //row["SetKksBuilding"] = "";
            //row["SetCodeName"] = "СКУД";
            //row["GipStart"] = "";
            //row["GipEnd"] = "";

            //start = new DateTime(plan_date.Year, plan_date.Month, 1, 0, 0, 0);
            //end = new DateTime(plan_date.Year, plan_date.Month, DateTime.DaysInMonth(plan_date.Year, plan_date.Month), 0, 0, 0);
            //for (DateTime dt = start; dt <= end; dt = dt.AddDays(1))
            //{
            //    string t = skud.GetUserWorkTime(SelectedUserLastName, SelectedUserFirstName, SelectedUserMiddleName, dt.ToShortDateString());
            //    row[dt.ToShortDateString()] = t;
            //}
            //_dt_individual_plan.Rows.Add(row);


            //Формируем строку итого
            //row = _dtUserPlan.NewRow();
            row = _dt_individual_plan.NewRow();
            row["param"] = "result";
            row["Contract"] = "";
            row["StageName"] = "";
            row["StageGuid"] = "";
            row["SetGuid"] = "";
            row["SetCode"] = "";
            row["SetName"] = "";
            //row["SetKks"] = "";
            row["Building"] = "";
            //row["SetKksBuilding"] = "";
            row["SetCodeName"] = "Списания";
            row["GipStart"] = "";
            row["GipEnd"] = "";

            start = new DateTime(plan_date.Year, plan_date.Month, 1, 0, 0, 0);
            end = new DateTime(plan_date.Year, plan_date.Month, DateTime.DaysInMonth(plan_date.Year, plan_date.Month), 0, 0, 0);
            for (DateTime dt = start; dt <= end; dt = dt.AddDays(1))
            {
                try
                {
                    row[dt.ToShortDateString()] = GetDayFact(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
            }
            _dt_individual_plan.Rows.Add(row);




            foreach (var set in _listUserSets)
            {

                //Формируем строку с планом
                row = _dt_individual_plan.NewRow();
                row["param"] = "plan";
                row["Contract"] = set.Contract;
                row["StageName"] = set.StageName;
                row["StageGuid"] = set.StageGuid;
                row["SetGuid"] = set.SetGuid;
                row["SetCode"] = set.SetCode;
                row["SetName"] = set.SetName;
                //row["SetKks"] = set.Kks;
                row["Building"] = set.Building;
                //row["SetKksBuilding"] = set.Kks + " - " + set.Building;
                row["SetCodeName"] = set.SetCode + " - " + set.SetName;
                row["GipStart"] = set.GipStart.ToShortDateString();
                row["GipEnd"] = set.GipEnd.ToShortDateString();

                if (_listUserTableRecords != null)
                {

                    List<clTableRecord> tempList = _listUserTableRecords.FindAll(x => (x.SetGuid.Equals(set.SetGuid)) && (x.StageGuid.Equals(set.StageGuid)));
                    if (tempList != null)
                    {
                        foreach (var tr in tempList)
                        {
                            try
                            {
                                row[tr.WorkDate.ToShortDateString()] = tr.WorkPlan;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                            }
                        }
                    }
                    
                }
                _dt_individual_plan.Rows.Add(row);

                //Формируем строку с фактом
                row = _dt_individual_plan.NewRow();
                row["param"] = "fact";
                row["Contract"] = set.Contract;
                row["StageName"] = set.StageName;
                row["StageGuid"] = set.StageGuid;
                row["SetGuid"] = set.SetGuid;
                row["SetCode"] = set.SetCode;
                row["SetName"] = set.SetName;
                //row["SetKks"] = set.Kks;
                row["Building"] = set.Building;
                //row["SetKksBuilding"] = set.Kks + " - " + set.Building;
                row["SetCodeName"] = set.SetCode + " - " + set.SetName;
                row["GipStart"] = set.GipStart.ToShortDateString();
                row["GipEnd"] = set.GipEnd.ToShortDateString();

                if (_listUserTableRecords != null)
                {
                    
                    List<clTableRecord> tempList = _listUserTableRecords.FindAll(x => (x.SetGuid.Equals(set.SetGuid)));
                    if (tempList != null)
                    {
                        foreach (var tr in tempList)
                        {
                            try
                            {
                                row[tr.WorkDate.ToShortDateString()] = tr.WorkFact;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                            }
                        }
                    }
                    
                }
                _dt_individual_plan.Rows.Add(row);
            }

            

        }

        public  double GetDayFact(DateTime work_date)
        {
            double result = 0;

            if ((_listUserTableRecords != null) && (_listUserTableRecords.Count > 0))
            {
                List<clTableRecord> temp = _listUserTableRecords.FindAll(x => x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString()));
                if ((temp != null) && (temp.Count > 0))
                {
                    result = temp.Sum(x => x.WorkFact);
                }
            }

            return result;
        }

        public void CreateOrUpdateTableRecord(string set_guid, string stage_guid, DateTime work_date, double work_fact)
        {
            if ((_listUserTableRecords == null))
                _listUserTableRecords = new List<clTableRecord>();

            clTableRecord tr = _listUserTableRecords.Find(x =>
                                                                  (x.SetGuid.Equals(set_guid))
                                                               && (x.StageGuid.Equals(stage_guid))
                                                               && (x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString()))
                                                         );
            if (tr == null)
            {
                //Создать новую табельную запись
                CreateTableRecord(set_guid, stage_guid, work_date, work_fact);
            }
            else
            {
                //Обновить табельную запись
                UpdateTableRecord(set_guid, stage_guid, work_date, work_fact);
            }
        }

        private void UpdateTableRecord(string set_guid, string stage_guid, DateTime work_date, double work_fact)
        {
            clSet set = _listUserSets.Find(x => (x.SetGuid.Equals(set_guid)));
            if (set == null)
                return;

            int tr_index = _listUserTableRecords.FindIndex(x =>
                                                                (x.SetGuid.Equals(set_guid))
                                                                && (x.StageGuid.Equals(stage_guid))
                                                                && (x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString()))
                                                          );
            double day_max_fact = 15;
            double day_actualy_fact = 0;
            double day_free_fact = 0;

            List<clTableRecord> _templist = _listUserTableRecords.FindAll(x => (x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString())));
            if ((_templist != null) && (_templist.Count > 0))
            {
                day_actualy_fact = _templist.Sum(x => x.WorkFact);
                day_free_fact = day_max_fact - day_actualy_fact + _listUserTableRecords[tr_index].WorkFact;

                if (work_fact > day_free_fact)
                {
                    work_fact = day_free_fact;
                }

                if (day_free_fact <= 0)
                {
                    MessageBox.Show("Количество отработанных часов в сутки должно быть меньше 15", "Превышен лимит", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _listUserTableRecords[tr_index].WorkFact = work_fact;
                _listUserTableRecords[tr_index].ModifyDate = DateTime.Now;
                _listUserTableRecords[tr_index].ModifyUserId = this.UserId;
            }
        }

        private void CreateTableRecord(string set_guid, string stage_guid, DateTime work_date, double work_fact)
        {
            clSet set = _listUserSets.Find(x => (x.SetGuid.Equals(set_guid)) && (x.StageGuid.Equals(stage_guid)));
            if (set == null)
                return;

            double day_max_fact = 15;
            double day_actualy_fact = 0;

            List<clTableRecord> _templist = _listUserTableRecords.FindAll(x => (x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString())));
            if ((_templist != null) && (_templist.Count > 0))
            {
                day_actualy_fact = _templist.Sum(x => x.WorkFact);
                if (work_fact > day_max_fact - day_actualy_fact)
                {
                    work_fact = day_max_fact - day_actualy_fact;
                }

                if (work_fact <= 0)
                {
                    MessageBox.Show("Количество отработанных часов в сутки должно быть меньше 15", "Превышен лимит", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            clTableRecord tr = new clTableRecord();
            
            tr.CreateDate = DateTime.Now;
            tr.CreateUserId = this.UserId;
            
            tr.DeptId = this.DepartmentId;
            tr.ExecutorId = SelectedUserID;
            tr.ManuallyInput = 1;
            tr.ModifyDate = DateTime.Now;
            tr.ModifyUserId = this.SelectedUserID;
            tr.SetGuid = set.SetGuid;
            tr.StageGuid = set.StageGuid;

            tr.WorkDate = work_date;
            tr.WorkFact = work_fact;
            tr.WorkPlan = 0;
            
            _listUserTableRecords.Add(tr);

        }

        public  void DeleteTableRecord(string set_guid, DateTime column_date)
        {
            if (_listUserTableRecords == null)
                return;

            int tr_index = _listUserTableRecords.FindIndex(x => (x.SetGuid.Equals(set_guid))
                                                                && (x.WorkDate.ToShortDateString().Equals(column_date.ToShortDateString()))
                                                          );
            if (tr_index < 0)
                return;

            if (_listUserTableRecords[tr_index].WorkPlan == 0)
            {
                _listUserTableRecords.RemoveAt(tr_index);
            }
            else
            {
                _listUserTableRecords[tr_index].WorkFact = 0;
                _listUserTableRecords[tr_index].ModifyDate = DateTime.Now;
                _listUserTableRecords[tr_index].ModifyUserId = this.UserId;
            }
        }

        public clTableRecord GetTableRecord(string set_guid, DateTime work_date)
        {
            clTableRecord result = null;

            if ((_listUserTableRecords != null) && (_listUserTableRecords.Count > 0))
            {
                var tr = _listUserTableRecords.Find(x=> 
                                                        (x.SetGuid.Equals(set_guid)) 
                                                        && (x.WorkDate.ToShortDateString().Equals(work_date.ToShortDateString()))
                                                      );
                if(tr != null)
                {
                    result = tr;
                }
            }

            return result;
        }

        public bool IsSetExists(string set_guid)
        {
            bool result = true;

            if (_listUserSets == null)
                _listUserSets = new List<clSet>();

            clSet set = _listUserSets.Find(x=> (x.SetGuid.Equals(set_guid)));

            if (set == null)
                result = false;

            return result;
        }

        public string AddSet(string set_guid, string stage_guid, string contract, string stage_name)
        {
            string result = "";

            try
            {
                using (SqlConnection con = new SqlConnection(this.ConnStringSprut))
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
                                    newSet.GipEnd = Convert.ToDateTime("1900-01-01"); ;

                                newSet.SetGuid = dr["set_guid"].ToString();
                                newSet.StageGuid = stage_guid;
                                //newSet.StatusFromTdms = dr["status_from_tdms"].ToString();


                                //Добавить новый комплект
                                //_listPlanSets.Add(newSet);
                                _listUserSets.Add(newSet);
                                result = newSet.SetCode;

                                //Обновить таблицу комплектов
                                //DepartmentPlan_AddSet(newSet);
                                
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

        public bool IsModified()
        {
            bool result = false;

            if (_listUserTableRecords == null)
                _listUserTableRecords = new List<clTableRecord>();
            if (_listUserTableRecords_Original == null)
                _listUserTableRecords_Original = new List<clTableRecord>();

            //Ищем новые табельные записи
            foreach (var item in _listUserTableRecords)
            {
                clTableRecord tr = _listUserTableRecords_Original.Find(x => (x.SetGuid.Equals(item.SetGuid))
                                                    && (x.StageGuid.Equals(item.StageGuid))
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
                    if ((tr.WorkFact != item.WorkFact))
                    {
                        result = true;
                        return result;
                    }
                }
            }

            //Ищем удаленные табельные записи
            foreach (var item in _listUserTableRecords_Original)
            {
                clTableRecord tr = _listUserTableRecords.Find(x => (x.SetGuid.Equals(item.SetGuid))
                                                    && (x.StageGuid.Equals(item.StageGuid))
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

        public void FindChanges(ref List<clTableRecord> _original, ref List<clTableRecord> _modified, ref List<clTableRecord> _new, ref List<clTableRecord> _updated, ref List<clTableRecord> _deleted)
        {
            //Ищем новые табельные записи
            foreach (var item in _modified)
            {
                clTableRecord tr = _original.Find(x => (x.SetGuid.Equals(item.SetGuid))
                                                    && (x.StageGuid.Equals(item.StageGuid))
                                                    && (x.ExecutorId.Equals(item.ExecutorId))
                                                    && (x.WorkDate.ToShortDateString().Equals(item.WorkDate.ToShortDateString()))
                                                );
                if (tr == null)
                {
                    //Новая табельная запись, ее нет в исходном списке
                    _new.Add(item);
                }
                else
                {
                    //Проверить была ли изменена табельная запись
                    if ((tr.WorkFact != item.WorkFact))
                    {
                        _updated.Add(item);
                    }
                }
            }

            //Ищем удаленные табельные записи
            foreach (var item in _original)
            {
                clTableRecord tr = _modified.Find(x => (x.SetGuid.Equals(item.SetGuid))
                                                    && (x.StageGuid.Equals(item.StageGuid))
                                                    && (x.ExecutorId.Equals(item.ExecutorId))
                                                    && (x.WorkDate.ToShortDateString().Equals(item.WorkDate.ToShortDateString()))
                                                );
                if (tr == null)
                {
                    //Табельная запись есть в исходном но отсутствует в модифицированном - была удалена
                    _deleted.Add(item);
                }
            }
        }

        public void Save()
        {
            List<clTableRecord> NewTableRecords = new List<clTableRecord>();
            List<clTableRecord> UpdateTableRecords = new List<clTableRecord>();
            List<clTableRecord> DeleteTableRecords = new List<clTableRecord>();

            //DateTime sss = DateTime.Now;
            FindChanges(ref _listUserTableRecords_Original, ref _listUserTableRecords, ref NewTableRecords, ref UpdateTableRecords, ref DeleteTableRecords);
            //DateTime eee = DateTime.Now;

            //MessageBox.Show("NewTableRecords count = " + NewTableRecords.Count.ToString() + "\n" + "UpdateTableRecords count = " + UpdateTableRecords.Count.ToString() + "\n" + "DeleteTableRecords count = " + DeleteTableRecords.Count.ToString() + "\nfind changes = " + (eee - sss).ToString());


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
                    using (SqlConnection con = new SqlConnection(this.ConnStringPlatan))
                    {
                        using (SqlCommand cmd = new SqlCommand("[dbo].[PL_MergeTableRecords]"))
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
                        using (SqlConnection con = new SqlConnection(this._conn_string_platan))
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

        public void ChangeDepartment(string dept_name_small)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conn_string_sprut))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[GetDepartmentId]", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@dept_small_name", SqlDbType.NVarChar).Value = dept_name_small;
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    this.DepartmentId = dr["dept_id"].ToString();
                                    this.DepartmentSmall = dr["dept_small_name"].ToString();
                                }
                            }
                        }

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

