using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sprut
{	
    
    public class clSet   //clSet.cs - изменение plotnikovsp - пробелы 10.07.18
    {
        
        //private string _is_month_plan;
        //public string IsMonthPlan
        //{
        //    get { return _is_month_plan; }
        //    set { _is_month_plan = value; }
        //}
        
        private string _contract;
        public string Contract
        {
            get { return _contract; }
            set { _contract = value; }
        }

        private string _stage_name;
        public string StageName
        {
            get { return _stage_name; }
            set { _stage_name = value; }
        }

        private string _building;
        public string Building
        {
            get { return _building; }
            set { _building = value; }
        }

        

        //private string _kks;
        //public string Kks
        //{
        //    get { return _kks; }
        //    set { _kks = value; }
        //}

        

        private string _set_code;
        public string SetCode
        {
            get { return _set_code; }
            set { _set_code = value; }
        }

        //private string _status_from_tdms; //EL
        //public string StatusFromTdms
        //{
        //    get { return _status_from_tdms; }
        //    set { _status_from_tdms = value; }
        //}

        private string _set_name;
        public string SetName 
        {
            get { return _set_name; }
            set { _set_name = value; }
        }

        //private string _mark;
        //public string Mark
        //{
        //    get { return _mark; }
        //    set { _mark = value; }
        //}
        
        private DateTime _gip_start;
        public DateTime GipStart
        {
            get { return _gip_start; }
            set { _gip_start = value; }
        }

        private DateTime _gip_end;
        public DateTime GipEnd
        {
            get { return _gip_end; }
            set { _gip_end = value; }
        }

        private DateTime _agreed_start;
        public DateTime AgreedStart
        {
            get { return _agreed_start; }
            set { _agreed_start = value; }
        }

        private DateTime _agreed_end;
        public DateTime AgreedEnd
        {
            get { return _agreed_end; }
            set { _agreed_end = value; }
        }

        private DateTime _set_start;
        public DateTime SetStart
        {
            get { return _set_start; }
            set { _set_start = value; }
        }

        private DateTime _set_end;
        public DateTime SetEnd
        {
            get { return _set_end; }
            set { _set_end = value; }
        }

        private string _agreed_status;
        public string AgreedStatus
        {
            get 
            {
                string result = "";

                switch(_agreed_status)
                {
                    case "delete":
                        {
                            result = "Удален";
                            break;
                        }
                    case "agree" :
                        {
                            result = "Согласован";
                            break;
                        }
                    case "disagree":
                        {
                            result = "Не согласован";
                            break;
                        }
                    case "request":
                        {
                            result = "На согласовании";
                            break;
                        }
                }
                return result; 
            }
            set { _agreed_status = value; }
        }
        
        private string _set_guid;
        public string SetGuid
        {
            get { return _set_guid; }
            set { _set_guid = value; }
        }

        private string _stage_guid;
        public string StageGuid
        {
            get { return _stage_guid; }
            set { _stage_guid = value; }
        }

        private string _set_dept;
        public string SetDept
        {
            get { return _set_dept; }
            set { _set_dept = value; }
        }

        //private string _set_cpkdate;
        //public string SetCpkDate
        //{
        //    get { return _set_cpkdate; }
        //    set { _set_cpkdate = value; }
        //}

        //private string _set_revision;
        //public string SetRevision
        //{
        //    get { return _set_revision; }
        //    set { _set_revision = value; }
        //}

        //private string _comment_gip;  //EL_2
        //public string CommentGip
        //{
        //    get { return _comment_gip; }
        //    set { _comment_gip = value; }
        //}

        //private string _status_pp;  //EL_2
        //public string StatusPP
        //{
        //    get { return _status_pp; }
        //    set { _status_pp = value; }
        //}
        
        public clResponsibleUser _responsible_user = new clResponsibleUser();
        public string ResponsibleUser
        {
            get 
            { 
                return _responsible_user.Fio; 
            }
        }

        public List<clUser> _list_executors = new List<clUser>();
        public string Executors
        {
            get 
            {
                string result = "";
                if (_list_executors == null)
                    _list_executors = new List<clUser>();
                
                if (_list_executors.Count == 0)
                    return result;
                
                foreach(var u in _list_executors)
                {
                    result += u.Fio + "; ";
                }
                result = _list_executors.Count + " - " + result;

                return result; 
            }
        }

        //private string _percent_comlete = "";
        //public string PercentComplete
        //{
        //    get { return _percent_comlete; }
        //    set { _percent_comlete = value; }
        //}

        //private string _work_hr_mng = "";
        //public string WorkHrMng
        //{
        //    get { return _work_hr_mng; }
        //    set { _work_hr_mng = value; }
        //}

        //private string _work_hr_fakt = "";
        //public string WorkHrFakt
        //{
        //    get { return _work_hr_fakt; }
        //    set { _work_hr_fakt = value; }
        //}

        //private string _work_hr_delta = "";
        //public string WorkHrDelta
        //{
        //    get { return _work_hr_delta; }
        //    set { _work_hr_delta = value; }
        //}

        //private string _work_hr_plan = "";
        //public string WorkHrPlan
        //{
        //    get { return _work_hr_plan; }
        //    set { _work_hr_plan = value; }
        //}

        //private string _work_hr_plan_tek = "";
        //public string WorkHrPlanTek
        //{
        //    get { return _work_hr_plan_tek; }
        //    set { _work_hr_plan_tek = value; }
        //}

        //private string _price_dog = "";
        //public string PriceDog
        //{
        //    get { return _price_dog; }
        //    set { _price_dog = value; }
        //}

        //private string _price_mng = "";
        //public string PriceMng
        //{
        //    get { return _price_mng; }
        //    set { _price_mng = value; }
        //}

        //private string _price_dop = "";
        //public string PriceDop
        //{
        //    get { return _price_dop; }
        //    set { _price_dop = value; }
        //}

        //private string _dop_rev = "";
        //public string DopRev
        //{
        //    get { return _dop_rev; }
        //    set { _dop_rev = value; }
        //}
        
    }
}
