using CiotNet.Serializer.Infrastructure;
using CiotNetNS.Application.DTOs;
using CiotNetNS.Application.DTOs.Interface;
using CiotNetNS.Application.DTOs.Uart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CiotNet.Tests.Infrastructure
{
    public class MessageDtoTests
    {
        [Test]
        public void SerialzationTest()
        {
            var msg = new MessageDto();
            msg.Type = MessageType.Start;
            msg.Interface.Id = 0;
            msg.Interface.Type = InterfaceType.Uart;
            msg.Data.Uart = new UartDataDto()
            {
                Config = new UartConfigDto()
                {
                    BaudRate = 115200,
                    Num = 13
                }
            };

            var serializer = new BinarySerializer();
            var data = serializer.Serialize(msg);

            var newMsg = serializer.Deserialize<MessageDto>(data);

            Console.WriteLine("FIM");
        }
    }
}
