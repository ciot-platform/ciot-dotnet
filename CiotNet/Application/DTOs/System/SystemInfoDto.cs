using CiotNet.Serializer.Infrastructure;
using CiotNetNS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs.System
{
    [Flags]
    public enum FeatureFlags
    {
        None = 0,
        Ble = 1 << 0,
        BleDfu = 1 << 1,
        Wifi = 1 << 2,
        Eth = 1 << 3,
        Rs485 = 1 << 4,
        UsbComm = 1 << 5,
        UsbPortA = 1 << 6,
        UsbMicroPortB = 1 << 7,
        Storage = 1 << 8,
        Ntp = 1 << 9,
        Mqtt = 1 << 10,
        HttpServer = 1 << 11,
        HttpClient = 1 << 12,
        ModbusRtu = 1 << 13,
        ModbusTcp = 1 << 14,
        Ota = 1 << 15,
        Datalogger = 1 << 16,
    }

    public class SystemInfoDto
    {
        [Size(16)]
        public byte[] HardwareName { get; set; }

        [Size(3)]
        public byte[] FirmwareVer { get; set; }

        public FeatureFlags Features { get; set; }
    }
}
