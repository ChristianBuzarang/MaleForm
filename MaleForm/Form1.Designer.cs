namespace MaleForm
{
    partial class Form1
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
            this.btnLoadCsv = new System.Windows.Forms.Button();
            this.btnTrainNN = new System.Windows.Forms.Button();
            this.btnEvaluateUser = new System.Windows.Forms.Button();
            this.txtUserLength = new System.Windows.Forms.TextBox();
            this.txtUserGirth = new System.Windows.Forms.TextBox();
            this.rtbResults = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadCsv
            // 
            this.btnLoadCsv.Location = new System.Drawing.Point(646, 177);
            this.btnLoadCsv.Name = "btnLoadCsv";
            this.btnLoadCsv.Size = new System.Drawing.Size(107, 59);
            this.btnLoadCsv.TabIndex = 0;
            this.btnLoadCsv.Text = "Load CSV Dataset";
            this.btnLoadCsv.UseVisualStyleBackColor = true;
            this.btnLoadCsv.Click += new System.EventHandler(this.btnLoadCsv_Click);
            // 
            // btnTrainNN
            // 
            this.btnTrainNN.Location = new System.Drawing.Point(646, 260);
            this.btnTrainNN.Name = "btnTrainNN";
            this.btnTrainNN.Size = new System.Drawing.Size(107, 59);
            this.btnTrainNN.TabIndex = 1;
            this.btnTrainNN.Text = "Train NN Model";
            this.btnTrainNN.UseVisualStyleBackColor = true;
            this.btnTrainNN.Click += new System.EventHandler(this.btnTrainNN_Click);
            // 
            // btnEvaluateUser
            // 
            this.btnEvaluateUser.Location = new System.Drawing.Point(646, 340);
            this.btnEvaluateUser.Name = "btnEvaluateUser";
            this.btnEvaluateUser.Size = new System.Drawing.Size(107, 59);
            this.btnEvaluateUser.TabIndex = 2;
            this.btnEvaluateUser.Text = "Find Global Peers";
            this.btnEvaluateUser.UseVisualStyleBackColor = true;
            this.btnEvaluateUser.Click += new System.EventHandler(this.btnEvaluateUser_Click);
            // 
            // txtUserLength
            // 
            this.txtUserLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserLength.Location = new System.Drawing.Point(645, 52);
            this.txtUserLength.Name = "txtUserLength";
            this.txtUserLength.Size = new System.Drawing.Size(121, 23);
            this.txtUserLength.TabIndex = 3;
            this.txtUserLength.Text = "0";
            // 
            // txtUserGirth
            // 
            this.txtUserGirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserGirth.Location = new System.Drawing.Point(646, 115);
            this.txtUserGirth.Name = "txtUserGirth";
            this.txtUserGirth.Size = new System.Drawing.Size(121, 23);
            this.txtUserGirth.TabIndex = 4;
            this.txtUserGirth.Text = "0";
            // 
            // rtbResults
            // 
            this.rtbResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbResults.Location = new System.Drawing.Point(35, 32);
            this.rtbResults.Name = "rtbResults";
            this.rtbResults.Size = new System.Drawing.Size(563, 355);
            this.rtbResults.TabIndex = 5;
            this.rtbResults.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(643, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Erect Length (CM)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(643, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Erect Girth (CM)";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtbResults);
            this.tabPage1.Controls.Add(this.btnLoadCsv);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnTrainNN);
            this.tabPage1.Controls.Add(this.btnEvaluateUser);
            this.tabPage1.Controls.Add(this.txtUserLength);
            this.tabPage1.Controls.Add(this.txtUserGirth);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Global Metric Explorer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 421);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Raw Data Viewer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(786, 415);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 421);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Progress & Tracker";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(792, 421);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Mindset & Wellness";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadCsv;
        private System.Windows.Forms.Button btnTrainNN;
        private System.Windows.Forms.Button btnEvaluateUser;
        private System.Windows.Forms.TextBox txtUserLength;
        private System.Windows.Forms.TextBox txtUserGirth;
        private System.Windows.Forms.RichTextBox rtbResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

