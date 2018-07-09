using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sprut
{
    struct time_sheet
    {
        public string user_name;
        public double h_real;
        public double h_plan;
    }

    public partial class frmSetHistory : Form
    {
        DataTable dt_set_tdms_history = new DataTable("dt_set_tdms_history");
        DataTable dt_set_plan_history = new DataTable("dt_set_plan_history");
        DataTable dt_set_plan_history_result = new DataTable("dt_set_plan_history_result");
        DataTable dt_set_agreement_history = new DataTable("dt_set_agreement_history");
        List<time_sheet> list_ts = new List<time_sheet>();

        public frmSetHistory()
        {
            InitializeComponent();
            dgvSetTDMSHistory.DataSource = dt_set_tdms_history.DefaultView;
            dgvSetPLANHistory.DataSource = dt_set_plan_history.DefaultView;
            dgvSetPLANHistoryResult.DataSource = dt_set_plan_history_result.DefaultView;
            dgvSetAgreementHistory.DataSource = dt_set_agreement_history.DefaultView;
        }


        private void RenameDgvSetPLANHistoryResultColumns()
        {
            try
            {
                dgvSetPLANHistoryResult.Columns["user"].HeaderText = "Сотрудник";
                dgvSetPLANHistoryResult.Columns["h_real"].HeaderText = "Факт, ч";
                dgvSetPLANHistoryResult.Columns["h_plan"].HeaderText = "План, ч";
            }
            catch
            {

            }
        }

        private void RenameDgvSetTDMSHistoryColumns()
        {
            try
            {
                dgvSetTDMSHistory.Columns["F_DESCR"].HeaderText = "Описание";
                dgvSetTDMSHistory.Columns["F_EVENTTYPE"].HeaderText = "Событие";
                dgvSetTDMSHistory.Columns["F_USER"].HeaderText = "Пользователь";
                dgvSetTDMSHistory.Columns["F_DATEVAL"].HeaderText = "Дата";
            }
            catch
            {
            }
        }

        private void RenameDgvSetPLANHistoryColumns()
        {
            try
            {
                dgvSetPLANHistory.Columns["user"].HeaderText = "Сотрудник";
                dgvSetPLANHistory.Columns["h_real"].HeaderText = "Факт, ч";
                dgvSetPLANHistory.Columns["h_plan"].HeaderText = "План, ч";
                dgvSetPLANHistory.Columns["date"].HeaderText = "Дата";
            }
            catch
            {
            }
        }

        private void RenameDgvSetAgreementHistory()
        {
            try
            {
                dgvSetAgreementHistory.Columns["set_guid"].HeaderText = "set_guid";
                dgvSetAgreementHistory.Columns["set_guid"].Visible = false;
                dgvSetAgreementHistory.Columns["stage_guid"].HeaderText = "stage_guid";
                dgvSetAgreementHistory.Columns["stage_guid"].Visible = false;
                dgvSetAgreementHistory.Columns["request_user_id"].HeaderText = "request_user_id";
                dgvSetAgreementHistory.Columns["request_user_id"].Visible = false;
                dgvSetAgreementHistory.Columns["request_user_fio"].HeaderText = "Запросил";
                dgvSetAgreementHistory.Columns["request_start_date"].HeaderText = "Запрошенная дата начала";
                dgvSetAgreementHistory.Columns["request_end_date"].HeaderText = "Запрошенная дата окончания";
                dgvSetAgreementHistory.Columns["request_date"].HeaderText = "Дата запроса";
                dgvSetAgreementHistory.Columns["request_reason"].HeaderText = "Причина переноса";
                dgvSetAgreementHistory.Columns["request_comment"].HeaderText = "Комментарий";
                dgvSetAgreementHistory.Columns["request_dept_id"].HeaderText = "Отдел";
                dgvSetAgreementHistory.Columns["request_dept_id"].Visible = false;
                dgvSetAgreementHistory.Columns["request_status"].HeaderText = "Статус запроса";
                
                dgvSetAgreementHistory.Columns["response_user_id"].HeaderText = "response_user_id";
                dgvSetAgreementHistory.Columns["response_user_id"].Visible = false;
                dgvSetAgreementHistory.Columns["response_user_fio"].HeaderText = "Ответил";
                dgvSetAgreementHistory.Columns["response_date"].HeaderText = "Дата ответа";
                dgvSetAgreementHistory.Columns["response_comment"].HeaderText = "Комментарий";
                dgvSetAgreementHistory.Columns["response_status"].HeaderText = "Статус ответа";
                dgvSetAgreementHistory.Columns["response_status"].Visible = false;

            }
            catch
            { 
            }
        }

        private void frmSetHistory_Load(object sender, EventArgs e)
        {
            RenameDgvSetTDMSHistoryColumns();
            RenameDgvSetPLANHistoryColumns();
            RenameDgvSetPLANHistoryResultColumns();
            RenameDgvSetAgreementHistory();
        }
    
        public void CreateSetHistory(string project, string set_guid, string stage_guid, string stage_name, string set, string set_name, string set_status, string set_cpk_date, string set_cpk_nom, string gip_start, string gip_end, string conn_string)
        {
            lblStage.Text = stage_name;
            lblSetName.Text = set_name;
            lblSet.Text = set;
            lblProject.Text = project;

            if (gip_start.Length > 10)
                gip_start = gip_start.Substring(0, 10);
            if (gip_end.Length > 10)
                gip_end = gip_end.Substring(0, 10);

            lblGipStart.Text = gip_start;
            lblGipEnd.Text = gip_end;

            CreateTDMSHistory(set_guid, conn_string);
            CreatePLANHistory(set_guid, stage_guid, conn_string);
            CreatePLANHistoryResult();
        }
         

        private void CreatePLANHistoryResult()
        {
            ClearPLANResultDataTable();
            if (list_ts == null)
                list_ts = new List<time_sheet>();
            else
                list_ts.Clear();

            for(int i = 0 ; i < dgvSetPLANHistory.RowCount; i++)
            {
                string user = (dgvSetPLANHistory.Rows[i].Cells["user"].Value ?? "Неизвестно").ToString();
                int index = -1;
                index = list_ts.FindIndex(x=> (x.user_name == user));
                if(index > -1)
                {
                    time_sheet ts = list_ts[index];
                    
                    string h_real = (dgvSetPLANHistory.Rows[i].Cells["h_real"].Value ?? "0").ToString();
                    double h_r = 0;
                    if(Double.TryParse(h_real, out h_r))
                    {
                        ts.h_real += h_r;
                    }

                    string h_plan = (dgvSetPLANHistory.Rows[i].Cells["h_plan"].Value ?? "0").ToString();
                    double h_p = 0;
                    if (Double.TryParse(h_plan, out h_p))
                    {
                        ts.h_plan += h_p;
                    }

                    list_ts[index] = ts;
                }
                else
                {
                    time_sheet ts;
                    ts.user_name = user;
                    ts.h_real = 0;
                    ts.h_plan = 0;

                    string h_real = (dgvSetPLANHistory.Rows[i].Cells["h_real"].Value ?? "0").ToString();
                    double h_r = 0;
                    if (Double.TryParse(h_real, out h_r))
                    {
                        ts.h_real = h_r;
                    }

                    string h_plan = (dgvSetPLANHistory.Rows[i].Cells["h_plan"].Value ?? "0").ToString();
                    double h_p = 0;
                    if (Double.TryParse(h_plan, out h_p))
                    {
                        ts.h_plan = h_p;
                    }

                    list_ts.Add(ts);
                }
            }

            if(list_ts.Count > 0)
            {
                double p = 0;
                double r = 0;
                foreach (var ts in list_ts)
                {
                    DataRow dr = dt_set_plan_history_result.NewRow();
                    dr["user"] = ts.user_name;
                    dr["h_real"] = ts.h_real;
                    r += ts.h_real;
                    dr["h_plan"] = ts.h_plan;
                    p += ts.h_plan;
                    dt_set_plan_history_result.Rows.Add(dr);
                }
                DataRow dra = dt_set_plan_history_result.NewRow();
                dra["user"] = "Всего";
                dra["h_real"] = r;
                dra["h_plan"] = p;
                dt_set_plan_history_result.Rows.Add(dra);
            }
        }

        private void ClearPLANResultDataTable()
        {
            try
            {
                if (dt_set_plan_history_result == null)
                {
                    dt_set_plan_history_result = new DataTable();
                }
                dt_set_plan_history_result.Rows.Clear();
                dt_set_plan_history_result.Columns.Clear();

                dt_set_plan_history_result.Columns.Add("user", typeof(string));
                dt_set_plan_history_result.Columns.Add("h_real", typeof(string));
                dt_set_plan_history_result.Columns.Add("h_plan", typeof(string));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void CreatePLANHistory(string set_guid, string stage_guid, string conn_string)
        {
            ClearPLANDataTable();
            SqlConnection conn = new SqlConnection(conn_string);
            SqlDataReader dr = null;

            try
            {
                SqlCommand command = new SqlCommand("[dbo].[PL_GetSetPlanHistory]", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@set_guid", SqlDbType.NVarChar).Value = set_guid;
                command.Parameters.Add("@stage_guid", SqlDbType.NVarChar).Value = stage_guid;
                conn.Open();
                dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DataRow d_row = dt_set_plan_history.NewRow();
                        d_row["user"] = dr["user"].ToString();
                        //string event_type = dr["F_EVENTTYPE"].ToString();
                        //event_type = GetEvent(event_type);
                        //d_row["F_EVENTTYPE"] = event_type;
                        d_row["date"] = dr["date"].ToString();
                        d_row["h_real"] = dr["h_real"].ToString();
                        d_row["h_plan"] = dr["h_plan"].ToString();
                        dt_set_plan_history.Rows.Add(d_row);
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

        private void CreateTDMSHistory(string set_guid, string conn_string)
        {
            ClearTDMSDataTable();
            SqlConnection conn = new SqlConnection(conn_string);
            SqlDataReader dr = null;

            try
            {
                SqlCommand command = new SqlCommand("[dbo].[PL_GetSetTDMSHistory]", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@set_guid", SqlDbType.NVarChar).Value = set_guid;
                conn.Open();
                dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DataRow d_row = dt_set_tdms_history.NewRow();
                        d_row["F_DESCR"] = dr["F_DESCR"].ToString();
                        string event_type = dr["F_EVENTTYPE"].ToString();
                        event_type = GetEvent(event_type);
                        d_row["F_EVENTTYPE"] = event_type;
                        d_row["F_USER"] = dr["F_USER"].ToString();
                        d_row["F_DATEVAL"] = dr["F_DATEVAL"].ToString();
                        dt_set_tdms_history.Rows.Add(d_row);
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

        private string GetEvent(string event_type)
        {
            string result = "";
            switch (event_type)
            {
                case "2049":
                    result = "Объект создан";
                    break;
                case "2050":
                    result = "Объект модифицирован";
                    break;
                case "2052":
                    result = "Версия создана";
                    break;
                case "2054":
                    result = "Объект дублирован";
                    break;
                case "2055":
                    result = "Статус изменен";
                    break;
                case "2057":
                    result = "Добавить в состав";
                    break;
                case "2058":
                    result = "Удалить из состава";
                    break;
                case "4097":
                    result = "Запуск команды";
                    break;
                case "16385":
                    result = "Ошибка";
                    break;
                case "32769":
                    result = "Файл создан";
                    break;
                case "32770":
                    result = "Файл удален";
                    break;
                case "32771":
                    result = "Файл выложен на диск";
                    break;
                case "32772":
                    result = "Файл загружен в базу";
                    break;
                case "32773":
                    result = "Файл изменен";
                    break;
                default:
                    result = "Неизвестно";
                    break;
            }
            return result;
        }

        private void ClearTDMSDataTable()
        {
            try
            {
                if (dt_set_tdms_history == null)
                {
                    dt_set_tdms_history = new DataTable();
                }
                dt_set_tdms_history.Rows.Clear();
                dt_set_tdms_history.Columns.Clear();

                dt_set_tdms_history.Columns.Add("F_DATEVAL", typeof(DateTime));
                dt_set_tdms_history.Columns.Add("F_EVENTTYPE", typeof(string));
                dt_set_tdms_history.Columns.Add("F_DESCR", typeof(string));
                //dt_set_history.Columns.Add("F_USERID", typeof(string));
                //dt_set_history.Columns.Add("F_COMPUTERNAME", typeof(string));
                dt_set_tdms_history.Columns.Add("F_USER", typeof(string));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ClearPLANDataTable()
        {
            try
            {
                if (dt_set_plan_history == null)
                {
                    dt_set_plan_history = new DataTable();
                }
                dt_set_plan_history.Rows.Clear();
                dt_set_plan_history.Columns.Clear();

                dt_set_plan_history.Columns.Add("user", typeof(string));
                dt_set_plan_history.Columns.Add("date",typeof(DateTime));
                dt_set_plan_history.Columns.Add("h_real", typeof(string));
                dt_set_plan_history.Columns.Add("h_plan", typeof(string));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void CreateSetHistory(string contract, string set_guid, string stage_guid, string stage_name, string set_code, string set_name, string gip_start, string gip_end, string conn_string)
        {
            lblStage.Text = stage_name;
            lblSetName.Text = set_name;
            //lblSetStatus.Text = set_status;
            lblSet.Text = set_code;
            //lblCpkDate.Text = set_cpk_date;
            //lblCpkNom.Text = set_cpk_nom;
            lblProject.Text = contract;

            if (gip_start.Length > 10)
                gip_start = gip_start.Substring(0, 10);
            if (gip_end.Length > 10)
                gip_end = gip_end.Substring(0, 10);

            lblGipStart.Text = gip_start;
            lblGipEnd.Text = gip_end;

            CreateTDMSHistory(set_guid, conn_string);
            CreatePLANHistory(set_guid, stage_guid, conn_string);
            CreatePLANHistoryResult();
            CreateAgreementhistory(set_guid, conn_string);
        }

        private void CreateAgreementhistory(string set_guid, string conn_string)
        {
            ClearAgreementTable();
            try
            {
                using(SqlConnection conn = new SqlConnection(conn_string))
                {
                    conn.Open();

                    using(SqlCommand comm = new SqlCommand("[dbo].[PL_GetSetAgreementHistory]",conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("@set_guid", set_guid);

                        using(SqlDataReader dr = comm.ExecuteReader())
                        {
                            if(dr.HasRows)
                            {
                                while(dr.Read())
                                {
                                    DataRow row = dt_set_agreement_history.NewRow();

                                    row["set_guid"] = dr["set_guid"].ToString();
                                    row["stage_guid"] = dr["stage_guid"].ToString();

                                    row["request_user_id"] = dr["request_user_id"].ToString();
                                    row["request_start_date"] = dr["request_start_date"].ToString();
                                    row["request_end_date"] = dr["request_end_date"].ToString();
                                    row["request_date"] = dr["request_date"].ToString();
                                    row["request_reason"] = dr["request_reason"].ToString();
                                    row["request_comment"] = dr["request_comment"].ToString();
                                    row["request_dept_id"] = dr["request_dept_id"].ToString();
                                    string request_status = dr["request_status"].ToString();
                                    row["request_status"] = "Неизвестный статус";
                                    switch(request_status)
                                    {
                                        case "agree" :
                                            {
                                                row["request_status"] = "Согласован";
                                                break;
                                            }
                                        case "disagree":
                                            {
                                                row["request_status"] = "Не согласован";
                                                break;
                                            }
                                        case "request":
                                            {
                                                row["request_status"] = "На согласовании";
                                                break;
                                            }
                                    }
                                    row["request_user_fio"] = dr["request_user_fio"].ToString();

                                    row["response_user_id"] = dr["response_user_id"].ToString();
                                    row["response_date"] = dr["response_date"].ToString();
                                    row["response_comment"] = dr["response_comment"].ToString();
                                    row["response_status"] = dr["response_status"].ToString();
                                    row["response_user_fio"] = dr["response_user_fio"].ToString();

                                    dt_set_agreement_history.Rows.Add(row);
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

        private void ClearAgreementTable()
        {
            if(dt_set_agreement_history != null)
            {
                dt_set_agreement_history.Rows.Clear();
                dt_set_agreement_history.Columns.Clear();
            }
            else
            {
                dt_set_agreement_history = new DataTable("dt_set_agreement_history");
            }

            dt_set_agreement_history.Columns.Add("set_guid");
            dt_set_agreement_history.Columns.Add("stage_guid");
            dt_set_agreement_history.Columns.Add("request_user_id");
            dt_set_agreement_history.Columns.Add("request_user_fio");
            dt_set_agreement_history.Columns.Add("request_date");
            dt_set_agreement_history.Columns.Add("request_reason");
            dt_set_agreement_history.Columns.Add("request_start_date");
            dt_set_agreement_history.Columns.Add("request_end_date");
            dt_set_agreement_history.Columns.Add("request_comment");
            dt_set_agreement_history.Columns.Add("request_dept_id");
            dt_set_agreement_history.Columns.Add("request_status");

            dt_set_agreement_history.Columns.Add("response_user_id");
            dt_set_agreement_history.Columns.Add("response_user_fio");
            dt_set_agreement_history.Columns.Add("response_date");
            dt_set_agreement_history.Columns.Add("response_comment");
            dt_set_agreement_history.Columns.Add("response_status");
           

        }
    }
}
