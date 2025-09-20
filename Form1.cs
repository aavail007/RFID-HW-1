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

            cbKeyType.Items.Add("A");
            cbKeyType.Items.Add("B");
        }

        MW_EasyPOD EasyPOD;

        UInt32 dwResult, Index;

        unsafe public void btnGetUID_Click(object sender, EventArgs e)
        {

        }

        unsafe public void btnReadData_Click(object sender, EventArgs e)
        {
                // UI 選項
                byte KEYA = 0x60;
                byte KEYB = 0x61;
                byte SECTOR_NO = 0x00;
                byte BLOCK_NO = 0x01;
                byte[] KEY_VALUE = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };


                byte STX = 0x02;
                byte LEN = 0x00;
                byte CMD = 0x15;
                byte[] DATA = new byte[0];
                var DATA_LIST = new List<byte>();
            DATA_LIST.Add(cbKeyType.Text == "A" ? KEYA : KEYB);
                DATA_LIST.AddRange(KEY_VALUE);
                DATA_LIST.Add(SECTOR_NO);
                DATA_LIST.Add(BLOCK_NO);

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
                EasyPOD.VID = 0xe6a;
                EasyPOD.PID = 0x317;
                uint Index = 1;
                uint dwResult;
                uiLength = 64;

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

                        dwResult = PODfuncs.WriteData(pPOD, WriteBuffer, 6, &uiWritten);    //Send a request command to reader
                        uiResult = PODfuncs.ReadData(pPOD, ReadBuffer, uiLength, &uiRead);  //Read the response data from reader

                        var i = uiRead - 4; // RX Length -[ STX - LEN - CMD - STATUS ] Length
                    txtResult.Text = BitConverter.ToString(ReadBuffer, 4, (int)i).Replace("-", " ");  //HEX
                }
                    dwResult = PODfuncs.ClearPODBuffer(pPOD);
                    dwResult = PODfuncs.DisconnectPOD(pPOD);
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
