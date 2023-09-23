using CiotNetNS.Domain.Enums;
using CiotNetNS.Domain.Interfaces;
using CiotNetNS.Shared;
using System.IO.Ports;
using System;
using CiotNetNS.Application.DTOs;

namespace CiotNetNS.Infrastructure
{
    public class UsbProtocolSerial : IUsbProtocol
    {
        public ProtocolTypeUsb ProtocolType => throw new NotImplementedException();

        private SerialPort serial;

        public event EventHandler<MessageDto> DataReceived;

        public UsbProtocolSerial(SerialPort serial)
        {
            this.serial = serial;

            serial.DataReceived += Serial_DataReceived;
        }

        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
        }

        public Result Connect()
        {
            var ports = SerialPort.GetPortNames();
            var msg = new MessageDto(MessageType.GetDeviceInfo);

            foreach (var port in ports)
            {
                if (serial.IsOpen) serial.Close();
                serial.PortName = port;
                try
                {
                    serial.Open();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            return Result.Failure();
        }

        public Result Disconnect()
        {
            throw new NotImplementedException();
        }

        public Result SendData(MessageDto message)
        {
            throw new NotImplementedException();
        }
    }
}
