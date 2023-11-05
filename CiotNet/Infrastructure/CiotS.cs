using CiotNetNS.Application.DTOs;
using CiotNetNS.Domain.Interfaces;
using System;

namespace CiotNetNS.Infrastructure
{
    public class CiotS : ICiotS
    {
        enum Status
        {
            WaitStartData,
            WaitSize,
            ReadData,
        }

        const byte START_CH = (byte)'{';
        const byte END_CH = (byte)'}';
        const byte LENGHT_SIZE = 2;

        private readonly IInterface iface;
        private Status status;
        private byte[] buf;
        int idx;
        int len;

        public event EventHandler<byte[]> OnMessage;

        public CiotS(IInterface iface, int size = 512) 
        {
            this.iface = iface;
            buf = new byte[size];
        }

        public ErrorCode Send(byte[] data)
        {
            if (data == null) return ErrorCode.NullArg;

            byte[] header =
            {
                (byte)'{', 
                (byte)(data.Length & 0xff), 
                (byte)((data.Length >> 8) & 0xFF)
            };
            byte[] end = { (byte)'}' };

            iface.SendData(header);
            iface.SendData(data);
            iface.SendData(end);

            return ErrorCode.Ok;
        }

        public ErrorCode ProcessByte(byte @byte)
        {
            if(idx < buf.Length && @byte != END_CH)
            {
                buf[idx] = @byte;
            }
            else
            {
                return ErrorCode.Overflow;
            }

            switch (status)
            {
                case Status.WaitStartData:
                    if(@byte == START_CH)
                    {
                        idx = 0;
                        len = 0;
                        buf[idx] = @byte;
                        status = Status.WaitSize;
                    }
                    break;
                case Status.WaitSize:
                    len++;
                    if(len == LENGHT_SIZE)
                    {
                        len = buf[2] << 8 | buf[1];
                        status = Status.ReadData;
                        idx = 0;
                    }
                    break;
                case Status.ReadData:
                    if (@byte == END_CH)
                    {
                        if (idx == len)
                        {
                            OnMessage?.Invoke(this, buf);
                            status = Status.WaitStartData;
                            return ErrorCode.Ok;
                        }
                        else
                        {
                            idx = 0;
                            status = Status.WaitStartData;
                            return ErrorCode.InvalidSize;
                        }
                    }
                    if (idx == len)
                    {
                        idx = 0;
                        status = Status.WaitStartData;
                        return ErrorCode.TerminatorMissing;
                    }
                    break;
                default:
                    break;
            }

            return ErrorCode.Ok;
        }
    }
}
