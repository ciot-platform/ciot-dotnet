using CiotNetNS.Application.DTOs;
using CiotNetNS.Domain.Abstractions;
using CiotNetNS.Domain.Enums;
using CiotNetNS.Domain.Interfaces;
using CiotNetNS.Properties;
using CiotNetNS.Shared;
using System;
using System.Threading.Tasks;
using System.Threading;
using CiotNetNS.Application.DTOs.System;

namespace CiotNetNS.Infrastructure.Serial
{
    public class SerialScan : ISerialScanner
    {
        private readonly IProtocol protocol;
        private readonly ISerialWrapper serial;
        private readonly int timeout;

        public event EventHandler<SystemStatusDto> OnConnection;

        public SerialScan(IProtocol protocol, ISerialWrapper serial, int timeout = 250)
        {
            this.protocol = protocol;
            this.serial = serial;
            this.timeout = timeout;
            this.protocol.OnMessage += Protocol_OnMessage;
        }

        private void Protocol_OnMessage(object sender, MessageDto e)
        {
            if (e != null && e.Interface.Type == InterfaceType.System && e.Type == MessageType.GetStatus)
            {
                OnConnection?.Invoke(this, e.Data.Get<SystemStatusDto>());
            }
        }

        public async Task<Result> Scan()
        {
            var ports = serial.GetPorts();
            var connMsg = new MessageDto(MessageType.GetStatus, InterfaceType.System);

            foreach (var port in ports)
            {
                try
                {
                    var cancellationTokenSource = new CancellationTokenSource(timeout);
                    var task = WaitForProtocolMessageAsync(cancellationTokenSource.Token);

                    serial.Close();
                    serial.Port = port;
                    serial.Open();
                    protocol.SendMessage(connMsg);

                    if (await Task.WhenAny(task, Task.Delay(timeout, cancellationTokenSource.Token)) == task)
                    {
                        return Result.Success();
                    }
                }
                catch (Exception)
                {

                }
            }

            return Result.Failure(ErrorCode.NotFound, Messages.NotFound);
        }

        private Task WaitForProtocolMessageAsync(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();

            void handler(object sender, MessageDto e)
            {
                protocol.OnMessage -= handler;
                tcs.TrySetResult(true);
            }

            protocol.OnMessage += handler;

            cancellationToken.Register(() =>
            {
                protocol.OnMessage -= handler;
                tcs.TrySetCanceled();
            });

            return tcs.Task;
        }
    }
}
