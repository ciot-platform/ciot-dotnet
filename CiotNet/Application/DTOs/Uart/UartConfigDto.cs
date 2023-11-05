using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs.Uart
{
    public class UartConfigDto : IUartData, IMessageConfig
    {
        public uint BaudRate { get; set; }

        public byte Num {  get; set; }

        public byte RxPin { get; set; }

        public byte TxPin { get; set; }

        public byte RstPin { get; set; }

        public byte CtsPin { get; set; }

        public byte FlowControl { get; set; }

        public byte Parity { get; set; }
    }
}
