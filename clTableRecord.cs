using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sprut
{
    public class clTableRecord
    {
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

        private string _executor_id;
        public string ExecutorId
        {
            get { return _executor_id; }
            set { _executor_id = value; }
        }

        private DateTime _work_date;
        public DateTime WorkDate
        {
            get { return _work_date; }
            set { _work_date = value; }
        }

        private Double _work_plan;
        public Double WorkPlan
        {
            get { return _work_plan; }
            set { _work_plan = value; }
        }

        private Double _work_fact;
        public Double WorkFact
        {
            get { return _work_fact; }
            set { _work_fact = value; }
        }

        private Double _manually_input;
        public Double ManuallyInput
        {
            get { return _manually_input; }
            set { _manually_input = value; }
        }

        private string _dept_id;
        public string DeptId
        {
            get { return _dept_id; }
            set { _dept_id = value; }
        }

        private DateTime _modify_date;
        public DateTime ModifyDate
        {
            get { return _modify_date; }
            set { _modify_date = value; }
        }

        private string _modify_user_id;
        public string ModifyUserId
        {
            get { return _modify_user_id; }
            set { _modify_user_id = value; }
        }

        private DateTime _create_date;
        public DateTime CreateDate
        {
            get { return _create_date; }
            set { _create_date = value; }
        }

        private string _create_user_id;
        public string CreateUserId
        {
            get { return _create_user_id; }
            set { _create_user_id = value; }
        }
        
    }
}
