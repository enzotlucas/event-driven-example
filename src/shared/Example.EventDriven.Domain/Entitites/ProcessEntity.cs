using Example.EventDriven.Domain.ValueObjects;

namespace Example.EventDriven.Domain.Entitites
{
    public sealed class ProcessEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProcessStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public bool Exists()
        {
            return Id != Guid.Empty && CreatedAt != DateTime.MinValue; 
        }

        public ProcessEntity()
        {
            Id = Guid.Empty;
            CreatedAt = DateTime.MinValue;
        }
    }
}
