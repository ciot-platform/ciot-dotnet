using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs.Uart
{
    public enum UartState
    {
        Disconnected,
        Connected,
        Error
    }

    public class UartStatusDto : IUartData, IMessageStatus
    {
        public UartState State { get; set; }
    }
}
