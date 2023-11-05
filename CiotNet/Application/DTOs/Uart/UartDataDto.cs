using CiotNet.Serializer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs.Uart
{
    public interface IUartData { }

    public class UartDataDto : Union<IUartData>, IMessageData
    {
        public UartConfigDto Config { get => Get<UartConfigDto>(); set => Set(value); }
        public UartStatusDto Status { get => Get<UartStatusDto>(); set => Set(value); }
        public UartRequestDto Request { get => Get<UartRequestDto>(); set => Set(value); }
    }
}
