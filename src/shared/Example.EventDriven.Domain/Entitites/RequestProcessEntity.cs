namespace Example.EventDriven.Domain.Entitites
{
    public class RequestProcessEntity<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public bool IsCompleted { get; set; }
        public T Value { get; set; }
    }
}
