using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        // 常數定義
        const byte STX = 0x02; // 封包起始符號，用於標記命令封包的開頭
        const byte CMD_READ = 0x15; // 讀取命令
        const UInt32 VENDOR_ID = 0xe6a; // 設定裝置 Vendor ID (對應 RD200_RD300_Tools_V0225_20160913.exe 應用程式中的 VID)
        const UInt32 PRODUCT_ID = 0x317; // 設定裝置 Product ID (對應 RD200_RD300_Tools_V0225_20160913.exe 應用程式中的 PID)
        const UInt32 READ_LENGTH = 64; // 讀取緩衝區長度
        const int DATA_OFFSET = 4; // 資料區起始位置 (跳過 STX、LEN、CMD、STATUS)
        const int DATA_SIZE = 16; // 成功時回傳的卡片區塊內容固定 16 Bytes
        const uint DEVICE_INDEX = 1; // 裝置索引
        const string DEFAULT_LOAD_KEY = "FFFFFFFFFFFF"; // Load Key 預設值

        public Form1()
        {
            InitializeComponent();

            // 初始化下拉選單選項
            initialCbSector();
            initialCbBlock();
            initialCbKey();

            // 設定預設值
            loadKey.Text = DEFAULT_LOAD_KEY;

            // 綁定下拉選單與輸入框的變更事件，確保表單驗證即時更新
            cbSector.SelectedIndexChanged += (s, e) => ValidateForm();
            cbBlock.SelectedIndexChanged += (s, e) => ValidateForm();
            cbKeyType.SelectedIndexChanged += (s, e) => ValidateForm();
            loadKey.TextChanged += (s, e) => ValidateForm();
        }

        MW_EasyPOD EasyPOD; // 讀卡機結構體
        UInt32 dwResult, Index;

        /// <summary>
        /// 讀取卡片資料按鈕事件
        /// </summary>
        unsafe public void btnReadData_Click(object sender, EventArgs e)
        {
            try
            {
                // 取得 UI 選項、輸入框的值
                byte keyType = (byte)cbKeyType.SelectedValue;
                byte sectorNo = byte.Parse(cbSector.SelectedItem.ToString());
                byte blockNo = byte.Parse(cbBlock.SelectedItem.ToString());
                byte[] keyValue = StringToByteArray(loadKey.Text);

                // 組出 WriteBuffer 傳送給讀卡機的指令
                byte[] WriteBuffer = BuildReadCommand(STX, CMD_READ, keyType, keyValue, sectorNo, blockNo);
                byte[] ReadBuffer = new byte[READ_LENGTH];

                // 設定裝置參數
                EasyPOD.VID = VENDOR_ID;
                EasyPOD.PID = PRODUCT_ID;

                UInt32 uiRead = 0, uiWritten = 0, uiResult = 0;
                dwResult = 0;

                // 使用 fixed 取得 EasyPOD 結構體指標，進行底層 DLL 操作
                fixed (MW_EasyPOD* pPOD = &EasyPOD)
                {
                    // 連線到讀卡機
                    dwResult = PODfuncs.ConnectPOD(pPOD, DEVICE_INDEX);
                    if (dwResult != 0)
                    {
                        MessageBox.Show("讀卡機尚未連線");
                        return;
                    }

                    // 設定逾時時間
                    EasyPOD.ReadTimeOut = 200;
                    EasyPOD.WriteTimeOut = 200;

                    // 寫入指令到讀卡機
                    dwResult = PODfuncs.WriteData(pPOD, WriteBuffer, (UInt32)WriteBuffer.Length, &uiWritten);
                    // 讀取回應資料
                    uiResult = PODfuncs.ReadData(pPOD, ReadBuffer, READ_LENGTH, &uiRead);

                    // 檢查回應長度，至少要有狀態碼
                    if (uiRead < DATA_OFFSET)
                    {
                        MessageBox.Show("沒有收到正確回應，缺少 Status");
                        return;
                    }

                    byte status = ReadBuffer[3]; // STATUS 在回應的第 4 byte

                    switch (status)
                    {
                        case 0x10:
                            MessageBox.Show("Command error：指令錯誤");
                            break;

                        case 0x01:
                            MessageBox.Show("No card 或 Key 錯誤");
                            break;

                        case 0x00:
                            // 成功，檢查資料長度
                            if (uiRead < DATA_OFFSET + DATA_SIZE)
                            {
                                MessageBox.Show("回應資料長度不足");
                                return;
                            }

                            // 取出 16 Bytes 資料並轉 HEX 字串顯示
                            string hex = BitConverter.ToString(ReadBuffer, DATA_OFFSET, DATA_SIZE).Replace("-", "");
                            txtResult.Text = hex;
                            break;

                        default:
                            MessageBox.Show($"未知狀態碼: 0x{status:X2}");
                            break;
                    }
                    // 清除緩衝區並斷線
                    PODfuncs.ClearPODBuffer(pPOD);
                    PODfuncs.DisconnectPOD(pPOD);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"發生錯誤：{ex.Message}");
            }
        }

        /// <summary>
        /// 組封裝包函式 Request  = STX + LEN + CMD + DATA
        /// </summary>
        private byte[] BuildReadCommand(byte stx, byte cmd, byte keyType, byte[] keyValue, byte sectorNo, byte blockNo)
        {
            var data = new List<byte> { keyType };
            data.AddRange(keyValue);
            data.Add(sectorNo);
            data.Add(blockNo);
            byte len = (byte)(data.Count + 1); // CMD + DATA length
            var buffer = new List<byte> { stx, len, cmd };
            buffer.AddRange(data);
            return buffer.ToArray();
        }

        /// <summary>
        /// 初始化 Sector 選項
        /// </summary>
        private void initialCbSector()
        {
            // 限定只能用選的，不能輸入
            cbSector.DropDownStyle = ComboBoxStyle.DropDownList;
            // 建立 Sector 選單
            for (int i = 0; i < 16; i++)
            {
                cbSector.Items.Add(i.ToString("D2")); // 兩位數格式
            }
        }

        /// <summary>
        /// 初始化 Block 選項 (00~03)
        /// </summary>
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

        /// <summary>
        /// 初始化 Key 選項 (A/B)
        /// </summary>
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

            cbKeyType.SelectedIndex = -1; // 預設不選
        }

        /// <summary>
        /// 將 12 碼 HEX 字串轉為 byte 陣列
        /// </summary>
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

        /// <summary>
        /// 驗證表單輸入是否合法，決定是否啟用讀取按鈕
        /// </summary>
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

        /// <summary>
        /// 關閉表單
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
