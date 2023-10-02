using CiotNet.Serializer.Domain.Interfaces;
using CiotNetNS.Application.DTOs;
using CiotNetNS.Domain.Enums;
using CiotNetNS.Domain.Interfaces;
using CiotNetNS.Properties;
using CiotNetNS.Shared;
using System;

namespace CiotNetNS.Infrastructure.Serial
{
    public class SerialProtocol : IProtocol
    {
        public event EventHandler<MessageDto> OnMessage;
        public event EventHandler<Result> OnError;

        private enum MessageStatus
        {
            WaitStartData,
            WaitSize,
            ReadData,
            SerializeData,
            Done,
        }

        private readonly IConnection connection;
        private readonly ISerializer serializer;
        private MessageStatus status;

        private const byte startChar = (byte)'{';
        private const byte endChar = (byte)'}';

        private const int sizeByte1Idx = 1;
        private const int sizeByte2Idx = 2;
        private const int headerSize = 3;
        private const int msgInterfaceIdx = 3;
        private const int msgTypeIdx = 4;

        private readonly byte[] buffer;
        private short size = 0;
        private int idx = 0;

        private MessageStatus Status { get => status; set => status = value; }

        public SerialProtocol(IConnection connection, ISerializer serializer, int size = 4095)
        {
            this.connection = connection;
            this.serializer = serializer;
            buffer = new byte[size];
            connection.DataReceived += Connection_DataReceived;
        }

        private void Connection_DataReceived(object sender, byte[] e)
        {
            foreach (byte b in e)
            {
                var result = ProcessByte(b, e.Length);
                if (result.Failed)
                {
                    OnError?.Invoke(this, result);
                }
            }
        }

        public Result SendMessage(MessageDto message)
        {
            byte[] @byte;

            try
            {
                @byte = serializer.Serialize(message);
            }
            catch (Exception ex)
            {
                return Result.Failure(ErrorCode.SerializationError, ex.Message);
            }

            var size = BitConverter.GetBytes((short)@byte.Length);

            try
            {
                connection.SendData(new byte[1] { startChar });
                connection.SendData(size);
                connection.SendData(@byte);
                connection.SendData(new byte[] { endChar });

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ErrorCode.SendDataError, ex.Message);
            }
        }

        private Result ProcessByte(byte @byte, int len)
        {
            if (idx < buffer.Length)
            {
                buffer[idx] = @byte;
            }
            else
            {
                return Result.Failure(ErrorCode.BufferOverflow, Messages.BufferOverflow);
            }

            switch (Status)
            {
                case MessageStatus.WaitStartData:
                    if (@byte == startChar)
                    {
                        size = 0;
                        Status = MessageStatus.WaitSize;
                        if (idx != 0)
                        {
                            idx = 0;
                            buffer[idx] = @byte;
                        }
                    }
                    break;
                case MessageStatus.WaitSize:
                    size++;
                    if (size == 2)
                    {
                        size = (short)(buffer[sizeByte2Idx] << 8 | buffer[sizeByte1Idx]);
                        Status = MessageStatus.ReadData;
                    }
                    break;
                case MessageStatus.ReadData:
                    if (@byte == endChar)
                    {
                        if (idx - headerSize == size)
                        {
                            byte[] msg = new byte[size];
                            var @interface = (InterfaceType)buffer[msgInterfaceIdx];
                            var type = (MessageType)buffer[msgTypeIdx];
                            Array.Copy(buffer, msgInterfaceIdx, msg, 0, msg.Length);
                            var data = new MessageDto(@interface, type, msg);
                            OnMessage?.Invoke(this, data);
                            Status = MessageStatus.Done;
                        }
                        else
                        {
                            idx = 0;
                            Status = MessageStatus.WaitStartData;
                            return Result.Failure(ErrorCode.IncorrectSize, Messages.IncorrectSize);
                        }
                    }
                    if (idx == len - 1)
                    {
                        idx = 0;
                        Status = MessageStatus.WaitStartData;
                        return Result.Failure(ErrorCode.TerminatorMissing, Messages.TerminatorMissing);
                    }
                    break;
                case MessageStatus.Done:

                    break;
            }

            idx++;

            return Result.Success();
        }
    }
}
