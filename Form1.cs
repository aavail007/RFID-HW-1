using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
//using System.BitConverter;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

            // 初始化選項
            initialCbSector();
            initialCbBlock();
            initialCbKey();

            // 設定預設值
            loadKey.Text = "FFFFFFFFFFFF";

            // 綁定事件
            cbSector.SelectedIndexChanged += (s, e) => ValidateForm();
            cbBlock.SelectedIndexChanged += (s, e) => ValidateForm();
            cbKeyType.SelectedIndexChanged += (s, e) => ValidateForm();
            loadKey.TextChanged += (s, e) => ValidateForm();
        }

        MW_EasyPOD EasyPOD;
        UInt32 dwResult, Index;

        unsafe public void btnReadData_Click(object sender, EventArgs e)
        {
                 // 取得 UI 選項、輸入框的值
                byte keyType = (byte)cbKeyType.SelectedValue;
                byte sectorNo = byte.Parse(cbSector.SelectedItem.ToString());
                byte blockNo = byte.Parse(cbBlock.SelectedItem.ToString());
                byte[] keyValue = StringToByteArray(loadKey.Text);

                /// Protocol
                ///  Read Data (0x15) 
                ///  <STX> + LEN + CMD + DATA
                byte STX = 0x02;
                byte LEN = 0x00;
                byte CMD = 0x15; // 命令碼
                byte[] DATA = new byte[0]; // [Key Type] + [Key Value] +  [Sector Number] + [Block Number]
                var DATA_LIST = new List<byte>();
                DATA_LIST.Add(keyType);
                DATA_LIST.AddRange(keyValue);
                DATA_LIST.Add(sectorNo);
                DATA_LIST.Add(blockNo);

                DATA = DATA_LIST.ToArray();
                LEN = (byte)(DATA.Length + 1); // CMD + DATA Length

                byte[] ReadBuffer = new byte[0x40];
                byte[] WriteBuffer = new byte[0];
                var writeData = new List<byte>();

                writeData.Add(STX);
                writeData.Add(LEN);
                writeData.Add(CMD);
                writeData.AddRange(DATA);
                WriteBuffer = writeData.ToArray();


                UInt32 uiLength, uiRead, uiResult, uiWritten;
                EasyPOD.VID = 0xe6a; // 設定裝置 Vendor ID (對應 RD200_RD300_Tools_V0225_20160913.exe 應用程式中的 VID)
                EasyPOD.PID = 0x317; // 設定裝置 Product ID (對應 RD200_RD300_Tools_V0225_20160913.exe 應用程式中的 PID)
                uint Index = 1;
                uint dwResult;
                uiLength = 64; // 設定預期讀取長度

                fixed (MW_EasyPOD* pPOD = &EasyPOD)
                {

                    dwResult = PODfuncs.ConnectPOD(pPOD, Index);

                    if ((dwResult != 0))
                    {
                        MessageBox.Show("Not connected yet");
                    }
                    else
                    {
                        EasyPOD.ReadTimeOut = 200;
                        EasyPOD.WriteTimeOut = 200;

                        dwResult = PODfuncs.WriteData(pPOD, WriteBuffer, (UInt32)WriteBuffer.Length, &uiWritten);   // 傳送指令給讀卡機
                        uiResult = PODfuncs.ReadData(pPOD, ReadBuffer, uiLength, &uiRead);  // 從裝置讀取回應資料

                        // 取出 16 Bytes 資料
                        byte[] dataBytes = new byte[16];
                        Array.Copy(ReadBuffer, 4, dataBytes, 0, 16);

                        // 轉成連續 HEX 字串（每 byte 兩位，不加空格）
                        StringBuilder sb = new StringBuilder();
                        foreach (byte b in dataBytes)
                        {
                            sb.Append(b.ToString("X2"));
                        }

                        txtResult.Text = sb.ToString();
                    }
                    dwResult = PODfuncs.ClearPODBuffer(pPOD); // 清除裝置緩衝區
                    dwResult = PODfuncs.DisconnectPOD(pPOD);  // 中斷裝置連線
                }
        }

        // 初始化 Sector 選項
        private void initialCbSector()
        {
            // 限定只能用選的，不能輸入
            cbSector.DropDownStyle = ComboBoxStyle.DropDownList;
            // 建立 Sector 選單 (00 ~ 15)
            for (int i = 0; i < 16; i++)
            {
                cbSector.Items.Add(i.ToString("D2")); // 兩位數格式
            }
        }

        // 初始化Block 選項
        private void initialCbBlock()
        {
            // 限定只能用選的，不能輸入
            cbBlock.DropDownStyle = ComboBoxStyle.DropDownList;
            // 建立 Block 選單項目
            for (int j = 0; j < 4; j++)
            {
                cbBlock.Items.Add(j.ToString("D2"));
            }
        }

        // 初始化 Key 選項
        private void initialCbKey()
        {
            // 限定只能用選的，不能輸入
            cbKeyType.DropDownStyle = ComboBoxStyle.DropDownList;
            // 建立 Key 選單項目
            var keyOptions = new Dictionary<string, byte>
            {
                { "A", 0x60 },
                { "B", 0x61 }
            };

            cbKeyType.DataSource = new BindingSource(keyOptions, null);
            cbKeyType.DisplayMember = "Key";
            cbKeyType.ValueMember = "Value";
        }

        private byte[] StringToByteArray(string txtKey)
        {
            string hex = txtKey.Trim(); // 移除前後空白
            hex = hex.Replace(" ", ""); // 移除空白
            if (hex.Length % 2 != 0)
                throw new ArgumentException("Key 必須是偶數長度");

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        private void ValidateForm()
        {
            string key = loadKey.Text.Trim();

            // 驗證 Key: 必須是 12 碼十六進位字元
            bool keyValid = Regex.IsMatch(key, @"^[0-9A-Fa-f]{12}$");

            // 條件：cbSector cbBlock cbKeyType 三個下拉選項有選值 + Key 格式正確
            bool valid = cbSector.SelectedIndex >= 0 &&
                         cbBlock.SelectedIndex >= 0 &&
                         cbKeyType.SelectedIndex >= 0 &&
                         keyValid;

            btnReadData.Enabled = valid;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
