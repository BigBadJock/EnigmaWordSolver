namespace WinformsTestApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtWordsToSolve = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSolve = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.txtGivenValues = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtWordsToSolve
            // 
            this.txtWordsToSolve.Location = new System.Drawing.Point(55, 66);
            this.txtWordsToSolve.Multiline = true;
            this.txtWordsToSolve.Name = "txtWordsToSolve";
            this.txtWordsToSolve.Size = new System.Drawing.Size(485, 1009);
            this.txtWordsToSolve.TabIndex = 0;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(603, 69);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(150, 31);
            this.txtKey.TabIndex = 1;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(767, 70);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(150, 31);
            this.txtValue.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(938, 70);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(112, 34);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(603, 127);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(112, 34);
            this.btnSolve.TabIndex = 4;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(1140, 69);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(466, 1021);
            this.txtResults.TabIndex = 5;
            // 
            // txtGivenValues
            // 
            this.txtGivenValues.Location = new System.Drawing.Point(610, 187);
            this.txtGivenValues.Multiline = true;
            this.txtGivenValues.Name = "txtGivenValues";
            this.txtGivenValues.Size = new System.Drawing.Size(457, 891);
            this.txtGivenValues.TabIndex = 6;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(745, 127);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(112, 34);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2237, 1327);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtGivenValues);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtWordsToSolve);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtWordsToSolve;
        private TextBox txtKey;
        private TextBox txtValue;
        private Button btnAdd;
        private Button btnSolve;
        private TextBox txtResults;
        private TextBox txtGivenValues;
        private Button btnClear;
    }
}