using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DgvFilterPopup;
using System.Data.SqlClient;

namespace sprut
{
    public partial class FrmShowPlTask : Form
    {
        
        public FrmShowPlTask()
        {
            InitializeComponent();
        }
        public void CreateSet(string project, string stage_name, string building, string set, string set_name, string gip_start, string gip_end, string set_start, string set_end, string agreed_status, string executors, string responsibleUser)
        {
            
            txt1.Text = project;
            txt2.Text = stage_name;
            txt3.Text = building;
            txt4.Text = set;
            txt5.Text = set_name;
            
            if (gip_start.Length > 10)
                gip_start = gip_start.Substring(0, 10);
            if (gip_end.Length > 10)
                gip_end = gip_end.Substring(0, 10);

            if (set_start.Length > 10)
                set_start = set_start.Substring(0, 10);
            if (set_end.Length > 10)
                set_end = set_end.Substring(0, 10);

            txt6.Text = gip_start;
            txt7.Text = gip_end;
            txt8.Text = set_start;
            txt9.Text = set_end;
            txt10.Text = agreed_status;
            txt11.Text = executors;
            txt12.Text = responsibleUser;

                  }
    }
}
