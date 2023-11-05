using CiotNet.Serializer.Domain.Interfaces;
using CiotNet.Serializer.Infrastructure;
using CiotNetNS.Application.DTOs;
using CiotNetNS.Application.DTOs.Interface;
using CiotNetNS.Application.DTOs.Uart;
using CiotNetNS.Domain.Abstractions;
using CiotNetNS.Domain.Interfaces;
using System;

namespace CiotNetNS.Infrastructure
{
    public class Uart : InterfaceBase <UartConfigDto, UartStatusDto, UartRequestDto>
    {
        private ISerialWrapper serial;
        private ISerializer serializer;
        private ICiotS ciotS;

        public override UartConfigDto Config { get; }

        public override UartStatusDto Status { get; }

        public override event EventHandler<InterfaceEvent> InterfaceEvent;

        public Uart(ISerialWrapper serial, ICiotS ciotS, ISerializer serializer = null)
        {
            if (serializer == null) serializer = DefaultSerializer.instance;
            this.serial = serial;
            this.ciotS = ciotS;
        }

        public override ErrorCode Start(UartConfigDto config)
        {
            serial.Port = "COM" + config.Num;
            serial.BaudRate = (int)config.BaudRate;
            try
            {
                serial.Open();
                return serial.IsOpen ? ErrorCode.Ok : ErrorCode.Fail;
            }
            catch (Exception)
            {
                return ErrorCode.Fail;
            }
        }

        public override ErrorCode Stop()
        {
            try
            {
                serial.Close();
                return ErrorCode.Ok;
            }
            catch (Exception)
            {
                return ErrorCode.Fail;
            }
        }

        public override ErrorCode ProcessRequest(UartRequestDto request)
        {
            switch (request.Id)
            {
                default:
                    break;
            }
            return ErrorCode.Ok;
        }

        public override ErrorCode SendData(byte[] data)
        {
            return ciotS.Send(data);
        }

        public override ErrorCode SendMessage(MessageDto message)
        {
            throw new NotImplementedException();
        }
    }
}
