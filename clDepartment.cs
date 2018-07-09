using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sprut
{
    public class clDepartment
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        private string _small_name;
        public string SmallName
        {
            get { return _small_name; }
            set { _small_name = value; }
        }

        private string _full_name;
        public string FullName
        {
            get { return _full_name; }
            set { _full_name = value; }
        }
        
        public clDepartment()
        {
                
        }
    }
}
