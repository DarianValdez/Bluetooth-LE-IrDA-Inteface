using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BluetoothService
{
    public delegate void DeviceInitializedHandler();
    public delegate void DeviceNotInitializedHandler();
    public delegate void DataStructureReceivedHandler(byte[] rxData);
    public delegate void ErrorReceivedHandler(byte[] rxData);

    public class BluetoothIrDAService
    {
        Guid serviceGuid;
        GattDeviceService service;
        GattCharacteristic commandData;
        GattCharacteristic respData;
        GattCharacteristic error;
        DeviceInformation device;

        private int _dataIndex = 0;
        private int _setRadioSize = 48;
        private byte[] _rxData = new byte[200];
        private byte[] _registerData;
        private bool _setRegister = false;

        public event DeviceInitializedHandler DeviceInitialized;
        public event DeviceNotInitializedHandler DeviceNotInitialied;
        public event DataStructureReceivedHandler DataStructureReceived;
        public event ErrorReceivedHandler ErrorRecerived;

        /// <summary>
        /// Creates a new BlutoothIrDA with the given service GUID.
        /// </summary>
        /// <param name="Guid">Primary service GUID</param>
        public BluetoothIrDAService(string Guid)
        {
            serviceGuid = new Guid(Guid);
        }

        /// <summary>
        /// Creates a new BluetoothIrDA with the default service GUID.
        /// </summary>
        public BluetoothIrDAService()
        {
            serviceGuid = new Guid("73e22652-55de-4339-950c-78a0d8aba637");
        }

        /// <summary>
        /// Finds connected Bluetooth device with the service GUID.
        /// </summary>
        /// <exception cref="System.TimeoutException">
        /// Could not find connected IrDA bridge within timeout.
        /// </exception>
        /// <returns></returns>
        public async void InitializeTranciever()
        {
            device = (await DeviceInformation.FindAllAsync(GattDeviceService.GetDeviceSelectorFromUuid(serviceGuid)))[0];
            if (device != null)
            {
                GattCommunicationStatus status;

                service = await GattDeviceService.FromIdAsync(device.Id);
                commandData = service.GetCharacteristics(new Guid("d40c53c0-4462-4714-99cc-f90772bd7f61"))[0];
                respData = service.GetCharacteristics(new Guid("54d8deac-1c1d-4841-947f-ac4406f3034e"))[0];
                error = service.GetCharacteristics(new Guid("c29a164e-9406-4d81-800b-29efcb447933"))[0];
                respData.ValueChanged += Characteristic_ValueChanged;
                error.ValueChanged += Characteristic_ValueChanged;

                status = await respData.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
                status = await error.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);

                if (status == GattCommunicationStatus.Success)
                    DeviceInitialized();
                else
                    DeviceNotInitialied();
            }
        }

        /// <summary>
        /// Private Event Handler: Copies all received data into rxData, then raises the appropriate event to handle the data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            byte[] data = (await sender.ReadValueAsync()).Value.ToArray();
            foreach (byte b in data)
            {
                _rxData[_dataIndex] = b;
                _dataIndex++;
            }
            if (_setRegister && _dataIndex == 146)
            {
                _setRegister = false;
                _dataIndex = 0;
                await WriteToGatt(_registerData);
            }
            else if (_dataIndex == 146)
            {
                _dataIndex = 0;
                DataStructureReceived(_rxData);
            }
            else if (sender == error)
            {
                _dataIndex = 0;
                ErrorRecerived(data);
            }
        }

        /// <summary>
        /// General purpose data transmission to the CommandData Characteristic. Calculates CRC16 and writes data to the Command Characteristic.
        /// </summary>
        /// <param name="data">
        /// Data to be written to Command Characteristic.
        /// </param>
        /// <param name="calcCRC">
        /// Calculate and append CRC16
        /// </param>
        /// <returns></returns>
        public async Task WriteCommandData(byte[] data, bool calcCRC)
        {
            byte[] writeData;
            if (calcCRC)
            {
                writeData = new byte[data.Length + 2];
                Array.Copy(data, data, data.Length);
                Array.Copy(CRC16(data, (uint)(data.Length - 1)), 0, data, data.Length - 1, 2);
            }
            else
            {
                writeData = data;
            }
            await WriteToGatt(writeData);
        }

        /// <summary>
        /// Transmit SET Radio/Register Commands.
        /// </summary>
        /// <param name="radio">
        /// Radio configuration data.
        /// </param>
        /// <param name="register">
        /// Register configuration data.
        /// </param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public async Task WriteCommandData(byte[] radio, byte[] register, char direction)
        {
            byte[] radioData;

            radioData = new byte[radio.Length + 3];
            _registerData = new byte[register.Length + 3];

            _setRadioSize = radioData.Length;

            Array.Copy(radio, radioData, radio.Length);
            Array.Copy(register, _registerData, register.Length);

            Array.Copy(CRC16(radio, (uint)(radio.Length)), 0, radioData, radio.Length, 2);
            Array.Copy(CRC16(register, (uint)(register.Length)), 0, _registerData, register.Length, 2);

            radioData[radio.Length + 2] = (byte)direction;
            _registerData[register.Length + 2] = (byte)direction;

            _setRegister = true;
            await WriteToGatt(radioData);
        }

        /// <summary>
        /// Private Task: Write to Command Data Characteristic in 19 byte chunks.
        /// </summary>
        /// <param name="data">
        /// Data to write.
        /// </param>
        /// <returns></returns>
        private async Task WriteToGatt(byte[] data)
        {
            int length;
            for (int i = 0; i < data.Length; i += 19)
            {
                length = 19;
                if (data.Length - i < 19)
                {
                    length = data.Length - i;
                }
                byte[] temp = new byte[length + 1];
                Array.Copy(data, i, temp, 1, length);
                temp[0] = (byte)(data.Length);
                await commandData.WriteValueAsync(temp.AsBuffer());
            }
        }

        /// <summary>
        /// Standard CRC16 algorithm for all IrDA enabled devices.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public byte[] CRC16(byte[] p, uint n)
        {
            Byte i;
            int index = 0;
            ushort crc = 0xFFFF;
            ushort POLY = 0xA001;

            while (n > 0)
            {
                crc ^= p[index++];
                for (i = 8; i != 0; i--)
                {
                    if ((crc & 0x01) == 0x01)
                        crc = (ushort)((crc >> 0x01) ^ POLY);
                    else
                        crc >>= 1;
                }
                n--;
            }
            byte[] calcCRC = BitConverter.GetBytes((short)crc);
            Array.Reverse(calcCRC);
            return (calcCRC);
        }
    }
}
