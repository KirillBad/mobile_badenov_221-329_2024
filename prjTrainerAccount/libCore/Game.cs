namespace libCore
{
    public class Game
    {
        private Random rnd = new Random();
        
        public int CountCorrect { get; private set; }
        
        public int CountWrong { get; private set; }
        
        public string QuestionText { get; private set; }

        private bool answerCorrect;

        public event Action? ChangeQuestion;
        public event Action? ChangeStatistic;

        public void GameStart()
        {
            CountCorrect = 0;
            CountWrong = 0;
            DoContinue();
        }

        private void DoContinue()
        {
            // QuestionText = "20 + 25 = 45";
            // answerCorrect = true;

            int xValue1 = rnd.Next(20);
            int xValue2 = rnd.Next(20);
            int xResult = xValue1 + xValue2;
            int xResultNew = xResult;

            if (rnd.Next(2) == 1)
                xResultNew += rnd.Next(1, 7) * (rnd.Next(2) == 1 ? 1 : -1);

            QuestionText = $"{xValue1} + {xValue2} = {xResultNew}";
            answerCorrect = xResult == xResultNew;

            ChangeQuestion?.Invoke();
        }

        public void DoAnswer(bool v)
        {
            if (v == answerCorrect)
                CountCorrect++;
            else
                CountWrong++;
            ChangeStatistic?.Invoke(); // TODO 

            DoContinue();
        }
    }
}
