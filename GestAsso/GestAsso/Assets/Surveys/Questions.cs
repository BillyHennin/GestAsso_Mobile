using System.Collections.Generic;

namespace GestAsso.Assets.Surveys
{
    public enum QuestionType
    {
        YesNo,
        List,
        SingleChoice,
        MultipleChoice,
        Free
    }

    public class Questions
    {
        public string Title { get; set; }
        public QuestionType TypeQuestion { get; set; }
        public List<Awnsers> AwnsersList { get; set; }
    }
}