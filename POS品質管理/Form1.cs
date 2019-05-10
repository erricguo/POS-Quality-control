using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraCharts;
using System.IO;
using VerTrans;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraReports.UI;
using System.Diagnostics;
using Nini.Config;
using Microsoft.VisualBasic.Devices;
using DevExpress.XtraPrinting.Export.Pdf;


namespace POS品質管理
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static Computer myComputer = new Computer();
        public static string Mydocument = myComputer.FileSystem.SpecialDirectories.MyDocuments;
        public static string DirPOSQTSystem = Mydocument + @"\POSQTSystem";
        public static string ConfigPath = Mydocument + @"\POSQTSystem\Config.ini";
        string[] FPrograms = new string[] { "DATASET.EXE", "DSCPOSB01.EXE", "DSCPOSB01_31.EXE", "DSCPOSDEVICE.EXE", "DSCPOSNET01.EXE", "DSCPOSNET02.DLL", "DSCPOSSETUP.EXE", "JEDIINSTALLER.EXE", "POSINSTALL.EXE", "POSMAIN.EXE", "PROJECT1.EXE", "WHOCALLS.EXE", "POSEI11.DLL", "POSEI10.DLL", "POSEI09.DLL", "POSEI08.DLL", "POSEI05.DLL", "POSEI04.DLL", "POSEI03.DLL", "POSEI02.DLL", "POSEI01.DLL", "POSRI04.DLL", "POSRI03.DLL", "POSRI02.DLL", "POSRI01.DLL", "POSQI29.DLL", "POSQI28.DLL", "POSQI27.DLL", "POSQI26.DLL", "POSQI25.DLL", "POSQI24.DLL", "POSQI23.DLL", "POSQI22.DLL", "POSQI21.DLL", "POSQI20.DLL", "POSQI19.DLL", "POSQI18.DLL", "POSQI17.DLL", "POSQI16.DLL", "POSQI15.DLL", "POSQI14.DLL", "POSQI13.DLL", "POSQI12.DLL", "POSQI11.DLL", "POSQI09.DLL", "POSQI08.DLL", "POSQI07.DLL", "POSQI06.DLL", "POSQI05.DLL", "POSQI04.DLL", "POSQI02.DLL", "POSQI01.DLL", "POSFI20.DLL", "POSFI19.DLL", "POSFI17.DLL", "POSFI16.DLL", "POSFI15.DLL", "POSFI14.DLL", "POSFI13.DLL", "POSFI12.DLL", "POSFI11.DLL", "POSFI10.DLL", "POSFI09.DLL", "POSFI08.DLL", "POSFI07.DLL", "POSFI06.DLL", "POSFI05.DLL", "POSFI04.DLL", "POSFI03.DLL", "POSFI02.DLL", "POSVI11.DLL", "POSVI10.DLL", "POSVI09.DLL", "POSVI08.DLL", "POSVI07.DLL", "POSVI06.DLL", "POSVI05.DLL", "POSVI04.DLL", "POSVI03.DLL", "POSVI02.DLL", "POSCI07.DLL", "POSCI06.DLL", "POSCI05.DLL", "POSCI04.DLL", "SYNCDBMT.EXE", "POSFI01.DLL", "POSIMG.DLL", "POSCI01.DLL", "POSPI01.DLL", "POSMI01.DLL", "POSVI01.DLL", "POSMAINGP.EXE" };
        string FFilter = "";
        string FBeginDate = "";
        string FEndDate = "";
        List<string> fileList = new List<string>();
        Dictionary<string, Dictionary<string, int>> ChartLineList = new Dictionary<string, Dictionary<string, int>>();
        int TotalLines = 0;
        string[] extfilter = new string[] {".dcu",".bmp",".bpl",".dll",".db",".scc",".jpg" };
        //Dictionary<string, int> VerTotLineList = new Dictionary<string, int>();
        List<LineNode> LNList = new List<LineNode>();
        Dictionary<string, string> VerNoMatchDic = new Dictionary<string, string>();
        string FXA009 = "";
        public class LineNode
        {
            public string mDate = "";
            public string mVer = "";
            public int mLineCount = 0;
            public LineNode(string xDate, string xVer, int xLineCount)
            {
                mDate = xDate; mVer = xVer; mLineCount = xLineCount;
            }
        }
        public class YM
        {
            string mYear = "";
            string mMonth = "";
            public YM(string xy,string xm)
            {
                mYear =xy;
                mMonth = xm;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Init();            
        }
        private void Init()
        {
            this.Text = "POS品質管理 Ver " + GetVersion() + " @ ErricGuo ";
            // TODO: 這行程式碼會將資料載入 'pMSDataSet.POSXA' 資料表。您可以視需要進行移動或移除。
            pOSXABindingSource.Filter = "XA002 >= '" + DateTime.Now.ToString("yyyyMM") + "'";
            this.pOSXATableAdapter.Fill(this.pMSDataSet.POSXA);
            gridView1.BestFitColumns();
            gridView1.Columns["XA002"].DisplayFormat.Format = new BaseFormatter();
            gridView1.Columns["XA012"].DisplayFormat.Format = new BaseFormatter();
            gridView1.Columns["XA009"].DisplayFormat.Format = new BaseFormatter();
            textEdit5.Properties.DisplayFormat.Format = new BaseFormatter();
            tbDate.Properties.DisplayFormat.Format = new BaseFormatter();
            gridView1.Columns["XA008"].Width = 150;
            gridView1.Columns["XA010"].Width = 120;
            gridView1.Columns["XA011"].Width = 150;
            gridView1.Columns["XA012"].Width = 120;
            FBeginDate = CheckBeginDate();
            FEndDate = CheckEndDate();
            GetXA010Info();
            InitConfig();
            ReadConfig();
            tbBeginDate.DateTime = DateTime.Now;
            tbEndDate.DateTime = DateTime.Now;
            /*VerNoMatchDic.Add("POS_21", "2.1");
            VerNoMatchDic.Add("POS_31", "3.1");
            VerNoMatchDic.Add("POS_32", "3.2");
            VerNoMatchDic.Add("POS_33", "3.3");
            VerNoMatchDic.Add("POS_34", "3.4");
            VerNoMatchDic.Add("POS_GP10", "10.1");
            VerNoMatchDic.Add("POS_GP20", "11.1");
            VerNoMatchDic.Add("POS_GP30", "13.1");*/
            SetXA009();
        }
        public static string GetVersion()
        {
            string s = "";
            try
            {
                s += System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (Exception)
            {
                s = "開發程式階段";
            }
            return s;
        }
        /// <summary>
        /// 產生亂數。
        /// </summary>
        /// <param name="start">起始數字。</param>
        /// <param name="end">結束數字。</param>        
        private string GetTime(int xtype)
        {
            switch (xtype)
            {
                case 1:
                    return DateTime.Now.ToString("yyyyMMdd");            	
                default:
                    return "";
            }
        }
        private void InitConfig()
        {
            if (!isDirectory(DirPOSQTSystem))
            {
                Directory.CreateDirectory(DirPOSQTSystem);
            }
            if (!File.Exists(ConfigPath))
            {
                File.Create(ConfigPath).Close();
            }
            IConfigSource source = new IniConfigSource(ConfigPath);
           /* if (source.Configs["Date"] == null)
            {
                source.Configs.Add("Date");
                source.Configs["Date"].Set("Today", GetTime(1));
            }*/
            if (source.Configs["Lines"] == null)
            {
                source.Configs.Add("Lines");
                for (int i = 0; i < tbVer.Properties.Items.Count; i++)
                {
                    source.Configs["Lines"].Set(tbVer.Properties.Items[i].ToString(),GetTime(1)+"|0");
                }
            }
            source.Save();            
        }
        private void ReadConfig()
        {
            IConfigSource source = new IniConfigSource(ConfigPath);
            for (int i = 0; i < source.Configs["Lines"].GetKeys().Length; i++)
            {
                string node = source.Configs["Lines"].GetKeys()[i];
                string[] Tmp = source.Configs["Lines"].GetString(node).Split('|');
                if (Tmp.Length > 1)
                {
                    LNList.Add(new LineNode( node, Tmp[0].ToString(), Int32.Parse(Tmp[1].ToString())));
                   //VerTotLineList.Add(Tmp[0].ToString(), Int32.Parse(Tmp[1].ToString()));
                }                
            }                        
        }
        private void SaveConfig(string xNode, string xVer, string xDate, int xLineCount)
        {
            IConfigSource source = new IniConfigSource(ConfigPath);
            /*foreach (LineNode ln in LNList)
            {
                source.Configs["Lines"].Set(ln.mDate,ln.mVer+"|"+ln.mLineCount.ToString());
            }*/
            source.Configs[xNode].Set(xVer, xDate + "|" + xLineCount.ToString());
            source.Save();                     
        }
        private void ShowBoxMessage(string xTitle,string xMsg)
        {
            BoxMessage b = new BoxMessage();
            b.SetTitle = xTitle;
            b.SetMsg = xMsg;
            b.ShowDialog();
        }
        private int GetVerLines(string xNode,string xVer)
        {
            IConfigSource source = new IniConfigSource(ConfigPath);
            string[] tmp = source.Configs[xNode].GetString(xVer).Split('|');
            if (tmp.Length > 1)
            {
                if (tmp[0] == GetTime(1))
                {
                    return Int32.Parse(tmp[1]);
                }
                else
                    return 0;
            }
            else
                return 0;
            //int mcount = source.Configs[xNode].GetInt(xVer);            
            //return source.Configs[xNode].GetInt(xVer);
        }
        public static bool isDirectory(string p)//目錄是否存在
        {
            if (p == "")
            {
                return false;
            }
            return System.IO.Directory.Exists(p);
        }
        private int GetRandom(int xMax,int offset)
        {
            Random Counter = new Random(Guid.NewGuid().GetHashCode());
            return (Counter.Next(xMax)+offset);
        }
        private void InserValue()
        {
            string SQL = "";
            SqlConnection conn = null;
            conn = new SqlConnection(makeConnectString(@"10.40.13.42", "", "sa", "zxcvbnm,./"));
            try
            {
                conn.Open();
                SQL = "INSERT INTO PMS.dbo.POSXA(XA002,XA009,XA010) VALUES(@XA002, @XA009,@XA010)";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                string mxa002 = "2013" + string.Format("{0:00}", GetRandom(12, 1)) + string.Format("{0:00}", GetRandom(28, 1));
                string mxa009 = tbVer.Properties.Items[GetRandom(6, 0)].ToString();
                string mxa010 = FPrograms[GetRandom(FPrograms.Length, 0)];
                cmd.Parameters.Add("@XA002", mxa002);
                cmd.Parameters.Add("@XA009", mxa009);
                cmd.Parameters.Add("@XA010", mxa010);
                for (int i = 0; i < 800;i++ )
                {
                    mxa002 = "2013" + string.Format("{0:00}",GetRandom(12, 1)) + string.Format("{0:00}",GetRandom(28, 1));
                    //mxa009 = tbVer.Properties.Items[GetRandom(6, 0)].ToString();
                    mxa010 = FPrograms[GetRandom(FPrograms.Length, 0)];
                    cmd.Parameters["@XA002"].Value = mxa002;
                    cmd.Parameters["@XA009"].Value = "POS_GP20";
                    cmd.Parameters["@XA010"].Value = mxa010;
                    cmd.ExecuteNonQuery();
                }
                cmd.Cancel();
                conn.Close();
              /*  string mxa002 ="2013"+GetRandom(12,1).ToString() + GetRandom(28,1).ToString();
                string mxa009 = tbVer.Properties.Items[GetRandom(6, 0)].ToString();
                string mxa010 = FPrograms[GetRandom(FPrograms.Length,0)];
                cmd.Parameters.Add("@XA002", mxa002);
                cmd.Parameters.Add("@XA009", mxa009);
                cmd.Parameters.Add("@XA010", mxa010);
                cmd.ExecuteNonQuery();*/
            }
            catch (Exception ex)
            {
                ShowBoxMessage("錯誤", ex.Message);
            }
        }
        private void RefreshPOSXA(string type)
        {
            pMSDataSet.Clear();
            if (type =="A")
            {
                this.pOSXATableAdapter.Fill(this.pMSDataSet.POSXA);
            }
            else if (type =="B")
            {
                this.pOSXATableAdapter.FillByStatistics(this.pMSDataSet.POSXA);
            }
            
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshPOSXA("A");
                Search s1 = new Search();
                s1.SetFilter = FFilter;
                if (s1.ShowDialog() == DialogResult.OK)
                {
                    pOSXABindingSource.Filter = s1.GetFilter;
                    FFilter = s1.GetFilter;
                    tabcontrol.SelectedTabPage = tab02;
                }     
            }
            catch (System.Exception ex)
            {
            	
            }
       
        }
        public void GetSQL(string xid, string xpw, string xip, string msql)
        {
            string SQL = "";
            SqlConnection conn = null;
            SQL = msql;
            conn = new SqlConnection(makeConnectString(xip, "", xid, xpw));
            DataTable dt = new DataTable("123");
            dt.Columns.Add("TA001", typeof(string));
            AddColumn("交易日期", "TA001", 100);
            try
            {
                conn.Open();
                {
                    SqlCommand myCommand = null; SqlDataReader myDataReader = null;
                    myCommand = new SqlCommand(SQL, conn);
                    myDataReader = myCommand.ExecuteReader();

                    while (myDataReader.Read())
                    {
                        dt.Rows.Add(new object[] { myDataReader["TA001"].ToString()});
                    }
                    conn.Close();
                    myCommand.Cancel();
                    myDataReader.Close();
                }
                gridControl1.DataSource = dt;
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            }
            catch (Exception ex)
            {
                ShowBoxMessage("錯誤", ex.Message);
            }
        }
        public static string makeConnectString(string xFServer, string xFDataBase, string xFUserID, string xFPassword)
        {
            bool xFSecurity_info = false;
            string xFIntegrated_Security = "";
            string mB = "";
            string mRes = "";

            if (xFUserID == "")
            {
                xFSecurity_info = false;
                xFIntegrated_Security = "SSPI";
                mB = "False";
            }
            else
            {
                xFSecurity_info = true;
                xFIntegrated_Security = "";
                mB = "true";
            }

            mRes = /*"Provider=" + FProvider +
                      ";*/
            "Persist Security Info=" + xFSecurity_info +
                      ";Integrated Security=" + xFIntegrated_Security +
                      ";Data Source=" + xFServer +
                      ";Initial Catalog=" + xFDataBase +
                      ";User ID=" + xFUserID +
                      ";Password=" + xFPassword;
            return mRes;
        }
        public void AddColumn(string xCaption, string xFieldName, int xWidth)
        {//DevExpress.XtraGrid.Columns.GridColumn
            DevExpress.XtraGrid.Columns.GridColumn g = new DevExpress.XtraGrid.Columns.GridColumn();
            g.AppearanceCell.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            g.AppearanceCell.Options.UseFont = true; 
            g.AppearanceHeader.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            g.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            g.AppearanceHeader.Options.UseFont = true;
            g.AppearanceHeader.Options.UseForeColor = true;
            g.AppearanceHeader.Options.UseTextOptions = true;
            g.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            
            if (xFieldName == "Index")
            {
                g.AppearanceCell.Options.UseTextOptions = true;
                g.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            g.Caption = xCaption;
            g.FieldName = xFieldName;
            g.Name = xFieldName;
            g.OptionsColumn.AllowEdit = false;
            g.Visible = true;
            g.VisibleIndex = 0;
            g.Width = xWidth;
            gridView1.Columns.Add(g);
        }
        public class BaseFormatter : IFormatProvider, ICustomFormatter
        {
            // The GetFormat method of the IFormatProvider interface.
            // This must return an object that provides formatting services for the specified type.
            public object GetFormat(Type format)
            {
                if (format == typeof(ICustomFormatter)) return this;
                else return null;
            }
            // The Format method of the ICustomFormatter interface.
            // This must format the specified value according to the specified format settings.
            public string Format(string format, object arg, IFormatProvider provider)
            {
                if (arg.ToString() == "")
                    return "";
                if (format == null)
                {
                    if (arg is IFormattable)
                        return ((IFormattable)arg).ToString(format, provider);
                    else
                        return arg.ToString();
                }
                if (format == "A")
                {
                    string s = arg.ToString().Substring(0, 4) + "/" + arg.ToString().Substring(4, 2) + "/" + arg.ToString().Substring(6, 2);
                    return s;
                }
                else if (format =="B")
                {
                    //0~3
                    if (arg.ToString() == "1")
                        return "1.輕微";
                    else if (arg.ToString() == "2")
                        return "2.一般";
                    else if (arg.ToString() == "3")
                        return "3.嚴重";
                    else if (arg.ToString() == "4")
                        return "4.致命";
                    else
                        return arg.ToString();
                }
                else if (format == "C")
                {
                    //0~3
                    if (arg.ToString() == "2.1")
                        return "POS_21";
                    else if (arg.ToString() == "3.1")
                        return "POS_31";
                    else if (arg.ToString() == "3.2")
                        return "POS_32";
                    else if (arg.ToString() == "3.3")
                        return "POS_33";
                    else if (arg.ToString() == "3.4")
                        return "POS_34";
                    else if (arg.ToString().StartsWith("10."))
                        return "POS_GP10";
                    else if (arg.ToString().StartsWith("11."))
                        return "POS_GP20";
                    else if (arg.ToString().StartsWith("13."))
                        return "POS_GP30";
                    else if (arg.ToString().StartsWith("14."))
                        return "POS_GP40";
                    else
                        return arg.ToString();
                }
                else
                    return arg.ToString();
            }
        }
        private void pOSXABindingSource_CurrentChanged(object sender, EventArgs e)
        {
            GetXA010Info();
        }
        private void GetXA010Info()
        {
            if (tbXA010.Text.Trim() == "")
            {
                return;
            }
            
            string[] sp = tbXA010.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            DataTable dt = new DataTable("456");
            dt.Columns.Add("XA010", typeof(string));
            for (int i = 0; i < sp.Length; i++)
            {
                dt.Rows.Add(new object[] { sp[i] });
            }
            gridControl2.DataSource = dt;
            gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tabcontrol.SelectedTabPage = tab01;
            }
            else
            {
                if (e.Clicks == 2)
                {
                    //object cellValue = e.CellValue;
                    if (e.Column.FieldName == "XA008" || e.Column.FieldName == "XA010" || e.Column.FieldName == "XA011")
                    {
                        ShowInfoBox sb1 = new ShowInfoBox();
                        sb1.SetMemoInfo = e.CellValue.ToString();
                        sb1.SetCaption = e.Column.Caption;
                        sb1.ShowDialog();
                    }
                    else
                    {
                        Clipboard.SetData(DataFormats.Text, e.CellValue.ToString());
                    }
                }
            }

        }
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(FBeginDate) >= Int32.Parse(FEndDate))
            {
                ShowBoxMessage("錯誤", "起始日期不可大於結束日期!!");
                return;
            }

            IConfigSource source = new IniConfigSource(ConfigPath);
            if (source.Configs["Lines"] == null)
            {
                source.Configs.Add("Lines");
                for (int i = 0; i < tbVer.Properties.Items.Count; i++)
                {
                    source.Configs["Lines"].Set(tbVer.Properties.Items[i].ToString(), GetTime(1) + "|0");
                }
            }

            if (source.Configs["Lines"].GetString(tbVer.Text, "Not Exists") == "Not Exists")
            {
                source.Configs["Lines"].Set(tbVer.Text, GetTime(1) + "|0");
                source.Save(); 
            }
             

            RefreshPOSXA("B");
           /* if (FXA009.ToString() == "2.1")
                FXA009 = "POS_21";
            else if (FXA009.ToString() == "3.1")
                FXA009 = "POS_31";
            else if (FXA009.ToString() == "3.2")
                FXA009 = "POS_32";
            else if (FXA009.ToString() == "3.3")
                FXA009 = "POS_33";
            else if (FXA009.ToString() == "3.4")
                FXA009 = "POS_34";
            else if (FXA009.ToString() == "10.1")
                FXA009 = "POS_GP10";
            else if (FXA009.ToString() == "11.1")
                FXA009 = "POS_GP20";*/

            string chklistStr = "";
            /*if (chklist01.Items.Count > 0)
            {
                chklistStr = " AND (";
            }*/
            for (int i = 0; i <= chklist01.Items.Count - 1;i++ )
            {
                
                if (chklist01.Items[i].CheckState == CheckState.Checked)
                {
                    if (chklistStr == "")
                    {
                        chklistStr = " AND ( XA004 = '" + chklist01.Items[i].Value + "'";
                    }
                    else
                    {
                        chklistStr += " OR XA004 = '" + chklist01.Items[i].Value + "'";
                    }
                    
                }
            }
            if (chklistStr != "")
            {
                chklistStr += " )";
            }


            /*pOSXABindingSource.Filter = "XA002 >= '" + FBeginDate + "26'" + " AND XA002 <= " + "'" + FEndDate + "25'" +
                                        " AND XA014 = '" + FXA009 + "'" + " AND (XA004 = 'AP01' OR XA004 = 'AP02' )" +
                                        " AND XA009 = XA014";*/

            pOSXABindingSource.Filter = "XA002 >= '" + FBeginDate + "26'" + " AND XA002 <= " + "'" + FEndDate + "25'" +
                                        " AND XA014 = '" + FXA009 + "'" + chklistStr +
                                        " AND XA009 = XA014";
            SetGrid3();
            SetGrid4();
            //excel();                                
        }
        private string GetVerPath()
        {
            string path = @"\\10.40.13.42\WorkingFolder\Standard_POS";
            if (tbVer.Text.EndsWith("21"))
                path += "21";
            else if (tbVer.Text.EndsWith("31"))
                path += "31";
            else if (tbVer.Text.EndsWith("32"))
                path += "32";
            else if (tbVer.Text.EndsWith("33"))
                path += "33";
            else if (tbVer.Text.EndsWith("34"))
                path += "34";
            else if (tbVer.Text.EndsWith("GP10"))
                path += "GP10";
            else if (tbVer.Text.EndsWith("GP20"))
                path += "GP20";
            else if (tbVer.Text.EndsWith("GP30"))
                path += "GP30";
            else if (tbVer.Text.EndsWith("GP40"))
                path += "GP40";
            else
                path = "";

            if (path != "")
            {
                path += @"\POS\Source";
            }
            return path;
            
        }
        private string CheckBeginDate()
        {
            string[] s = tbBeginDate.Text.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length <= 1)
            {
                return "";
            }

            int mMonth = Int32.Parse(s[1]);
            int mYear = Int32.Parse(s[0]);
            if (mMonth == 1)
            {
                mMonth = 12;
                mYear -= 1;
            }
            else
            {
                mMonth -= 1;
            }

            return string.Format("{0:0000}", (mYear)) + string.Format("{0:00}", (mMonth));            
        }
        private void tbBeginDate_EditValueChanged(object sender, EventArgs e)
        {
            
           FBeginDate = CheckBeginDate();
        }
        private string CheckEndDate()
        {
            string[] s = tbEndDate.Text.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length <= 1)
            {
                return "";
            }

            /*string yyyy = s[0];
            string MM = string.Format("{0:00}", Int32.Parse(s[1]));
            if (Int32.Parse(MM) + 1 > 12)
            {
                yyyy = (Int32.Parse(yyyy) + 1).ToString();
                MM = "01";
            }
            else
            {
                MM = string.Format("{0:00}", (Int32.Parse(MM) + 1));
            }
            return yyyy + MM;*/
            return s[0] + string.Format("{0:00}", (Int32.Parse(s[1])));  
        }
        private void tbEndDate_EditValueChanged(object sender, EventArgs e)
        {
            FEndDate = CheckEndDate();
        }
        private bool SetGrid3()
        {
            DataTable dt = new DataTable("456");
            dt.Columns.Add("XA010", typeof(string));
            dt.Columns.Add("XA010C", typeof(int));
            List<string> tmp = new List<string>();       
            for (int i = 0; i < gridView1.RowCount;i++ )
            {
                tmp.Add(gridView1.GetRowCellValue(i, "XA010").ToString());
            }
         
            Dictionary<string, int> dic = new Dictionary<string, int>();
            for (int i = 0; i < tmp.Count;i++ )
            {
                string[] s = tmp[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < s.Length;j++ )
                {
                    if (dic.ContainsKey(s[j]))
                    {
                        dic[s[j]]++;
                    }
                    else
                    {
                        dic.Add(s[j], 1);
                    }
                }                
            }

            var items = from pair in dic
                        orderby pair.Value descending
                        select pair;
            chartControl1.Series[0].Points.Clear();
            foreach (KeyValuePair<string, int> item in items)
            {
                dt.Rows.Add(new object[] { item.Key, item.Value });
                if (chartControl1.Series[0].Points.Count < 5)
                {
                    chartControl1.Series[0].Points.Add(new SeriesPoint(item.Key, item.Value));
                }
                
            }
            gridControl3.DataSource = dt;
            gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView3.BestFitColumns();

            if (gridView3.RowCount > 0)
            {
                string path = GetVerPath();
                if (path != "")
                {
                    fileList.Clear();
                    ChkFileLines(path);
                    DirSearch(path);
                    if ((TotalLines = GetVerLines("Lines", tbVer.Text)) == 0)
                    {
                        FileCopyEx f = new FileCopyEx();
                        f.SetP1 = fileList;
                        f.ShowDialog();
                        TotalLines = f.GetTotLines;
                        SaveConfig("Lines", tbVer.Text, GetTime(1), TotalLines);
                    }
                }
            }
            chartControl1.Titles[0].Text = "POS程式修改次數統計 " + tbBeginDate.Text + " ~ " + tbEndDate.Text + "\r\n";
            return true;
        }
        private void SetGrid4()
        {
            DataTable dt = new DataTable("456");
            dt.Columns.Add("Year", typeof(string));
            dt.Columns.Add("Month", typeof(string));
            dt.Columns.Add("BugCount", typeof(int));
            Dictionary<string, Dictionary<string, int>> tmp2 = new Dictionary<string, Dictionary<string, int>>();     
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                string mY = gridView1.GetRowCellValue(i, "XA002").ToString().Substring(0, 4);
                string mM = gridView1.GetRowCellValue(i, "XA002").ToString().Substring(4, 2);

                if (tmp2.ContainsKey(mY))
                {
                    if (tmp2[mY].ContainsKey(mM))
                    {
                        tmp2[mY][mM]++;
                    }
                    else
                        tmp2[mY].Add(mM, 1);
                }
                else
                {
                    Dictionary<string, int> c = new Dictionary<string, int>();
                    c.Add(mM, 1);
                    tmp2.Add(mY, c);
                }
            }
            ChartLineList = tmp2;
            
            for (int i = 0; i < chartControl2.Series.Count;i++ )
            {
                chartControl2.Series[i].Points.Clear();
            }
            int k = 0;
            var items = from pair in ChartLineList
                        orderby pair.Key ascending
                        select pair;

            foreach (KeyValuePair<string, Dictionary<string, int>> item in items)
            {
                if (chartControl2.Series.Count - 1 < k)
                {
                    Series s = new Series();
                    chartControl2.Series.Add(s);
                }
                chartControl2.Series[k].LegendText = item.Key + "年";

                var items2 = from pair in ChartLineList[item.Key]
                            orderby pair.Key ascending
                            select pair;
                foreach (KeyValuePair<string, int> item2 in items2)
                {
                    double db = CalcPDR(item2.Value);
                    chartControl2.Series[k].Points.Add(new SeriesPoint(item2.Key, string.Format("{0:f3}", db)));
                    dt.Rows.Add(new object[] { item.Key, item2.Key, item2.Value });
                }
                k++;
            }
            for (int i = 0; i < chartControl2.Series.Count; i++)
            {
                if (chartControl2.Series[i].Points.Count <= 0)
                    chartControl2.Series[i].Visible = false;
                else
                    chartControl2.Series[i].Visible = true;
            }
            gridControl4.DataSource = dt;
            gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView4.BestFitColumns();
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;
                    //NImageExporter imageExporter = chartControl.ImageExporter;
                    switch (fileExtenstion)
                    {
                        case ".xls":
                            gridControl3.ExportToXls(exportFilePath);
                            break;
                        case ".xlsx":
                            gridControl3.ExportToXlsx(exportFilePath);
                            break;
                        case ".rtf":
                            gridControl3.ExportToRtf(exportFilePath);
                            break;
                        case ".pdf":
                            gridControl3.ExportToPdf(exportFilePath);
                            break;
                        case ".html":
                            gridControl3.ExportToHtml(exportFilePath);
                            break;
                        case ".mht":
                            gridControl3.ExportToMht(exportFilePath);
                            break;
                        default:
                            break;
                    }

                }
            }
        }
        private double CalcPDR(int BugCount)
        {
           /* List<int> PDR = new List<int>();
            PDR.Add(0); PDR.Add(0); PDR.Add(0); PDR.Add(0);
            for (int i = 0; i < gridView1.RowCount;i++ )
            {
                if (gridView1.GetRowCellValue(i, "XA014").ToString() == "1")
                    PDR[0]++;
                else if (gridView1.GetRowCellValue(i, "XA014").ToString() == "2")
                    PDR[1]++;
                else if (gridView1.GetRowCellValue(i, "XA014").ToString() == "3")
                    PDR[2]++;
                else if (gridView1.GetRowCellValue(i, "XA014").ToString() == "4")
                    PDR[3]++;                                
            }           
            double PDRKLOC = (PDR[0] * 1.0 / 4 + PDR[0] * 1.0 * 1 + PDR[0] * 1.0 * 2 + PDR[0] * 1.0 * 3) / (TotalLines /1000);*/
            //double PDRKLOC = 1000.0 * gridView3.RowCount / TotalLines;
            double PDRKLOC = 1000.0 * BugCount / TotalLines;
            return PDRKLOC;
        }
        private void DirSearch(string sDir) 
        {            
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {                    
                    foreach (string f in Directory.GetFiles(d))                    
                    {
                        string s = Path.GetExtension(f).ToLower();
                        if (!extfilter.Contains(s))
                        {
                            fileList.Add(f);
                        }                        
                        //FileInfo fi = new FileInfo(f);
                        //FileCheck(fi); //如果是文件，执行FileCheck
                    }
                    DirSearch(d); //递归查询
                }
            }
            catch (System.Exception ex)
            {
                ShowBoxMessage("錯誤", ex.Message);
            }
        }
        private void ChkFileLines(string d)
        {
            foreach (string f in Directory.GetFiles(d))
            {
                string s = Path.GetExtension(f).ToLower();
                if (!extfilter.Contains(s))
                {
                    fileList.Add(f);
                }
                //FileInfo fi = new FileInfo(f);
                //FileCheck(fi); //如果是文件，执行FileCheck
            }
        }
        private void excel()
        {
            GridControl[] grids = new GridControl[] { gridControl1, gridControl2 };
            PrintingSystem ps = new PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
            compositeLink.PrintingSystem = ps;
            foreach (GridControl grid in grids)
            {
                PrintableComponentLink link = new PrintableComponentLink();
                link.Component = grid;
                compositeLink.Links.Add(link);
            }
            compositeLink.CreateDocument();
            compositeLink.ShowPreview();
        }
        private void chartControl2_MouseClick(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < (sender as ChartControl).Series.Count; i++)
                {
                    if ((sender as ChartControl).Series[i].LabelsVisibility == DevExpress.Utils.DefaultBoolean.False)
                    {
                        (sender as ChartControl).Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    }
                    else (sender as ChartControl).Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
        private void chartControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            if (chartControl2.Height < 653)
            {
                chartControl2.Location = new Point(272, 0);
                chartControl2.Size = new Size(717, 653);
                chartControl1.Visible = false;
            }
            else
            {
                chartControl2.Location = new Point(272, 363);
                chartControl2.Size = new Size(717, 288);
                chartControl1.Visible = true;
            }
        }
        private void chartControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            if (chartControl1.Height < 653)
            {
                chartControl1.Size = new Size(717, 653);
                chartControl2.Visible = false;
            }
            else
            {
                chartControl1.Size = new Size(717, 357);
                chartControl2.Visible = true;
            }
        }

        private void tbVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetXA009();
        }
        private void SetXA009()
        {
            FXA009 = tbVer.Text;
            /*if (VerNoMatchDic.ContainsKey(tbVer.Text))
            {
                FXA009 = VerNoMatchDic[tbVer.Text];
            }*/
        }
      
    }
}
