using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Net;
using System.Reflection;
using System.Management;
using System.Data.SqlClient;


namespace VerTrans
{
    public class fc
    {
        //DLL
        [DllImport("Crypt.dll", CallingConvention = CallingConvention.StdCall)]     // "TestDll.dll"为dll名称,EntryPoint 为函数名
        public static extern string ERPEncrypt(string S);              //delphi 中的函数

        [DllImport("Crypt.dll", CallingConvention = CallingConvention.StdCall)]     // "TestDll.dll"为dll名称,EntryPoint 为函数名
        public static extern string ERPDecrypt(string S);              //delphi 中的函数
        //API
        /// 根据句柄查找进程ID
        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(
            IntPtr hWnd,
            int nCmdShow
        );
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, int hwndChildAfter, string lpszClass, string lpszWindow);
        //切换窗体显示  
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern long SendMessage(IntPtr hWnd, uint msg, uint wparam, int text);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern byte MapVirtualKey(byte wCode, int wMap);
        //Dim
        static Computer myComputer = new Computer();
        public static string keyPathString = GetkeyPath();
        public static string Mydocument = myComputer.FileSystem.SpecialDirectories.MyDocuments;
        public static string DirVerTrans = Mydocument + @"\VerTrans";
        public static string ConfigTmpPath = Mydocument + @"\VerTrans\Config_Temp.ini";
        public static string ConfigPath = Mydocument + @"\VerTrans\Config.ini";
        public static string TmpPath = Mydocument + @"\VerTrans\VerTrans_Temp.ini";
        public static string path = Mydocument + @"\VerTrans\VerTrans.ini";
        //public static string TmpCodePath = Mydocument + @"\VerTrans\Code_Temp.ini"; //20131223 mark Code 拿掉
        //public static string CodePath = Mydocument + @"\VerTrans\Code.ini"; //20131223 mark Code 拿掉
        //public static string TmpHotKeyPath = Mydocument + @"\VerTrans\Hotkey_Temp.ini";//20131223 mark Hotkey 拿掉
        //public static string HotKeyPath = Mydocument + @"\VerTrans\Hotkey.ini";//20131223 mark Hotkey 拿掉
        public static string CustPath = Mydocument + @"\VerTrans\CustPath.ini";
        public static string CustTmpPath = Mydocument + @"\VerTrans\CustPath_Temp.ini";
        //public static string Library2003Path = Mydocument + @"\VerTrans\Library2003.ini";
        //public static string Config2Path = Mydocument + @"\VerTrans\Config2.ini";
        public static string DirVerTransLog = Mydocument + @"\VerTrans\Log";
        public static string splitline = "*************************************************************************";
        public static string[] cboA = new string[] { @"\\10.40.40.127\Cosmos_patch\對外區", @"\\10.40.13.42\Work\2259\WorkingFolder" };
        public static string[] cboB1 = new string[] { "對外_POS21", "對外_POS31", "對外_POS32", "對外_POS33", "對外_POS34", "對外_GPPOS1.0" };
        public static string[] cboB2 = new string[] { "Standard_POS21", "Standard_POS31", "Standard_POS33", "Standard_POS34", "Standard_POSGP10" };
        public static string user = "";
        //static string[] Hotkey = new string[] { "CTRL", "ALT" };  //20131223 mark Hotkey 拿掉
        static List<string> Editor = new List<string>() { "2259", "3188", "3396", "3997", "4084", "4497", "4939", "4940" };
        

        public enum CPUMode
        {
            x32 = 0,
            x64 = 1,
            Unknown = -1
        }
        public static CPUMode PCCPUMode
        {
            get
            {
                // 取得這個執行個體的大小，以位元組為單位。這個屬性的值在 32 位元平台上為 4，而在 64 位元平台上為 8。
                if (IntPtr.Size == 8)
                {
                    return CPUMode.x64;
                }
                else
                {
                    return CPUMode.x32;
                }
            }
        }
        private static byte CheckVirtualKey(char c, out bool shift)
        {
            byte y = new byte();
            shift = false;
            switch (c)
            {
                case '`':
                    y = 0xC0;
                    break;
                case '~':
                    y = 0xC0;
                    shift = true;
                    break;
                case '!':
                    y = 0x31;
                    shift = true;
                    break;
                case '@':
                    y = 0x32;
                    shift = true;
                    break;
                case '#':
                    y = 0x33;
                    shift = true;
                    break;
                case '$':
                    y = 0x34;
                    shift = true;
                    break;
                case '%':
                    y = 0x35;
                    shift = true;
                    break;
                case '^':
                    y = 0x36;
                    shift = true;
                    break;
                case '&':
                    y = 0x37;
                    shift = true;
                    break;
                case '*':
                    y = 0x38;
                    shift = true;
                    break;
                case '(':
                    y = 0x39;
                    shift = true;
                    break;
                case ')':
                    y = 0x30;
                    shift = true;
                    break;
                case '-':
                    y = 0xBD;
                    break;
                case '_':
                    y = 0xBD;
                    shift = true;
                    break;
                case '=':
                    y = 0xBB;
                    break;
                case '+':
                    y = 0xBB;
                    shift = true;
                    break;
                case '[':
                    y = 0xDB;
                    break;
                case '{':
                    y = 0xDB;
                    shift = true;
                    break;
                case ']':
                    y = 0xDD;
                    break;
                case '}':
                    y = 0xDD;
                    shift = true;
                    break;
                case '\\':
                    y = 0xDC;
                    break;
                case '|':
                    y = 0xDC;
                    shift = true;
                    break;
                case ';':
                    y = 0xBA;
                    break;
                case ':':
                    y = 0xBA;
                    shift = true;
                    break;
                case '\'':
                    y = 0xDE;
                    break;
                case '"':
                    y = 0xDE;
                    shift = true;
                    break;
                case ',':
                    y = 0xBC;
                    break;
                case '<':
                    y = 0xBC;
                    shift = true;
                    break;
                case '.':
                    y = 0xBE;
                    break;
                case '>':
                    y = 0xBE;
                    shift = true;
                    break;
                case '/':
                    y = 0xBF;
                    break;
                case '?':
                    y = 0xBF;
                    shift = true;
                    break;
            }
            return y;
        }
        public static void SendKey(char[] c)
        {
            //char[] c = tb_PW.Text.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if ((byte)c[i] >= 0x41 && (byte)c[i] <= 0x60)
                {
                    byte b = (byte)c[i];
                    keybd_event(0xa0, MapVirtualKey(0xa0, 0), 0, 0);//鍵下shift鍵。
                    keybd_event(b, MapVirtualKey(b, 0), 0, 0);//鍵下f鍵。
                    keybd_event(b, MapVirtualKey(b, 0), 0x2, 0);//放開f鍵。
                    keybd_event(0xa0, MapVirtualKey(0xa0, 0), 0x2, 0);//鍵下shift鍵。
                    continue;
                }
                if (c[i] >= 'a' && c[i] <= 'z')
                {
                    char ch = (char)(c[i] - 0x20);
                    byte b = (byte)ch;
                    keybd_event(b, MapVirtualKey(b, 0), 0, 0);//鍵下f鍵。
                    keybd_event(b, MapVirtualKey(b, 0), 0x2, 0);//放開f鍵。
                    continue;
                }
                if (((byte)c[i] >= 0x30 && (byte)c[i] <= 0x39))
                {
                    byte b = (byte)c[i];
                    keybd_event(b, MapVirtualKey(b, 0), 0, 0);//鍵下f鍵。
                    keybd_event(b, MapVirtualKey(b, 0), 0x2, 0);//放開f鍵。
                    continue;
                }

                bool shift;
                byte k = CheckVirtualKey(c[i], out shift);
                if (shift)
                {
                    keybd_event(0xa0, MapVirtualKey(0xa0, 0), 0, 0);//鍵下shift鍵。
                    keybd_event(k, MapVirtualKey(k, 0), 0, 0);//鍵下f鍵。
                    keybd_event(k, MapVirtualKey(k, 0), 0x2, 0);//放開f鍵。
                    keybd_event(0xa0, MapVirtualKey(0xa0, 0), 0x2, 0);//鍵下shift鍵。
                }
                else
                {
                    keybd_event(k, MapVirtualKey(k, 0), 0, 0);//鍵下f鍵。
                    keybd_event(k, MapVirtualKey(k, 0), 0x2, 0);//放開f鍵。
                }
            }
            keybd_event(0x0D, MapVirtualKey(0x0D, 0), 0, 0);//鍵下f鍵。
            keybd_event(0x0D, MapVirtualKey(0x0D, 0), 0x2, 0);//放開f鍵。
        }

        public static List<string> GetIni(string name)
        {
            List<string> tmp = new List<string>();
            if (File.Exists(name))
            {
                StreamReader r = new StreamReader(name);
                while (!r.EndOfStream)
                {
                    tmp.Add(r.ReadLine());
                }
                r.Close();
            }
            return tmp;
        }
        public static string[] Split(string separator, string Text)
        {

            string[] sp = new string[] { separator };

            return Text.Split(sp, StringSplitOptions.RemoveEmptyEntries);

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
        public static bool isDirectory(string p)//目錄是否存在
        {
            if (p == "")
            {
                return false;
            }
            return System.IO.Directory.Exists(p);
        }
        public static object iif(bool s, object T_Value, object F_Value)
        {
            if (s)
                return T_Value;
            else
                return F_Value;
        }
        public static bool SaveTemp(string xpath, string xtmppath)
        {
            if (System.IO.File.Exists(xpath))//若檔案存在，則存檔前先備份
            {
                System.IO.File.Copy(xpath, xtmppath,true);
            }
            return true;
        }
        public static bool ReturnTemp(string xpath, string xtmppath)
        {
            System.IO.File.Delete(xpath);
            System.IO.File.Copy(xtmppath, xpath, true);
            System.IO.File.Delete(xtmppath);
            return true;
        }
        public static void DelTemp(string xtmppath)
        {
            System.IO.File.Delete(xtmppath);
        }
        public static string GetkeyPath()
        {
            if (fc.PCCPUMode == fc.CPUMode.x32)
            {
                return @"SOFTWARE\DSC\COSMOS_POS";
            }
            else if (fc.PCCPUMode == fc.CPUMode.x64)
            {
                return @"SOFTWARE\Wow6432Node\DSC\COSMOS_POS";
            }
            return "";
        }
        public static bool Isx64()
        {
            try
            {
                string addressWidth = String.Empty;
                ConnectionOptions mConnOption = new ConnectionOptions();
                ManagementScope mMs = new ManagementScope("\\\\localhost", mConnOption);
                ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
                ManagementObjectCollection mObjectCollection = mSearcher.Get();
                foreach (ManagementObject mObject in mObjectCollection)
                {
                    addressWidth = mObject["AddressWidth"].ToString();
                }
                if (addressWidth == "64")
                {
                    return true;
                }
                    return false;               
            }
            catch (Exception ex)
            {
                ShowBoxMessage(ex.ToString());
                return false;
            }
        }
        public static List<string> LoadConfigIni(string ConfigName)
        {
            /*List<string> tmp = new List<string>(GetIni(ConfigPath));
            List<string> mReturn = new List<string>();
            string[] spread = new string[4];
            bool StartPath = false;
            string name = "--" + ConfigName;
            for (int i = 0; i < tmp.Count; i++)
            {
                //if (tmp[i] == "--SetupPath")
                if (tmp[i] == name)
                {
                    StartPath = true;
                    continue;
                }
                else if (tmp[i] != name && !StartPath)
                {
                    continue;
                }
                else if (tmp[i].StartsWith("--") && StartPath)
                {
                    return mReturn;
                }
                else if (tmp[i] == "")
                {
                    continue;
                }
                spread = fc.Split("=", tmp[i]);
                string s_tmp;
                if (spread.Length > 1)
                    s_tmp = spread[1];
                else
                    s_tmp = spread[0];
                string s;
                s = (fc.iif(s_tmp == "^^", "", s_tmp)).ToString();
                mReturn.Add(s);
            }
            return mReturn;*/
            IniConfigSource source = new IniConfigSource(ConfigPath);
            //string[] tmp = new List<string>(GetIni(ConfigPath));
            string[] tmp = source.Configs[ConfigName].GetValues();
            return StringSToListS(tmp);
        }
        //20131223 mark Code 拿掉
        /*public static Dictionary<string, string[]> LoadCodeIni(string CodeName)
        {
            List<string> tmp = GetIni(CodePath);
            int idx = 0;
            string Dickey = "";
            List<string> tmpvalue = new List<string>();
            Dictionary<string, string[]> mReturn = new Dictionary<string, string[]>();
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].StartsWith("##"))
                {
                    string s = tmp[i].Substring(2);
                    Dickey = Split("*", s)[0];
                    continue;
                }
                else if ((!tmp[i].StartsWith("##")) && (!tmp[i].StartsWith("**")))
                {
                    //if (tmp[i] == "") continue;
                    tmpvalue.Add(tmp[i]);
                    idx++;
                    continue;
                }
                else if (tmp[i].StartsWith("**"))
                {
                    string[] Dicvalue = new string[tmpvalue.Count];
                    for (int j = 0; j < tmpvalue.Count; j++)
                    {
                        Dicvalue[j] = tmpvalue[j];
                    }
                    tmpvalue.Clear();
                    mReturn.Add(Dickey, Dicvalue);
                    i++;
                }
            }
            return mReturn;
        }*/
        //20131223 mark Code 拿掉
        //20131223 mark Hotkey 拿掉
        /*public static Dictionary<string, string[]> LoadHotkeyIni(string CodeName)
        {
            List<string> tmp = GetIni(HotKeyPath);
            int idx = 0;
            string Dickey = "";
            List<string> tmpvalue = new List<string>();
            Dictionary<string, string[]> mReturn = new Dictionary<string, string[]>();
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].StartsWith("##"))
                {
                    string s = tmp[i].Substring(2);
                    Dickey = Split("*", s)[0];
                    continue;
                }
                else if ((!tmp[i].StartsWith("##")) && (!tmp[i].StartsWith("**")))
                {
                    //if (tmp[i] == "") continue;
                    tmpvalue.Add(tmp[i]);
                    idx++;
                    continue;
                }
                else if (tmp[i].StartsWith("**"))
                {
                    string[] Dicvalue = new string[tmpvalue.Count];
                    for (int j = 0; j < tmpvalue.Count; j++)
                    {
                        Dicvalue[j] = tmpvalue[j];
                    }
                    tmpvalue.Clear();
                    mReturn.Add(Dickey, Dicvalue);
                    i++;
                }
            }
            return mReturn;
        }*/
        //20131223 mark Hotkey 拿掉
        public static void ChkFiles()
        {
            if (!fc.isDirectory(DirVerTrans))
            {
                Directory.CreateDirectory(DirVerTrans);
            }
            string[] sFileName = new string[] { "CustPath.ini", "VerTrans.ini", "Config.ini" };
            string[] sTmpFileName = new string[] { "CustTmpPath.ini", "TmpPath.ini", "ConfigTmpPath.ini" };         
            for (int i = 0; i < sFileName.Length; i++)
            {
                string xxpath = DirVerTrans + @"\" + sFileName[i];
                string xxpath2 = DirVerTrans + @"\" + sTmpFileName[i];
                if (!File.Exists(xxpath))
                {
                    if (sFileName[i] == "VerTrans.ini")                    
                        continue;
                    
                    File.Create(xxpath).Close();
                }
                else//20131223 add 修改舊的INI檔格式
                {
                    if (readFileAsUtf8(xxpath, xxpath2))
                        WriteLog("已將" + xxpath + "由ANSI格式修改成UTF-8格式", true);

                    if (FixFormat(xxpath, xxpath2))
                        WriteLog("已將舊的" + xxpath + "格式修改成INI標準格式", true); ;
                }
            }
           /* if (!File.Exists(CustPath))
            {
                File.Create(CustPath).Close();
            }
            else//20131223 add 修改舊的INI檔格式
            {
                if (readFileAsUtf8(CustPath, CustTmpPath))
                    WriteLog("已將CustPath.ini由ANSI格式修改成UTF-8格式", true);

            }

            if (!File.Exists(ConfigPath))
            {
                File.Create(ConfigPath).Close();                
            }
            else //20131223 add 修改舊的INI檔格式
            {
                try
                {
                    if (readFileAsUtf8(CustPath, CustTmpPath))
                        WriteLog("已將Config.ini由ANSI格式修改成UTF-8格式", true);   

                     if(FixFormat(@path,@TmpPath))
                         WriteLog("已將舊的Config格式修改成INI標準格式", true);                                         
                }
                catch (System.Exception ex)
                {

                    ShowBoxMessage("初始化Config.ini失敗" + ex.Message);
                    ReturnTemp(@ConfigPath, @ConfigTmpPath);
                }
 
            }
            //20131223 add 修改舊的INI檔格式*/

            //20131223 mark Code 拿掉
            /*if (!File.Exists(CodePath))
            {
                File.Create(CodePath).Close();
            }*/
            //20131223 mark Code 拿掉
            //20131223 mark Hotkey 拿掉
            /*if (!File.Exists(HotKeyPath))
            {
                File.Create(HotKeyPath).Close();
                StreamWriter w = new StreamWriter(@HotKeyPath, false);
                try
                {
                    for (int i = 0; i < Hotkey.Length; i++)
                    {
                        for (int j = 1; j <= 9; j++)
                        {
                            w.WriteLine("##" + Hotkey[i] + "-" + j + splitline.Substring(4 + Hotkey[i].Length));
                            w.WriteLine(splitline);
                            w.WriteLine("");
                        }
                    }
                    w.Close();
                }
                catch (Exception ex)
                {
                    fc.ShowBoxMessage("存檔失敗!!\r\n" + ex.Message);
                }
            }*/
            //20131223 mark Hotkey 拿掉
            //path------------------------------------------------------------------------------
            {
                bool b;
                b = File.Exists(path);        // 判定檔案是否存在
                DialogResult IsReWrite = DialogResult.No;
                if (!b)
                    IsReWrite = fc.ShowBoxMessage("找不到VerTrans.ini，程式將重新寫入預設客服資訊!", "警告");

                if (IsReWrite == DialogResult.OK)
                {
                    File.Create(path).Close();

                    try
                    {
                        StreamWriter w = new StreamWriter(@path, false,Encoding.UTF8);
                        string[] sr = fc.Split("\r\n", Properties.Resources.TextFile1);
                        for (int i = 0; i < sr.Length; i++)
                        {
                            w.WriteLine(sr[i]);
                        }
                        w.Close();
                        WriteLog("建立VerTrans Default 設定值", true);
                    }
                    catch (Exception ex)
                    {
                        fc.ShowBoxMessage("初始化VerTrans.ini失敗!!\r\n" + ex.Message);
                    }
                }
                else
                {
                    if (!b)
                        return;
                    else //修正成正確的INI格式
                    {
                        try
                        {
                            if (readFileAsUtf8(@path, @TmpPath))
                                WriteLog("已將VerTrans.ini由ANSI格式修改成UTF-8格式", true); 
  
                            if (FixFormat(@path, @TmpPath))
                                WriteLog("已修改VerTrans.ini間隔符號為|", true);

                            StreamReader r = new StreamReader(@path,Encoding.UTF8);
                            List<string> tmp1 = new List<string>();
                            while (!r.EndOfStream)
                            {
                                string sss = r.ReadLine();
                                sss = sss.Replace("|^|", "|^^|");
                                sss = sss.Replace("|^\"|", "|^^|");
                                tmp1.Add(sss);
                            }
                            r.Close();

                            SaveTemp(@path, @TmpPath);
                            File.Delete(@path);

                            StreamWriter w = new StreamWriter(@path, false, Encoding.UTF8);
                            if (tmp1[0] != "[CustNo]")
                                w.WriteLine("[CustNo]");
                            for (int i = 0; i < tmp1.Count; i++)
                            {
                                w.WriteLine(tmp1[i]);
                            }
                            w.Close();
                            WriteLog("已修改VerTrans.ini為正確的ini格式", true);
                            DelTemp(@TmpPath);
                        }
                        catch (Exception ex)
                        {
                            fc.ShowBoxMessage("初始化VerTrans.ini失敗!!\r\n" + ex.Message);
                            ReturnTemp(@path, @TmpPath);
                        }
                    }
                }
            }
            //----------------------------------------------------------------------------------------

        }
        public static List<string[]> ReadVerTransIni(string xpath)
        {
            List<string[]> mReturn = new List<string[]>();
            IConfigSource source = new IniConfigSource(xpath);
            string[] tmp1 = source.Configs["CustNo"].GetKeys();
            List<string[]> serverList = new List<string[]>();
            for (int j = 0; j < tmp1.Length;j++ )
            {
                serverList.Add(source.Configs["CustNo"].Get(tmp1[j]).Split('|'));
            }
            for (int i = 0; i < tmp1.Length; i++)
            {
                string[] s = new string[5];
                for (int j = 0; j < s.Length; j++)
                {
                    if (j < serverList[i].Length)
                    {
                        s[j] = (fc.iif(serverList[i][j] == "^^", "", serverList[i][j])).ToString();
                    }
                    else
                    {
                        s[j] = "";
                    }
                }
                mReturn.Add(s);
            }
            /*string[] tmp1 = IniEX.INIGetAllItemsValue(@xpath, "CustNo");
            StreamReader r = new StreamReader(@xpath);
            List<string> tmp = new List<string>();
            while (!r.EndOfStream)
            {
                tmp.Add(r.ReadLine());
            }
            r.Close();
            for (int i = 0; i < tmp1.Length; i++)
            {
                string[] spread = fc.Split("=", tmp1[i]);
                string[] s_tmp = fc.Split(";", spread[1]);
                string[] s = new string[4];
                for (int j = 0; j < s.Length; j++)
                {
                    if (j < s_tmp.Length)
                    {
                        s[j] = (fc.iif(s_tmp[j] == "^^", "", s_tmp[j])).ToString();
                    }
                    else
                    {
                        s[j] = "";
                    }
                }
                mReturn.Add(s);
            }*/            
            return mReturn;
        }

        public static bool IsOldVersion()
        {
            //List<string> mList = fc.GetIni(ConfigPath);
            IniConfigSource source = new IniConfigSource(ConfigPath);
            if (source.Configs["ProductVersion"]==null)
            {
                return true;
            }
            string mVersion = source.Configs["ProductVersion"].GetString("Version");
            bool OldVersion = false;
            if (mVersion == "")
            {
                OldVersion = true;
            }
            else
            {
                if (mVersion != fc.GetVersion())
                {
                    string[] v1 = fc.Split(".", mVersion);
                    string[] v2 = fc.Split(".", fc.iif(fc.GetVersion() == "開發程式階段", "1.0.0.1", fc.GetVersion()).ToString());
                    for (int i = 0; i < v1.Length; i++)
                    {
                        if (Int32.Parse(v1[i]) < Int32.Parse(v2[i]))
                        {
                            OldVersion = true;
                            break;
                        }
                    }
                }
            }
            return (bool)iif(mVersion=="", true, OldVersion);

            /*foreach (string k in mList)
            {
                if (StartWriteVersion)
                {
                    if (k == "")
                    {
                        OldVersion = true;
                        continue;
                    }
                    string[] s = fc.Split("=", k);
                    if (k != fc.GetVersion())
                    {
                        string[] v1 = fc.Split(".", s[1]);
                        string[] v2 = fc.Split(".", fc.iif(fc.GetVersion() == "開發程式階段", "1.0.0.1", fc.GetVersion()).ToString());
                        for (int i = 0; i < v1.Length; i++)
                        {
                            if (Int32.Parse(v1[i]) < Int32.Parse(v2[i]))
                            {
                                OldVersion = true;
                                break;
                            }
                        }
                    }
                    StartWriteVersion = false;
                    break;
                }

                if (k == "--ProductVersion")
                {
                    StartWriteVersion = true;
                    continue;
                }
            }
            return (bool)iif(mList.Count==0,true,OldVersion);*/
        }
        public static void WriteConfigIni1(string xSetupName, string[] xValue, string[] tp06, string VerNo, string VerNoDefault, string CurrenInfo, string SetupVerNo,string tp04_Path)
        {
            try
            {
                fc.SaveTemp(fc.ConfigPath, fc.ConfigTmpPath);//備份前存檔
                IConfigSource source = new IniConfigSource(fc.ConfigPath);
                //----檢查節點是否存在                
                string[] Snode = new string[] { "ProductVersion", "SetupName", "SetupPath", "Handle", "Account", "ToolPath", "NoCopyFiles", "TP06", "Library2003", "VerNo", "CurrenInfo","SetupVerNo" ,"TP04"};
                string[] SRnode = new string[] { "Editor","User" };
                foreach (string c in Snode)
                {
                    if (source.Configs[c] == null)                    
                        source.AddConfig(c);                    
                }
                foreach (string c in SRnode)
                {
                    if (source.Configs[c] != null)
                        source.Configs.Remove(source.Configs[c]);
                }
                string file = fc.ConfigPath;
                Dictionary<string, string[]> account = Form1.AccountList;
                string[] tmp3 = source.Configs["Account"].GetValues();
                string[] tmp4 = source.Configs["NoCopyFiles"].GetValues();                                
                string[] tmp5 = source.Configs["TP06"].GetKeys();
                string[] tmp6 = source.Configs["TP06"].GetValues();
                //ProductVersion---------------------------------------------
                source.Configs["ProductVersion"].Set("Version", fc.iif(fc.GetVersion() == "開發程式階段", "1.0.0.1", fc.GetVersion()).ToString());                
                //SetupName-------------------------------------------------
                source.Configs["SetupName"].Set("Name", xSetupName);                
                //SetupPath-------------------------------------------------
                for (int i = 0; i < xValue.Length; i++)
                {
                    source.Configs["SetupPath"].Set("Path" + string.Format("{0:00}", i), xValue[i]);                    
                }
                if (xValue.Length == 0)
                {
                    source.Configs["SetupPath"].Set("Path00", @"C:\COSMOS_POS");                    
                }

                //Handel
                source.Configs["Handle"].Set("Hwnd", Form1.hwnd.ToString());                
                //Account         
                if (account.Count == 0)
                {
                    for (int i = 0; i < tmp3.Length; i++)
                    {
                        source.Configs["Account"].Set("Space" + string.Format("{0:00}", i), tmp3[i]);
                    }
                }
                else
                {
                    string ip = "";
                    string id = "";
                    string pw = "";
                    int count_account = 0;
                    foreach (KeyValuePair<string, string[]> item in account)
                    {
                        ip = item.Key;
                        id = Split("|", item.Value[0])[1];
                        pw = Split("|", item.Value[0])[2];
                        source.Configs["Account"].Set("Space" + string.Format("{0:00}", count_account), ip + "|" + id + "|" + desEncryptBase64(pw));
                        count_account++;
                    }
                }
                //ToolPath
                if (Form1.ToolPath != "")
                    source.Configs["ToolPath"].Set("Path", Form1.ToolPath);

                //NoCopyFiles
                if (Form1.NoCopyList.Count > 0)
                {
                    for (int Noi = 0; Noi < Form1.NoCopyList.Count; Noi++)
                    {
                        source.Configs["ToolPath"].Set("Path", Form1.ToolPath);
                    }
                }
                else if (tmp4.Length > 0)
                {
                    for (int i = 0; i < tmp4.Length; i++)
                    {
                        source.Configs["NoCopyFiles"].Set("NoCopy" + string.Format("{0:00}", i), tmp4[i]);
                    }
                }
                else
                {
                    int mmi = 0;
                    source.Configs["NoCopyFiles"].Set("NoCopy" + string.Format("{0:00}", mmi++), "POSInstall.exe");
                    source.Configs["NoCopyFiles"].Set("NoCopy" + string.Format("{0:00}", mmi++), "DSCPOSDEVICE.exe");
                    source.Configs["NoCopyFiles"].Set("NoCopy" + string.Format("{0:00}", mmi++), "POSMain.exe");
                    source.Configs["NoCopyFiles"].Set("NoCopy" + string.Format("{0:00}", mmi++), "DSCPOSB01.exe");
                    source.Configs["NoCopyFiles"].Set("NoCopy" + string.Format("{0:00}", mmi++), "ALTERDB.exe");
                    source.Configs["NoCopyFiles"].Set("NoCopy" + string.Format("{0:00}", mmi++), "UpdateFile.exe");
                }
                //-----------------------------------------------------------
                //TP06
                if (tmp5.Length > 0)
                {
                    for (int i = 0; i < tmp5.Length; i++)
                    {
                        source.Configs["TP06"].Set(tmp5[i], tmp6[i]);
                    }
                    source.Configs["TP06"].Set("ID", source.Configs["TP06"].GetString("ID", ""));
                    source.Configs["TP06"].Set("PW", source.Configs["TP06"].GetString("PW", ""));                    
                }
                else
                {
                    string[] sr = fc.Split("\r\n", Properties.Resources.tp06_LoadInfo);
                    for (int i = 0; i < sr.Length; i++)
                    {
                        string[] sp = Split("=", sr[i]);
                        source.Configs["TP06"].Set(sp[0], sp[1]);
                    }
                    source.Configs["TP06"].Set("ID", tp06[0]);
                    source.Configs["TP06"].Set("PW", tp06[1]);

                }
                //VerNo
                if (VerNo == "")
                {
                    string[] sr = fc.Split("\r\n", Properties.Resources.VerNo);
                    for (int i = 0; i < sr.Length; i++)
                    {
                        string[] sp = Split("=", sr[i]);
                        source.Configs["VerNo"].Set(sp[0].Trim(), sp[1].Trim());
                    }
                }
                else
                    source.Configs["VerNo"].Set("No", VerNo);

                if (VerNoDefault == "")
                    source.Configs["VerNo"].Set("Default", "ALL");
                else
                    source.Configs["VerNo"].Set("Default", VerNoDefault); 
                //CurrenInfo                
                source.Configs["CurrenInfo"].Set("VerNo", CurrenInfo);  
                //SetupVerNo
                source.Configs["SetupVerNo"].Set("VerNo", SetupVerNo);  
                //TP04
                source.Configs["TP04"].Set("LoadPath", tp04_Path);  
                
                
                

            
                source.Save();
                System.IO.File.Delete(fc.ConfigTmpPath);
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage("存檔失敗!!\r\n" + ex.Message);
                fc.ReturnTemp(fc.ConfigPath, fc.ConfigTmpPath);
            }
        }
        //20131223 mark 重寫
        public static void WriteConfigIni(string xSetupName,string[] xValue)
        {  
            fc.SaveTemp(fc.ConfigPath, fc.ConfigTmpPath);//備份前存檔
            StreamWriter w = null;
            try
            {
                List<string> tmp = LoadConfigIni("Editor");
                if (tmp.Count == 0) tmp = Editor;
                List<string> tmp2 = new List<string>();
                if (user == "") tmp2 = LoadConfigIni("User");
                if (tmp2.Count == 0) tmp2 = new List<string>() { "4940" };           
                List<string> tmp3 = LoadConfigIni("Account");
                List<string> tmp4 = LoadConfigIni("NoCopyFiles");
                Dictionary<string, string[]> account = Form1.AccountList;
                w = new StreamWriter(@fc.ConfigPath, false);
                //ProductVersion---------------------------------------------
                w.WriteLine("--ProductVersion");
                w.WriteLine("Version=" + fc.iif(fc.GetVersion() == "開發程式階段", "1.0.0.1", fc.GetVersion()).ToString());
                w.WriteLine("");
                //SetupName-------------------------------------------------
                w.WriteLine("--SetupName");
                w.WriteLine("Name="+xSetupName);
                w.WriteLine("");
                //SetupPath-------------------------------------------------
                w.WriteLine("--SetupPath");
                for (int i = 0; i < xValue.Length; i++)
                {
                    w.Write("Path" + string.Format("{0:00}", i) + "=");
                    w.Write(xValue[i] + "\r\n");
                }
                if (xValue.Length == 0)
                {
                    w.WriteLine(@"Path00=C:\COSMOS_POS");
                }
                w.WriteLine("");
                //Editer-----------------------------------------------------

                w.WriteLine("--Editor");
                for (int i = 0; i < tmp.Count; i++)
                {
                    w.Write("Ed" + string.Format("{0:00}", i) + "=");
                    w.Write(tmp[i] + "\r\n");
                }
                w.WriteLine("");
                //User         
                w.WriteLine("--User");
                if (user == "")
                    w.WriteLine("us="+tmp2[0]);
                else
                    w.WriteLine("us=" + user);
                w.WriteLine("");
                //Handel
                w.WriteLine("--Handle");
                w.WriteLine("Hwnd=" + Form1.hwnd.ToString());
                w.WriteLine("");
                //Account
                w.WriteLine("--Account");
                if (account.Count == 0)
                {
                    for (int i = 0; i < tmp3.Count; i++)
                    {
                        w.Write("Space" + string.Format("{0:00}", i) + "=");
                        w.Write(tmp3[i] + "\r\n");
                    }
                }
                else
                {
                    string ip = "";
                    string id = "";
                    string pw = "";
                    int count_account = 0;
                    foreach(KeyValuePair<string, string[]> item in account)
                    {
                        ip = item.Key;
                        id = Split(";", item.Value[0])[1];
                        pw = Split(";", item.Value[0])[2];
                        w.Write("Space" + string.Format("{0:00}", count_account) + "=");
                        //w.Write(ip + "\r\n");
                        //count_account++;
                        //w.Write("Space" + string.Format("{0:00}", count_account) + "=");
                        w.Write(ip + ";" + id + ";" + desEncryptBase64(pw) + "\r\n");
                        count_account++;
                    }

                   /* for (int i2 = 0; i2 < account.Count; i2++)
                    {
                        ip = account[i2].
                        if (i2 % 2 == 0)
                            ip = tmp3[i2];
                        else
                        {
                            id = Split(";", tmp3[i2])[0];
                            pw = Split(";", tmp3[i2])[1];

                        }
                    }*/
                }
                w.WriteLine("");
                //ToolPath
                w.WriteLine("--ToolPath");
                if (Form1.ToolPath !="")
                w.WriteLine("Path=" + Form1.ToolPath);
                w.WriteLine("");
                //NoCopyFiles
                w.WriteLine("--NoCopyFiles");
                if (Form1.NoCopyList.Count > 0)
                {
                    for (int Noi = 0; Noi < Form1.NoCopyList.Count;Noi++ )
                    {
                        w.Write("NoCopy" + string.Format("{0:00}", Noi) + "=");
                        w.Write(Form1.NoCopyList[Noi]+"\r\n");
                    }
                }
                else if (tmp4.Count > 0)
                {
                    for (int i = 0; i < tmp4.Count; i++)
                    {
                        w.Write("NoCopy" + string.Format("{0:00}", i) + "=");
                        w.Write(tmp4[i] + "\r\n");
                    }
                }
                else
                {
                    int mmi = 0;
                    w.Write("NoCopy" + string.Format("{0:00}", mmi++) + "=POSInstall.exe\r\n");
                    w.Write("NoCopy" + string.Format("{0:00}", mmi++) + "=DSCPOSDEVICE.exe\r\n");
                    w.Write("NoCopy" + string.Format("{0:00}", mmi++) + "=POSMain.exe\r\n");
                    w.Write("NoCopy" + string.Format("{0:00}", mmi++) + "=DSCPOSB01.exe\r\n");
                    w.Write("NoCopy" + string.Format("{0:00}", mmi++) + "=ALTERDB.exe\r\n");
                    w.Write("NoCopy" + string.Format("{0:00}", mmi++) + "=UpdateFile.exe\r\n");
                }
                w.WriteLine("");
                //-----------------------------------------------------------
                w.Close();
                tmp = null;
                System.IO.File.Delete(fc.ConfigTmpPath);
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage("存檔失敗!!\r\n" + ex.Message);
                w.Close();
                fc.ReturnTemp(fc.ConfigPath, fc.ConfigTmpPath);
            }
        }
        //20131223 mark Code 拿掉
        /*public static void WriteCodeIni(Dictionary<string, string[]> xValue)
        {
            fc.SaveTemp(fc.CodePath, fc.TmpCodePath);//備份前存檔
            try
            {
                StreamWriter w = new StreamWriter(@fc.CodePath, false);
                foreach (KeyValuePair<string, string[]> item in xValue)
                {
                    w.WriteLine("##" + item.Key + splitline.Substring(2 + item.Key.Length));
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        w.WriteLine(item.Value[i]);
                    }
                    w.WriteLine(splitline);
                    w.WriteLine("");
                }
                w.Close();
                System.IO.File.Delete(fc.TmpCodePath);
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage("存檔失敗!!\r\n" + ex.Message);
                fc.ReturnTemp(fc.ConfigPath, fc.ConfigTmpPath);
            }
        }*/
        //20131223 mark Code 拿掉
        //20131223 mark Hotkey 拿掉
        /*public static void WriteHotkeyIni(Dictionary<string, string[]> xValue)
        {
            fc.SaveTemp(fc.HotKeyPath, fc.TmpHotKeyPath);//備份前存檔
            try
            {
                StreamWriter w = new StreamWriter(@fc.HotKeyPath, false);
                foreach (KeyValuePair<string, string[]> item in xValue)
                {
                    w.WriteLine("##" + item.Key + splitline.Substring(2 + item.Key.Length));
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        w.WriteLine(item.Value[i]);
                    }
                    w.WriteLine(splitline);
                    w.WriteLine("");
                }
                w.Close();
                System.IO.File.Delete(fc.TmpHotKeyPath);
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage("存檔失敗!!\r\n" + ex.Message);
                fc.ReturnTemp(fc.HotKeyPath, fc.TmpHotKeyPath);
            }
        }*/
        //20131223 mark Hotkey 拿掉
        public static void GetSingleThread()
        {
            int hwnd;
            string name = Process.GetCurrentProcess().ProcessName;
            int id = Process.GetCurrentProcess().Id;
            Process[] prc = Process.GetProcesses();
            // 取得本機端上執行中的應用程式
            foreach (Process pr in prc)
            {
                if ((name == pr.ProcessName) && (pr.Id != id))
                {
                    List<string> tmp = LoadConfigIni("Handle");
                    if (tmp.Count > 0)
                        hwnd = Int32.Parse(tmp[0]);
                    else
                        hwnd = 0;
                    ShowWindow(hwnd, 10);
                    System.Environment.Exit(0);
                }
            }
        }
        [DllImport("user32")]
        public static extern int ShowWindow(int hwnd, int nCmdShow);

        public static List<string> LoadVerInfo()
        {
            List<string> tmp = new List<string>();
            string[] sr = fc.Split("##", Properties.Resources.VerInfo);
            for (int i = 0; i < sr.Length;i++ )
            {
                tmp.Add("##"+sr[i]);
            }
            return tmp;
        }
        public static string GetPassWord()
        {
            DateTime dt = DateTime.Now;
            string dd = string.Format("{0:dd}", dt);
            string d = "";
            if (dd.Length > 1)
                d = dd.Substring(1, 1);
            string Lyy = string.Format("{0:yyyy}", dt).Substring(0, 2);
            string Ryy = string.Format("{0:yyyy}", dt).Substring(2, 2);
            string mm = string.Format("{0:MM}", dt);

            string s = d + Lyy + (Int32.Parse(Ryy) + Int32.Parse(mm) + Int32.Parse(dd)).ToString() + d;
            return s;
        }
        public static int GetTEditWin_SetPassword(IntPtr Hwnd ,string s,bool ShowMsg)
        {
            //fc.SetForegroundWindow(Hwnd);
            IntPtr TEdit = fc.FindWindowEx(Hwnd, 0, "TEdit", "");
            if (TEdit != IntPtr.Zero)
            {
              /*  if (ShowMsg)
                {
                    
                    if (fc.ShowBoxMessage("確定要執行嗎?", "詢問",MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return 2;
                    }
                }*/
                byte[] ch = (UnicodeEncoding.Default.GetBytes(s));
                char[] cChar = Encoding.ASCII.GetChars(ch);
                fc.SendKey(cChar);
                return 1;
           }
           return 0;
        }
        public static string ZeroAtFirst(string xvalue,int xLen)
        {
            string mResult = "";
            if (xvalue.Length >= xLen)
            {
                return xvalue;
            }
            else
            {
                int count = xLen - xvalue.Length;
                for (int i = 0; i < count; i++)
                {
                    mResult += "0";
                }
                mResult += xvalue;
                return mResult;
            }
        }
        public static int ASC(char C)
        {
            int N = Convert.ToInt32(C);
            return N;
        }
        public static bool CheckNum(int xAsc)
        {
            if (xAsc>=48 && xAsc<=57)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*public static string Encrypt(string toEncrypt)//AES 加密
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string toDecrypt)//AES 解密
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }*/
        public static string desEncryptBase64(string source)
        {
            if (source == "")
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes("12345678");
            byte[] iv = Encoding.ASCII.GetBytes("87654321");
            byte[] dataByteArray = Encoding.UTF8.GetBytes(source);

            des.Key = key;
            des.IV = iv;
            string encrypt = "";
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                //輸出資料
                foreach (byte b in ms.ToArray())
                {
                    sb.AppendFormat("{0:X2}", b);
                }
                encrypt = sb.ToString();
            }
            return encrypt;
        }
        public static string desDecryptBase64(string encrypt)
        {
            if (encrypt == "")
            {
                return "";
            }
            byte[] dataByteArray = new byte[encrypt.Length / 2];
            for (int x = 0; x < encrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(encrypt.Substring(x * 2, 2), 16));
                dataByteArray[x] = (byte)i;
            }

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes("12345678");
            byte[] iv = Encoding.ASCII.GetBytes("87654321");
            des.Key = key;
            des.IV = iv;

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
        public static TreeNode AddNode(string name)
        {
            TreeNode tn = new TreeNode();
            tn.Name = name;
            tn.Text = name;
            //tn.Tag = tag;
            //tn.Checked = Ischecked;
            return tn;
        }
        public static bool ConnNetUse(string ip,string id,string pw)
        {
            ProcessStartInfo PInfo;
            Process P;
            PInfo = new ProcessStartInfo("cmd", @"/c net use \\" + ip + @"\IPC$ " + @" /user:workflowerp\" + id + " \""+ pw +"\"" );
            PInfo.CreateNoWindow = true; //nowindow
            PInfo.UseShellExecute = false; //use shell
            P = Process.Start(PInfo);
            P.WaitForExit(1000); //give it some time to finish
            P.Close();

           /* Process p = new Process();
            p.StartInfo.FileName = "net";
            p.StartInfo.Arguments = @"use \\" + ip + @"\IPC$ " + @" /user:workflowerp\" + id + " \""+ pw +"\"" ;
            p.StartInfo.CreateNoWindow = true;
            fc.ShowBoxMessage(p.StartInfo.Arguments.ToString());
            Thread.Sleep(1000);*/
            return true;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

        public static string GetLocalIP()
        {
            System.Net.IPAddress SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
            return SvrIP.ToString();
            //Response.Write("SvrIP=" + SvrIP.ToString());
        }

        public static DialogResult ShowMsg(string MsgText, string MsgCaption, string xIdx)
        {
            MBox Mbox = new MBox();//Form2是自己新建的form，外观做的和fc.ShowBoxMessage出来的效果一样。设置窗体的StartPosition为CenterParent.
            Mbox.SetMsg = new string[]{MsgText,MsgCaption,xIdx};
            return Mbox.ShowDialog();
        }

        public static DialogResult ShowBoxMessage(string s)
        {
            BoxMessage b = new BoxMessage();
            b.SetMsg = s;
            return b.ShowDialog();            
        }
        public static DialogResult ShowBoxMessage(string s ,string title)
        {
            BoxMessage b = new BoxMessage();
            b.SetMsg = s;
            b.SetTitle = title;
            return b.ShowDialog();
        }

        public static DialogResult ShowConfirm(string s)
        {
            BoxConfirm b = new BoxConfirm();
            b.SetMsg = s;
            return b.ShowDialog();
        }
        public static DialogResult ShowConfirm(string s, string title)
        {
            BoxConfirm b = new BoxConfirm();
            b.SetMsg = s;
            b.SetTitle = title;
            return b.ShowDialog();
        }
        public static bool WriteLog(string xinfo,bool xWriteTime)
        {
            try
            {
                string xtime = "";
                if (xWriteTime)
                {
                    xtime = "[" + DateTime.Now.ToString() + "] ";
                }
                string path = DirVerTransLog;
                if (!isDirectory(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filename = path + @"\" + DateTime.Now.ToString("yyyyMMdd") + @".txt";
                if (!File.Exists(filename))
                {
                    File.Create(filename).Close();
                }
                FileStream myFile = File.Open(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter myWriter = new StreamWriter(myFile,Encoding.Default);
                myWriter.WriteLine(xtime + xinfo);
                myWriter.Close();
                myWriter.Dispose();
                myFile.Dispose();
                return true;
            }
            catch (System.Exception ex)
            {
                ShowBoxMessage(ex.Message.ToString());
                return false;
            }

        }
        public static string AsciiToUnicode(string paramstr)
        {
            return System.Text.Encoding.Unicode.GetString(System.Text.Encoding.ASCII.GetBytes(paramstr));
        }
        public static string[] ListSToStringS(List<string> s)
        {
            string[] tmpS = new string[s.Count];
            for (int j = 0; j < s.Count; j++)
            {
                tmpS[j] = s[j];
            }
            return tmpS;
        }
        public static List<string> StringSToListS(string[] s)
        {
            List<string> t = new List<string>();
            foreach (string ss in s)
            {
                t.Add(ss);
            }
            return t;
        }
        public static bool IsFileLocked(string file)
        {
            try
            {
                using (File.Open(file, FileMode.Open, FileAccess.Write, FileShare.None))
                {
                    return false;
                }
            }
            catch (IOException exception)
            {
                var errorCode = Marshal.GetHRForException(exception) & 65535;
                return errorCode == 32 || errorCode == 33;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool TryOpenFile(string s)
        {
            IntPtr p2;
            int id=0;
            bool find = false;
            try
            {
                // 取得本機端上執行中的應用程式
                foreach (Process pp in Process.GetProcesses())
                {
 
                    if (pp.ProcessName.ToUpper() == Path.GetFileNameWithoutExtension(s).ToUpper())  // 判斷 MainWindowHandle 為非零值的應用程式，表示有主視窗                    
                    {
                        //ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
                        //SetForegroundWindow(instance.MainWindowHandle); 
                        p2 = pp.MainWindowHandle;
                        /*if (p2.ToInt32() > 0)
                        {
                            find = true;
                        }
                        if (!find)
                        {
                            p2 = FindWindow("DSC_DataSync",null);
                            if (p2.ToInt32() > 0)
                            {
                                GetWindowThreadProcessId(p2, out id);
                                if (pp.Id == id)
                                {
                                    find = true;
                                }
                            }
                        }

                        if (!find)
                        {
                            p2 = FindWindow("CheckConnection", null);
                            if (p2.ToInt32() > 0)
                            {
                                GetWindowThreadProcessId(p2, out id);
                                if (pp.Id == id)
                                {
                                    find = true;
                                }
                            }
                        }*/

                        //if(find) 
                        if (p2.ToInt32()>0)
                        {
                            ShowWindow(p2.ToInt32(), 4);
                            SetForegroundWindow(p2);
                        }
                        else
                        {
                            fc.ShowBoxMessage("程式已經開啟!!");
                        }
                        //pp.MainWindowTitle
                        
                        //
                        return false;
                    }
                }
                System.Diagnostics.Process.Start(@s); 
                /*Process p = new Process();
                p.StartInfo.FileName = s;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();*/
                return true;
            }
            catch (System.Exception ex)
            {
                ShowBoxMessage(ex.Message.ToString());
                return false;
            }
        }
        public static Encoding CheckEnCoding(string xpath)
        {
            byte[] header = new byte[4];
            Encoding enc = Encoding.Default;
            using (FileStream fs = File.Open(xpath, FileMode.Open, FileAccess.Read))
            {
                if (fs.Length > 3)//判斷檔案長度需大於3
                {
                    fs.Read(header, 0, 4);//讀取開頭的前4個byte到header的byte array
                    //以下幾種編碼的判斷來源,可以參考文章後的參考.
                    if ((header[0] == 0xef && header[1] == 0xbb && header[2] == 0xbf))
                        enc = Encoding.UTF8;
                    else if ((header[0] == 0xfe && header[1] == 0xff))
                        enc = Encoding.BigEndianUnicode;
                    else if ((header[0] == 0xff && header[1] == 0xfe))
                        enc = Encoding.Unicode;//LittleEndianUnicode
                    else if ((header[0] == 0 && header[1] == 0 && header[2] == 0xfe && header[3] == 0xff) ||
                       (header[0] == 0xff && header[1] == 0xfe && header[2] == 0 && header[3] == 0))
                        enc = Encoding.UTF32;
                    else
                        enc = Encoding.Default;
                }
                else
                {
                    enc = Encoding.Default;
                }
                fs.Close();
            }
            ShowBoxMessage(enc.BodyName);
            return enc;
        }
        public static bool utf8_probability(byte[] rawtext)
        {
            int score = 0;
            int i, rawtextlen = 0;
            int goodbytes = 0, asciibytes = 0;

            // Maybe also use UTF8 Byte Order Mark: EF BB BF

            // Check to see if characters fit into acceptable ranges
            rawtextlen = rawtext.Length;
            for (i = 0; i < rawtextlen; i++)
            {
                if ((rawtext[i] & (byte)0x7F) == rawtext[i])
                { // One byte
                    asciibytes++;
                    // Ignore ASCII, can throw off count
                }
                else
                {
                    int m_rawInt0 = Convert.ToInt16(rawtext[i]);
                    int m_rawInt1 = Convert.ToInt16(rawtext[i + 1]);
                    int m_rawInt2 = Convert.ToInt16(rawtext[i + 2]);

                    if (256 - 64 <= m_rawInt0 && m_rawInt0 <= 256 - 33 && // Two bytes
                     i + 1 < rawtextlen &&
                     256 - 128 <= m_rawInt1 && m_rawInt1 <= 256 - 65)
                    {
                        goodbytes += 2;
                        i++;
                    }
                    else if (256 - 32 <= m_rawInt0 && m_rawInt0 <= 256 - 17 && // Three bytes
                     i + 2 < rawtextlen &&
                     256 - 128 <= m_rawInt1 && m_rawInt1 <= 256 - 65 &&
                     256 - 128 <= m_rawInt2 && m_rawInt2 <= 256 - 65)
                    {
                        goodbytes += 3;
                        i += 2;
                    }
                }
            }

            if (asciibytes == rawtextlen) { return false; }

            score = (int)(100 * ((float)goodbytes / (float)(rawtextlen - asciibytes)));

            // If not above 98, reduce to zero to prevent coincidental matches
            // Allows for some (few) bad formed sequences
            if (score > 98)
            {
                return true;
            }
            else if (score > 95 && goodbytes > 30)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool readFileAsUtf8(string xpath, string xtmppath)
        {
            Encoding encoding = Encoding.Default;
            String original = String.Empty;

            FileStream myFile = File.Open(xpath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            using (BinaryReader br = new BinaryReader(myFile))
            {
               byte[] b = br.ReadBytes((int)myFile.Length);
               if (utf8_probability(b)) //若判斷為UTF-8 則返回
                   encoding = Encoding.UTF8;
            }

            using (StreamReader sr = new StreamReader(xpath, encoding, true))
            {
                original = sr.ReadToEnd();
                //encoding = sr.CurrentEncoding;
                sr.Close();
            }           
            
            //if (encoding == Encoding.UTF8)
            //    return true;// original;

            byte[] encBytes = encoding.GetBytes(original);
            byte[] utf8Bytes = Encoding.Convert(encoding, Encoding.UTF8, encBytes);
            string utf8S = Encoding.UTF8.GetString(utf8Bytes);
            File.WriteAllText(xpath, utf8S, Encoding.UTF8);
            
            return true;
        }
        public static bool FixFormat(string xpath,string xtmppath)
        {
            try
            {
                StreamReader r = new StreamReader(xpath);
                List<string> tmp = new List<string>();
                while (!r.EndOfStream)
                {
                    string s = r.ReadLine();
                    if (s.StartsWith("--"))
                    {
                        s = "[" + s.Substring(2) + "]";
                    }
                    s = s.Replace(';', '|');
                    tmp.Add(s);
                }
                r.Close();
                SaveTemp(xpath, xtmppath);
                File.Delete(xpath);

                StreamWriter w = new StreamWriter(xpath, false,Encoding.UTF8);
                for (int i = 0; i < tmp.Count; i++)
                {
                    w.WriteLine(tmp[i]);
                }
                w.Close();
                DelTemp(xtmppath);
                return true;
            }
            catch (System.Exception ex)
            {
                ShowBoxMessage("初始化Config.ini失敗" + ex.Message);
                ReturnTemp(xpath, xtmppath);
                return false;
            }
        }
        public static void SetAliasForNini(IConfigSource source)
        {
            source.Alias.AddAlias("0", false);
            source.Alias.AddAlias("1", true);
        }
        public static void WriteConfigIni(string xsecion, string[] xkeys, string[] xvalues)
        {
            IConfigSource source = new IniConfigSource(ConfigPath);
            SetAliasForNini(source);
            if (source.Configs[xsecion] == null)
            {
                source.Configs.Add(xsecion);
            }
            for (int i = 0; i < xkeys.Length; i++)
            {
                source.Configs[xsecion].Set(xkeys[i], xvalues[i]);
            }
            source.Save();
        }
        public static string GetStrFromBool(bool b)
        {
            if (b)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        public static string makeConnectString( string xFServer, string xFDataBase,
            string xFUserID, string xFPassword)
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
        public static List<string> GetSQL(string xid, string xpw, string xip, string msql)
        {
            string SQL = "";
            SqlConnection conn = null;
            List<string> mResult = new List<string>();
            SQL = msql;
            conn = new SqlConnection(makeConnectString(xip, "", xid, xpw));
            try
            {
                conn.Open();
                //if ((sender as SimpleButton).Name == "gbtnERPTest")
                {
                    SqlCommand myCommand = null; SqlDataReader myDataReader = null;
                    //SQL = " Select MU001, MU003 from " + DBName + ".dbo.POSMU ";
                    myCommand = new SqlCommand(SQL, conn);
                    myDataReader = myCommand.ExecuteReader();

                    while (myDataReader.Read())
                    {
                        mResult.Add(myDataReader["name"].ToString());
                    }
                    conn.Close();
                    myCommand.Cancel();
                    myDataReader.Close();
                }
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage(ex.Message);
            }
            return mResult;
        }
       /* public static void tp06_WriteDefLoadInfo()
        {
            //path------------------------------------------------------------------------------
            {
                try
                {
                    IConfigSource source = new IniConfigSource(@ConfigPath);
                    source.AddConfig("TP06");
                    
                    //StreamWriter w = new StreamWriter(@ConfigPath, false);
                    string[] sr = fc.Split("\r\n", Properties.Resources.tp06_LoadInfo);
                    for (int i = 0; i < sr.Length; i++)
                    {
                        string[] sp = Split("=", sr[i]);
                        source.Configs["TP06"].Set(sp[0], sp[1]);
                        //w.WriteLine(sr[i]);
                    }
                    source.Save();
                    //w.Close();
                }
                catch (Exception ex)
                {
                    fc.ShowBoxMessage("存檔失敗!!\r\n" + ex.Message);
                }
            }
            //----------------------------------------------------------------------------------------
        }*/

    }

}
