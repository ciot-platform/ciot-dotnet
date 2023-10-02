using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Domain.Enums
{
    public enum InterfaceType
    {
        Unknown,
        System,
        Serial,
        Hid,
        Ble,
        Ethernet,
        Wifi,
        Ntp = 32,
        Mqtt,
        Ota
    }
}
