﻿namespace WindowsFormsApplication1
{
    partial class RDLDataSourceFinder
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
            this.RDLPathTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CriteriaTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.OutputTxtBox = new System.Windows.Forms.TextBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.OutputPathTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(191, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "RDL Data Source Locator";
            // 
            // RDLPathTxtBox
            // 
            this.RDLPathTxtBox.Location = new System.Drawing.Point(229, 106);
            this.RDLPathTxtBox.Name = "RDLPathTxtBox";
            this.RDLPathTxtBox.Size = new System.Drawing.Size(362, 20);
            this.RDLPathTxtBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(77, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "RDL Parent Directory";
            // 
            // CriteriaTextBox
            // 
            this.CriteriaTextBox.Location = new System.Drawing.Point(229, 164);
            this.CriteriaTextBox.Multiline = true;
            this.CriteriaTextBox.Name = "CriteriaTextBox";
            this.CriteriaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CriteriaTextBox.Size = new System.Drawing.Size(362, 110);
            this.CriteriaTextBox.TabIndex = 3;
            this.CriteriaTextBox.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Databases/Tables/Columns";
            // 
            // OutputTxtBox
            // 
            this.OutputTxtBox.Location = new System.Drawing.Point(48, 314);
            this.OutputTxtBox.Multiline = true;
            this.OutputTxtBox.Name = "OutputTxtBox";
            this.OutputTxtBox.ReadOnly = true;
            this.OutputTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTxtBox.Size = new System.Drawing.Size(543, 156);
            this.OutputTxtBox.TabIndex = 5;
            this.OutputTxtBox.WordWrap = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(282, 484);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(110, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Output File Path";
            // 
            // OutputPathTxtBox
            // 
            this.OutputPathTxtBox.Location = new System.Drawing.Point(229, 132);
            this.OutputPathTxtBox.Name = "OutputPathTxtBox";
            this.OutputPathTxtBox.Size = new System.Drawing.Size(362, 20);
            this.OutputPathTxtBox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(244, 277);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(333, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Place each item on a new line. Format DATABASE.TABLE.COLUMN\r\n";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(626, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "This application will search through all RDL files in a directory and subdirector" +
    "ies for data sources.\r\n";
            // 
            // RDLDataSourceFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 547);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.OutputPathTxtBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OutputTxtBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CriteriaTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RDLPathTxtBox);
            this.Controls.Add(this.label1);
            this.Name = "RDLDataSourceFinder";
            this.Text = "RDL Data Source Locator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RDLPathTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CriteriaTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TextBox OutputTxtBox;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox OutputPathTxtBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

