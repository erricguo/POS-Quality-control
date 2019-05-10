using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace POS品質管理
{
    public partial class ShowInfoBox : DevExpress.XtraEditors.XtraForm
    {
        public ShowInfoBox()
        {
            InitializeComponent();
        }
        public string SetMemoInfo
        {
            set { memo1.Text = value; }
        }
        public string SetCaption
        {
            set { this.Text = value; }
        }

        private void ShowInfoBox_Load(object sender, EventArgs e)
        {
            memo1.DeselectAll();
            //memo1.SelectionStart = 0;// memo1.SelectionStart + memo1.Text.Length;
            //memo1.SelectionLength = 0;
        }
    }
}