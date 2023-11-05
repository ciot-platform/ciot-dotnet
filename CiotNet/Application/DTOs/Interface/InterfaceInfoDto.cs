using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs.Interface
{
    public enum InterfaceType
    {
        Unknown,
        System,
        Uart,
        Tcp,
        Ethernet,
        Wifi,
        Bluetooth,
        Ntp,
        Ota,
        HttpClient,
        HttpServer,
        Mqtt
    }

    public class InterfaceInfoDto
    {
        public InterfaceType Type { get; set; }

        public byte Id { get; set; }
    }
}
