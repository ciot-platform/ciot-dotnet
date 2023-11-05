namespace CiotNetNS.Application.DTOs.Interface
{
    public enum InterfaceEventId
    {
        Unknown = 0,
        Started,
        Stopped,
        Error,
        Data,
        ReqDone,
        Custom
    }

    public class InterfaceEvent
    {
        public InterfaceEventId Id { get; set; }

        public MessageDto Message { get; set; }

        public object Args { get; set; }
    }
}
