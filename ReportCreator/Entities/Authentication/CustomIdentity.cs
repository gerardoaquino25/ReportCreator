﻿using System.Security.Principal;

namespace ReportCreator.Entities.Authentication
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name, string email, string[] roles)
        {
            Name = name;
            Email = email;
            Roles = roles;
        }

        public CustomIdentity(int id, string name, string email, string[] roles)
        {
            Id = id;
            Name = name;
            Email = email;
            Roles = roles;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string[] Roles { get; private set; }

        #region IIdentity Members
        public string AuthenticationType { get { return "Custom authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
        #endregion
    }
}
