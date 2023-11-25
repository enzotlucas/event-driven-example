using Example.EventDriven.Domain.ValueObjects;

namespace Example.EventDriven.Domain.Entitites
{
    public class RequestEntity<T>
    {
        public Guid RequestId { get; set; }
        public ResponseMessage Message { get; set; }
        public RequestStatus Status { get; set; }
        public T Value { get; set; }

        public RequestEntity() { }

        public RequestEntity(ResponseMessage message, RequestStatus status)
        {
            Message = message;
            Status = status;
        }

        public RequestEntity(ResponseMessage message, RequestStatus status, T value)
        {
            Message = message;
            Status = status;
            Value = value;
        }
    }
}
