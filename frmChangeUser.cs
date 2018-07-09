using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DgvFilterPopup;

namespace sprut
{
    public partial class frmNoApproveSets : Form
    {
        public string SelectedUserLogin = "";
        public string SelectedUserLastName = "";
        public string SelectedUserFirstName = "";
        public string SelectedUserMiddleName = "";
        public string SelectedUserFIO = "";
        public string SelectedUserID = "";
        
        DgvFilterManager UsersFilterManager = null;
        DataTable _dtUsers = new DataTable("dtUsers");
        

        public frmNoApproveSets(List<clUser> users)
        {
            InitializeComponent();

            CreateDtUsers();
            CreateDgvUsers();
            FillDtUsers(users);

            dgvChangeDepartmentUser.DataSource = _dtUsers;

            UsersFilterManager = new DgvFilterManager(dgvChangeDepartmentUser);
        }

        private void CreateDtUsers()
        {
            try
            {
                if (_dtUsers == null)
                    _dtUsers = new DataTable("dtUsers");
                else
                {
                    _dtUsers.Rows.Clear();
                    _dtUsers.Columns.Clear();
                }
                _dtUsers.Columns.Add("FullName", typeof(string));
                _dtUsers.Columns.Add("LastName", typeof(string));
                _dtUsers.Columns.Add("FirstName", typeof(string));
                _dtUsers.Columns.Add("MiddleName", typeof(string));
                _dtUsers.Columns.Add("Fio", typeof(string));
                _dtUsers.Columns.Add("UserId", typeof(string));
                _dtUsers.Columns.Add("Position", typeof(string));
                _dtUsers.Columns.Add("Speciality", typeof(string));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void CreateDgvUsers()
        {
            if (dgvChangeDepartmentUser == null)
                return;

            dgvChangeDepartmentUser.AutoGenerateColumns = false;
            dgvChangeDepartmentUser.ColumnCount = 8;
            dgvChangeDepartmentUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvChangeDepartmentUser.Columns[0].Name = "FullName";
            dgvChangeDepartmentUser.Columns[0].DataPropertyName = "FullName";
            dgvChangeDepartmentUser.Columns[0].HeaderText = "Сотрудник";
            dgvChangeDepartmentUser.Columns[0].Visible = false;
            dgvChangeDepartmentUser.Columns[0].ReadOnly = true;

            dgvChangeDepartmentUser.Columns[1].Name = "LastName";
            dgvChangeDepartmentUser.Columns[1].DataPropertyName = "LastName";
            dgvChangeDepartmentUser.Columns[1].HeaderText = "LastName";
            dgvChangeDepartmentUser.Columns[1].Visible = false;
            dgvChangeDepartmentUser.Columns[1].ReadOnly = true;

            dgvChangeDepartmentUser.Columns[2].Name = "FirstName";
            dgvChangeDepartmentUser.Columns[2].DataPropertyName = "FirstName";
            dgvChangeDepartmentUser.Columns[2].HeaderText = "FirstName";
            dgvChangeDepartmentUser.Columns[2].Visible = false;
            dgvChangeDepartmentUser.Columns[2].ReadOnly = true;

            dgvChangeDepartmentUser.Columns[3].Name = "MiddleName";
            dgvChangeDepartmentUser.Columns[3].DataPropertyName = "MiddleName";
            dgvChangeDepartmentUser.Columns[3].HeaderText = "MiddleName";
            dgvChangeDepartmentUser.Columns[3].Visible = false;
            dgvChangeDepartmentUser.Columns[3].ReadOnly = true;

            dgvChangeDepartmentUser.Columns[4].Name = "Fio";
            dgvChangeDepartmentUser.Columns[4].DataPropertyName = "Fio";
            dgvChangeDepartmentUser.Columns[4].HeaderText = "ФИО";
            dgvChangeDepartmentUser.Columns[4].Visible = true;
            dgvChangeDepartmentUser.Columns[4].ReadOnly = true;

            dgvChangeDepartmentUser.Columns[5].Name = "UserId";
            dgvChangeDepartmentUser.Columns[5].DataPropertyName = "UserId";
            dgvChangeDepartmentUser.Columns[5].HeaderText = "Userid";
            dgvChangeDepartmentUser.Columns[5].Visible = false;
            dgvChangeDepartmentUser.Columns[5].ReadOnly = true;

            dgvChangeDepartmentUser.Columns[6].Name = "Position";
            dgvChangeDepartmentUser.Columns[6].DataPropertyName = "Position";
            dgvChangeDepartmentUser.Columns[6].HeaderText = "Должность";
            dgvChangeDepartmentUser.Columns[6].Visible = true;
            dgvChangeDepartmentUser.Columns[6].ReadOnly = true;


            dgvChangeDepartmentUser.Columns[7].Name = "Speciality";
            dgvChangeDepartmentUser.Columns[7].DataPropertyName = "Speciality";
            dgvChangeDepartmentUser.Columns[7].HeaderText = "Площадка";
            dgvChangeDepartmentUser.Columns[7].Visible = true;
            dgvChangeDepartmentUser.Columns[7].ReadOnly = true;
        }

        private void FillDtUsers(List<clUser> users)
        {
            if (_dtUsers == null)
                return;

            _dtUsers.Rows.Clear();

            foreach(var item in users)
            {
                DataRow dr = _dtUsers.NewRow();

                dr["FullName"] = item.FullName;
                dr["LastName"] = item.LastName;
                dr["FirstName"] = item.FirstName;
                dr["MiddleName"] = item.MiddleName;
                dr["Fio"] = item.Fio;
                dr["UserId"] = item.UserId;
                dr["Position"] = item.Position;
                dr["Speciality"] = item.Speciality;

                _dtUsers.Rows.Add(dr);
            }
        }

        private void btnChangeUser_Click(object sender, EventArgs e)
        {
            if(dgvChangeDepartmentUser.CurrentRow != null)
            {
                this.Cursor = Cursors.WaitCursor;
                GetNewExecutorInfo();
            }
            this.Close();
        }

        private void dgvChangeDepartmentUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChangeDepartmentUser.CurrentRow != null)
            {
                GetNewExecutorInfo();
            }
            this.Close();
        }

        private void GetNewExecutorInfo()
        {
            int row = dgvChangeDepartmentUser.CurrentRow.Index;
            SelectedUserLastName = dgvChangeDepartmentUser.Rows[row].Cells["LastName"].Value.ToString();
            SelectedUserFirstName = dgvChangeDepartmentUser.Rows[row].Cells["FirstName"].Value.ToString();
            SelectedUserMiddleName = dgvChangeDepartmentUser.Rows[row].Cells["MiddleName"].Value.ToString();
            SelectedUserFIO = dgvChangeDepartmentUser.Rows[row].Cells["Fio"].Value.ToString();
            SelectedUserID = dgvChangeDepartmentUser.Rows[row].Cells["UserId"].Value.ToString();
        }

        private void frmNoApproveSets_Load(object sender, EventArgs e)
        {

        }

    }
}
