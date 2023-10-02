using System;
using System.IO.Ports;

namespace CiotNetNS.Domain.Abstractions
{
    public class SerialAdapter : ISerialWrapper
    {
        public event EventHandler<byte[]> DataReceived;

        readonly SerialPort serial;

        public string Port { get => serial.PortName; set => serial.PortName = value; }

        public bool IsOpen => serial?.IsOpen ?? false;

        public SerialAdapter(SerialPort serial)
        {
            this.serial = serial;
            this.serial.DataReceived += Serial_DataReceived;
        }

        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = new byte[serial.BytesToRead];
            serial.Read(data, 0, data.Length);
            DataReceived?.Invoke(this, data);
        }

        public void Close()
        {
            serial.Close();
        }

        public void Open()
        {
            serial.Open();
        }

        public void Write(byte[] data)
        {
            serial.Write(data, 0, data.Length);
        }

        public void Read(byte[] data)
        {
            serial.Read(data, 0, data.Length);
        }

        public string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
