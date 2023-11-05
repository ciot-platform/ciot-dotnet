using CiotNet.Serializer.Infrastructure;
using System;

namespace CiotNetNS.Application.DTOs.System
{
    [Flags]
    public enum HardwareFeatureFlags
    {
        None = 0,
        Uart = 1 << 0,
        Ethernet = 1 << 1,
        Wifi = 1 << 2,
        Bluetooth = 1 << 3,
    }

    [Flags]
    public enum SoftwareFeatureFlags
    {
        None = 0,
        Ntp = 1 << 0,
        Ota = 1 << 1,
        HttpClient = 1 << 2,
        HttpServer = 1 << 3,
        MqttClient = 1 << 4,
    }

    public class SystemFeaturesDto
    {
        public HardwareFeatureFlags Hardware { get; set; }

        public SoftwareFeatureFlags Software { get; set; }
    }

    public class SystemInfoDto
    {
        [Size(16)]
        public byte[] HardwareName { get; set; }

        [Size(3)]
        public byte[] FirmwareVersion { get; set; }

        public HardwareFeatureFlags Features { get; set; }
    }

    public class SystemStatusDto
    {
        public int ResetReason { get; set; }

        public int ResetCount { get; set; }

        public uint FreeMemory { get; set; }

        public uint Lifetime { get; set; }

        public SystemInfoDto Info { get; set; }
    }
}
