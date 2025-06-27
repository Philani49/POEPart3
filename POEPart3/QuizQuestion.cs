namespace POEPart3
{
    public class QuizQuestion
    {
        //getters and setters
        public string Question { get; set; }
        public string CorrectChoice { get; set; }

        //get and set on the list
        public List<string> Choices { get; set; }

    }
}