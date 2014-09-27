using System;
using System.Collections.Generic;
using Starscream.Domain.ValueObjects;

namespace Starscream.Domain.Entities
{
    public class User : Entity
    {
        public virtual string Name { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Profile {get; protected set; }
        public virtual bool IsActive { get; protected set; }
         IEnumerable<Role> _userRoles = new List<Role>(); 

        public User(string name, string email)
        {
            Name = name;
            Email = email;
            Id = Guid.NewGuid();
            IsActive = true;
            Profile = "Default Profile";
        }

        protected User()
        {
            
        }

        public virtual void ChangeEmailAddress(string emailAddress)
        {
            Email = emailAddress;
        }

        public virtual void AssignProfile(IProfile profile)
        {
            Profile = profile.Name;
        }

        public virtual void EnableUser()
        {
            IsActive = true;
        }

        public virtual void DisableUser()
        {
            IsActive = false;
        }

        public virtual void ChangeName(string name)
        {
            Name = name;
        }

        public virtual IEnumerable<Role> UserRoles
        {
            get { return _userRoles; } 
            protected set { _userRoles = value; }
        }

        public virtual void AddRol(Role role)
        {
           ( (IList<Role>)_userRoles).Add(role);
        }

    }
}