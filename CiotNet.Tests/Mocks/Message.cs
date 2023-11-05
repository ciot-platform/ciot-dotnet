using CiotNetNS.Application.DTOs;
using CiotNetNS.Application.DTOs.Interface;
using CiotNetNS.Domain.Interfaces;

namespace CiotNet.Tests.Mocks
{
    public class MessageDtoMocks
    {
        public static readonly MessageDto Mock = new()
        {
            Type = MessageType.GetStatus,
            Interface = new InterfaceInfoDto()
            {
                Type = InterfaceType.System
            }
        };

        public static readonly byte[] MockData = new byte[] {
            0x04, 0x01, 0x00
        };
    }
}
