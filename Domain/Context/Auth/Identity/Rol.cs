using Domain.AggregateModels;
using Domain.Common;
using Domain.ValueObject;

namespace Domain.Entities
{
    public class Rol : BaseEntity
    {
        public Rol()
        {

        }
        public Rol(string name, string description, bool root)
        {
            setProperties(name, description, root);
        }
        public Rol(string id, string name, string description, bool root)
        {
            Id = id;
            setProperties(name, description, root);
        }

        private void setProperties(string name, string description, bool root)
        {
            Name = NameValueObject.Create(name, RolMetadata.Name).Value.Value;
            Description = NameValueObject.Create(description, RolMetadata.Description).Value.Value;
            Root = root;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Root { get; private set; }
    }
}
