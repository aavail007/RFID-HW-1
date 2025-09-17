using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.BitConverter;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }


        MW_EasyPOD EasyPOD;

        UInt32 dwResult, Index;

        unsafe public void btnGetUID_Click(object sender, EventArgs e)
        {

        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
            try
            {
                // 取得 UI 選擇的值
                byte sector = Convert.ToByte(cbSector.SelectedItem.ToString(), 16);
                byte block = Convert.ToByte(cbBlock.SelectedItem.ToString(), 16);
                byte keyType = (cbKeyType.SelectedItem.ToString() == "A") ? (byte)0x60 : (byte)0x61;
                byte[] keyBytes = StringToByteArray(txtKey.Text); // 將 "FFFFFFFFFFFF" 轉成 6-byte array

                // 檢查 Key 長度
                if (keyBytes.Length != 6)
                {
                    MessageBox.Show("Key 必須是 6 Bytes (12 個十六進位字元)");
                    return;
                }

                // 組出 WriteBuffer
                byte[] WriteBuffer = new byte[12]; // STX + LEN + CMD + KeyType + 6-byte key + sector + block
                WriteBuffer[0] = 0x02; // STX
                WriteBuffer[1] = 0x0A; // LEN = 10 bytes (不包含 STX、LEN)
                WriteBuffer[2] = 0x15; // CMD = Read Data
                WriteBuffer[3] = keyType;
                Array.Copy(keyBytes, 0, WriteBuffer, 4, 6);
                WriteBuffer[10] = sector;
                WriteBuffer[11] = block;

                // 建立接收緩衝區
                byte[] ReadBuffer = new byte[64];
                UInt32 uiRead = 0, uiWritten = 0;

                //// 連線讀卡機
                //fixed (MW_EasyPOD* pPOD = &EasyPOD)
                //{
                //    dwResult = PODfuncs.ConnectPOD(pPOD, Index);

                //    if (dwResult != 0)
                //    {
                //        MessageBox.Show("讀卡機尚未連線");
                //        return;
                //    }

                //    EasyPOD.ReadTimeOut = 200;
                //    EasyPOD.WriteTimeOut = 200;

                //    // 送出讀取指令
                //    dwResult = PODfuncs.WriteData(pPOD, WriteBuffer, (UInt32)WriteBuffer.Length, &uiWritten);
                //    UInt32 uiResult = PODfuncs.ReadData(pPOD, ReadBuffer, (UInt32)ReadBuffer.Length, &uiRead);

                //    if (uiRead < 4)
                //    {
                //        MessageBox.Show("沒有收到正確回應");
                //    }
                //    else if (ReadBuffer[3] != 0x00) // STATUS 不等於 0x00 表示失敗
                //    {
                //        MessageBox.Show("讀取失敗或 Key 錯誤");
                //    }
                //    else
                //    {
                //        // 將讀取到的 16 Bytes 資料轉成 HEX
                //        string dataHex = BitConverter.ToString(ReadBuffer, 4, 16).Replace("-", " ");
                //        txtResult.Text = dataHex;
                //    }

                //    PODfuncs.ClearPODBuffer(pPOD);
                //    PODfuncs.DisconnectPOD(pPOD);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("發生錯誤：" + ex.Message);
            }
        }

        // 工具函式：把 "FFFFFFFFFFFF" 轉成 byte[]
        private byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace(" ", ""); // 去掉空白
            if (hex.Length % 2 != 0)
                throw new ArgumentException("Key 格式錯誤，必須是偶數長度");

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }


        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txbGetUIDd_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
