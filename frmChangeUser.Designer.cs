namespace sprut
{
    partial class frmNoApproveSets
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvChangeDepartmentUser = new System.Windows.Forms.DataGridView();
            this.btnChangeUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChangeDepartmentUser)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvChangeDepartmentUser
            // 
            this.dgvChangeDepartmentUser.AllowUserToAddRows = false;
            this.dgvChangeDepartmentUser.AllowUserToDeleteRows = false;
            this.dgvChangeDepartmentUser.AllowUserToResizeRows = false;
            this.dgvChangeDepartmentUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChangeDepartmentUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChangeDepartmentUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChangeDepartmentUser.Location = new System.Drawing.Point(1, 1);
            this.dgvChangeDepartmentUser.MultiSelect = false;
            this.dgvChangeDepartmentUser.Name = "dgvChangeDepartmentUser";
            this.dgvChangeDepartmentUser.ReadOnly = true;
            this.dgvChangeDepartmentUser.RowHeadersVisible = false;
            this.dgvChangeDepartmentUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChangeDepartmentUser.Size = new System.Drawing.Size(456, 347);
            this.dgvChangeDepartmentUser.TabIndex = 0;
            this.dgvChangeDepartmentUser.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChangeDepartmentUser_CellDoubleClick);
            // 
            // btnChangeUser
            // 
            this.btnChangeUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeUser.Location = new System.Drawing.Point(351, 354);
            this.btnChangeUser.Name = "btnChangeUser";
            this.btnChangeUser.Size = new System.Drawing.Size(106, 23);
            this.btnChangeUser.TabIndex = 1;
            this.btnChangeUser.Text = "Выбрать сотрудника";
            this.btnChangeUser.UseVisualStyleBackColor = true;
            this.btnChangeUser.Click += new System.EventHandler(this.btnChangeUser_Click);
            // 
            // frmNoApproveSets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 379);
            this.Controls.Add(this.btnChangeUser);
            this.Controls.Add(this.dgvChangeDepartmentUser);
            this.Name = "frmNoApproveSets";
            this.Text = "Сотрудники отдела";
            this.Load += new System.EventHandler(this.frmNoApproveSets_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChangeDepartmentUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvChangeDepartmentUser;
        private System.Windows.Forms.Button btnChangeUser;

    }
}