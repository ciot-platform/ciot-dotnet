using CiotNetNS.Domain.Abstractions;
using CiotNetNS.Domain.Enums;
using CiotNetNS.Domain.Interfaces;
using CiotNetNS.Properties;
using CiotNetNS.Shared;
using System;
using System.Threading.Tasks;

namespace CiotNetNS.Infrastructure.Serial
{
    public class SerialConnection : IConnection
    {
        public ConnectionType ConnectionType => throw new NotImplementedException();

        public event EventHandler<byte[]> DataReceived;

        private readonly ISerialWrapper serial;
        private readonly ISerialScanner scanner;

        public SerialConnection(ISerialWrapper serial, ISerialScanner scanner)
        {
            this.serial = serial;
            this.scanner = scanner;
            this.serial.DataReceived += Serial_DataReceived;
        }

        private void Serial_DataReceived(object sender, byte[] data)
        {
            DataReceived?.Invoke(this, data);
        }

        public async Task<Result> Connect()
        {
            try
            {
                return await scanner.Scan();
            }
            catch (Exception ex)
            {
                return Result.Failure(ErrorCode.Exception, ex.Message);
            }
        }

        public Result Disconnect()
        {
            try
            {
                if (serial.IsOpen)
                {
                    serial.Close();
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ErrorCode.Exception, ex.Message);
            }

            return !serial.IsOpen ? Result.Success() : Result.Failure(ErrorCode.ConnectError, Messages.DisconnectError);
        }

        public Result SendData(byte[] data)
        {
            if (serial.IsOpen)
            {
                try
                {
                    serial.Write(data);
                    return Result.Success();
                }
                catch (Exception ex)
                {
                    return Result.Failure(ErrorCode.Exception, ex.Message);
                }
            }
            else
            {
                return Result.Failure(ErrorCode.SerialPortClosed, Messages.SerialPortClosed);
            }
        }
    }
}
