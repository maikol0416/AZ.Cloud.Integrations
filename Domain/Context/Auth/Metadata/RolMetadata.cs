using Domain.Common;
using Domain.Entities;

namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion la metadata para la Entidad (Empresa)
    /// </summary>
    public static class RolMetadata
    {
        public static Metadata Name => new(nameof(Rol.Name), nameof(Rol.Name), 100, 5);
        public static Metadata Description => new(nameof(Rol.Description), nameof(Rol.Description), 250, 1);

    }
}
