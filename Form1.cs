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
            UInt32 uiLength, uiRead, uiResult ,uiWritten;
            byte[] ReadBuffer  = new byte[0x40];
            byte[] WriteBuffer = new byte[] { 0x2, 0x2, 0x1, 0x1 }; //Command {STX, LEN, CMD, DATA1, DATA2.....}
            
            byte[] sResponse = null;
            sResponse = new byte[21];
            
            EasyPOD.VID = 0xe6a;
            EasyPOD.PID = 0x317;
            Index = 1;
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

                    dwResult = PODfuncs.WriteData(pPOD, WriteBuffer, 4, &uiWritten);    //Send a request command to reader
                    uiResult = PODfuncs.ReadData(pPOD, ReadBuffer, uiLength, &uiRead);  //Read the response data from reader
                    
                    txbGetUIDh.Text = BitConverter.ToString(ReadBuffer, 4, (Int32)uiRead).Replace("-", " ");  //HEX
                    txbGetUIDd.Text = BitConverter.ToInt32(ReadBuffer, 4).ToString();  //DEC
                }
                dwResult = PODfuncs.ClearPODBuffer(pPOD);
                dwResult = PODfuncs.DisconnectPOD(pPOD);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
