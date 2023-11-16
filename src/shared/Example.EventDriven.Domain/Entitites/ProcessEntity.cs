namespace Example.EventDriven.Domain.Entitites
{
    public class ProcessEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
