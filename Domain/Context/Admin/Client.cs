using Domain.Common;

#nullable disable

namespace Domain.Entities
{
    public partial class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
