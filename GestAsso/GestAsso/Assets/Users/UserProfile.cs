using System;
using System.Collections.Generic;
using GestAsso.Assets.ToolBox;
using Newtonsoft.Json;

namespace GestAsso.Assets.Users
{
    public enum UserRole
    {
        President,
        VicePresident,
        Secretaire,
        Tresorier,
        Adherent,
        Bizut,
        EnAttente
    }

    //[Serializable]
    public class UserProfile
    {
        [JsonProperty]
        private Guid ID { get; set; }

        [JsonProperty]
        public string Email { get; set; }

        [JsonProperty]
        public string FirstName { get; set; }

        [JsonProperty]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [JsonProperty]
        public UserRole Role { get; set; }

        [JsonProperty]
        public string Phone { get; set; }

        public UserProfile(IReadOnlyDictionary<string, string> dictionary)
        {
            ID = new Guid(dictionary["ID"]);
            Email = dictionary["Email"];
            FirstName = dictionary["FirstName"];
            LastName = dictionary["LastName"];
            Role = (UserRole) Enum.Parse(typeof(UserRole), dictionary["UserRole"], true);
            Phone = dictionary["Phone"];
        }

        #region "Update"

        public enum UpdateType
        {
            Email,
            Phone,
            Role
        }

        private bool Update(object newValue, UpdateType updateType) 
            => XApi.UpdateUser(ID, updateType.ToString(), newValue);

        public bool UpdateMail(string newEmail)
        {
            var updateSuccess = Update(newEmail, UpdateType.Email);
            if (updateSuccess)
            {
                Email = newEmail;
            }
            return updateSuccess;
        }

        public bool UpdateTelephone(string newPhone)
        {
            var updateSuccess = Update(newPhone, UpdateType.Phone);
            if (updateSuccess)
            {
                Phone = newPhone;
            }
            return updateSuccess;
        }

        public bool UpdateRole(UserRole newRole)
        {
            var updateSuccess = Update(newRole, UpdateType.Role);
            if (updateSuccess)
            {
                Role = newRole;
            }
            return updateSuccess;
        }
        #endregion
    }
}