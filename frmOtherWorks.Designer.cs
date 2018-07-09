namespace sprut
{
    partial class frmOtherWorks
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddOtherWork = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvStages = new System.Windows.Forms.DataGridView();
            this.tabControlSets = new System.Windows.Forms.TabControl();
            this.tabPageSets = new System.Windows.Forms.TabPage();
            this.dgvSets = new System.Windows.Forms.DataGridView();
            this.tabPageOtherWorks = new System.Windows.Forms.TabPage();
            this.dgvOtherWorks = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStages)).BeginInit();
            this.tabControlSets.SuspendLayout();
            this.tabPageSets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSets)).BeginInit();
            this.tabPageOtherWorks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherWorks)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.82981F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.17019F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(917, 493);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.btnAddOtherWork);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 455);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 35);
            this.panel1.TabIndex = 3;
            // 
            // btnAddOtherWork
            // 
            this.btnAddOtherWork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddOtherWork.Location = new System.Drawing.Point(670, 2);
            this.btnAddOtherWork.Name = "btnAddOtherWork";
            this.btnAddOtherWork.Size = new System.Drawing.Size(239, 31);
            this.btnAddOtherWork.TabIndex = 2;
            this.btnAddOtherWork.Text = "Добавить выбранный комплект";
            this.btnAddOtherWork.UseVisualStyleBackColor = true;
            this.btnAddOtherWork.Click += new System.EventHandler(this.btnAddOtherWork_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.SetColumnSpan(this.splitContainer1, 2);
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvStages);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlSets);
            this.splitContainer1.Size = new System.Drawing.Size(911, 446);
            this.splitContainer1.SplitterDistance = 390;
            this.splitContainer1.TabIndex = 5;
            // 
            // dgvStages
            // 
            this.dgvStages.AllowUserToAddRows = false;
            this.dgvStages.AllowUserToDeleteRows = false;
            this.dgvStages.AllowUserToResizeRows = false;
            this.dgvStages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStages.Location = new System.Drawing.Point(0, 0);
            this.dgvStages.Margin = new System.Windows.Forms.Padding(1);
            this.dgvStages.MultiSelect = false;
            this.dgvStages.Name = "dgvStages";
            this.dgvStages.ReadOnly = true;
            this.dgvStages.RowHeadersVisible = false;
            this.dgvStages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStages.Size = new System.Drawing.Size(390, 446);
            this.dgvStages.TabIndex = 0;
            this.dgvStages.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStages_CellClick);
            // 
            // tabControlSets
            // 
            this.tabControlSets.Controls.Add(this.tabPageSets);
            this.tabControlSets.Controls.Add(this.tabPageOtherWorks);
            this.tabControlSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSets.Location = new System.Drawing.Point(0, 0);
            this.tabControlSets.Margin = new System.Windows.Forms.Padding(1);
            this.tabControlSets.Name = "tabControlSets";
            this.tabControlSets.SelectedIndex = 0;
            this.tabControlSets.Size = new System.Drawing.Size(517, 446);
            this.tabControlSets.TabIndex = 1;
            // 
            // tabPageSets
            // 
            this.tabPageSets.Controls.Add(this.dgvSets);
            this.tabPageSets.Location = new System.Drawing.Point(4, 22);
            this.tabPageSets.Name = "tabPageSets";
            this.tabPageSets.Padding = new System.Windows.Forms.Padding(1);
            this.tabPageSets.Size = new System.Drawing.Size(509, 420);
            this.tabPageSets.TabIndex = 0;
            this.tabPageSets.Text = "Задачи";
            this.tabPageSets.UseVisualStyleBackColor = true;
            // 
            // dgvSets
            // 
            this.dgvSets.AllowUserToAddRows = false;
            this.dgvSets.AllowUserToDeleteRows = false;
            this.dgvSets.AllowUserToResizeRows = false;
            this.dgvSets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSets.Location = new System.Drawing.Point(1, 1);
            this.dgvSets.Margin = new System.Windows.Forms.Padding(1);
            this.dgvSets.MultiSelect = false;
            this.dgvSets.Name = "dgvSets";
            this.dgvSets.ReadOnly = true;
            this.dgvSets.RowHeadersVisible = false;
            this.dgvSets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSets.Size = new System.Drawing.Size(507, 418);
            this.dgvSets.TabIndex = 0;
            // 
            // tabPageOtherWorks
            // 
            this.tabPageOtherWorks.Controls.Add(this.dgvOtherWorks);
            this.tabPageOtherWorks.Location = new System.Drawing.Point(4, 22);
            this.tabPageOtherWorks.Name = "tabPageOtherWorks";
            this.tabPageOtherWorks.Padding = new System.Windows.Forms.Padding(1);
            this.tabPageOtherWorks.Size = new System.Drawing.Size(509, 420);
            this.tabPageOtherWorks.TabIndex = 1;
            this.tabPageOtherWorks.Text = "Другие работы";
            this.tabPageOtherWorks.UseVisualStyleBackColor = true;
            // 
            // dgvOtherWorks
            // 
            this.dgvOtherWorks.AllowUserToAddRows = false;
            this.dgvOtherWorks.AllowUserToDeleteRows = false;
            this.dgvOtherWorks.AllowUserToResizeRows = false;
            this.dgvOtherWorks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOtherWorks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOtherWorks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOtherWorks.Location = new System.Drawing.Point(1, 1);
            this.dgvOtherWorks.Margin = new System.Windows.Forms.Padding(1);
            this.dgvOtherWorks.MultiSelect = false;
            this.dgvOtherWorks.Name = "dgvOtherWorks";
            this.dgvOtherWorks.ReadOnly = true;
            this.dgvOtherWorks.RowHeadersVisible = false;
            this.dgvOtherWorks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOtherWorks.Size = new System.Drawing.Size(507, 418);
            this.dgvOtherWorks.TabIndex = 0;
            // 
            // frmOtherWorks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 493);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmOtherWorks";
            this.Text = "Другие работы и комплекты";
            this.Shown += new System.EventHandler(this.frmOtherWorks_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStages)).EndInit();
            this.tabControlSets.ResumeLayout(false);
            this.tabPageSets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSets)).EndInit();
            this.tabPageOtherWorks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherWorks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvStages;
        private System.Windows.Forms.TabControl tabControlSets;
        private System.Windows.Forms.TabPage tabPageSets;
        private System.Windows.Forms.DataGridView dgvSets;
        private System.Windows.Forms.TabPage tabPageOtherWorks;
        private System.Windows.Forms.DataGridView dgvOtherWorks;
        private System.Windows.Forms.Button btnAddOtherWork;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}