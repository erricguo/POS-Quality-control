using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Threading;

namespace VerTrans
{
    public partial class FileCopyEx : Form
    {
        List<string> FileLockedList = new List<string>();
        public FileCopyEx()
        {
            InitializeComponent();
        }
        List<string> P1 = null;//new List<string>(); 來原路徑及目的路徑
        string Fmsg = "";
        int TotalLines = 0;
        public List<string> SetP1
        {
            set
            {
                P1 = value;
            }
        }
        public string GetMsgInfo
        {
            get { return Fmsg; }
        }
        public int GetTotLines
        {
            get { return TotalLines; }
        }
        /*public string SetDestinationDir
        {
            set { DestinationDir = value; }
        }
        public string SetSourceDir
        {
            set { SourceDir = value; }
        }*/
        
        private void FileCopyEx_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lb_Destination.Text = "";
            lb_Source.Text = "";
        }

        private void DoCopy()
        {

            try
            {
                SetPBC1(PBC1, P1);
                int i = 0;
                int max = P1.Count;
                
                PBC1.Properties.Maximum = max;
                
                for (i = 0; i < P1.Count; i++)//0:安裝資料夾 1:PKG 2:System 3:MODI
                {
                    lb_Source.Text = P1[i];
                    //lb_Destination.Text = P1[i][1] + @"\" + P2[i][j];
                    StreamReader txtRe = new StreamReader(P1[i]);//打开当前文件
                    string[] txtlist = txtRe.ReadToEnd().Split('\n');//txtlist.Length就是行数
                    TotalLines += txtlist.Length;
                    lb_Destination.Text = TotalLines.ToString();
                    Application.DoEvents();
                    PBC1.PerformStep();
                }
                timer2.Enabled = true;
            }
            catch (System.Exception ex)
            {             
                timer2.Enabled = true;
            }
            
        }
        private void SetPBC1(ProgressBarControl p, List<string> m)
        {
            //设置一个最小值
            p.Properties.Minimum = 0;
            //设置一个最大值
            p.Properties.Maximum = m.Count;
            //设置步长，即每次增加的数
            p.Properties.Step = 1;
            //设置进度条的样式
            p.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            p.Position = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            DoCopy();
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            this.DialogResult = DialogResult.OK;
        }

    }
}
