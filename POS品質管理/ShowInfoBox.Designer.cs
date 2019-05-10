namespace POS品質管理
{
    partial class ShowInfoBox
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
            this.memo1 = new DevExpress.XtraEditors.MemoEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.memo1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memo1
            // 
            this.memo1.EditValue = "";
            this.memo1.Location = new System.Drawing.Point(12, 12);
            this.memo1.Name = "memo1";
            this.memo1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.memo1.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.memo1.Properties.Appearance.Options.UseBackColor = true;
            this.memo1.Properties.Appearance.Options.UseFont = true;
            this.memo1.Properties.ReadOnly = true;
            this.memo1.Size = new System.Drawing.Size(582, 265);
            this.memo1.TabIndex = 117;
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::POS品質管理.Properties.Resources.Greencheckboxwithcheckmark;
            this.btnOK.Location = new System.Drawing.Point(254, 280);
            this.btnOK.LookAndFeel.SkinName = "Seven Classic";
            this.btnOK.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.btnOK.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnOK.LookAndFeel.UseWindowsXPTheme = true;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 46);
            this.btnOK.TabIndex = 118;
            this.btnOK.Text = "確定";
            // 
            // ShowInfoBox
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 330);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.memo1);
            this.Name = "ShowInfoBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ShowInfoBox";
            this.Load += new System.EventHandler(this.ShowInfoBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memo1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memo1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}