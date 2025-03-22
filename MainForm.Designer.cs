namespace EventOrganizer
{
    partial class MainForm
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
            this.grpNewEvent = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateEvent = new System.Windows.Forms.Button();
            this.txtFeePerParticipant = new System.Windows.Forms.TextBox();
            this.txtCostPerParicipant = new System.Windows.Forms.TextBox();
            this.txtEventTitle = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.grpAddParticipant = new System.Windows.Forms.GroupBox();
            this.comboBoxCountry = new System.Windows.Forms.ComboBox();
            this.btnAddParticipant = new System.Windows.Forms.Button();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grpEventEconomy = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblSurplusDeficit = new System.Windows.Forms.Label();
            this.lblTotalFees = new System.Windows.Forms.Label();
            this.lblTotalCost = new System.Windows.Forms.Label();
            this.lblNumberOfParticipants = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.listBoxParticipants = new System.Windows.Forms.ListBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.grpNewEvent.SuspendLayout();
            this.grpAddParticipant.SuspendLayout();
            this.grpEventEconomy.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNewEvent
            // 
            this.grpNewEvent.Controls.Add(this.label3);
            this.grpNewEvent.Controls.Add(this.label2);
            this.grpNewEvent.Controls.Add(this.label1);
            this.grpNewEvent.Controls.Add(this.btnCreateEvent);
            this.grpNewEvent.Controls.Add(this.txtFeePerParticipant);
            this.grpNewEvent.Controls.Add(this.txtCostPerParicipant);
            this.grpNewEvent.Controls.Add(this.txtEventTitle);
            this.grpNewEvent.Location = new System.Drawing.Point(12, 27);
            this.grpNewEvent.Name = "grpNewEvent";
            this.grpNewEvent.Size = new System.Drawing.Size(390, 259);
            this.grpNewEvent.TabIndex = 0;
            this.grpNewEvent.TabStop = false;
            this.grpNewEvent.Text = "NewEvent";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 35);
            this.label3.TabIndex = 2;
            this.label3.Text = "Fee per participant";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 35);
            this.label2.TabIndex = 2;
            this.label2.Text = " Cost per participant ";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Event title";
            // 
            // btnCreateEvent
            // 
            this.btnCreateEvent.Location = new System.Drawing.Point(101, 199);
            this.btnCreateEvent.Name = "btnCreateEvent";
            this.btnCreateEvent.Size = new System.Drawing.Size(117, 33);
            this.btnCreateEvent.TabIndex = 4;
            this.btnCreateEvent.Text = "Create Event";
            this.btnCreateEvent.UseVisualStyleBackColor = true;
            this.btnCreateEvent.Click += new System.EventHandler(this.btnCreateEvent_Click);
            // 
            // txtFeePerParticipant
            // 
            this.txtFeePerParticipant.Location = new System.Drawing.Point(241, 142);
            this.txtFeePerParticipant.Name = "txtFeePerParticipant";
            this.txtFeePerParticipant.Size = new System.Drawing.Size(100, 26);
            this.txtFeePerParticipant.TabIndex = 3;
            // 
            // txtCostPerParicipant
            // 
            this.txtCostPerParicipant.Location = new System.Drawing.Point(239, 95);
            this.txtCostPerParicipant.Name = "txtCostPerParicipant";
            this.txtCostPerParicipant.Size = new System.Drawing.Size(102, 26);
            this.txtCostPerParicipant.TabIndex = 2;
            // 
            // txtEventTitle
            // 
            this.txtEventTitle.Location = new System.Drawing.Point(153, 46);
            this.txtEventTitle.Name = "txtEventTitle";
            this.txtEventTitle.Size = new System.Drawing.Size(188, 26);
            this.txtEventTitle.TabIndex = 0;
            // 
            // grpAddParticipant
            // 
            this.grpAddParticipant.Controls.Add(this.comboBoxCountry);
            this.grpAddParticipant.Controls.Add(this.btnAddParticipant);
            this.grpAddParticipant.Controls.Add(this.txtZipCode);
            this.grpAddParticipant.Controls.Add(this.txtCity);
            this.grpAddParticipant.Controls.Add(this.txtStreet);
            this.grpAddParticipant.Controls.Add(this.txtLastName);
            this.grpAddParticipant.Controls.Add(this.txtFirstName);
            this.grpAddParticipant.Controls.Add(this.label10);
            this.grpAddParticipant.Controls.Add(this.label9);
            this.grpAddParticipant.Controls.Add(this.label7);
            this.grpAddParticipant.Controls.Add(this.label8);
            this.grpAddParticipant.Controls.Add(this.label5);
            this.grpAddParticipant.Controls.Add(this.label4);
            this.grpAddParticipant.Location = new System.Drawing.Point(26, 438);
            this.grpAddParticipant.Name = "grpAddParticipant";
            this.grpAddParticipant.Size = new System.Drawing.Size(385, 428);
            this.grpAddParticipant.TabIndex = 0;
            this.grpAddParticipant.TabStop = false;
            this.grpAddParticipant.Text = "Add participant";
            // 
            // comboBoxCountry
            // 
            this.comboBoxCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCountry.FormattingEnabled = true;
            this.comboBoxCountry.Location = new System.Drawing.Point(181, 303);
            this.comboBoxCountry.Name = "comboBoxCountry";
            this.comboBoxCountry.Size = new System.Drawing.Size(195, 28);
            this.comboBoxCountry.TabIndex = 9;
            // 
            // btnAddParticipant
            // 
            this.btnAddParticipant.Location = new System.Drawing.Point(103, 360);
            this.btnAddParticipant.Name = "btnAddParticipant";
            this.btnAddParticipant.Size = new System.Drawing.Size(75, 39);
            this.btnAddParticipant.TabIndex = 7;
            this.btnAddParticipant.Text = "Add";
            this.btnAddParticipant.UseVisualStyleBackColor = true;
            this.btnAddParticipant.Click += new System.EventHandler(this.btnAddParticipant_Click);
            // 
            // txtZipCode
            // 
            this.txtZipCode.Location = new System.Drawing.Point(181, 248);
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(195, 26);
            this.txtZipCode.TabIndex = 8;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(184, 197);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(195, 26);
            this.txtCity.TabIndex = 3;
            // 
            // txtStreet
            // 
            this.txtStreet.Location = new System.Drawing.Point(181, 151);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(195, 26);
            this.txtStreet.TabIndex = 3;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(181, 102);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(195, 26);
            this.txtLastName.TabIndex = 7;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(181, 51);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(195, 26);
            this.txtFirstName.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(16, 296);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 35);
            this.label10.TabIndex = 6;
            this.label10.Text = "Country";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 248);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(150, 35);
            this.label9.TabIndex = 5;
            this.label9.Text = "Zip Code";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 35);
            this.label7.TabIndex = 3;
            this.label7.Text = "Street";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 35);
            this.label8.TabIndex = 4;
            this.label8.Text = "City";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 35);
            this.label5.TabIndex = 3;
            this.label5.Text = "Last Name";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "First Name";
            // 
            // grpEventEconomy
            // 
            this.grpEventEconomy.Controls.Add(this.label20);
            this.grpEventEconomy.Controls.Add(this.label19);
            this.grpEventEconomy.Controls.Add(this.label18);
            this.grpEventEconomy.Controls.Add(this.label17);
            this.grpEventEconomy.Controls.Add(this.lblSurplusDeficit);
            this.grpEventEconomy.Controls.Add(this.lblTotalFees);
            this.grpEventEconomy.Controls.Add(this.lblTotalCost);
            this.grpEventEconomy.Controls.Add(this.lblNumberOfParticipants);
            this.grpEventEconomy.Controls.Add(this.label12);
            this.grpEventEconomy.Location = new System.Drawing.Point(693, 502);
            this.grpEventEconomy.Name = "grpEventEconomy";
            this.grpEventEconomy.Size = new System.Drawing.Size(422, 210);
            this.grpEventEconomy.TabIndex = 1;
            this.grpEventEconomy.TabStop = false;
            this.grpEventEconomy.Text = "Event economy";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(19, 162);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(150, 35);
            this.label20.TabIndex = 8;
            this.label20.Text = "Surplus/deficit";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(19, 114);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(79, 20);
            this.label19.TabIndex = 7;
            this.label19.Text = "Total fees";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(19, 73);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(150, 35);
            this.label18.TabIndex = 6;
            this.label18.Text = "Total Cost";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(19, 38);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(169, 35);
            this.label17.TabIndex = 5;
            this.label17.Text = "Number of participants ";
            // 
            // lblSurplusDeficit
            // 
            this.lblSurplusDeficit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSurplusDeficit.Location = new System.Drawing.Point(257, 162);
            this.lblSurplusDeficit.Name = "lblSurplusDeficit";
            this.lblSurplusDeficit.Size = new System.Drawing.Size(150, 35);
            this.lblSurplusDeficit.TabIndex = 4;
            // 
            // lblTotalFees
            // 
            this.lblTotalFees.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalFees.Location = new System.Drawing.Point(257, 114);
            this.lblTotalFees.Name = "lblTotalFees";
            this.lblTotalFees.Size = new System.Drawing.Size(150, 35);
            this.lblTotalFees.TabIndex = 3;
            // 
            // lblTotalCost
            // 
            this.lblTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalCost.Location = new System.Drawing.Point(257, 77);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(150, 35);
            this.lblTotalCost.TabIndex = 2;
            // 
            // lblNumberOfParticipants
            // 
            this.lblNumberOfParticipants.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNumberOfParticipants.Location = new System.Drawing.Point(257, 30);
            this.lblNumberOfParticipants.Name = "lblNumberOfParticipants";
            this.lblNumberOfParticipants.Size = new System.Drawing.Size(150, 35);
            this.lblNumberOfParticipants.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(302, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 35);
            this.label12.TabIndex = 0;
            // 
            // listBoxParticipants
            // 
            this.listBoxParticipants.FormattingEnabled = true;
            this.listBoxParticipants.ItemHeight = 20;
            this.listBoxParticipants.Location = new System.Drawing.Point(538, 47);
            this.listBoxParticipants.Name = "listBoxParticipants";
            this.listBoxParticipants.Size = new System.Drawing.Size(796, 284);
            this.listBoxParticipants.TabIndex = 0;
            this.listBoxParticipants.SelectedIndexChanged += new System.EventHandler(this.listBoxParticipants_SelectedIndexChanged);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(633, 362);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(118, 32);
            this.btnChange.TabIndex = 3;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(917, 362);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(103, 30);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(583, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 35);
            this.label6.TabIndex = 5;
            this.label6.Text = "Participant ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1047, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 20);
            this.label11.TabIndex = 6;
            this.label11.Text = "Address";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(521, 741);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(8, 8);
            this.button2.TabIndex = 8;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1388, 970);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.listBoxParticipants);
            this.Controls.Add(this.grpEventEconomy);
            this.Controls.Add(this.grpAddParticipant);
            this.Controls.Add(this.grpNewEvent);
            this.Name = "MainForm";
            this.Text = "Event Organizer  by <Abenezer Anglo>";
            this.grpNewEvent.ResumeLayout(false);
            this.grpNewEvent.PerformLayout();
            this.grpAddParticipant.ResumeLayout(false);
            this.grpAddParticipant.PerformLayout();
            this.grpEventEconomy.ResumeLayout(false);
            this.grpEventEconomy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNewEvent;
        private System.Windows.Forms.TextBox txtEventTitle;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox grpAddParticipant;
        private System.Windows.Forms.GroupBox grpEventEconomy;
        private System.Windows.Forms.ListBox listBoxParticipants;
        private System.Windows.Forms.TextBox txtCostPerParicipant;
        private System.Windows.Forms.TextBox txtFeePerParticipant;
        private System.Windows.Forms.Button btnCreateEvent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblTotalFees;
        private System.Windows.Forms.Label lblTotalCost;
        private System.Windows.Forms.Label lblNumberOfParticipants;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblSurplusDeficit;
        private System.Windows.Forms.ComboBox comboBoxCountry;
        private System.Windows.Forms.Button btnAddParticipant;
        private System.Windows.Forms.Button button2;
    }
}

