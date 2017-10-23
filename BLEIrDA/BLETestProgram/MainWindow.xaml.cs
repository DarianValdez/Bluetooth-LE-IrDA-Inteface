using BluetoothService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BLETestProgram
{
    enum eRadioState
    {
        RadioStateOff = 0,
        RadioStateStandby = 85,
        RadioStateOn = 255
    }

    enum eMtrModel {
    MtrS15, 
    MtrS22,
    MtrS30, 
    MtrS50, 
    MtrS88, 
    MtrS130,
    MtrS175, 
    MtrS260, 
    MtrS440, 
    MtrE2000, 
    MtrE2400,
    MtrE2800
    };


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BluetoothIrDAService bridge;
        int dataOffset = 4;
        String[] txScaling = { "0.01", "0.1", "1.0", "10.0", "100.0", "1000.0", "10000.0", "100000.0" };
        byte[] fskGet = {0x3b, 0x47, 0x8e, 0x71, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x69, 0x37, 0x4F};
        byte[] fskReset = {0x3B, 0x52, 0x03, 0xFC, 0x01, 0xBD, 0xA1, 0x4F}; //Reset
        byte[] fskTest = {0x3B, 0x58, 0x05, 0xFA, 0x01, 0x00, 0x00, 0xC3, 0x31, 0x4F}; //Enter Test
        byte[] fskFlowRate = {0x3B, 0x4D, 0x03, 0xFC, 0x00, 0xA9, 0x67, 0x4F}; //Enter Flow Rate
        byte[] fskExitTest = {0x3B, 0x58, 0x05, 0xFA, 0x00, 0x00, 0x00, 0x03, 0x60, 0x4F}; //Exit Test
        byte[] fskExitFlowRate = {0x3B, 0x4D, 0x03, 0xFC, 0x01, 0x69, 0xA6, 0x4F}; //Exit Flow Rate

        byte[] switchToVN = { 0x3F, 0x4D, 0x00, 0x03, 0xFF, 0xFC, 0x01, 0x6A, 0x6B };
        byte[] switchToI8 = { 0x3F, 0x4D, 0x00, 0x03, 0xFF, 0xFC, 0x00, 0xAA, 0xAA };
        byte[] vnReset = {0x3B, 0x72, 0x00, 0x03, 0xFF, 0xFC, 0x01, 0xA5, 0x2B}; //Reset
        byte[] vnTest = {0x3B, 0x78, 0x00, 0x05, 0xFF, 0xFA, 0x01, 0x00, 0x00, 0x1A, 0x5E}; //Enter Test
        byte[] vnFlowRate = {0x3B, 0x6D, 0x00, 0x03, 0xFF, 0xFC, 0x00, 0x0A, 0xE8}; //Enter Flow Rate
        byte[] Rssi = {0x3B, 0x7A, 0x00, 0x06, 0xFF, 0xF9, 0x00, 0x05, 0x00, 0x00, 0x65, 0xE4, 0x4F}; //Enter RSSI Mode
        byte[] dailyBroadcast = {0x3B, 0x62, 0x00, 0x06, 0xFF, 0xF9, 0x00, 0x00, 0x00, 0x00, 0x64, 0x5E}; //Daily Broadcast
        byte[] Otasp = {0x3B, 0x61, 0x00, 0x06, 0xFF, 0xF9, 0x00, 0x00, 0x00, 0x00, 0x94, 0x4A}; //OTSAP
        byte[] LTEToggle = {0x3B, 0x67, 0x00, 0x8E, 0xFF, 0x71, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x82, 0xD7, 0x4F}; //LTE Toggle
        byte[] vnExitTest = {0x3B, 0x78, 0x00, 0x05, 0xFF, 0xFA, 0x00, 0x00, 0x00, 0xDA, 0x0F}; //Exit Test
        byte[] vnExitFlowRate = {0x3B, 0x6D, 0x00, 0x03, 0xFF, 0xFC, 0x01, 0xCA, 0x29}; //Exit Flow Rate
        byte[] exitRssi = {0x3B, 0x7A, 0x00, 0x06, 0xFF, 0xF9, 0x03, 0x00, 0x00, 0x00, 0x20, 0xF4, 0x4F}; //Exit RSSI Mode

        byte[] setRadioTest = {0x3B, 0x53, 0x2C, 0xD3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x00, 0x1E, 0x00, 0x00, 0x00,
                                0x00, 0x01, 0x00, 0x02, 0x07, 0x00, 0x00, 0x00, 0xFF, 0x05, 0x01, 0x00, 0x00, 0x00, 0x57, 0x6C,
                                0x4F};

        int[] RTCHold = new int[6];

        public MainWindow()
        {
            InitializeComponent();
             
            //BluetoothIrDA ir = new BluetoothIrDA();

        }

        private void btnPair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process pair = Process.Start("C:\\Windows\\System32\\DevicePairingWizard.exe");
                while(true)
                {
                    if(pair.HasExited)
                        break;
                }
            } catch(Exception ex)
            {
                lblConnectMsg.Content = ex.Message;
                lblConnectMsg.Foreground = Brushes.Red;
            }
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            bridge = new BluetoothIrDAService();
            bridge.DeviceInitialized += new DeviceInitializedHandler(bridge_DeviceInitialized);
            bridge.DeviceNotInitialied += new DeviceNotInitializedHandler(bridge_DeviceNotInitialied);
            bridge.DataStructureReceived += new DataStructureReceivedHandler(bridge_DataStructureReceived);
            bridge.ErrorRecerived += new ErrorReceivedHandler(bridge_ErrorRecerived);
            bridge.InitializeTranciever();
        }

        void bridge_ErrorRecerived(byte[] rxData)
        {
            this.Dispatcher.Invoke(() =>
                {
                    boxRawData.Text = BitConverter.ToString(rxData).Replace("-", " ");
                });
        }

        void bridge_DataStructureReceived(byte[] rxData)
        {
            int temp = 0;
            byte[] tempArr = new byte[2];

            this.Dispatcher.Invoke(() =>
            {
                boxRawData.Text = BitConverter.ToString(rxData).Replace("-", " ");

                boxEndUnallowableWindow.Text = (Convert.ToInt16(rxData[0 + dataOffset])).ToString();

                Array.Copy(rxData, 10 + dataOffset, tempArr, 0, 2);
                temp = 0;
                temp |= (tempArr[0] << 8);
                temp |= tempArr[1];
                boxAllowableHourlyThreshold.Text = temp.ToString();

                boxTemperature.Text = (Convert.ToInt16(rxData[12 + dataOffset])).ToString();

                Array.Copy(rxData, 14+ dataOffset, tempArr, 0, 2);
                temp = 0;
                temp |= (tempArr[0] << 8);
                temp |= tempArr[1];
                boxLogLen.Text = temp.ToString();

                if((rxData[18 + dataOffset] & 0x01) == 0x01)
                {
                    boxBackFlow.IsChecked = true;
                }
                if((rxData[18 + dataOffset] & 0x02) == 0x02)
                {
                    boxHighUsage.IsChecked = true;
                }
                if((rxData[18 + dataOffset] & 0x04) == 0x04)
                {
                    boxLeakDetect.IsChecked = true;
                }
                if((rxData[18 + dataOffset] & 0x08) == 0x08)
                {
                    boxZeroUsage.IsChecked = true;
                }
                if((rxData[18 + dataOffset] & 0x10) == 0x10)
                {
                    boxLowBattery.IsChecked = true;
                }

                boxHighUsageDays.Text = (Convert.ToInt16(rxData[22 + dataOffset])).ToString();

                Array.Copy(rxData, 23, tempArr, 0, 2);
                temp = 0;
                temp |= (tempArr[0] << 8);
                temp |= tempArr[1];
                boxHighUsageTripLevel.Text = temp.ToString();

                String HWversion = rxData[27 + dataOffset].ToString("X2");
                HWversion += "." + rxData[28 + dataOffset].ToString("X2");
                boxHardwareVersion.Text = HWversion;

                boxNoConsumption.Text = (Convert.ToInt16(rxData[29 + dataOffset])).ToString();

                if((rxData[30 + dataOffset] & 0x01) == 0x01)
                {
                    boxBackFlowOn.IsChecked = true;
                }
                if((rxData[30 + dataOffset] & 0x02) == 0x02)
                {
                    boxHighUsageOn.IsChecked = true;
                }
                if((rxData[30 + dataOffset] & 0x04) == 0x04)
                {
                    boxLeakDetectOn.IsChecked = true;
                }
                if((rxData[30 + dataOffset] & 0x08) == 0x08)
                {
                    boxZeroUsageOn.IsChecked = true;
                }

                boxTransmitScaling.Text = txScaling[Convert.ToInt16(rxData[31 + dataOffset])];

                boxTransmitDigits.Text = (Convert.ToInt16(rxData[32 + dataOffset])).ToString();

                temp = Convert.ToInt16(rxData[36 + dataOffset]);
                eRadioState state = (eRadioState)temp;
                boxRadioOn.Text = state.ToString();

                boxTxInterval.Text = (Convert.ToInt16(rxData[37 + dataOffset])).ToString();
                boxLogInterval.Text = (Convert.ToInt16(rxData[38 + dataOffset])).ToString();

                Array.Copy(rxData, 39 + dataOffset, tempArr, 0, 2);
                temp = 0;
                temp |= (tempArr[0] << 8);
                temp |= tempArr[1];
                boxBatteryVoltage.Text = temp.ToString();

                boxBegUnallowableWindow.Text = (Convert.ToInt16(rxData[41 + dataOffset])).ToString();
                boxCommaPosReg_memo.Text = (Convert.ToInt16(rxData[42 + dataOffset])).ToString();

                String version = rxData[47 + dataOffset].ToString("X2");
                version += "." + rxData[48 + dataOffset].ToString("X2");
                for(int i = 0; i < 10; i+=2)
                {
                    version += ".";
                    version += Encoding.ASCII.GetString(rxData, i + 49 + dataOffset, 2);
                }
                boxVersionNumber.Text = version;

                byte[] revTemp8 = new byte[8];
                Array.Copy(rxData, 59 + dataOffset, revTemp8, 0, 8);
                Array.Reverse(revTemp8, 0, 8);
                boxCurrentIndexReg_memo.Text = (BitConverter.ToUInt64(revTemp8, 0)).ToString();

                String RTC = "";
                for(int i = 0; i < 6; i++)
                {
                    RTC += Convert.ToInt16(rxData[i + 67 + dataOffset]).ToString();
                    RTCHold[i] = Convert.ToInt16(rxData[i + 67 + dataOffset]);
                    RTC += ":";
                }
                RTC = RTC.Remove(RTC.Length - 1);
                boxRTC.Text = RTC;

                boxSensus_MfgID.Text = Encoding.ASCII.GetString(rxData, 73 + dataOffset, 12);
                boxFirstDigitReg_memo.Text = (Convert.ToInt16(rxData[85 + dataOffset])).ToString();
                boxSensus_FactoryID.Text = Encoding.ASCII.GetString(rxData, 86 + dataOffset, 12);
                boxMeterID_memo.Text = Encoding.ASCII.GetString(rxData, 98 + dataOffset, 8);

                boxSensus_Rollers.Text = (Convert.ToInt16(rxData[106 + dataOffset])).ToString();
                boxSensus_Variable.Text = (Convert.ToInt16(rxData[107 + dataOffset])).ToString();

                Array.Copy(rxData, 108 + dataOffset, revTemp8, 0, 8);
                Array.Reverse(revTemp8, 0, 8);
                boxLeakageReg_memo.Text = (BitConverter.ToUInt64(revTemp8, 0)).ToString();

                boxUnitReg_memo.Text = Convert.ToChar(rxData[116 + dataOffset]).ToString();
                boxUnitMult_memo.Text = Convert.ToChar(rxData[117 + dataOffset]).ToString();

                Array.Copy(rxData, 122 + dataOffset, revTemp8, 0, 4);
                Array.Reverse(revTemp8, 0, 4);
                boxPulseThresholdReg_memo.Text = (BitConverter.ToInt32(revTemp8, 0)).ToString();

                Array.Copy(rxData, 126 + dataOffset, revTemp8, 0, 4);
                Array.Reverse(revTemp8, 0, 4);
                boxIndexRatioReg_memo.Text = (BitConverter.ToInt32(revTemp8, 0)).ToString();

                Array.Copy(rxData, 130 + dataOffset, tempArr, 0, 2);
                temp = 0;
                temp |= (tempArr[0] << 8);
                temp |= tempArr[1];
                boxStatusReg_memo.Text = temp.ToString();

                boxWaterType_memo.Text = (Convert.ToInt16(rxData[132 + dataOffset])).ToString();
                boxSensus_Multiplier.Text = txScaling[rxData[133 + dataOffset]];
                Array.Copy(rxData, 134 + dataOffset, tempArr, 0, 2);
                temp = 0;
                temp |= (tempArr[0] << 8);
                temp |= tempArr[1];
                boxMaxFlowRate.Text = temp.ToString();

                boxFourToTwentyTime.Text = (Convert.ToInt16(rxData[136 + dataOffset])).ToString();

                boxSensus_Manufacturer.Text = Convert.ToChar(rxData[137 + dataOffset]).ToString();
                boxMeterModel.Text = ((eMtrModel)(Convert.ToInt16(rxData[138 + dataOffset]))).ToString();

                box4to20_On.Text = (Convert.ToInt16(rxData[139 + dataOffset])).ToString();

            });

        }

        void bridge_DeviceInitialized()
        {
            lblConnectMsg.Content = "Device Connected";
            lblConnectMsg.Foreground = Brushes.Green;
            btnGet.IsEnabled = true;
            btnSetConfig.IsEnabled = true;
            btnTestMode.IsEnabled = true;
            btnExitTest.IsEnabled = true;
            btnFlowRate.IsEnabled = true;
            btnExitFlowRate.IsEnabled = true;
            btnReset.IsEnabled = true;
        }

        void bridge_DeviceNotInitialied()
        {
            lblConnectMsg.Content = "Connection Failed";
            lblConnectMsg.Foreground = Brushes.Red;
        }

        private async void btnGet_Click(object sender, RoutedEventArgs e)
        {
            await bridge.WriteCommandData(fskGet, false);
        }

        private async void btnSetConfig_Click(object sender, RoutedEventArgs e)
        {
            byte[] setRadio = new byte[46];
            byte[] temp = new byte[12];
            setRadio[0] = 0x3B;
            setRadio[1] = 0x53;
            setRadio[2] = 0x2C;
            setRadio[3] = 0xD3;
            setRadio[4] = Convert.ToByte(boxEndUnallowableWindow.Text);
            temp = BitConverter.GetBytes(Convert.ToUInt16(boxAllowableHourlyThreshold.Text));
            setRadio[14] = temp[0];
            setRadio[15] = temp[1];
            setRadio[26] = Convert.ToByte(boxHighUsageDays.Text);
            temp = BitConverter.GetBytes(Convert.ToUInt16(boxHighUsageTripLevel.Text));
            setRadio[27] = temp[0];
            setRadio[28] = temp[1];
            setRadio[35] = (byte)Array.IndexOf(txScaling, boxTransmitScaling.Text);
            setRadio[36] = Convert.ToByte(boxTransmitDigits.Text);
            setRadio[40] = (byte)((eRadioState)Enum.Parse(typeof(eRadioState), boxRadioOn.Text));
            setRadio[41] = Convert.ToByte(boxTxInterval.Text);
            setRadio[42] = Convert.ToByte(boxLogInterval.Text);
            setRadio[45] = Convert.ToByte(boxBegUnallowableWindow.Text);

            //Calculates CRC

            byte[] setRegister = new byte[102];
            setRegister[0] = 0x3B;
            setRegister[1] = 0x45;
            setRegister[2] = 0x64;
            setRegister[3] = 0x9B;
            setRegister[4] = Convert.ToByte(boxCommaPosReg_memo.Text);
            for(int i = 0; i < 6; i++)
            {
                setRegister[i + 29] = (byte)RTCHold[i];
            }

            temp = Encoding.ASCII.GetBytes(boxSensus_MfgID.Text);
            Array.Copy(temp, 0, setRegister, 35, 12);
            setRegister[47] = Convert.ToByte(boxFirstDigitReg_memo.Text);
            temp = Encoding.ASCII.GetBytes(boxSensus_FactoryID.Text);
            Array.Copy(temp, 0, setRegister, 48, 12);
            temp = Encoding.ASCII.GetBytes(boxMeterID_memo.Text);
            Array.Copy(temp, 0, setRegister, 60, 8);

            setRegister[68] = Convert.ToByte(boxSensus_Rollers.Text);
            setRegister[69] = Convert.ToByte(boxSensus_Variable.Text);
            temp = BitConverter.GetBytes(Convert.ToInt64(boxLeakageReg_memo.Text));
            Array.Copy(temp, 0, setRegister, 70, 8);

            setRegister[78] = (byte)Convert.ToChar(boxUnitReg_memo.Text);
            setRegister[79] = (byte)Convert.ToChar(boxUnitMult_memo.Text);
            
            temp = BitConverter.GetBytes(Convert.ToInt32(boxPulseThresholdReg_memo.Text));
            Array.Copy(temp, 0, setRegister, 84, 4);

            temp = BitConverter.GetBytes(Convert.ToInt32(boxIndexRatioReg_memo.Text));
            Array.Copy(temp, 0, setRegister, 88, 4);

            temp = BitConverter.GetBytes(Convert.ToInt16(boxStatusReg_memo.Text));
            setRegister[92] = temp[0];
            setRegister[93] = temp[1];

            setRegister[94] = Convert.ToByte(boxWaterType_memo.Text);
            setRegister[95] = (byte)Array.IndexOf(txScaling, boxSensus_Multiplier.Text);

            temp = BitConverter.GetBytes(Convert.ToInt16(boxMaxFlowRate.Text));
            setRegister[96] = temp[0];
            setRegister[97] = temp[1];

            setRegister[98] = Convert.ToByte(boxFourToTwentyTime.Text);
            setRegister[99] = (byte)Convert.ToChar(boxSensus_Manufacturer.Text);
            setRegister[100] = (byte)((eMtrModel)Enum.Parse(typeof(eMtrModel), boxMeterModel.Text));
            setRegister[101] = Convert.ToByte(box4to20_On.Text);

            await bridge.WriteCommandData(setRadio, setRegister, 'O');

        }

        private async void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if(btnFSK.IsChecked == true)
            {
                await bridge.WriteCommandData(fskReset, false);
            }
            else
            {
                await bridge.WriteCommandData(vnReset, false);
            }
        }

        private async void btnTestMode_Click(object sender, RoutedEventArgs e)
        {
            if(btnFSK.IsChecked == true)
            {
                await bridge.WriteCommandData(fskTest, false);
            }
            else
            {
                await bridge.WriteCommandData(vnTest, false);
            }
        }

        private async void btnExitTest_Click(object sender, RoutedEventArgs e)
        {
            if(btnFSK.IsChecked == true)
            {
                await bridge.WriteCommandData(fskExitTest, false);
            }
            else
            {
                await bridge.WriteCommandData(vnExitTest, false);
            }
        }

        private async void btnExitFlowRate_Click(object sender, RoutedEventArgs e)
        {
            if(btnFSK.IsChecked == true)
            {
                await bridge.WriteCommandData(fskExitFlowRate, false);
            }
            else
            {
                await bridge.WriteCommandData(vnExitFlowRate, false);
            }
        }

        private async void btnFlowRate_Click(object sender, RoutedEventArgs e)
        {
            if(btnFSK.IsChecked == true)
            {
                await bridge.WriteCommandData(fskFlowRate, false);
            }
            else
            {
                await bridge.WriteCommandData(vnFlowRate, false);
            }
        }

        private async void btnExitRssi_Click(object sender, RoutedEventArgs e)
        {
               await bridge.WriteCommandData(exitRssi, false);
        }

        private async void btnRssi_Click(object sender, RoutedEventArgs e)
        {
            await bridge.WriteCommandData(Rssi, false);
        }

        private async void btnDaily_Click(object sender, RoutedEventArgs e)
        {
            await bridge.WriteCommandData(dailyBroadcast, false);
        }

        private async void btnLTE_Click(object sender, RoutedEventArgs e)
        {
            await bridge.WriteCommandData(LTEToggle, false);
        }

        private async void btnOTASP_Click(object sender, RoutedEventArgs e)
        {
            await bridge.WriteCommandData(Otasp, false);
        }

        private async void btnFSK_Checked(object sender, RoutedEventArgs e)
        {
            if (bridge != null)
            {
            await bridge.WriteCommandData(switchToI8, false);
            btnRssi.IsEnabled = false;
            btnExitRssi.IsEnabled = false;
            btnDaily.IsEnabled = false;
            btnOTASP.IsEnabled = false;
            btnLTE.IsEnabled = false;
            }
        }

        private async void btnVN_Checked(object sender, RoutedEventArgs e)
        {
            await bridge.WriteCommandData(switchToVN, false);
            btnRssi.IsEnabled = true;
            btnExitRssi.IsEnabled = true;
            btnDaily.IsEnabled = true;
            btnOTASP.IsEnabled = true;
            btnLTE.IsEnabled = true;
        }
    }
}
