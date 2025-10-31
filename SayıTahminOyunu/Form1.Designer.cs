namespace SayıTahminOyunu
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblKalanHak = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBaslat = new System.Windows.Forms.Button();
            this.btnTahminEt = new System.Windows.Forms.Button();
            this.txtTahmin = new System.Windows.Forms.TextBox();
            this.lstTahminler = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.MidnightBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "3 Haneli Sayı Tahmin Oyunu";
            // 
            // lblKalanHak
            // 
            this.lblKalanHak.AutoSize = true;
            this.lblKalanHak.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblKalanHak.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKalanHak.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblKalanHak.Location = new System.Drawing.Point(123, 175);
            this.lblKalanHak.Name = "lblKalanHak";
            this.lblKalanHak.Size = new System.Drawing.Size(104, 20);
            this.lblKalanHak.TabIndex = 1;
            this.lblKalanHak.Text = "Kalan Hak :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.AliceBlue;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(123, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tahmininizi Giriniz!";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBaslat
            // 
            this.btnBaslat.BackColor = System.Drawing.Color.SteelBlue;
            this.btnBaslat.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnBaslat.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBaslat.Location = new System.Drawing.Point(70, 116);
            this.btnBaslat.Name = "btnBaslat";
            this.btnBaslat.Size = new System.Drawing.Size(262, 45);
            this.btnBaslat.TabIndex = 0;
            this.btnBaslat.Text = "Başlat / Yeniden Başlat";
            this.btnBaslat.UseVisualStyleBackColor = false;
            this.btnBaslat.Click += new System.EventHandler(this.btnBaslat_Click);
            // 
            // btnTahminEt
            // 
            this.btnTahminEt.AutoEllipsis = true;
            this.btnTahminEt.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTahminEt.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTahminEt.ForeColor = System.Drawing.Color.Azure;
            this.btnTahminEt.Location = new System.Drawing.Point(233, 209);
            this.btnTahminEt.Name = "btnTahminEt";
            this.btnTahminEt.Size = new System.Drawing.Size(121, 42);
            this.btnTahminEt.TabIndex = 2;
            this.btnTahminEt.Text = "Tahmin Et";
            this.btnTahminEt.UseVisualStyleBackColor = false;
            this.btnTahminEt.Click += new System.EventHandler(this.btnTahminEt_Click);
            // 
            // txtTahmin
            // 
            this.txtTahmin.BackColor = System.Drawing.Color.SteelBlue;
            this.txtTahmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTahmin.ForeColor = System.Drawing.Color.AliceBlue;
            this.txtTahmin.Location = new System.Drawing.Point(50, 217);
            this.txtTahmin.Name = "txtTahmin";
            this.txtTahmin.Size = new System.Drawing.Size(177, 27);
            this.txtTahmin.TabIndex = 1;
            this.txtTahmin.Text = "Tahmininizi giriniz.";
            this.txtTahmin.Enter += new System.EventHandler(this.txtTahmin_Enter);
            // 
            // lstTahminler
            // 
            this.lstTahminler.BackColor = System.Drawing.Color.AliceBlue;
            this.lstTahminler.FormattingEnabled = true;
            this.lstTahminler.ItemHeight = 16;
            this.lstTahminler.Location = new System.Drawing.Point(83, 314);
            this.lstTahminler.Name = "lstTahminler";
            this.lstTahminler.Size = new System.Drawing.Size(239, 164);
            this.lstTahminler.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.Controls.Add(this.lstTahminler);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtTahmin);
            this.panel1.Controls.Add(this.lblKalanHak);
            this.panel1.Controls.Add(this.btnTahminEt);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnBaslat);
            this.panel1.Location = new System.Drawing.Point(164, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 535);
            this.panel1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.BackgroundImage = global::SayıTahminOyunu.Properties.Resources._82113680_math_linear_mathematics_education_circle_background_with_geometrical_plots_seamless_background;
            this.ClientSize = new System.Drawing.Size(701, 642);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sayı Tahmin Oyunu";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblKalanHak;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBaslat;
        private System.Windows.Forms.Button btnTahminEt;
        private System.Windows.Forms.TextBox txtTahmin;
        private System.Windows.Forms.ListBox lstTahminler;
        private System.Windows.Forms.Panel panel1;
    }
}

