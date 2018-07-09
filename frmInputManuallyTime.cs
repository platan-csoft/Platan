using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sprut
{
    public partial class frmInputManuallyTime : Form
    {
        public string InputManualPlanTime;

        public frmInputManuallyTime()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            InputManualPlanTime = txtBoxInputPlanValue.Text;
            ValidateInputNumber(ref InputManualPlanTime);
            this.Close();
        }

        private void ValidateInputNumber(ref string InputManualPlanTime)
        {
            InputManualPlanTime = InputManualPlanTime.Replace(" ", string.Empty);
            InputManualPlanTime = InputManualPlanTime.Replace('.', ',');
            double input = 0;
            if (Double.TryParse(InputManualPlanTime, out input))
            {
                InputManualPlanTime = input.ToString();
            }
            else
            {
                InputManualPlanTime = "null";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            InputManualPlanTime = "null";
            this.Close();
        }

        private void txtBoxInputPlanValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(sender, (EventArgs)e);
            }
        }
    }
}
