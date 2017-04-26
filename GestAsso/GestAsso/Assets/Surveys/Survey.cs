using System.Collections.Generic;

namespace GestAsso.Assets.Surveys
{
    public class Survey
    {
        public List<Questions> ListQuestions { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<SurveyAwnser> ListAwnsers { get; set; }
    }
}