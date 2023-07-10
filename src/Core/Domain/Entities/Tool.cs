using Domain.Entities.Base;

namespace Domain.Entities
{
    public class Tool : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public byte[] Image { get; set; }
        public string Technologies { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}