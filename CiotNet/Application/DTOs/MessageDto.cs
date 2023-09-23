
using CiotNetNS.Domain.Enums;

namespace CiotNetNS.Application.DTOs
{
    public class MessageDto
    {
        public MessageType Type { get; set; }

        public object Data { get; set; }

        public MessageDto(MessageType type, object data = null)
        {
            Type = type;
            Data = data;
        }
    }
}
