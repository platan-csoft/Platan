using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sprut
{
    public class clRequest
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
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

        //private string _kks;
        //public string KKS
        //{
        //    get { return _kks; }
        //    set { _kks = value; }
        //}

        private string _building;
        public string Building
        {
            get { return _building; }
            set { _building = value; }
        }    

        private string _set_code;
        public string SetCode
        {
            get { return _set_code; }
            set { _set_code = value; }
        }

        private string _set_name;
        public string SetName
        {
            get { return _set_name; }
            set { _set_name = value; }
        }        

        private string _user_id;
        public string UserId
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        private string _fio;
        public string Fio
        {
            get { return _fio; }
            set { _fio = value; }
        }

        private string _department_small_name;
        public string DepartmentSmallName
        {
            get { return _department_small_name; }
            set { _department_small_name = value; }
        }

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

        private DateTime _start_date;
        public DateTime StartDate 
        {
            get { return _start_date; }
            set { _start_date = value; }
        }

        private DateTime _end_date;
        public DateTime EndDate
        {
            get { return _end_date; }
            set { _end_date = value; }
        }

        private DateTime _request_date;
        public DateTime RequestDate
        {
            get { return _request_date; }
            set { _request_date = value; }
        }

        private string _reason;
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        private string _comment_request;
        public string CommentRequest
        {
            get { return _comment_request; }
            set { _comment_request = value; }
        }

        private string _dept_id;
        public string DepartmentId
        {
            get { return _dept_id; }
            set { _dept_id = value; }
        }

        private string _comment_response;
        public string CommentResponse
        {
            get { return _comment_response; }
            set { _comment_response = value; }
        }

        private string _agreed_status;
        public string AgreedStatus
        {
            get { return _agreed_status; }
            set { _agreed_status = value; }
        }

        public string VisibleAgreedStatus 
        {
            get 
            {
                string result = "unknown";

                switch(_agreed_status)
                {
                    case "agree":
                        {
                            result = "Согласован";
                            break;
                        }
                    case "disagree":
                        {
                            result = "Отклонен";
                            break;
                        }
                    case "request":
                        {
                            result = "Запрос";
                            break;
                        }
                    default :
                        {
                            break;
                        }
                }

                return result;
            }
        }

        private int _transfer_to_prj;
        public int TransferToPrj
        {
            get { return _transfer_to_prj; }
            set { _transfer_to_prj = value; }
        }

        private string _btn_agree = "Согласовать";
        public string BtnAgree
        {
            get { return _btn_agree; }
            set { _btn_agree = value;}
        }

        private string _btn_disagree = "Отклонить";
        public string BtnDisAgree
        {
            get { return _btn_disagree; }
        }

        private string _btn_transfer_to_project = "Для группы планирования (перенесено в Project)";
        public string BtnTransferToProject
        {
            get { return _btn_transfer_to_project; }
        }
        
        public clRequest()
        {
              
        }
    }
}
