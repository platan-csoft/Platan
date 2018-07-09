using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sprut
{
    public class clUser
    {
        
        private string _id;
        public string UserId
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _department_id;
        public string DepartmentId
        {
            get { return _department_id; }
            set { _department_id = value; }
        }

        private string _last_name;
        public string LastName
        {
            get { return _last_name; }
            set { _last_name = value; }
        }

        private string _first_name;
        public string FirstName
        {
            get { return _first_name; }
            set { _first_name = value; }
        }

        private string _middle_name;
        public string MiddleName
        {
            get { return _middle_name; }
            set { _middle_name = value; }
        }

        public string Fio
        {
            get
            {
                string fio = "";
                if (_last_name != "")
                    fio += _last_name;
                if ((_first_name != null) && (_first_name != "") && (_first_name.Length > 0))
                    fio += " " + _first_name[0] + '.';

                if ((_middle_name != null) && (_middle_name != "") && (_middle_name.Length > 0))
                    fio += " " + _middle_name[0] + '.';
                return fio;
            }
        }

        private double _resource;
        public double Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        private string _position;
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        

        private string _full_name;
        public string FullName
        {
            get { return _full_name; }
            set { 
                _full_name = value;
                string[] words = _full_name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length <= 0)
                {
                }

                if (words.Length > 0)
                {
                    if (words.Length == 4)
                    {
                        //Это Доктор Берндт
                        _last_name = words[0] + " " + words[1] + " ";
                        _first_name = words[2];
                        _middle_name = words[3];
                    }
                    else
                        if (words.Length > 0)
                        {
                            //Есть фамилия
                            _last_name = words[0];
                            if (words.Length > 1)
                            {
                                //Есть имя
                                _first_name = words[1];
                                if (words.Length > 2)
                                {
                                    //Есть отчество
                                    _middle_name = words[2];
                                }
                            }
                        }
                }

            }
        }

        private string _department_small;
        public string DepartmentSmall
        {
            get { return _department_small; }
            set { _department_small = value; }
        }

        private string _speciality;
        public string Speciality
        {
            get { return _speciality; }
            set { _speciality = value; }
        }

        private string _department_full;
        public string DepartmentFull
        {
            get { return _department_full; }
            set { _department_full = value; }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _permission = "executor";
        public string Permission
        {
            get { return _permission ; }
            set { _permission = value; }
        }   

        public clUser( )
        {
        }
    }
}
