namespace DockSample
{
    partial class SDTM_New_Variable_Form
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
            this.lst_Variables = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_VarName = new System.Windows.Forms.TextBox();
            this.txt_VarOrdNum = new System.Windows.Forms.TextBox();
            this.txt_Var_Length = new System.Windows.Forms.TextBox();
            this.cmb_VarCore = new System.Windows.Forms.ComboBox();
            this.cmb_VarRole = new System.Windows.Forms.ComboBox();
            this.cmb_VarType = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a pre-defined variable from the list, or create a custom variable.";
            // 
            // lst_Variables
            // 
            this.lst_Variables.FormattingEnabled = true;
            this.lst_Variables.Location = new System.Drawing.Point(15, 92);
            this.lst_Variables.Name = "lst_Variables";
            this.lst_Variables.Size = new System.Drawing.Size(175, 342);
            this.lst_Variables.TabIndex = 1;
            this.lst_Variables.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pre-Defined Variables";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(267, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(267, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Order Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(267, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Core:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(267, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Role:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(267, 300);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "Type:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(267, 364);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "Length:";
            // 
            // txt_VarName
            // 
            this.txt_VarName.Enabled = false;
            this.txt_VarName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_VarName.Location = new System.Drawing.Point(324, 70);
            this.txt_VarName.Name = "txt_VarName";
            this.txt_VarName.Size = new System.Drawing.Size(355, 22);
            this.txt_VarName.TabIndex = 10;
            // 
            // txt_VarOrdNum
            // 
            this.txt_VarOrdNum.Enabled = false;
            this.txt_VarOrdNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_VarOrdNum.Location = new System.Drawing.Point(369, 121);
            this.txt_VarOrdNum.Name = "txt_VarOrdNum";
            this.txt_VarOrdNum.Size = new System.Drawing.Size(55, 22);
            this.txt_VarOrdNum.TabIndex = 11;
            // 
            // txt_Var_Length
            // 
            this.txt_Var_Length.Enabled = false;
            this.txt_Var_Length.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Var_Length.Location = new System.Drawing.Point(324, 361);
            this.txt_Var_Length.Name = "txt_Var_Length";
            this.txt_Var_Length.Size = new System.Drawing.Size(100, 22);
            this.txt_Var_Length.TabIndex = 14;
            // 
            // cmb_VarCore
            // 
            this.cmb_VarCore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_VarCore.Enabled = false;
            this.cmb_VarCore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_VarCore.FormattingEnabled = true;
            this.cmb_VarCore.Items.AddRange(new object[] {
            "Req",
            "Perm",
            "Exp"});
            this.cmb_VarCore.Location = new System.Drawing.Point(324, 168);
            this.cmb_VarCore.Name = "cmb_VarCore";
            this.cmb_VarCore.Size = new System.Drawing.Size(170, 24);
            this.cmb_VarCore.TabIndex = 15;
            // 
            // cmb_VarRole
            // 
            this.cmb_VarRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_VarRole.Enabled = false;
            this.cmb_VarRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_VarRole.FormattingEnabled = true;
            this.cmb_VarRole.Items.AddRange(new object[] {
            "Identifier",
            "Topic",
            "Synonym Qualifier",
            "Variable Qualifier",
            "Grouping Qualifier",
            "Record Qualifier",
            "Result Qualifier",
            "Rule",
            "Timing"});
            this.cmb_VarRole.Location = new System.Drawing.Point(324, 221);
            this.cmb_VarRole.Name = "cmb_VarRole";
            this.cmb_VarRole.Size = new System.Drawing.Size(170, 24);
            this.cmb_VarRole.TabIndex = 16;
            // 
            // cmb_VarType
            // 
            this.cmb_VarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_VarType.Enabled = false;
            this.cmb_VarType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_VarType.FormattingEnabled = true;
            this.cmb_VarType.Items.AddRange(new object[] {
            "text",
            "integer",
            "Float",
            "date",
            "datetime"});
            this.cmb_VarType.Location = new System.Drawing.Point(324, 297);
            this.cmb_VarType.Name = "cmb_VarType";
            this.cmb_VarType.Size = new System.Drawing.Size(170, 24);
            this.cmb_VarType.TabIndex = 17;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(270, 440);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Add to Domain";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SDTM_New_Variable_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 511);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cmb_VarType);
            this.Controls.Add(this.cmb_VarRole);
            this.Controls.Add(this.cmb_VarCore);
            this.Controls.Add(this.txt_Var_Length);
            this.Controls.Add(this.txt_VarOrdNum);
            this.Controls.Add(this.txt_VarName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lst_Variables);
            this.Controls.Add(this.label1);
            this.Name = "SDTM_New_Variable_Form";
            this.Text = "New Variable";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lst_Variables;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_VarName;
        private System.Windows.Forms.TextBox txt_VarOrdNum;
        private System.Windows.Forms.TextBox txt_Var_Length;
        private System.Windows.Forms.ComboBox cmb_VarCore;
        private System.Windows.Forms.ComboBox cmb_VarRole;
        private System.Windows.Forms.ComboBox cmb_VarType;
        private System.Windows.Forms.Button button2;
    }
}