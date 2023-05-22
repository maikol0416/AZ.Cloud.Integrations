using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : BaseEntity
    {

        public User()
        {
        }

        public User(string userName, string email, string name, string password, List<Rol> roles)
        {
            UserName = userName;
            Email = email;
            Nombre = name;
            Password = password;
            Roles = roles;
        }


        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Nombre { get; private set; }
        public string Password { get; private set; }
        public List<Rol> Roles { get; private set; }
    }
}
