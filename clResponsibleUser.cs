using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sprut
{
    public class clResponsibleUser
    {
        //private string _id = "0";
        //public string Id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        private string _last_name = "";
        public string LastName
        {
            get { return _last_name; }
            set { _last_name = value; }
        }

        private string _first_name = "";
        public string FirstName
        {
            get { return _first_name; }
            set { _first_name = value; }
        }

        private string _middle_name = "";
        public string MiddleName
        {
            get { return _middle_name; }
            set { _middle_name = value; }
        }

        private string _full_name = "";
        public string FullName
        {
            get { return _full_name; }
            set { _full_name = value; }
                
                //string[] words = _full_name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //if (words.Length <= 0)
                //{
                //}

                //if (words.Length > 0)
                //{
                //    if (words.Length == 4)
                //    {
                //        //Это Доктор Берндт
                //        _last_name = words[0] + " " + words[1] + " ";
                //        _first_name = words[2];
                //        _middle_name = words[3];
                //    }
                //    else
                //        if (words.Length > 0)
                //        {
                //            //Есть фамилия
                //            _last_name = words[0];
                //            if (words.Length > 1)
                //            {
                //                //Есть имя
                //                _first_name = words[1];
                //                if (words.Length > 2)
                //                {
                //                    //Есть отчество
                //                    _middle_name = words[2];
                //                }
                //            }
                //        }
                //}

            
        }

        public string Fio
        {
            get
            {
                string fio = "";
            //if (_last_name != "")
            //    fio += _last_name;
            //if ((_first_name != null) && (_first_name != "") && (_first_name.Length > 0))
            //    fio += " " + _first_name[0] + '.';

            //if ((_middle_name != null) && (_middle_name != "") && (_middle_name.Length > 0))
            //    fio += " " + _middle_name[0] + '.';
            fio = _full_name;
                return fio;
            }
        }
        
    }
}
