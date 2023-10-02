using CiotNet.Serializer.Infrastructure;

namespace CiotNetNS.Application.DTOs.System
{
    public class SystemStatusDto
    {
        public int ErrorCode { get; set; }

        public SystemInfoDto Info { get; set; }
    }
}
