namespace Shutdowntor
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_time = new System.Windows.Forms.DateTimePicker();
            this.button_start = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelRemainTime = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.actionOptiontoolStrip = new System.Windows.Forms.ToolStrip();
            this.actionOptiontoolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.actionOptiontoolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.actionOptiontoolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.panelMain.SuspendLayout();
            this.actionOptiontoolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker_date
            // 
            this.dateTimePicker_date.CalendarFont = new System.Drawing.Font("PMingLiU", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dateTimePicker_date.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dateTimePicker_date.Location = new System.Drawing.Point(25, 39);
            this.dateTimePicker_date.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateTimePicker_date.Name = "dateTimePicker_date";
            this.dateTimePicker_date.Size = new System.Drawing.Size(232, 36);
            this.dateTimePicker_date.TabIndex = 0;
            // 
            // dateTimePicker_time
            // 
            this.dateTimePicker_time.CalendarFont = new System.Drawing.Font("PMingLiU", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dateTimePicker_time.CustomFormat = "HH:mm";
            this.dateTimePicker_time.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dateTimePicker_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_time.Location = new System.Drawing.Point(48, 105);
            this.dateTimePicker_time.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateTimePicker_time.Name = "dateTimePicker_time";
            this.dateTimePicker_time.ShowUpDown = true;
            this.dateTimePicker_time.Size = new System.Drawing.Size(159, 36);
            this.dateTimePicker_time.TabIndex = 1;
            // 
            // button_start
            // 
            this.button_start.BackColor = System.Drawing.Color.Red;
            this.button_start.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button_start.Font = new System.Drawing.Font("PMingLiU", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_start.ForeColor = System.Drawing.Color.Black;
            this.button_start.Location = new System.Drawing.Point(0, 206);
            this.button_start.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(278, 43);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "START";
            this.button_start.UseVisualStyleBackColor = false;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Controls.Add(this.actionOptiontoolStrip);
            this.panelMain.Controls.Add(this.labelRemainTime);
            this.panelMain.Controls.Add(this.button_start);
            this.panelMain.Controls.Add(this.dateTimePicker_time);
            this.panelMain.Controls.Add(this.dateTimePicker_date);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(278, 249);
            this.panelMain.TabIndex = 3;
            // 
            // labelRemainTime
            // 
            this.labelRemainTime.AutoSize = true;
            this.labelRemainTime.Font = new System.Drawing.Font("PMingLiU", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelRemainTime.Location = new System.Drawing.Point(43, 161);
            this.labelRemainTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRemainTime.Name = "labelRemainTime";
            this.labelRemainTime.Size = new System.Drawing.Size(108, 30);
            this.labelRemainTime.TabIndex = 2;
            this.labelRemainTime.Text = "--.--:--:--";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Shutdowntor";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // actionOptiontoolStrip
            // 
            this.actionOptiontoolStrip.BackColor = System.Drawing.Color.Transparent;
            this.actionOptiontoolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionOptiontoolStripLabel,
            this.actionOptiontoolStripSeparator,
            this.actionOptiontoolStripComboBox});
            this.actionOptiontoolStrip.Location = new System.Drawing.Point(0, 0);
            this.actionOptiontoolStrip.Name = "actionOptiontoolStrip";
            this.actionOptiontoolStrip.Size = new System.Drawing.Size(278, 25);
            this.actionOptiontoolStrip.TabIndex = 4;
            this.actionOptiontoolStrip.Text = "toolStrip1";
            // 
            // actionOptiontoolStripLabel
            // 
            this.actionOptiontoolStripLabel.Name = "actionOptiontoolStripLabel";
            this.actionOptiontoolStripLabel.Size = new System.Drawing.Size(43, 22);
            this.actionOptiontoolStripLabel.Text = "Action";
            // 
            // actionOptiontoolStripSeparator
            // 
            this.actionOptiontoolStripSeparator.Name = "actionOptiontoolStripSeparator";
            this.actionOptiontoolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // actionOptiontoolStripComboBox
            // 
            this.actionOptiontoolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.actionOptiontoolStripComboBox.Name = "actionOptiontoolStripComboBox";
            this.actionOptiontoolStripComboBox.Size = new System.Drawing.Size(100, 25);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(161)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(278, 249);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "Shutdowntor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.actionOptiontoolStrip.ResumeLayout(false);
            this.actionOptiontoolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelSetting;
        private System.Windows.Forms.Panel panelTimer;
        private System.Windows.Forms.DateTimePicker dateTimePicker_date;
        private System.Windows.Forms.DateTimePicker dateTimePicker_time;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelRemainTime;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStrip actionOptiontoolStrip;
        private System.Windows.Forms.ToolStripLabel actionOptiontoolStripLabel;
        private System.Windows.Forms.ToolStripSeparator actionOptiontoolStripSeparator;
        private System.Windows.Forms.ToolStripComboBox actionOptiontoolStripComboBox;
    }
}

