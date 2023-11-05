using CiotNet.Serializer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs.Uart
{
    public enum UartRequestId
    {
        Unknown,
        SendData
    }

    public interface IUartRequestData { }

    public class UartRequestSendData : IUartRequestData
    {
        [Size(255)]
        public byte[] Data { get; set; }
    }

    public class UartRequestData : Union<IUartRequestData>
    {
        public UartRequestSendData SendData { get => Get<UartRequestSendData>(); set => Set(value); }
    }

    public class UartRequestDto : IUartData, IMessageRequest
    {
        public UartRequestId Id { get; set; }
        public UartRequestData Data { get; set; }
    }

}
