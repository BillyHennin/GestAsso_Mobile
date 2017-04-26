using System.Collections.Generic;

namespace GestAsso.Assets.Users
{
    public class UserTeam
    {
        public List<UserProfile> UsersInTeam { get; set; }

        public string Title { get; set; }
        //Chan discussion
        //

        public string MailAdminTeam { get; set; }
        public void SetAdmin(UserProfile user) => SetAdmin(user.Email);
        public void SetAdmin(string mail) => MailAdminTeam = mail;
    }
}