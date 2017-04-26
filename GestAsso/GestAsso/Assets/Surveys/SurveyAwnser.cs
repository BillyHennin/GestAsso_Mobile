using System.Collections.Generic;
using GestAsso.Assets.Users;

namespace GestAsso.Assets.Surveys
{
    public class SurveyAwnser
    {
        public UserProfile User { get; set; }
        public List<Awnsers> AwnsersList { get; set; }
    }
}