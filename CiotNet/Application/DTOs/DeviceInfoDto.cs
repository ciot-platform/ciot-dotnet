using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs
{
    public class DeviceInfoDto
    {
        public int ErrorCode { get; set; }

        public string HardwareVer { get; set; }

        public byte[] FirmwareVer { get; set; }
    }
}
