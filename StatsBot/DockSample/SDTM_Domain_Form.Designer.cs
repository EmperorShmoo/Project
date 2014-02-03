namespace DockSample
{
    partial class SDTM_Domain_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_DomainName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_DomainDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_DomainClass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_DomainStructure = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_DomainSplit = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listbx_Variables = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.listbx_SASexeOrder = new System.Windows.Forms.ListBox();
            this.btn_AddVar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Domain:";
            // 
            // txt_DomainName
            // 
            this.txt_DomainName.Enabled = false;
            this.txt_DomainName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DomainName.Location = new System.Drawing.Point(94, 10);
            this.txt_DomainName.Name = "txt_DomainName";
            this.txt_DomainName.Size = new System.Drawing.Size(65, 26);
            this.txt_DomainName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(165, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description:";
            // 
            // txt_DomainDescription
            // 
            this.txt_DomainDescription.Enabled = false;
            this.txt_DomainDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DomainDescription.Location = new System.Drawing.Point(276, 13);
            this.txt_DomainDescription.Name = "txt_DomainDescription";
            this.txt_DomainDescription.Size = new System.Drawing.Size(290, 22);
            this.txt_DomainDescription.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(572, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Class:";
            // 
            // txt_DomainClass
            // 
            this.txt_DomainClass.Enabled = false;
            this.txt_DomainClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DomainClass.Location = new System.Drawing.Point(636, 13);
            this.txt_DomainClass.Name = "txt_DomainClass";
            this.txt_DomainClass.Size = new System.Drawing.Size(172, 22);
            this.txt_DomainClass.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Structure:";
            // 
            // txt_DomainStructure
            // 
            this.txt_DomainStructure.Location = new System.Drawing.Point(109, 43);
            this.txt_DomainStructure.Name = "txt_DomainStructure";
            this.txt_DomainStructure.Size = new System.Drawing.Size(457, 20);
            this.txt_DomainStructure.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Split Domain?";
            // 
            // cmb_DomainSplit
            // 
            this.cmb_DomainSplit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DomainSplit.FormattingEnabled = true;
            this.cmb_DomainSplit.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.cmb_DomainSplit.Location = new System.Drawing.Point(140, 68);
            this.cmb_DomainSplit.Name = "cmb_DomainSplit";
            this.cmb_DomainSplit.Size = new System.Drawing.Size(36, 21);
            this.cmb_DomainSplit.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Variables";
            // 
            // listbx_Variables
            // 
            this.listbx_Variables.FormattingEnabled = true;
            this.listbx_Variables.Location = new System.Drawing.Point(17, 152);
            this.listbx_Variables.Name = "listbx_Variables";
            this.listbx_Variables.Size = new System.Drawing.Size(117, 316);
            this.listbx_Variables.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(188, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "SAS Execution Order";
            // 
            // listbx_SASexeOrder
            // 
            this.listbx_SASexeOrder.FormattingEnabled = true;
            this.listbx_SASexeOrder.Location = new System.Drawing.Point(192, 152);
            this.listbx_SASexeOrder.Name = "listbx_SASexeOrder";
            this.listbx_SASexeOrder.Size = new System.Drawing.Size(228, 316);
            this.listbx_SASexeOrder.TabIndex = 13;
            // 
            // btn_AddVar
            // 
            this.btn_AddVar.Location = new System.Drawing.Point(3, 474);
            this.btn_AddVar.Name = "btn_AddVar";
            this.btn_AddVar.Size = new System.Drawing.Size(40, 37);
            this.btn_AddVar.TabIndex = 14;
            this.btn_AddVar.Text = "Add New";
            this.btn_AddVar.UseVisualStyleBackColor = true;
            this.btn_AddVar.Click += new System.EventHandler(this.btn_AddVar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 474);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 37);
            this.button1.TabIndex = 15;
            this.button1.Text = "Edit/   Modify";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(104, 475);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 36);
            this.button2.TabIndex = 16;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // SDTM_Domain_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 588);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_AddVar);
            this.Controls.Add(this.listbx_SASexeOrder);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listbx_Variables);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmb_DomainSplit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_DomainStructure);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_DomainClass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_DomainDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_DomainName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SDTM_Domain_Form";
            this.Text = "SDTM_Domain_Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_DomainName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_DomainDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_DomainClass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_DomainStructure;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_DomainSplit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listbx_Variables;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listbx_SASexeOrder;
        private System.Windows.Forms.Button btn_AddVar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}