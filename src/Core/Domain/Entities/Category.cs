using Domain.Entities.Base;

namespace Domain.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}