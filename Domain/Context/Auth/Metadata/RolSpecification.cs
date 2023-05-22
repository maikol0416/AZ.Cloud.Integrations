using Domain.Common.Specification;
using Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Domain.AggregateModels
{
    public class RolSpecification : SpecificationBasic<Rol>
    {
        public static Expression<Func<Rol, bool>> ExistRolByName(string name)
        {
            return x => x.Name == name;
        }
        public static Expression<Func<Rol, bool>> ExistRolByDescription(string description)
        {
            return x => x.Description == description;
        }
    }
}
