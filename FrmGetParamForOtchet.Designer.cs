namespace sprut
{
    partial class FrmGetParamForOtchet
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
            this.dtpReportBeg = new System.Windows.Forms.DateTimePicker();
            this.btnReportBegInc = new System.Windows.Forms.Button();
            this.btnReportBegDec = new System.Windows.Forms.Button();
            this.dtpReportEnd = new System.Windows.Forms.DateTimePicker();
            this.btnReportEndInc = new System.Windows.Forms.Button();
            this.btnReportEndDec = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // dtpReportBeg
            // 
            this.dtpReportBeg.CustomFormat = "MMMM yyyy";
            this.dtpReportBeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpReportBeg.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReportBeg.Location = new System.Drawing.Point(116, 41);
            this.dtpReportBeg.Name = "dtpReportBeg";
            this.dtpReportBeg.Size = new System.Drawing.Size(163, 26);
            this.dtpReportBeg.TabIndex = 5;
            // 
            // btnReportBegInc
            // 
            this.btnReportBegInc.Location = new System.Drawing.Point(279, 40);
            this.btnReportBegInc.Name = "btnReportBegInc";
            this.btnReportBegInc.Size = new System.Drawing.Size(28, 28);
            this.btnReportBegInc.TabIndex = 4;
            this.btnReportBegInc.Text = ">";
            this.btnReportBegInc.UseVisualStyleBackColor = true;
            this.btnReportBegInc.Click += new System.EventHandler(this.btnReportBegInc_Click_1);
            // 
            // btnReportBegDec
            // 
            this.btnReportBegDec.Location = new System.Drawing.Point(88, 40);
            this.btnReportBegDec.Name = "btnReportBegDec";
            this.btnReportBegDec.Size = new System.Drawing.Size(28, 28);
            this.btnReportBegDec.TabIndex = 3;
            this.btnReportBegDec.Text = "<";
            this.btnReportBegDec.UseVisualStyleBackColor = true;
            this.btnReportBegDec.Click += new System.EventHandler(this.btnReportBegDec_Click_1);
            // 
            // dtpReportEnd
            // 
            this.dtpReportEnd.CustomFormat = "MMMM yyyy";
            this.dtpReportEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpReportEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReportEnd.Location = new System.Drawing.Point(374, 41);
            this.dtpReportEnd.Name = "dtpReportEnd";
            this.dtpReportEnd.Size = new System.Drawing.Size(163, 26);
            this.dtpReportEnd.TabIndex = 8;
            // 
            // btnReportEndInc
            // 
            this.btnReportEndInc.Location = new System.Drawing.Point(537, 40);
            this.btnReportEndInc.Name = "btnReportEndInc";
            this.btnReportEndInc.Size = new System.Drawing.Size(28, 28);
            this.btnReportEndInc.TabIndex = 7;
            this.btnReportEndInc.Text = ">";
            this.btnReportEndInc.UseVisualStyleBackColor = true;
            this.btnReportEndInc.Click += new System.EventHandler(this.btnReportEndInc_Click_1);
            // 
            // btnReportEndDec
            // 
            this.btnReportEndDec.Location = new System.Drawing.Point(346, 40);
            this.btnReportEndDec.Name = "btnReportEndDec";
            this.btnReportEndDec.Size = new System.Drawing.Size(28, 28);
            this.btnReportEndDec.TabIndex = 6;
            this.btnReportEndDec.Text = "<";
            this.btnReportEndDec.UseVisualStyleBackColor = true;
            this.btnReportEndDec.Click += new System.EventHandler(this.btnReportEndDec_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(164, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Период формирования отчета";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(64, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "с";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(316, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "по";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(34, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 19;
            this.label9.Text = "Проект";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(35, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "Отдел";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(192, 388);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "ОК";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(285, 388);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 24;
            this.button4.Text = "ОТМЕНА";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(32, 107);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(549, 95);
            this.listBox1.TabIndex = 25;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(32, 255);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox2.Size = new System.Drawing.Size(549, 95);
            this.listBox2.TabIndex = 26;
            // 
            // FrmGetParamForOtchet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 436);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpReportEnd);
            this.Controls.Add(this.btnReportEndInc);
            this.Controls.Add(this.btnReportEndDec);
            this.Controls.Add(this.dtpReportBeg);
            this.Controls.Add(this.btnReportBegInc);
            this.Controls.Add(this.btnReportBegDec);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGetParamForOtchet";
            this.Text = "Выбор параметров для отчета";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpReportBeg;
        private System.Windows.Forms.Button btnReportBegInc;
        private System.Windows.Forms.Button btnReportBegDec;
        private System.Windows.Forms.DateTimePicker dtpReportEnd;
        private System.Windows.Forms.Button btnReportEndInc;
        private System.Windows.Forms.Button btnReportEndDec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
    }
}