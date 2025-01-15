namespace wfaGraphicSinCos
{
    public partial class Form1 : Form
    {
        private MyGraphic myGraphic;
        public Form1()
        {
            InitializeComponent();

            this.BackgroundImageLayout = ImageLayout.None;
            this.Text += " : (sin - красный, cos _ синий, Tan - зеленый)";

            DrawAll();
            this.Resize += (s, e) => DrawAll();
        }

        private void DrawAll()
        {
            myGraphic = new MyGraphic (
                width: this.ClientSize.Width,
                height: this.ClientSize.Height,
                countWave: 5);

            myGraphic.DrawAxes(Color.Black);
            myGraphic.DrawSin(Color.Red);
            myGraphic.DrawCos(Color.Blue);
            myGraphic.DrawTan(Color.Green);

            //this.BackgroundImage = myGraphic.Picture;
            this.BackgroundImage = (Bitmap)myGraphic.Picture.Clone();
        }

    }
}
