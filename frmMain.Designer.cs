namespace sprut
{
    public partial class frmMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.tsmi_Reports = new System.Windows.Forms.ToolStripMenuItem();
            this.ОтчетТабельпоэтапуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетРасчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетПоТрудозатратамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtpApprovedPlan = new System.Windows.Forms.TabControl();
            this.tabPageDepartmentPlan = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelMainTop = new System.Windows.Forms.Panel();
            this.panelDepartments = new System.Windows.Forms.Panel();
            this.cmbxDepartmentPlan_Departments = new System.Windows.Forms.ComboBox();
            this.labelDepartment = new System.Windows.Forms.Label();
            this.picbxRefreshDepartmentPlan = new System.Windows.Forms.PictureBox();
            this.panelDepartmentPlanCalendar = new System.Windows.Forms.Panel();
            this.dtpDepartmentPlan = new System.Windows.Forms.DateTimePicker();
            this.btnDepartmentPlanInc = new System.Windows.Forms.Button();
            this.btnDepartmentPlanDec = new System.Windows.Forms.Button();
            this.cmbxStages = new System.Windows.Forms.ComboBox();
            this.splitContainerMainHor = new System.Windows.Forms.SplitContainer();
            this.splitContainerMainVert = new System.Windows.Forms.SplitContainer();
            this.dgvDepartmentPlan = new System.Windows.Forms.DataGridView();
            this.dgvDepartmentUsers = new System.Windows.Forms.DataGridView();
            this.tabControlSelectedPlanAndAgreement = new System.Windows.Forms.TabControl();
            this.tabPageSelectedPlan = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelSelectedPlan = new System.Windows.Forms.TableLayoutPanel();
            this.dgvSelectedPlan = new System.Windows.Forms.DataGridView();
            this.panelSelectedPlan = new System.Windows.Forms.Panel();
            this.btnAgreeSetPlaned = new System.Windows.Forms.Button();
            this.btnDepartmentPlan_Save = new System.Windows.Forms.Button();
            this.btnDepartmentPlan_AddSet = new System.Windows.Forms.Button();
            this.tabPageIndividualPlan = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelIndividualPlan = new System.Windows.Forms.TableLayoutPanel();
            this.panelindividualPlanTop = new System.Windows.Forms.Panel();
            this.picbxRefreshIndividualPlan = new System.Windows.Forms.PictureBox();
            this.panelIndividualPlanDatePicker = new System.Windows.Forms.Panel();
            this.dtpIndividualPlan = new System.Windows.Forms.DateTimePicker();
            this.btnIndividualPlanInc = new System.Windows.Forms.Button();
            this.btnIndividualPlanDec = new System.Windows.Forms.Button();
            this.panelIndividualPlanUser = new System.Windows.Forms.Panel();
            this.labelSelectedUser = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.panelIndividualPlanBottom = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnIndividualPlanAddSet = new System.Windows.Forms.Button();
            this.btnIndividualPlanSave = new System.Windows.Forms.Button();
            this.dgvIndividualPlan = new DoubleBufferedDGV.CustomDataGridView();
            this.tabPageAgreement = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel_Agreement = new System.Windows.Forms.TableLayoutPanel();
            this.dgvAgreement = new System.Windows.Forms.DataGridView();
            this.panelAgreementTop = new System.Windows.Forms.Panel();
            this.picbxRefreshAgreement = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbxAgreement_Departments = new System.Windows.Forms.ComboBox();
            this.labelAgreementDepartment = new System.Windows.Forms.Label();
            this.panelAgreementBottom = new System.Windows.Forms.Panel();
            this.tabPageProductionPlanDept = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.dgvProductionPlanDept = new System.Windows.Forms.DataGridView();
            this.panelApprovedTop = new System.Windows.Forms.Panel();
            this.dtpProductionPlanDept = new System.Windows.Forms.DateTimePicker();
            this.btnProductionPlanDeptInc = new System.Windows.Forms.Button();
            this.btnProductionPlanDeptIncDec = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPageProductionPlan = new System.Windows.Forms.TabPage();
            this.tlp_pp_main = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dtpProductionPlan = new System.Windows.Forms.DateTimePicker();
            this.btnProductionPlanMonthInc = new System.Windows.Forms.Button();
            this.btnProductionPlanMonthDec = new System.Windows.Forms.Button();
            this.dgvDepartments = new System.Windows.Forms.DataGridView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.butPrice = new System.Windows.Forms.Button();
            this.btnProductionPlan_Reject = new System.Windows.Forms.Button();
            this.btnProductionPlan_Approve = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvProductionPlan = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dgvIndividualPlan_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_IndividualPlan_DeleteTableRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvIndividualPlan_Menu_SaveToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvSelectedPlan_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dgvSelectedPlan_Menu_AutoPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvSelectedPlan_Menu_ManuallyPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvSelectedPlan_Menu_DeleteSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvSelectedPlan_Menu_DeleteExecutor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvSelectedPlan_Menu_SaveToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDepartmentPlan_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dgvDepartmentPlan_Menu_ShowSet = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDepartmentPlan_Menu_ShowSetHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDepartmentPlan_Menu_Request = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDepartmentPlan_Menu_DeleteRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDepartmentPlan_Menu_Agree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvDepartmentPlan_Menu_SaveToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDepartmentUsers_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dgvDepartmentUsers_Menu_SaveToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.dtpApprovedPlan.SuspendLayout();
            this.tabPageDepartmentPlan.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelMainTop.SuspendLayout();
            this.panelDepartments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxRefreshDepartmentPlan)).BeginInit();
            this.panelDepartmentPlanCalendar.SuspendLayout();
            this.splitContainerMainHor.Panel1.SuspendLayout();
            this.splitContainerMainHor.Panel2.SuspendLayout();
            this.splitContainerMainHor.SuspendLayout();
            this.splitContainerMainVert.Panel1.SuspendLayout();
            this.splitContainerMainVert.Panel2.SuspendLayout();
            this.splitContainerMainVert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartmentPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartmentUsers)).BeginInit();
            this.tabControlSelectedPlanAndAgreement.SuspendLayout();
            this.tabPageSelectedPlan.SuspendLayout();
            this.tableLayoutPanelSelectedPlan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedPlan)).BeginInit();
            this.panelSelectedPlan.SuspendLayout();
            this.tabPageIndividualPlan.SuspendLayout();
            this.tableLayoutPanelIndividualPlan.SuspendLayout();
            this.panelindividualPlanTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxRefreshIndividualPlan)).BeginInit();
            this.panelIndividualPlanDatePicker.SuspendLayout();
            this.panelIndividualPlanUser.SuspendLayout();
            this.panelIndividualPlanBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndividualPlan)).BeginInit();
            this.tabPageAgreement.SuspendLayout();
            this.tableLayoutPanel_Agreement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgreement)).BeginInit();
            this.panelAgreementTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxRefreshAgreement)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPageProductionPlanDept.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionPlanDept)).BeginInit();
            this.panelApprovedTop.SuspendLayout();
            this.tabPageProductionPlan.SuspendLayout();
            this.tlp_pp_main.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).BeginInit();
            this.panel7.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionPlan)).BeginInit();
            this.dgvIndividualPlan_Menu.SuspendLayout();
            this.dgvSelectedPlan_Menu.SuspendLayout();
            this.dgvDepartmentPlan_Menu.SuspendLayout();
            this.dgvDepartmentUsers_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Reports});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1091, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // tsmi_Reports
            // 
            this.tsmi_Reports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ОтчетТабельпоэтапуToolStripMenuItem,
            this.отчетРасчетToolStripMenuItem,
            this.отчетПоТрудозатратамToolStripMenuItem});
            this.tsmi_Reports.Name = "tsmi_Reports";
            this.tsmi_Reports.Size = new System.Drawing.Size(60, 20);
            this.tsmi_Reports.Text = "Отчеты";
            // 
            // ОтчетТабельпоэтапуToolStripMenuItem
            // 
            this.ОтчетТабельпоэтапуToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ОтчетТабельпоэтапуToolStripMenuItem.Image")));
            this.ОтчетТабельпоэтапуToolStripMenuItem.Name = "ОтчетТабельпоэтапуToolStripMenuItem";
            this.ОтчетТабельпоэтапуToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ОтчетТабельпоэтапуToolStripMenuItem.Text = "Отчет табель по этапу";
            this.ОтчетТабельпоэтапуToolStripMenuItem.Click += new System.EventHandler(this.отчетПоПланированиюToolStripMenuItem_Click);
            // 
            // отчетРасчетToolStripMenuItem
            // 
            this.отчетРасчетToolStripMenuItem.Image = global::platan.Properties.Resources.excel16;
            this.отчетРасчетToolStripMenuItem.Name = "отчетРасчетToolStripMenuItem";
            this.отчетРасчетToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.отчетРасчетToolStripMenuItem.Text = "Отчет Расчет";
            this.отчетРасчетToolStripMenuItem.Click += new System.EventHandler(this.отчетДляБухгалтерииToolStripMenuItem_Click);
            // 
            // отчетПоТрудозатратамToolStripMenuItem
            // 
            this.отчетПоТрудозатратамToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("отчетПоТрудозатратамToolStripMenuItem.Image")));
            this.отчетПоТрудозатратамToolStripMenuItem.Name = "отчетПоТрудозатратамToolStripMenuItem";
            this.отчетПоТрудозатратамToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.отчетПоТрудозатратамToolStripMenuItem.Text = "Отчет по трудозатратам";
            this.отчетПоТрудозатратамToolStripMenuItem.Click += new System.EventHandler(this.отчетПоПроизводственномуПлануToolStripMenuItem_Click);
            // 
            // dtpApprovedPlan
            // 
            this.dtpApprovedPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpApprovedPlan.Controls.Add(this.tabPageDepartmentPlan);
            this.dtpApprovedPlan.Controls.Add(this.tabPageIndividualPlan);
            this.dtpApprovedPlan.Controls.Add(this.tabPageAgreement);
            this.dtpApprovedPlan.Controls.Add(this.tabPageProductionPlanDept);
            this.dtpApprovedPlan.Controls.Add(this.tabPageProductionPlan);
            this.dtpApprovedPlan.Location = new System.Drawing.Point(0, 27);
            this.dtpApprovedPlan.Name = "dtpApprovedPlan";
            this.dtpApprovedPlan.Padding = new System.Drawing.Point(3, 1);
            this.dtpApprovedPlan.SelectedIndex = 0;
            this.dtpApprovedPlan.Size = new System.Drawing.Size(1091, 466);
            this.dtpApprovedPlan.TabIndex = 1;
            // 
            // tabPageDepartmentPlan
            // 
            this.tabPageDepartmentPlan.Controls.Add(this.tableLayoutPanelMain);
            this.tabPageDepartmentPlan.Location = new System.Drawing.Point(4, 20);
            this.tabPageDepartmentPlan.Name = "tabPageDepartmentPlan";
            this.tabPageDepartmentPlan.Padding = new System.Windows.Forms.Padding(1);
            this.tabPageDepartmentPlan.Size = new System.Drawing.Size(1083, 442);
            this.tabPageDepartmentPlan.TabIndex = 0;
            this.tabPageDepartmentPlan.Text = "Задачи отдела";
            this.tabPageDepartmentPlan.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.panelMainTop, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.splitContainerMainHor, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1081, 440);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelMainTop
            // 
            this.panelMainTop.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelMainTop.Controls.Add(this.panelDepartments);
            this.panelMainTop.Controls.Add(this.picbxRefreshDepartmentPlan);
            this.panelMainTop.Controls.Add(this.panelDepartmentPlanCalendar);
            this.panelMainTop.Controls.Add(this.cmbxStages);
            this.panelMainTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainTop.Location = new System.Drawing.Point(0, 0);
            this.panelMainTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelMainTop.Name = "panelMainTop";
            this.panelMainTop.Size = new System.Drawing.Size(1081, 36);
            this.panelMainTop.TabIndex = 0;
            // 
            // panelDepartments
            // 
            this.panelDepartments.Controls.Add(this.cmbxDepartmentPlan_Departments);
            this.panelDepartments.Controls.Add(this.labelDepartment);
            this.panelDepartments.Location = new System.Drawing.Point(606, 3);
            this.panelDepartments.Name = "panelDepartments";
            this.panelDepartments.Size = new System.Drawing.Size(371, 28);
            this.panelDepartments.TabIndex = 3;
            // 
            // cmbxDepartmentPlan_Departments
            // 
            this.cmbxDepartmentPlan_Departments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxDepartmentPlan_Departments.DropDownWidth = 350;
            this.cmbxDepartmentPlan_Departments.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbxDepartmentPlan_Departments.FormattingEnabled = true;
            this.cmbxDepartmentPlan_Departments.Location = new System.Drawing.Point(46, 3);
            this.cmbxDepartmentPlan_Departments.Name = "cmbxDepartmentPlan_Departments";
            this.cmbxDepartmentPlan_Departments.Size = new System.Drawing.Size(322, 21);
            this.cmbxDepartmentPlan_Departments.TabIndex = 1;
            this.cmbxDepartmentPlan_Departments.SelectedValueChanged += new System.EventHandler(this.cmbxDepartments_SelectedValueChanged);
            this.cmbxDepartmentPlan_Departments.MouseHover += new System.EventHandler(this.cmbxDepartmentPlan_Departments_MouseHover);
            // 
            // labelDepartment
            // 
            this.labelDepartment.AutoSize = true;
            this.labelDepartment.Location = new System.Drawing.Point(4, 8);
            this.labelDepartment.Name = "labelDepartment";
            this.labelDepartment.Size = new System.Drawing.Size(38, 13);
            this.labelDepartment.TabIndex = 0;
            this.labelDepartment.Text = "Отдел";
            // 
            // picbxRefreshDepartmentPlan
            // 
            this.picbxRefreshDepartmentPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picbxRefreshDepartmentPlan.BackColor = System.Drawing.Color.LightSteelBlue;
            this.picbxRefreshDepartmentPlan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picbxRefreshDepartmentPlan.BackgroundImage")));
            this.picbxRefreshDepartmentPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picbxRefreshDepartmentPlan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picbxRefreshDepartmentPlan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picbxRefreshDepartmentPlan.Location = new System.Drawing.Point(1046, 4);
            this.picbxRefreshDepartmentPlan.Name = "picbxRefreshDepartmentPlan";
            this.picbxRefreshDepartmentPlan.Size = new System.Drawing.Size(28, 28);
            this.picbxRefreshDepartmentPlan.TabIndex = 2;
            this.picbxRefreshDepartmentPlan.TabStop = false;
            this.picbxRefreshDepartmentPlan.Click += new System.EventHandler(this.picbxRefreshDepartmentPlan_Click);
            this.picbxRefreshDepartmentPlan.MouseEnter += new System.EventHandler(this.picbxRefreshDepartmentPlan_MouseEnter);
            this.picbxRefreshDepartmentPlan.MouseLeave += new System.EventHandler(this.picbxRefreshDepartmentPlan_MouseLeave);
            // 
            // panelDepartmentPlanCalendar
            // 
            this.panelDepartmentPlanCalendar.Controls.Add(this.dtpDepartmentPlan);
            this.panelDepartmentPlanCalendar.Controls.Add(this.btnDepartmentPlanInc);
            this.panelDepartmentPlanCalendar.Controls.Add(this.btnDepartmentPlanDec);
            this.panelDepartmentPlanCalendar.Location = new System.Drawing.Point(382, 3);
            this.panelDepartmentPlanCalendar.Name = "panelDepartmentPlanCalendar";
            this.panelDepartmentPlanCalendar.Size = new System.Drawing.Size(220, 28);
            this.panelDepartmentPlanCalendar.TabIndex = 1;
            // 
            // dtpDepartmentPlan
            // 
            this.dtpDepartmentPlan.CustomFormat = "MMMM yyyy";
            this.dtpDepartmentPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpDepartmentPlan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDepartmentPlan.Location = new System.Drawing.Point(29, 1);
            this.dtpDepartmentPlan.Name = "dtpDepartmentPlan";
            this.dtpDepartmentPlan.Size = new System.Drawing.Size(163, 26);
            this.dtpDepartmentPlan.TabIndex = 2;
            this.dtpDepartmentPlan.CloseUp += new System.EventHandler(this.dtpDepartmentPlan_CloseUp);
            // 
            // btnDepartmentPlanInc
            // 
            this.btnDepartmentPlanInc.Location = new System.Drawing.Point(191, 0);
            this.btnDepartmentPlanInc.Name = "btnDepartmentPlanInc";
            this.btnDepartmentPlanInc.Size = new System.Drawing.Size(28, 28);
            this.btnDepartmentPlanInc.TabIndex = 1;
            this.btnDepartmentPlanInc.Text = ">";
            this.btnDepartmentPlanInc.UseVisualStyleBackColor = true;
            this.btnDepartmentPlanInc.Click += new System.EventHandler(this.btnDepartmentPlanInc_Click);
            // 
            // btnDepartmentPlanDec
            // 
            this.btnDepartmentPlanDec.Location = new System.Drawing.Point(1, 0);
            this.btnDepartmentPlanDec.Name = "btnDepartmentPlanDec";
            this.btnDepartmentPlanDec.Size = new System.Drawing.Size(28, 28);
            this.btnDepartmentPlanDec.TabIndex = 0;
            this.btnDepartmentPlanDec.Text = "<";
            this.btnDepartmentPlanDec.UseVisualStyleBackColor = true;
            this.btnDepartmentPlanDec.Click += new System.EventHandler(this.btnDepartmentPlanDec_Click);
            // 
            // cmbxStages
            // 
            this.cmbxStages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxStages.Enabled = false;
            this.cmbxStages.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbxStages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbxStages.FormattingEnabled = true;
            this.cmbxStages.Location = new System.Drawing.Point(3, 3);
            this.cmbxStages.Name = "cmbxStages";
            this.cmbxStages.Size = new System.Drawing.Size(367, 28);
            this.cmbxStages.TabIndex = 0;
            this.cmbxStages.Visible = false;
            // 
            // splitContainerMainHor
            // 
            this.splitContainerMainHor.BackColor = System.Drawing.Color.LightSteelBlue;
            this.splitContainerMainHor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMainHor.Location = new System.Drawing.Point(0, 36);
            this.splitContainerMainHor.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerMainHor.Name = "splitContainerMainHor";
            this.splitContainerMainHor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMainHor.Panel1
            // 
            this.splitContainerMainHor.Panel1.Controls.Add(this.splitContainerMainVert);
            // 
            // splitContainerMainHor.Panel2
            // 
            this.splitContainerMainHor.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainerMainHor.Panel2.Controls.Add(this.tabControlSelectedPlanAndAgreement);
            this.splitContainerMainHor.Size = new System.Drawing.Size(1081, 404);
            this.splitContainerMainHor.SplitterDistance = 229;
            this.splitContainerMainHor.SplitterWidth = 5;
            this.splitContainerMainHor.TabIndex = 2;
            // 
            // splitContainerMainVert
            // 
            this.splitContainerMainVert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMainVert.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMainVert.Name = "splitContainerMainVert";
            // 
            // splitContainerMainVert.Panel1
            // 
            this.splitContainerMainVert.Panel1.Controls.Add(this.dgvDepartmentPlan);
            // 
            // splitContainerMainVert.Panel2
            // 
            this.splitContainerMainVert.Panel2.Controls.Add(this.dgvDepartmentUsers);
            this.splitContainerMainVert.Size = new System.Drawing.Size(1081, 229);
            this.splitContainerMainVert.SplitterDistance = 829;
            this.splitContainerMainVert.TabIndex = 0;
            // 
            // dgvDepartmentPlan
            // 
            this.dgvDepartmentPlan.AllowDrop = true;
            this.dgvDepartmentPlan.AllowUserToAddRows = false;
            this.dgvDepartmentPlan.AllowUserToDeleteRows = false;
            this.dgvDepartmentPlan.AllowUserToResizeRows = false;
            this.dgvDepartmentPlan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepartmentPlan.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartmentPlan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDepartmentPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDepartmentPlan.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDepartmentPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDepartmentPlan.Location = new System.Drawing.Point(0, 0);
            this.dgvDepartmentPlan.MultiSelect = false;
            this.dgvDepartmentPlan.Name = "dgvDepartmentPlan";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartmentPlan.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDepartmentPlan.RowHeadersVisible = false;
            this.dgvDepartmentPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartmentPlan.Size = new System.Drawing.Size(829, 229);
            this.dgvDepartmentPlan.TabIndex = 0;
            this.dgvDepartmentPlan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartmentPlan_CellClick);
            this.dgvDepartmentPlan.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartmentPlan_CellEndEdit);
            this.dgvDepartmentPlan.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartmentPlan_CellMouseEnter);
            this.dgvDepartmentPlan.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDepartmentPlan_CellPainting);
            this.dgvDepartmentPlan.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvDepartmentPlan_RowPrePaint);
            this.dgvDepartmentPlan.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvDepartmentPlan_DragDrop);
            this.dgvDepartmentPlan.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgvDepartmentPlan_DragEnter);
            this.dgvDepartmentPlan.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvDepartmentPlan_DragOver);
            this.dgvDepartmentPlan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvDepartmentPlan_MouseClick);
            // 
            // dgvDepartmentUsers
            // 
            this.dgvDepartmentUsers.AllowUserToAddRows = false;
            this.dgvDepartmentUsers.AllowUserToDeleteRows = false;
            this.dgvDepartmentUsers.AllowUserToResizeRows = false;
            this.dgvDepartmentUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepartmentUsers.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartmentUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDepartmentUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDepartmentUsers.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDepartmentUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDepartmentUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvDepartmentUsers.Name = "dgvDepartmentUsers";
            this.dgvDepartmentUsers.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartmentUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDepartmentUsers.RowHeadersVisible = false;
            this.dgvDepartmentUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartmentUsers.Size = new System.Drawing.Size(248, 229);
            this.dgvDepartmentUsers.TabIndex = 0;
            this.dgvDepartmentUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartmentUsers_CellClick);
            this.dgvDepartmentUsers.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDepartmentUsers_CellPainting);
            this.dgvDepartmentUsers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvDepartmentUsers_MouseClick);
            this.dgvDepartmentUsers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvDepartmentUsers_MouseDown);
            this.dgvDepartmentUsers.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvDepartmentUsers_MouseMove);
            this.dgvDepartmentUsers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvDepartmentUsers_MouseUp);
            // 
            // tabControlSelectedPlanAndAgreement
            // 
            this.tabControlSelectedPlanAndAgreement.Controls.Add(this.tabPageSelectedPlan);
            this.tabControlSelectedPlanAndAgreement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSelectedPlanAndAgreement.Location = new System.Drawing.Point(0, 0);
            this.tabControlSelectedPlanAndAgreement.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlSelectedPlanAndAgreement.Name = "tabControlSelectedPlanAndAgreement";
            this.tabControlSelectedPlanAndAgreement.Padding = new System.Drawing.Point(3, 1);
            this.tabControlSelectedPlanAndAgreement.SelectedIndex = 0;
            this.tabControlSelectedPlanAndAgreement.Size = new System.Drawing.Size(1081, 170);
            this.tabControlSelectedPlanAndAgreement.TabIndex = 0;
            // 
            // tabPageSelectedPlan
            // 
            this.tabPageSelectedPlan.Controls.Add(this.tableLayoutPanelSelectedPlan);
            this.tabPageSelectedPlan.Location = new System.Drawing.Point(4, 20);
            this.tabPageSelectedPlan.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageSelectedPlan.Name = "tabPageSelectedPlan";
            this.tabPageSelectedPlan.Size = new System.Drawing.Size(1073, 146);
            this.tabPageSelectedPlan.TabIndex = 0;
            this.tabPageSelectedPlan.Text = "План";
            this.tabPageSelectedPlan.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelSelectedPlan
            // 
            this.tableLayoutPanelSelectedPlan.ColumnCount = 1;
            this.tableLayoutPanelSelectedPlan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSelectedPlan.Controls.Add(this.dgvSelectedPlan, 0, 0);
            this.tableLayoutPanelSelectedPlan.Controls.Add(this.panelSelectedPlan, 0, 1);
            this.tableLayoutPanelSelectedPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSelectedPlan.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelSelectedPlan.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelSelectedPlan.Name = "tableLayoutPanelSelectedPlan";
            this.tableLayoutPanelSelectedPlan.RowCount = 2;
            this.tableLayoutPanelSelectedPlan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSelectedPlan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelSelectedPlan.Size = new System.Drawing.Size(1073, 146);
            this.tableLayoutPanelSelectedPlan.TabIndex = 0;
            // 
            // dgvSelectedPlan
            // 
            this.dgvSelectedPlan.AllowUserToAddRows = false;
            this.dgvSelectedPlan.AllowUserToDeleteRows = false;
            this.dgvSelectedPlan.AllowUserToResizeRows = false;
            this.dgvSelectedPlan.BackgroundColor = System.Drawing.Color.Silver;
            this.dgvSelectedPlan.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSelectedPlan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSelectedPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSelectedPlan.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvSelectedPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelectedPlan.EnableHeadersVisualStyles = false;
            this.dgvSelectedPlan.Location = new System.Drawing.Point(0, 0);
            this.dgvSelectedPlan.Margin = new System.Windows.Forms.Padding(0);
            this.dgvSelectedPlan.Name = "dgvSelectedPlan";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSelectedPlan.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvSelectedPlan.RowHeadersVisible = false;
            this.dgvSelectedPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSelectedPlan.Size = new System.Drawing.Size(1073, 106);
            this.dgvSelectedPlan.TabIndex = 0;
            this.dgvSelectedPlan.DataSourceChanged += new System.EventHandler(this.dgvSelectedPlan_DataSourceChanged);
            this.dgvSelectedPlan.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvSelectedPlan_CellBeginEdit);
            this.dgvSelectedPlan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSelectedPlan_CellDoubleClick);
            this.dgvSelectedPlan.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSelectedPlan_CellEndEdit);
            this.dgvSelectedPlan.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSelectedPlan_CellFormatting);
            this.dgvSelectedPlan.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSelectedPlan_CellMouseDown);
            this.dgvSelectedPlan.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSelectedPlan_CellMouseEnter);
            this.dgvSelectedPlan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvSelectedPlan_MouseClick);
            // 
            // panelSelectedPlan
            // 
            this.panelSelectedPlan.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelSelectedPlan.Controls.Add(this.btnAgreeSetPlaned);
            this.panelSelectedPlan.Controls.Add(this.btnDepartmentPlan_Save);
            this.panelSelectedPlan.Controls.Add(this.btnDepartmentPlan_AddSet);
            this.panelSelectedPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSelectedPlan.Location = new System.Drawing.Point(0, 106);
            this.panelSelectedPlan.Margin = new System.Windows.Forms.Padding(0);
            this.panelSelectedPlan.Name = "panelSelectedPlan";
            this.panelSelectedPlan.Size = new System.Drawing.Size(1073, 40);
            this.panelSelectedPlan.TabIndex = 1;
            // 
            // btnAgreeSetPlaned
            // 
            this.btnAgreeSetPlaned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgreeSetPlaned.Location = new System.Drawing.Point(829, 3);
            this.btnAgreeSetPlaned.Name = "btnAgreeSetPlaned";
            this.btnAgreeSetPlaned.Size = new System.Drawing.Size(135, 34);
            this.btnAgreeSetPlaned.TabIndex = 5;
            this.btnAgreeSetPlaned.Text = "Согласовать запланированные комплекты";
            this.btnAgreeSetPlaned.UseVisualStyleBackColor = true;
            this.btnAgreeSetPlaned.Visible = false;
            this.btnAgreeSetPlaned.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnDepartmentPlan_Save
            // 
            this.btnDepartmentPlan_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDepartmentPlan_Save.Location = new System.Drawing.Point(967, 3);
            this.btnDepartmentPlan_Save.Name = "btnDepartmentPlan_Save";
            this.btnDepartmentPlan_Save.Size = new System.Drawing.Size(103, 34);
            this.btnDepartmentPlan_Save.TabIndex = 3;
            this.btnDepartmentPlan_Save.Text = "Сохранить план";
            this.btnDepartmentPlan_Save.UseVisualStyleBackColor = true;
            this.btnDepartmentPlan_Save.Click += new System.EventHandler(this.btnDepartmentPlanSave_Click);
            // 
            // btnDepartmentPlan_AddSet
            // 
            this.btnDepartmentPlan_AddSet.Location = new System.Drawing.Point(3, 3);
            this.btnDepartmentPlan_AddSet.Name = "btnDepartmentPlan_AddSet";
            this.btnDepartmentPlan_AddSet.Size = new System.Drawing.Size(128, 34);
            this.btnDepartmentPlan_AddSet.TabIndex = 2;
            this.btnDepartmentPlan_AddSet.Text = "Добавить другие работы и комплекты";
            this.btnDepartmentPlan_AddSet.UseVisualStyleBackColor = true;
            this.btnDepartmentPlan_AddSet.Click += new System.EventHandler(this.btnDepartmentPlanAddSet_Click);
            // 
            // tabPageIndividualPlan
            // 
            this.tabPageIndividualPlan.Controls.Add(this.tableLayoutPanelIndividualPlan);
            this.tabPageIndividualPlan.Location = new System.Drawing.Point(4, 20);
            this.tabPageIndividualPlan.Name = "tabPageIndividualPlan";
            this.tabPageIndividualPlan.Padding = new System.Windows.Forms.Padding(1);
            this.tabPageIndividualPlan.Size = new System.Drawing.Size(1083, 442);
            this.tabPageIndividualPlan.TabIndex = 1;
            this.tabPageIndividualPlan.Text = "Индивидуальный план";
            this.tabPageIndividualPlan.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelIndividualPlan
            // 
            this.tableLayoutPanelIndividualPlan.ColumnCount = 1;
            this.tableLayoutPanelIndividualPlan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelIndividualPlan.Controls.Add(this.panelindividualPlanTop, 0, 0);
            this.tableLayoutPanelIndividualPlan.Controls.Add(this.panelIndividualPlanBottom, 0, 2);
            this.tableLayoutPanelIndividualPlan.Controls.Add(this.dgvIndividualPlan, 0, 1);
            this.tableLayoutPanelIndividualPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelIndividualPlan.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanelIndividualPlan.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelIndividualPlan.Name = "tableLayoutPanelIndividualPlan";
            this.tableLayoutPanelIndividualPlan.RowCount = 3;
            this.tableLayoutPanelIndividualPlan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelIndividualPlan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelIndividualPlan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanelIndividualPlan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelIndividualPlan.Size = new System.Drawing.Size(1081, 440);
            this.tableLayoutPanelIndividualPlan.TabIndex = 0;
            // 
            // panelindividualPlanTop
            // 
            this.panelindividualPlanTop.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelindividualPlanTop.Controls.Add(this.picbxRefreshIndividualPlan);
            this.panelindividualPlanTop.Controls.Add(this.panelIndividualPlanDatePicker);
            this.panelindividualPlanTop.Controls.Add(this.panelIndividualPlanUser);
            this.panelindividualPlanTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelindividualPlanTop.Location = new System.Drawing.Point(0, 0);
            this.panelindividualPlanTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelindividualPlanTop.Name = "panelindividualPlanTop";
            this.panelindividualPlanTop.Size = new System.Drawing.Size(1081, 36);
            this.panelindividualPlanTop.TabIndex = 0;
            // 
            // picbxRefreshIndividualPlan
            // 
            this.picbxRefreshIndividualPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picbxRefreshIndividualPlan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picbxRefreshIndividualPlan.BackgroundImage")));
            this.picbxRefreshIndividualPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picbxRefreshIndividualPlan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picbxRefreshIndividualPlan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picbxRefreshIndividualPlan.Location = new System.Drawing.Point(1046, 4);
            this.picbxRefreshIndividualPlan.Name = "picbxRefreshIndividualPlan";
            this.picbxRefreshIndividualPlan.Size = new System.Drawing.Size(28, 28);
            this.picbxRefreshIndividualPlan.TabIndex = 4;
            this.picbxRefreshIndividualPlan.TabStop = false;
            this.picbxRefreshIndividualPlan.Click += new System.EventHandler(this.picbxRefreshIndividualPLan_Click);
            this.picbxRefreshIndividualPlan.MouseEnter += new System.EventHandler(this.picbxRefreshIndividualPLan_MouseEnter);
            this.picbxRefreshIndividualPlan.MouseLeave += new System.EventHandler(this.picbxRefreshIndividualPLan_MouseLeave);
            // 
            // panelIndividualPlanDatePicker
            // 
            this.panelIndividualPlanDatePicker.Controls.Add(this.dtpIndividualPlan);
            this.panelIndividualPlanDatePicker.Controls.Add(this.btnIndividualPlanInc);
            this.panelIndividualPlanDatePicker.Controls.Add(this.btnIndividualPlanDec);
            this.panelIndividualPlanDatePicker.Location = new System.Drawing.Point(395, 3);
            this.panelIndividualPlanDatePicker.Name = "panelIndividualPlanDatePicker";
            this.panelIndividualPlanDatePicker.Size = new System.Drawing.Size(224, 31);
            this.panelIndividualPlanDatePicker.TabIndex = 3;
            // 
            // dtpIndividualPlan
            // 
            this.dtpIndividualPlan.CustomFormat = "MMMM yyyy";
            this.dtpIndividualPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpIndividualPlan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIndividualPlan.Location = new System.Drawing.Point(30, 1);
            this.dtpIndividualPlan.Name = "dtpIndividualPlan";
            this.dtpIndividualPlan.Size = new System.Drawing.Size(163, 26);
            this.dtpIndividualPlan.TabIndex = 2;
            // 
            // btnIndividualPlanInc
            // 
            this.btnIndividualPlanInc.Location = new System.Drawing.Point(192, 0);
            this.btnIndividualPlanInc.Name = "btnIndividualPlanInc";
            this.btnIndividualPlanInc.Size = new System.Drawing.Size(28, 28);
            this.btnIndividualPlanInc.TabIndex = 1;
            this.btnIndividualPlanInc.Text = ">";
            this.btnIndividualPlanInc.UseVisualStyleBackColor = true;
            this.btnIndividualPlanInc.Click += new System.EventHandler(this.btnIndividualPlanInc_Click);
            // 
            // btnIndividualPlanDec
            // 
            this.btnIndividualPlanDec.Location = new System.Drawing.Point(2, 0);
            this.btnIndividualPlanDec.Name = "btnIndividualPlanDec";
            this.btnIndividualPlanDec.Size = new System.Drawing.Size(28, 28);
            this.btnIndividualPlanDec.TabIndex = 0;
            this.btnIndividualPlanDec.Text = "<";
            this.btnIndividualPlanDec.UseVisualStyleBackColor = true;
            this.btnIndividualPlanDec.Click += new System.EventHandler(this.btnIndividualPlanDec_Click);
            // 
            // panelIndividualPlanUser
            // 
            this.panelIndividualPlanUser.Controls.Add(this.labelSelectedUser);
            this.panelIndividualPlanUser.Controls.Add(this.labelUser);
            this.panelIndividualPlanUser.Location = new System.Drawing.Point(5, 3);
            this.panelIndividualPlanUser.Name = "panelIndividualPlanUser";
            this.panelIndividualPlanUser.Size = new System.Drawing.Size(386, 31);
            this.panelIndividualPlanUser.TabIndex = 2;
            // 
            // labelSelectedUser
            // 
            this.labelSelectedUser.AutoSize = true;
            this.labelSelectedUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSelectedUser.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSelectedUser.Location = new System.Drawing.Point(69, 7);
            this.labelSelectedUser.Name = "labelSelectedUser";
            this.labelSelectedUser.Size = new System.Drawing.Size(20, 17);
            this.labelSelectedUser.TabIndex = 1;
            this.labelSelectedUser.Text = "...";
            this.labelSelectedUser.Click += new System.EventHandler(this.labelSelectedUser_Click);
            this.labelSelectedUser.MouseEnter += new System.EventHandler(this.labelSelectedUser_MouseEnter);
            this.labelSelectedUser.MouseLeave += new System.EventHandler(this.labelSelectedUser_MouseLeave);
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(3, 11);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(60, 13);
            this.labelUser.TabIndex = 0;
            this.labelUser.Text = "Сотрудник";
            // 
            // panelIndividualPlanBottom
            // 
            this.panelIndividualPlanBottom.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelIndividualPlanBottom.Controls.Add(this.label4);
            this.panelIndividualPlanBottom.Controls.Add(this.label3);
            this.panelIndividualPlanBottom.Controls.Add(this.label2);
            this.panelIndividualPlanBottom.Controls.Add(this.label1);
            this.panelIndividualPlanBottom.Controls.Add(this.panel5);
            this.panelIndividualPlanBottom.Controls.Add(this.panel4);
            this.panelIndividualPlanBottom.Controls.Add(this.panel3);
            this.panelIndividualPlanBottom.Controls.Add(this.panel2);
            this.panelIndividualPlanBottom.Controls.Add(this.btnIndividualPlanAddSet);
            this.panelIndividualPlanBottom.Controls.Add(this.btnIndividualPlanSave);
            this.panelIndividualPlanBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIndividualPlanBottom.Location = new System.Drawing.Point(0, 398);
            this.panelIndividualPlanBottom.Margin = new System.Windows.Forms.Padding(0);
            this.panelIndividualPlanBottom.Name = "panelIndividualPlanBottom";
            this.panelIndividualPlanBottom.Size = new System.Drawing.Size(1081, 42);
            this.panelIndividualPlanBottom.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(648, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "План";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Выходной";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(473, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Списание по плану";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(473, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Внеплановое списание";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Yellow;
            this.panel5.Location = new System.Drawing.Point(252, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(21, 17);
            this.panel5.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.SkyBlue;
            this.panel4.Location = new System.Drawing.Point(621, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(21, 17);
            this.panel4.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Orange;
            this.panel3.Location = new System.Drawing.Point(445, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(21, 17);
            this.panel3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel2.Location = new System.Drawing.Point(445, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(21, 17);
            this.panel2.TabIndex = 2;
            // 
            // btnIndividualPlanAddSet
            // 
            this.btnIndividualPlanAddSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIndividualPlanAddSet.Location = new System.Drawing.Point(2, 4);
            this.btnIndividualPlanAddSet.Name = "btnIndividualPlanAddSet";
            this.btnIndividualPlanAddSet.Size = new System.Drawing.Size(131, 35);
            this.btnIndividualPlanAddSet.TabIndex = 1;
            this.btnIndividualPlanAddSet.Text = "Добавить другие работы и комплекты";
            this.btnIndividualPlanAddSet.UseVisualStyleBackColor = true;
            this.btnIndividualPlanAddSet.Click += new System.EventHandler(this.btnIndividualPlanAddSet_Click);
            // 
            // btnIndividualPlanSave
            // 
            this.btnIndividualPlanSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIndividualPlanSave.Location = new System.Drawing.Point(975, 4);
            this.btnIndividualPlanSave.Name = "btnIndividualPlanSave";
            this.btnIndividualPlanSave.Size = new System.Drawing.Size(103, 35);
            this.btnIndividualPlanSave.TabIndex = 0;
            this.btnIndividualPlanSave.Text = "Сохранить";
            this.btnIndividualPlanSave.UseVisualStyleBackColor = true;
            this.btnIndividualPlanSave.Click += new System.EventHandler(this.btnIndividualPlanSave_Click);
            // 
            // dgvIndividualPlan
            // 
            this.dgvIndividualPlan.AllowUserToAddRows = false;
            this.dgvIndividualPlan.AllowUserToDeleteRows = false;
            this.dgvIndividualPlan.AllowUserToResizeRows = false;
            this.dgvIndividualPlan.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvIndividualPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIndividualPlan.EnableHeadersVisualStyles = false;
            this.dgvIndividualPlan.Location = new System.Drawing.Point(3, 39);
            this.dgvIndividualPlan.Name = "dgvIndividualPlan";
            this.dgvIndividualPlan.RowHeadersVisible = false;
            this.dgvIndividualPlan.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvIndividualPlan.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvIndividualPlan.Size = new System.Drawing.Size(1075, 356);
            this.dgvIndividualPlan.TabIndex = 2;
            this.dgvIndividualPlan.DataSourceChanged += new System.EventHandler(this.dgvIndividualPlan_DataSourceChanged);
            this.dgvIndividualPlan.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIndividualPlan_CellEndEdit);
            this.dgvIndividualPlan.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIndividualPlan_CellEnter);
            this.dgvIndividualPlan.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvIndividualPlan_CellMouseDown);
            this.dgvIndividualPlan.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIndividualPlan_CellMouseEnter);
            this.dgvIndividualPlan.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvIndividualPlan_CellPainting);
            this.dgvIndividualPlan.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvIndividualPlan_ColumnHeaderMouseClick);
            this.dgvIndividualPlan.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvIndividualPlan_RowPostPaint);
            this.dgvIndividualPlan.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvIndividualPlan_Scroll);
            this.dgvIndividualPlan.SelectionChanged += new System.EventHandler(this.dgvIndividualPlan_SelectionChanged);
            this.dgvIndividualPlan.VisibleChanged += new System.EventHandler(this.dgvIndividualPlan_VisibleChanged);
            this.dgvIndividualPlan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvIndividualPlan_MouseClick);
            // 
            // tabPageAgreement
            // 
            this.tabPageAgreement.Controls.Add(this.tableLayoutPanel_Agreement);
            this.tabPageAgreement.Location = new System.Drawing.Point(4, 20);
            this.tabPageAgreement.Name = "tabPageAgreement";
            this.tabPageAgreement.Size = new System.Drawing.Size(1083, 442);
            this.tabPageAgreement.TabIndex = 2;
            this.tabPageAgreement.Text = "Согласование сроков";
            this.tabPageAgreement.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel_Agreement
            // 
            this.tableLayoutPanel_Agreement.ColumnCount = 1;
            this.tableLayoutPanel_Agreement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Agreement.Controls.Add(this.dgvAgreement, 0, 1);
            this.tableLayoutPanel_Agreement.Controls.Add(this.panelAgreementTop, 0, 0);
            this.tableLayoutPanel_Agreement.Controls.Add(this.panelAgreementBottom, 0, 2);
            this.tableLayoutPanel_Agreement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Agreement.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Agreement.Name = "tableLayoutPanel_Agreement";
            this.tableLayoutPanel_Agreement.RowCount = 4;
            this.tableLayoutPanel_Agreement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel_Agreement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Agreement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel_Agreement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Agreement.Size = new System.Drawing.Size(1083, 442);
            this.tableLayoutPanel_Agreement.TabIndex = 0;
            // 
            // dgvAgreement
            // 
            this.dgvAgreement.AllowUserToAddRows = false;
            this.dgvAgreement.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvAgreement.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvAgreement.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAgreement.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvAgreement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAgreement.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvAgreement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAgreement.Location = new System.Drawing.Point(3, 40);
            this.dgvAgreement.Name = "dgvAgreement";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAgreement.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvAgreement.RowHeadersVisible = false;
            this.dgvAgreement.RowHeadersWidth = 90;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAgreement.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvAgreement.RowTemplate.Height = 120;
            this.dgvAgreement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAgreement.Size = new System.Drawing.Size(1077, 339);
            this.dgvAgreement.TabIndex = 0;
            this.dgvAgreement.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAgreement_CellContentClick);
            this.dgvAgreement.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvAgreement_CellPainting);
            this.dgvAgreement.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvAgreement_CurrentCellDirtyStateChanged);
            this.dgvAgreement.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvAgreement_RowsAdded);
            // 
            // panelAgreementTop
            // 
            this.panelAgreementTop.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelAgreementTop.Controls.Add(this.picbxRefreshAgreement);
            this.panelAgreementTop.Controls.Add(this.button2);
            this.panelAgreementTop.Controls.Add(this.panel1);
            this.panelAgreementTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAgreementTop.Location = new System.Drawing.Point(0, 0);
            this.panelAgreementTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelAgreementTop.Name = "panelAgreementTop";
            this.panelAgreementTop.Size = new System.Drawing.Size(1083, 37);
            this.panelAgreementTop.TabIndex = 2;
            // 
            // picbxRefreshAgreement
            // 
            this.picbxRefreshAgreement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picbxRefreshAgreement.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picbxRefreshAgreement.BackgroundImage")));
            this.picbxRefreshAgreement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picbxRefreshAgreement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picbxRefreshAgreement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picbxRefreshAgreement.Location = new System.Drawing.Point(1047, 5);
            this.picbxRefreshAgreement.Name = "picbxRefreshAgreement";
            this.picbxRefreshAgreement.Size = new System.Drawing.Size(28, 28);
            this.picbxRefreshAgreement.TabIndex = 1;
            this.picbxRefreshAgreement.TabStop = false;
            this.picbxRefreshAgreement.Click += new System.EventHandler(this.picbxRefreshAgreement_Click);
            this.picbxRefreshAgreement.MouseEnter += new System.EventHandler(this.picbxRefreshAgreement_MouseEnter);
            this.picbxRefreshAgreement.MouseLeave += new System.EventHandler(this.picbxRefreshAgreement_MouseLeave);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(800, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 25);
            this.button2.TabIndex = 4;
            this.button2.Text = "Выгрузить в EXCEL\r\n";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbxAgreement_Departments);
            this.panel1.Controls.Add(this.labelAgreementDepartment);
            this.panel1.Location = new System.Drawing.Point(8, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(139, 28);
            this.panel1.TabIndex = 0;
            // 
            // cmbxAgreement_Departments
            // 
            this.cmbxAgreement_Departments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxAgreement_Departments.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbxAgreement_Departments.FormattingEnabled = true;
            this.cmbxAgreement_Departments.Location = new System.Drawing.Point(53, 4);
            this.cmbxAgreement_Departments.Name = "cmbxAgreement_Departments";
            this.cmbxAgreement_Departments.Size = new System.Drawing.Size(83, 21);
            this.cmbxAgreement_Departments.TabIndex = 1;
            this.cmbxAgreement_Departments.Visible = false;
            this.cmbxAgreement_Departments.SelectionChangeCommitted += new System.EventHandler(this.cmbxAgreement_Departments_SelectionChangeCommitted);
            // 
            // labelAgreementDepartment
            // 
            this.labelAgreementDepartment.AutoSize = true;
            this.labelAgreementDepartment.Location = new System.Drawing.Point(8, 8);
            this.labelAgreementDepartment.Name = "labelAgreementDepartment";
            this.labelAgreementDepartment.Size = new System.Drawing.Size(38, 13);
            this.labelAgreementDepartment.TabIndex = 0;
            this.labelAgreementDepartment.Text = "Отдел";
            this.labelAgreementDepartment.Visible = false;
            // 
            // panelAgreementBottom
            // 
            this.panelAgreementBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAgreementBottom.Location = new System.Drawing.Point(3, 385);
            this.panelAgreementBottom.Name = "panelAgreementBottom";
            this.panelAgreementBottom.Size = new System.Drawing.Size(1077, 34);
            this.panelAgreementBottom.TabIndex = 3;
            // 
            // tabPageProductionPlanDept
            // 
            this.tabPageProductionPlanDept.Controls.Add(this.label7);
            this.tabPageProductionPlanDept.Controls.Add(this.richTextBox2);
            this.tabPageProductionPlanDept.Controls.Add(this.dgvProductionPlanDept);
            this.tabPageProductionPlanDept.Controls.Add(this.panelApprovedTop);
            this.tabPageProductionPlanDept.Location = new System.Drawing.Point(4, 20);
            this.tabPageProductionPlanDept.Name = "tabPageProductionPlanDept";
            this.tabPageProductionPlanDept.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProductionPlanDept.Size = new System.Drawing.Size(1083, 442);
            this.tabPageProductionPlanDept.TabIndex = 3;
            this.tabPageProductionPlanDept.Text = "Производственный план";
            this.tabPageProductionPlanDept.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 373);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Комментарий ";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox2.Enabled = false;
            this.richTextBox2.Location = new System.Drawing.Point(3, 386);
            this.richTextBox2.MaxLength = 2900;
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(1077, 53);
            this.richTextBox2.TabIndex = 4;
            this.richTextBox2.Text = "";
            // 
            // dgvProductionPlanDept
            // 
            this.dgvProductionPlanDept.AllowUserToAddRows = false;
            this.dgvProductionPlanDept.AllowUserToDeleteRows = false;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            this.dgvProductionPlanDept.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvProductionPlanDept.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductionPlanDept.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductionPlanDept.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvProductionPlanDept.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductionPlanDept.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvProductionPlanDept.Location = new System.Drawing.Point(0, 37);
            this.dgvProductionPlanDept.Name = "dgvProductionPlanDept";
            this.dgvProductionPlanDept.ReadOnly = true;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvProductionPlanDept.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvProductionPlanDept.RowTemplate.Height = 20;
            this.dgvProductionPlanDept.RowTemplate.ReadOnly = true;
            this.dgvProductionPlanDept.Size = new System.Drawing.Size(1080, 479);
            this.dgvProductionPlanDept.TabIndex = 0;
            // 
            // panelApprovedTop
            // 
            this.panelApprovedTop.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelApprovedTop.Controls.Add(this.dtpProductionPlanDept);
            this.panelApprovedTop.Controls.Add(this.btnProductionPlanDeptInc);
            this.panelApprovedTop.Controls.Add(this.btnProductionPlanDeptIncDec);
            this.panelApprovedTop.Controls.Add(this.label5);
            this.panelApprovedTop.Controls.Add(this.button1);
            this.panelApprovedTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelApprovedTop.Location = new System.Drawing.Point(3, 3);
            this.panelApprovedTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelApprovedTop.Name = "panelApprovedTop";
            this.panelApprovedTop.Size = new System.Drawing.Size(1077, 36);
            this.panelApprovedTop.TabIndex = 3;
            // 
            // dtpProductionPlanDept
            // 
            this.dtpProductionPlanDept.CustomFormat = "MMMM yyyy";
            this.dtpProductionPlanDept.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpProductionPlanDept.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpProductionPlanDept.Location = new System.Drawing.Point(426, 5);
            this.dtpProductionPlanDept.Name = "dtpProductionPlanDept";
            this.dtpProductionPlanDept.Size = new System.Drawing.Size(163, 26);
            this.dtpProductionPlanDept.TabIndex = 8;
            // 
            // btnProductionPlanDeptInc
            // 
            this.btnProductionPlanDeptInc.Location = new System.Drawing.Point(588, 4);
            this.btnProductionPlanDeptInc.Name = "btnProductionPlanDeptInc";
            this.btnProductionPlanDeptInc.Size = new System.Drawing.Size(28, 28);
            this.btnProductionPlanDeptInc.TabIndex = 7;
            this.btnProductionPlanDeptInc.Text = ">";
            this.btnProductionPlanDeptInc.UseVisualStyleBackColor = true;
            // 
            // btnProductionPlanDeptIncDec
            // 
            this.btnProductionPlanDeptIncDec.Location = new System.Drawing.Point(398, 4);
            this.btnProductionPlanDeptIncDec.Name = "btnProductionPlanDeptIncDec";
            this.btnProductionPlanDeptIncDec.Size = new System.Drawing.Size(28, 28);
            this.btnProductionPlanDeptIncDec.TabIndex = 6;
            this.btnProductionPlanDeptIncDec.Text = "<";
            this.btnProductionPlanDeptIncDec.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightYellow;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(8, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "label5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(859, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "Выгрузить в EXCEL\r\n";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabPageProductionPlan
            // 
            this.tabPageProductionPlan.Controls.Add(this.tlp_pp_main);
            this.tabPageProductionPlan.Location = new System.Drawing.Point(4, 20);
            this.tabPageProductionPlan.Name = "tabPageProductionPlan";
            this.tabPageProductionPlan.Size = new System.Drawing.Size(1083, 442);
            this.tabPageProductionPlan.TabIndex = 4;
            this.tabPageProductionPlan.Text = "Производственный план";
            this.tabPageProductionPlan.UseVisualStyleBackColor = true;
            // 
            // tlp_pp_main
            // 
            this.tlp_pp_main.ColumnCount = 2;
            this.tlp_pp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_pp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 488F));
            this.tlp_pp_main.Controls.Add(this.panel6, 1, 0);
            this.tlp_pp_main.Controls.Add(this.panel7, 1, 1);
            this.tlp_pp_main.Controls.Add(this.splitContainer1, 0, 0);
            this.tlp_pp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_pp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_pp_main.Name = "tlp_pp_main";
            this.tlp_pp_main.RowCount = 2;
            this.tlp_pp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_pp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlp_pp_main.Size = new System.Drawing.Size(1083, 442);
            this.tlp_pp_main.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dtpProductionPlan);
            this.panel6.Controls.Add(this.btnProductionPlanMonthInc);
            this.panel6.Controls.Add(this.btnProductionPlanMonthDec);
            this.panel6.Controls.Add(this.dgvDepartments);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(598, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(482, 403);
            this.panel6.TabIndex = 2;
            // 
            // dtpProductionPlan
            // 
            this.dtpProductionPlan.CustomFormat = "MMMM yyyy";
            this.dtpProductionPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpProductionPlan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpProductionPlan.Location = new System.Drawing.Point(140, 3);
            this.dtpProductionPlan.Name = "dtpProductionPlan";
            this.dtpProductionPlan.Size = new System.Drawing.Size(197, 26);
            this.dtpProductionPlan.TabIndex = 3;
            // 
            // btnProductionPlanMonthInc
            // 
            this.btnProductionPlanMonthInc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProductionPlanMonthInc.Location = new System.Drawing.Point(343, 2);
            this.btnProductionPlanMonthInc.Name = "btnProductionPlanMonthInc";
            this.btnProductionPlanMonthInc.Size = new System.Drawing.Size(28, 28);
            this.btnProductionPlanMonthInc.TabIndex = 2;
            this.btnProductionPlanMonthInc.Text = ">";
            this.btnProductionPlanMonthInc.UseVisualStyleBackColor = true;
            // 
            // btnProductionPlanMonthDec
            // 
            this.btnProductionPlanMonthDec.Location = new System.Drawing.Point(106, 2);
            this.btnProductionPlanMonthDec.Name = "btnProductionPlanMonthDec";
            this.btnProductionPlanMonthDec.Size = new System.Drawing.Size(28, 28);
            this.btnProductionPlanMonthDec.TabIndex = 1;
            this.btnProductionPlanMonthDec.Text = "<";
            this.btnProductionPlanMonthDec.UseVisualStyleBackColor = true;
            // 
            // dgvDepartments
            // 
            this.dgvDepartments.AllowUserToAddRows = false;
            this.dgvDepartments.AllowUserToDeleteRows = false;
            this.dgvDepartments.AllowUserToResizeRows = false;
            this.dgvDepartments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDepartments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvDepartments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDepartments.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvDepartments.Location = new System.Drawing.Point(-1, 33);
            this.dgvDepartments.MultiSelect = false;
            this.dgvDepartments.Name = "dgvDepartments";
            this.dgvDepartments.ReadOnly = true;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartments.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvDepartments.RowHeadersVisible = false;
            this.dgvDepartments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartments.Size = new System.Drawing.Size(483, 367);
            this.dgvDepartments.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel7.Controls.Add(this.butPrice);
            this.panel7.Controls.Add(this.btnProductionPlan_Reject);
            this.panel7.Controls.Add(this.btnProductionPlan_Approve);
            this.panel7.Location = new System.Drawing.Point(598, 412);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(482, 27);
            this.panel7.TabIndex = 3;
            // 
            // butPrice
            // 
            this.butPrice.Location = new System.Drawing.Point(404, 0);
            this.butPrice.Name = "butPrice";
            this.butPrice.Size = new System.Drawing.Size(75, 27);
            this.butPrice.TabIndex = 4;
            this.butPrice.Text = "Выручка";
            this.butPrice.UseVisualStyleBackColor = true;
            this.butPrice.Click += new System.EventHandler(this.butPrice_Click);
            // 
            // btnProductionPlan_Reject
            // 
            this.btnProductionPlan_Reject.Location = new System.Drawing.Point(79, 0);
            this.btnProductionPlan_Reject.Name = "btnProductionPlan_Reject";
            this.btnProductionPlan_Reject.Size = new System.Drawing.Size(74, 27);
            this.btnProductionPlan_Reject.TabIndex = 1;
            this.btnProductionPlan_Reject.Text = "Отклонить";
            this.btnProductionPlan_Reject.UseVisualStyleBackColor = true;
            // 
            // btnProductionPlan_Approve
            // 
            this.btnProductionPlan_Approve.Location = new System.Drawing.Point(1, 0);
            this.btnProductionPlan_Approve.Name = "btnProductionPlan_Approve";
            this.btnProductionPlan_Approve.Size = new System.Drawing.Size(75, 27);
            this.btnProductionPlan_Approve.TabIndex = 0;
            this.btnProductionPlan_Approve.Text = "Утвердить";
            this.btnProductionPlan_Approve.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvProductionPlan);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.tlp_pp_main.SetRowSpan(this.splitContainer1, 2);
            this.splitContainer1.Size = new System.Drawing.Size(589, 436);
            this.splitContainer1.SplitterDistance = 381;
            this.splitContainer1.TabIndex = 4;
            // 
            // dgvProductionPlan
            // 
            this.dgvProductionPlan.AllowUserToAddRows = false;
            this.dgvProductionPlan.AllowUserToDeleteRows = false;
            this.dgvProductionPlan.AllowUserToResizeRows = false;
            this.dgvProductionPlan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductionPlan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgvProductionPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductionPlan.DefaultCellStyle = dataGridViewCellStyle23;
            this.dgvProductionPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductionPlan.Location = new System.Drawing.Point(0, 0);
            this.dgvProductionPlan.Name = "dgvProductionPlan";
            this.dgvProductionPlan.ReadOnly = true;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductionPlan.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvProductionPlan.RowHeadersVisible = false;
            this.dgvProductionPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductionPlan.Size = new System.Drawing.Size(589, 381);
            this.dgvProductionPlan.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, -1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Комментарий ";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(1, 13);
            this.richTextBox1.MaxLength = 2900;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(587, 35);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // dgvIndividualPlan_Menu
            // 
            this.dgvIndividualPlan_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_IndividualPlan_DeleteTableRecord,
            this.dgvIndividualPlan_Menu_SaveToExcel});
            this.dgvIndividualPlan_Menu.Name = "dgvIndividualPlan_Menu";
            this.dgvIndividualPlan_Menu.Size = new System.Drawing.Size(218, 48);
            // 
            // ToolStripMenuItem_IndividualPlan_DeleteTableRecord
            // 
            this.ToolStripMenuItem_IndividualPlan_DeleteTableRecord.Name = "ToolStripMenuItem_IndividualPlan_DeleteTableRecord";
            this.ToolStripMenuItem_IndividualPlan_DeleteTableRecord.Size = new System.Drawing.Size(217, 22);
            this.ToolStripMenuItem_IndividualPlan_DeleteTableRecord.Text = "Удалить";
            this.ToolStripMenuItem_IndividualPlan_DeleteTableRecord.Click += new System.EventHandler(this.ToolStripMenuItem_IndividualPlan_DeleteTableRecord_Click);
            // 
            // dgvIndividualPlan_Menu_SaveToExcel
            // 
            this.dgvIndividualPlan_Menu_SaveToExcel.Name = "dgvIndividualPlan_Menu_SaveToExcel";
            this.dgvIndividualPlan_Menu_SaveToExcel.Size = new System.Drawing.Size(217, 22);
            this.dgvIndividualPlan_Menu_SaveToExcel.Text = "Выгрузить таблицу в Excel";
            this.dgvIndividualPlan_Menu_SaveToExcel.Click += new System.EventHandler(this.dgvIndividualPlan_Menu_SaveToExcel_Click);
            // 
            // dgvSelectedPlan_Menu
            // 
            this.dgvSelectedPlan_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dgvSelectedPlan_Menu_AutoPlan,
            this.dgvSelectedPlan_Menu_ManuallyPlan,
            this.dgvSelectedPlan_Menu_DeleteSelected,
            this.dgvSelectedPlan_Menu_DeleteExecutor,
            this.toolStripSeparator2,
            this.dgvSelectedPlan_Menu_SaveToExcel});
            this.dgvSelectedPlan_Menu.Name = "dgvSelectedPlan_Menu";
            this.dgvSelectedPlan_Menu.Size = new System.Drawing.Size(245, 120);
            // 
            // dgvSelectedPlan_Menu_AutoPlan
            // 
            this.dgvSelectedPlan_Menu_AutoPlan.Name = "dgvSelectedPlan_Menu_AutoPlan";
            this.dgvSelectedPlan_Menu_AutoPlan.Size = new System.Drawing.Size(244, 22);
            this.dgvSelectedPlan_Menu_AutoPlan.Text = "Запланировать автоматически";
            this.dgvSelectedPlan_Menu_AutoPlan.Click += new System.EventHandler(this.dgvSelectedPlan_Menu_AutoPlan_Click);
            // 
            // dgvSelectedPlan_Menu_ManuallyPlan
            // 
            this.dgvSelectedPlan_Menu_ManuallyPlan.Name = "dgvSelectedPlan_Menu_ManuallyPlan";
            this.dgvSelectedPlan_Menu_ManuallyPlan.Size = new System.Drawing.Size(244, 22);
            this.dgvSelectedPlan_Menu_ManuallyPlan.Text = "Запланировать вручную";
            this.dgvSelectedPlan_Menu_ManuallyPlan.Click += new System.EventHandler(this.dgvSelectedPlan_Menu_ManuallyPlan_Click);
            // 
            // dgvSelectedPlan_Menu_DeleteSelected
            // 
            this.dgvSelectedPlan_Menu_DeleteSelected.Name = "dgvSelectedPlan_Menu_DeleteSelected";
            this.dgvSelectedPlan_Menu_DeleteSelected.Size = new System.Drawing.Size(244, 22);
            this.dgvSelectedPlan_Menu_DeleteSelected.Text = "Удалить выбранные";
            this.dgvSelectedPlan_Menu_DeleteSelected.Click += new System.EventHandler(this.dgvSelectedPlan_Menu_DeleteSelected_Click);
            // 
            // dgvSelectedPlan_Menu_DeleteExecutor
            // 
            this.dgvSelectedPlan_Menu_DeleteExecutor.Name = "dgvSelectedPlan_Menu_DeleteExecutor";
            this.dgvSelectedPlan_Menu_DeleteExecutor.Size = new System.Drawing.Size(244, 22);
            this.dgvSelectedPlan_Menu_DeleteExecutor.Text = "Удалить исполнителя";
            this.dgvSelectedPlan_Menu_DeleteExecutor.Click += new System.EventHandler(this.dgvSelectedPlan_Menu_DeleteExecutor_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(241, 6);
            // 
            // dgvSelectedPlan_Menu_SaveToExcel
            // 
            this.dgvSelectedPlan_Menu_SaveToExcel.Name = "dgvSelectedPlan_Menu_SaveToExcel";
            this.dgvSelectedPlan_Menu_SaveToExcel.Size = new System.Drawing.Size(244, 22);
            this.dgvSelectedPlan_Menu_SaveToExcel.Text = "Выгрузить в таблицу Excell";
            this.dgvSelectedPlan_Menu_SaveToExcel.Click += new System.EventHandler(this.dgvSelectedPlan_Menu_SaveToExcel_Click);
            // 
            // dgvDepartmentPlan_Menu
            // 
            this.dgvDepartmentPlan_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dgvDepartmentPlan_Menu_ShowSet,
            this.dgvDepartmentPlan_Menu_ShowSetHistory,
            this.dgvDepartmentPlan_Menu_Request,
            this.dgvDepartmentPlan_Menu_DeleteRequest,
            this.dgvDepartmentPlan_Menu_Agree,
            this.toolStripSeparator1,
            this.dgvDepartmentPlan_Menu_SaveToExcel});
            this.dgvDepartmentPlan_Menu.Name = "dgvDepartmentPlan_Menu";
            this.dgvDepartmentPlan_Menu.Size = new System.Drawing.Size(277, 164);
            // 
            // dgvDepartmentPlan_Menu_ShowSet
            // 
            this.dgvDepartmentPlan_Menu_ShowSet.Name = "dgvDepartmentPlan_Menu_ShowSet";
            this.dgvDepartmentPlan_Menu_ShowSet.Size = new System.Drawing.Size(276, 22);
            this.dgvDepartmentPlan_Menu_ShowSet.Text = "Плановая задача";
            this.dgvDepartmentPlan_Menu_ShowSet.Click += new System.EventHandler(this.dgvDepartmentPlan_Menu_ShowSet_Click);
            // 
            // dgvDepartmentPlan_Menu_ShowSetHistory
            // 
            this.dgvDepartmentPlan_Menu_ShowSetHistory.Name = "dgvDepartmentPlan_Menu_ShowSetHistory";
            this.dgvDepartmentPlan_Menu_ShowSetHistory.Size = new System.Drawing.Size(276, 22);
            this.dgvDepartmentPlan_Menu_ShowSetHistory.Text = "История комплекта";
            this.dgvDepartmentPlan_Menu_ShowSetHistory.Click += new System.EventHandler(this.dgvDepartmentPlan_Menu_ShowSetHistory_Click);
            // 
            // dgvDepartmentPlan_Menu_Request
            // 
            this.dgvDepartmentPlan_Menu_Request.Name = "dgvDepartmentPlan_Menu_Request";
            this.dgvDepartmentPlan_Menu_Request.Size = new System.Drawing.Size(276, 22);
            this.dgvDepartmentPlan_Menu_Request.Text = "Запросить перенос сроков";
            this.dgvDepartmentPlan_Menu_Request.Click += new System.EventHandler(this.dgvDepartmentPlan_Menu_Request_Click);
            // 
            // dgvDepartmentPlan_Menu_DeleteRequest
            // 
            this.dgvDepartmentPlan_Menu_DeleteRequest.Name = "dgvDepartmentPlan_Menu_DeleteRequest";
            this.dgvDepartmentPlan_Menu_DeleteRequest.Size = new System.Drawing.Size(276, 22);
            this.dgvDepartmentPlan_Menu_DeleteRequest.Text = "Отменить запрос на перенос сроков";
            this.dgvDepartmentPlan_Menu_DeleteRequest.Click += new System.EventHandler(this.dgvDepartmentPlan_Menu_DeleteRequest_Click);
            // 
            // dgvDepartmentPlan_Menu_Agree
            // 
            this.dgvDepartmentPlan_Menu_Agree.Name = "dgvDepartmentPlan_Menu_Agree";
            this.dgvDepartmentPlan_Menu_Agree.Size = new System.Drawing.Size(276, 22);
            this.dgvDepartmentPlan_Menu_Agree.Text = "Согласовать указанные даты";
            this.dgvDepartmentPlan_Menu_Agree.Click += new System.EventHandler(this.dgvDepartmentPlan_Menu_Agree_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(273, 6);
            // 
            // dgvDepartmentPlan_Menu_SaveToExcel
            // 
            this.dgvDepartmentPlan_Menu_SaveToExcel.Name = "dgvDepartmentPlan_Menu_SaveToExcel";
            this.dgvDepartmentPlan_Menu_SaveToExcel.Size = new System.Drawing.Size(276, 22);
            this.dgvDepartmentPlan_Menu_SaveToExcel.Text = "Выгрузить в таблицу Excel";
            this.dgvDepartmentPlan_Menu_SaveToExcel.Click += new System.EventHandler(this.dgvDepartmentPlan_Manu_SaveToExcel_Click);
            // 
            // dgvDepartmentUsers_Menu
            // 
            this.dgvDepartmentUsers_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dgvDepartmentUsers_Menu_SaveToExcel});
            this.dgvDepartmentUsers_Menu.Name = "dgvDepartmentUsers_Menu";
            this.dgvDepartmentUsers_Menu.Size = new System.Drawing.Size(218, 26);
            // 
            // dgvDepartmentUsers_Menu_SaveToExcel
            // 
            this.dgvDepartmentUsers_Menu_SaveToExcel.Name = "dgvDepartmentUsers_Menu_SaveToExcel";
            this.dgvDepartmentUsers_Menu_SaveToExcel.Size = new System.Drawing.Size(217, 22);
            this.dgvDepartmentUsers_Menu_SaveToExcel.Text = "Выгрузить в таблицу Excel";
            this.dgvDepartmentUsers_Menu_SaveToExcel.Click += new System.EventHandler(this.dgvDepartmentUsers_Menu_SaveToExcel_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 490);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.dtpApprovedPlan);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "frmMain";
            this.Text = "ПЛАТАН";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.dtpApprovedPlan.ResumeLayout(false);
            this.tabPageDepartmentPlan.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelMainTop.ResumeLayout(false);
            this.panelDepartments.ResumeLayout(false);
            this.panelDepartments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxRefreshDepartmentPlan)).EndInit();
            this.panelDepartmentPlanCalendar.ResumeLayout(false);
            this.splitContainerMainHor.Panel1.ResumeLayout(false);
            this.splitContainerMainHor.Panel2.ResumeLayout(false);
            this.splitContainerMainHor.ResumeLayout(false);
            this.splitContainerMainVert.Panel1.ResumeLayout(false);
            this.splitContainerMainVert.Panel2.ResumeLayout(false);
            this.splitContainerMainVert.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartmentPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartmentUsers)).EndInit();
            this.tabControlSelectedPlanAndAgreement.ResumeLayout(false);
            this.tabPageSelectedPlan.ResumeLayout(false);
            this.tableLayoutPanelSelectedPlan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedPlan)).EndInit();
            this.panelSelectedPlan.ResumeLayout(false);
            this.tabPageIndividualPlan.ResumeLayout(false);
            this.tableLayoutPanelIndividualPlan.ResumeLayout(false);
            this.panelindividualPlanTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picbxRefreshIndividualPlan)).EndInit();
            this.panelIndividualPlanDatePicker.ResumeLayout(false);
            this.panelIndividualPlanUser.ResumeLayout(false);
            this.panelIndividualPlanUser.PerformLayout();
            this.panelIndividualPlanBottom.ResumeLayout(false);
            this.panelIndividualPlanBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndividualPlan)).EndInit();
            this.tabPageAgreement.ResumeLayout(false);
            this.tableLayoutPanel_Agreement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgreement)).EndInit();
            this.panelAgreementTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picbxRefreshAgreement)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPageProductionPlanDept.ResumeLayout(false);
            this.tabPageProductionPlanDept.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionPlanDept)).EndInit();
            this.panelApprovedTop.ResumeLayout(false);
            this.panelApprovedTop.PerformLayout();
            this.tabPageProductionPlan.ResumeLayout(false);
            this.tlp_pp_main.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).EndInit();
            this.panel7.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionPlan)).EndInit();
            this.dgvIndividualPlan_Menu.ResumeLayout(false);
            this.dgvSelectedPlan_Menu.ResumeLayout(false);
            this.dgvDepartmentPlan_Menu.ResumeLayout(false);
            this.dgvDepartmentUsers_Menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Reports;
        private System.Windows.Forms.TabControl dtpApprovedPlan;
        private System.Windows.Forms.TabPage tabPageDepartmentPlan;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelMainTop;
        private System.Windows.Forms.TabPage tabPageIndividualPlan;
        private System.Windows.Forms.SplitContainer splitContainerMainHor;
        private System.Windows.Forms.SplitContainer splitContainerMainVert;
        private System.Windows.Forms.DataGridView dgvDepartmentPlan;
        private System.Windows.Forms.DataGridView dgvDepartmentUsers;
        private System.Windows.Forms.TabControl tabControlSelectedPlanAndAgreement;
        private System.Windows.Forms.TabPage tabPageSelectedPlan;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSelectedPlan;
        private System.Windows.Forms.Panel panelDepartments;
        private System.Windows.Forms.ComboBox cmbxDepartmentPlan_Departments;
        private System.Windows.Forms.Label labelDepartment;
        private System.Windows.Forms.PictureBox picbxRefreshDepartmentPlan;
        private System.Windows.Forms.Panel panelDepartmentPlanCalendar;
        private System.Windows.Forms.DateTimePicker dtpDepartmentPlan;
        private System.Windows.Forms.Button btnDepartmentPlanInc;
        private System.Windows.Forms.Button btnDepartmentPlanDec;
        private System.Windows.Forms.ComboBox cmbxStages;
        private System.Windows.Forms.DataGridView dgvSelectedPlan;
        private System.Windows.Forms.Panel panelSelectedPlan;
        private System.Windows.Forms.Button btnDepartmentPlan_Save;
        private System.Windows.Forms.Button btnDepartmentPlan_AddSet;
        private System.Windows.Forms.ToolStripMenuItem отчетРасчетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ОтчетТабельпоэтапуToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelIndividualPlan;
        private System.Windows.Forms.Panel panelindividualPlanTop;
        private System.Windows.Forms.Panel panelIndividualPlanBottom;
        private System.Windows.Forms.Label labelSelectedUser;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Panel panelIndividualPlanDatePicker;
        private System.Windows.Forms.DateTimePicker dtpIndividualPlan;
        private System.Windows.Forms.Button btnIndividualPlanInc;
        private System.Windows.Forms.Button btnIndividualPlanDec;
        private System.Windows.Forms.Panel panelIndividualPlanUser;
        private System.Windows.Forms.PictureBox picbxRefreshIndividualPlan;
        private System.Windows.Forms.Button btnIndividualPlanAddSet;
        private System.Windows.Forms.Button btnIndividualPlanSave;

        //private System.Windows.Forms.DataGridView dgvIndividualPlan;
        private DoubleBufferedDGV.CustomDataGridView dgvIndividualPlan;

        private System.Windows.Forms.ContextMenuStrip dgvIndividualPlan_Menu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_IndividualPlan_DeleteTableRecord;
        private System.Windows.Forms.ContextMenuStrip dgvSelectedPlan_Menu;
        private System.Windows.Forms.ToolStripMenuItem dgvSelectedPlan_Menu_AutoPlan;
        private System.Windows.Forms.ToolStripMenuItem dgvSelectedPlan_Menu_ManuallyPlan;
        private System.Windows.Forms.ToolStripMenuItem dgvSelectedPlan_Menu_DeleteSelected;
        private System.Windows.Forms.ToolStripMenuItem dgvSelectedPlan_Menu_DeleteExecutor;
        private System.Windows.Forms.ContextMenuStrip dgvDepartmentPlan_Menu;
        private System.Windows.Forms.ToolStripMenuItem dgvDepartmentPlan_Menu_ShowSetHistory;
        private System.Windows.Forms.ToolStripMenuItem dgvDepartmentPlan_Menu_Request;
        private System.Windows.Forms.ToolStripMenuItem dgvDepartmentPlan_Menu_SaveToExcel;
        private System.Windows.Forms.ToolStripMenuItem dgvIndividualPlan_Menu_SaveToExcel;
        private System.Windows.Forms.ToolStripMenuItem dgvSelectedPlan_Menu_SaveToExcel;
        private System.Windows.Forms.ContextMenuStrip dgvDepartmentUsers_Menu;
        private System.Windows.Forms.ToolStripMenuItem dgvDepartmentUsers_Menu_SaveToExcel;
        private System.Windows.Forms.TabPage tabPageAgreement;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Agreement;
        private System.Windows.Forms.DataGridView dgvAgreement;
        private System.Windows.Forms.Panel panelAgreementTop;
        private System.Windows.Forms.Panel panelAgreementBottom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbxAgreement_Departments;
        private System.Windows.Forms.Label labelAgreementDepartment;
        private System.Windows.Forms.PictureBox picbxRefreshAgreement;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnAgreeSetPlaned;
        private System.Windows.Forms.TabPage tabPageProductionPlanDept;
        private System.Windows.Forms.Panel panelApprovedTop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvProductionPlanDept;
        private System.Windows.Forms.DateTimePicker dtpProductionPlanDept;
        private System.Windows.Forms.Button btnProductionPlanDeptInc;
        private System.Windows.Forms.Button btnProductionPlanDeptIncDec;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPageProductionPlan;
        private System.Windows.Forms.TableLayoutPanel tlp_pp_main;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DateTimePicker dtpProductionPlan;
        private System.Windows.Forms.Button btnProductionPlanMonthInc;
        private System.Windows.Forms.Button btnProductionPlanMonthDec;
        private System.Windows.Forms.DataGridView dgvDepartments;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnProductionPlan_Reject;
        private System.Windows.Forms.Button btnProductionPlan_Approve;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvProductionPlan;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.ToolStripMenuItem dgvDepartmentPlan_Menu_Agree;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem dgvDepartmentPlan_Menu_DeleteRequest;
        private System.Windows.Forms.ToolStripMenuItem отчетПоТрудозатратамToolStripMenuItem;
        private System.Windows.Forms.Button butPrice;
        private System.Windows.Forms.ToolStripMenuItem dgvDepartmentPlan_Menu_ShowSet;

    }
}

