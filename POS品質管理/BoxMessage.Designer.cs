namespace VerTrans
{
    partial class BoxMessage
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
            this.lb01 = new DevExpress.XtraEditors.LabelControl();
            this.tp05_OK = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb01
            // 
            this.lb01.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb01.Appearance.ForeColor = System.Drawing.Color.White;
            this.lb01.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lb01.Dock = System.Windows.Forms.DockStyle.Top;
            this.lb01.Location = new System.Drawing.Point(0, 0);
            this.lb01.Name = "lb01";
            this.lb01.Padding = new System.Windows.Forms.Padding(10);
            this.lb01.Size = new System.Drawing.Size(569, 55);
            this.lb01.TabIndex = 0;
            this.lb01.Text = "大於10以上請按[數量]處理!!";
            // 
            // tp05_OK
            // 
            this.tp05_OK.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.tp05_OK.Appearance.Options.UseFont = true;
            this.tp05_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.tp05_OK.Location = new System.Drawing.Point(240, 10);
            this.tp05_OK.LookAndFeel.SkinName = "Seven Classic";
            this.tp05_OK.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tp05_OK.Name = "tp05_OK";
            this.tp05_OK.Size = new System.Drawing.Size(93, 43);
            this.tp05_OK.TabIndex = 17;
            this.tp05_OK.Text = "確定";
            this.tp05_OK.Click += new System.EventHandler(this.tp05_OK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tp05_OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 183);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 65);
            this.panel1.TabIndex = 18;
            // 
            // BoxMessage
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.Appearance.Font = new System.Drawing.Font("微軟正黑體", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseBorderColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(21F, 44F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 248);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lb01);
            this.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.Margin = new System.Windows.Forms.Padding(9);
            this.MinimumSize = new System.Drawing.Size(585, 286);
            this.Name = "BoxMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "訊息";
            this.Load += new System.EventHandler(this.BoxMessage_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lb01;
        private DevExpress.XtraEditors.SimpleButton tp05_OK;
        private System.Windows.Forms.Panel panel1;
    }
}