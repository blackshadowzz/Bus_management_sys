namespace BusMS
{
    partial class AddTravelScheduleForm
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
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStartPoint = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.rbDescription = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbShowMessage = new System.Windows.Forms.RichTextBox();
            this.cbBus = new System.Windows.Forms.ComboBox();
            this.cbDriver = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.ndAmountPerSeat = new System.Windows.Forms.NumericUpDown();
            this.cbStartTime = new System.Windows.Forms.ComboBox();
            this.cbArrivelTime = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ndAmountPerSeat)).BeginInit();
            this.SuspendLayout();
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(12, 31);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(285, 26);
            this.txtID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Schedule ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bus";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Driver";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(315, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Starting Point";
            // 
            // txtStartPoint
            // 
            this.txtStartPoint.Location = new System.Drawing.Point(317, 87);
            this.txtStartPoint.Name = "txtStartPoint";
            this.txtStartPoint.Size = new System.Drawing.Size(285, 26);
            this.txtStartPoint.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Destination To";
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(14, 141);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(285, 26);
            this.txtDestination.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(313, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Schedule Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Start Time";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(313, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Arrival Time";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 223);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Amount Per Seat";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(313, 223);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 20);
            this.label10.TabIndex = 19;
            this.label10.Text = "Description";
            // 
            // rbDescription
            // 
            this.rbDescription.Location = new System.Drawing.Point(315, 246);
            this.rbDescription.Name = "rbDescription";
            this.rbDescription.Size = new System.Drawing.Size(285, 65);
            this.rbDescription.TabIndex = 20;
            this.rbDescription.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(314, 327);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(166, 38);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(486, 327);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 38);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // rbShowMessage
            // 
            this.rbShowMessage.BackColor = System.Drawing.SystemColors.Control;
            this.rbShowMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rbShowMessage.Enabled = false;
            this.rbShowMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbShowMessage.ForeColor = System.Drawing.Color.Tomato;
            this.rbShowMessage.Location = new System.Drawing.Point(12, 300);
            this.rbShowMessage.Name = "rbShowMessage";
            this.rbShowMessage.Size = new System.Drawing.Size(285, 65);
            this.rbShowMessage.TabIndex = 24;
            this.rbShowMessage.Text = "Message";
            // 
            // cbBus
            // 
            this.cbBus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBus.FormattingEnabled = true;
            this.cbBus.Location = new System.Drawing.Point(317, 31);
            this.cbBus.Name = "cbBus";
            this.cbBus.Size = new System.Drawing.Size(283, 28);
            this.cbBus.TabIndex = 25;
            // 
            // cbDriver
            // 
            this.cbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDriver.FormattingEnabled = true;
            this.cbDriver.Location = new System.Drawing.Point(14, 87);
            this.cbDriver.Name = "cbDriver";
            this.cbDriver.Size = new System.Drawing.Size(283, 28);
            this.cbDriver.TabIndex = 26;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(317, 141);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(283, 26);
            this.dateTimePicker1.TabIndex = 27;
            // 
            // ndAmountPerSeat
            // 
            this.ndAmountPerSeat.DecimalPlaces = 2;
            this.ndAmountPerSeat.Location = new System.Drawing.Point(12, 247);
            this.ndAmountPerSeat.Name = "ndAmountPerSeat";
            this.ndAmountPerSeat.Size = new System.Drawing.Size(285, 26);
            this.ndAmountPerSeat.TabIndex = 30;
            // 
            // cbStartTime
            // 
            this.cbStartTime.FormattingEnabled = true;
            this.cbStartTime.Location = new System.Drawing.Point(14, 193);
            this.cbStartTime.Name = "cbStartTime";
            this.cbStartTime.Size = new System.Drawing.Size(283, 28);
            this.cbStartTime.TabIndex = 31;
            // 
            // cbArrivelTime
            // 
            this.cbArrivelTime.FormattingEnabled = true;
            this.cbArrivelTime.Location = new System.Drawing.Point(317, 193);
            this.cbArrivelTime.Name = "cbArrivelTime";
            this.cbArrivelTime.Size = new System.Drawing.Size(283, 28);
            this.cbArrivelTime.TabIndex = 32;
            // 
            // AddTravelScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 377);
            this.Controls.Add(this.cbArrivelTime);
            this.Controls.Add(this.cbStartTime);
            this.Controls.Add(this.ndAmountPerSeat);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cbDriver);
            this.Controls.Add(this.cbBus);
            this.Controls.Add(this.rbShowMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.rbDescription);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtStartPoint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtID);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AddTravelScheduleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddTravelScheduleForm";
            ((System.ComponentModel.ISupportInitialize)(this.ndAmountPerSeat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtID;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtStartPoint;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtDestination;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.RichTextBox rbDescription;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.RichTextBox rbShowMessage;
        public System.Windows.Forms.ComboBox cbBus;
        public System.Windows.Forms.ComboBox cbDriver;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.NumericUpDown ndAmountPerSeat;
        public System.Windows.Forms.ComboBox cbStartTime;
        public System.Windows.Forms.ComboBox cbArrivelTime;
    }
}