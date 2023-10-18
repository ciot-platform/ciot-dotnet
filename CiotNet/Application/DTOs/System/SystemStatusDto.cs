using CiotNet.Serializer.Infrastructure;
using System;

namespace CiotNetNS.Application.DTOs.System
{
    [Flags]
    public enum FeatureFlags
    {
        None = 0,
        Serial = 1 << 0,
        HttpClient = 1 << 1,
        HttpServer = 1 << 2,
        MqttClient = 1 << 3,
        MqttServer = 1 << 4,
    }

    public class SystemInfoDto
    {
        [Size(16)]
        public byte[] HardwareName { get; set; }

        [Size(3)]
        public byte[] FirmwareVersion { get; set; }

        public FeatureFlags Features { get; set; }
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
