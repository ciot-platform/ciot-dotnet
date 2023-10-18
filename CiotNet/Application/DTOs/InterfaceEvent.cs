using CiotNetNS.Domain.Enums;

namespace CiotNetNS.Application.DTOs
{
    public class InterfaceEvent
    {
        public InterfaceEventId Id { get; set; }

        public MessageDto Message { get; set; }

        public object Args { get; set; }
    }
}
