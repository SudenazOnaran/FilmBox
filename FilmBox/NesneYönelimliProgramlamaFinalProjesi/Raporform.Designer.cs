namespace NYPfinalodev
{
    partial class Raporform
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
            this.istatistikdata = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.btnenyuksek = new System.Windows.Forms.Button();
            this.btnencokizlenen = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.istatistikdata)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // istatistikdata
            // 
            this.istatistikdata.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.istatistikdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.istatistikdata.Location = new System.Drawing.Point(210, 137);
            this.istatistikdata.Name = "istatistikdata";
            this.istatistikdata.RowHeadersWidth = 51;
            this.istatistikdata.RowTemplate.Height = 24;
            this.istatistikdata.Size = new System.Drawing.Size(593, 429);
            this.istatistikdata.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(375, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(240, 77);
            this.button3.TabIndex = 2;
            this.button3.Text = "En Çok Değerlendirilen Türler ";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnenyuksek
            // 
            this.btnenyuksek.BackColor = System.Drawing.Color.Red;
            this.btnenyuksek.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnenyuksek.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnenyuksek.ForeColor = System.Drawing.Color.Black;
            this.btnenyuksek.Location = new System.Drawing.Point(694, 23);
            this.btnenyuksek.Name = "btnenyuksek";
            this.btnenyuksek.Size = new System.Drawing.Size(190, 75);
            this.btnenyuksek.TabIndex = 1;
            this.btnenyuksek.Text = "Puanı En Yüksekler";
            this.btnenyuksek.UseVisualStyleBackColor = false;
            this.btnenyuksek.Click += new System.EventHandler(this.btnizle_Click);
            // 
            // btnencokizlenen
            // 
            this.btnencokizlenen.BackColor = System.Drawing.Color.Red;
            this.btnencokizlenen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnencokizlenen.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnencokizlenen.ForeColor = System.Drawing.Color.Black;
            this.btnencokizlenen.Location = new System.Drawing.Point(99, 20);
            this.btnencokizlenen.Name = "btnencokizlenen";
            this.btnencokizlenen.Size = new System.Drawing.Size(177, 78);
            this.btnencokizlenen.TabIndex = 0;
            this.btnencokizlenen.Text = "En çok izlenenler";
            this.btnencokizlenen.UseVisualStyleBackColor = false;
            this.btnencokizlenen.Click += new System.EventHandler(this.btnhome_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnenyuksek);
            this.panel1.Controls.Add(this.btnencokizlenen);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Location = new System.Drawing.Point(8, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1014, 116);
            this.panel1.TabIndex = 20;
            // 
            // Raporform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(14)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1023, 601);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.istatistikdata);
            this.Name = "Raporform";
            this.Text = "Raporform";
            ((System.ComponentModel.ISupportInitialize)(this.istatistikdata)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView istatistikdata;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnenyuksek;
        private System.Windows.Forms.Button btnencokizlenen;
        private System.Windows.Forms.Panel panel1;
    }
}