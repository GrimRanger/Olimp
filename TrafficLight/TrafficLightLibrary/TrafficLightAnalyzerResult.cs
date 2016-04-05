namespace TrafficLight.Domain.Core
{
    public class TrafficLightResult
    {
        public TrafficLightResult(int userAnswer, int rightAnswer, int steps)
        {
            UserAnswer = userAnswer;
            RightAnswer = rightAnswer;
            Steps = steps;
        }

        public bool IsCorrectResult { get { return UserAnswer == RightAnswer; } }
        public int UserAnswer { get; private set; }
        public int RightAnswer { get; private set; }
        public int Steps { get; private set; }
    }
}
