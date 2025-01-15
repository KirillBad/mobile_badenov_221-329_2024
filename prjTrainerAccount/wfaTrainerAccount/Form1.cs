using libCore;

namespace wfaTrainerAccount
{
    public partial class Form1 : Form
    {
        private readonly Game g;

        public Form1()
        {
            InitializeComponent();

            g = new Game();
            g.GameStart();
            g.ChangeQuestion += () => label3.Text = g.QuestionText;
            g.ChangeStatistic += delegate
            {
                label1.Text = $"Верно = {g.CountCorrect}";
                label2.Text = $"Неверно = {g.CountWrong}";
            };
            button1.Click += (s, e) => g.DoAnswer(true);
            button2.Click += (s, e) => g.DoAnswer(false);
        }
    }
}
