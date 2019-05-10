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
    public partial class Search : DevExpress.XtraEditors.XtraForm
    {
        public Search()
        {
            InitializeComponent();
        }
        string FFilter = "";
        Font ft1 = new Font("微軟正黑體", 13, FontStyle.Bold);
        public string SetFilter
        {
            set { memo1.Text = value; }
        }
        public string GetFilter
        {
            get { return FFilter; }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string s = tbName.Text.Trim().Substring(0, 5);
            switch (tbFilter.SelectedIndex)
            {
                case 6: //%LIKE%
                    s += " Like " + " '%" + tbValue.Text + "%'";
                    break;
                case 7: //LIKE%
                    s += " Like " + " '" + tbValue.Text + "%'";
                    break;
                case 8: //%LIKE
                    s += " Like " + " '%" + tbValue.Text + "'";
                    break;
                default:
                    s += " " + tbFilter.Text + " '" + tbValue.Text + "'";
                    break; ;

            }                        
            if (rg01.Enabled)
            {
                s = rg01.Properties.Items[rg01.SelectedIndex].Value.ToString() +" "+ s;
            }
            memo1.Text += " " + s+"\r\n";       
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            memo1.Text = "";
        }

        private void memo1_EditValueChanged(object sender, EventArgs e)
        {
            if (memo1.Text == "")
            {
                rg01.Enabled = false;
            }
            else
            {
                rg01.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FFilter = memo1.Text;
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }

        private void tbName_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            try
            {
                if (e.State != DrawItemState.Selected)
                {
                    e.Cache.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
                    e.Cache.DrawString(e.Item.ToString(), ft1, new SolidBrush(Color.FromArgb(255, 232, 166)), e.Bounds, e.Appearance.GetStringFormat());
                }
                else
                {
                    e.Cache.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
                    //e.Cache.FillRectangle(new SolidBrush(Color.FromArgb(255, 232, 166)), e.Bounds);
                    e.Cache.DrawString(e.Item.ToString(), ft1, new SolidBrush(Color.White), e.Bounds, e.Appearance.GetStringFormat());
                }
                e.Handled = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void tbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}