using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProbabilityTheoryGameForBirhday
{
    internal class Presenter
    {
        private readonly IMainForm _view;

        public Presenter(IMainForm form)
        {
            _view = form;
            _view.GeneratingButtons += _view_GeneratingButtons;
            _view.ClickButtonHint += _view_ClickButtonHint;
            _view.ClickButtonRestart += _view_ClickButtonRestart;
        }

        private void _view_ClickButtonRestart(object sender, EventArgs e)
        {
            //temporary solution
            Application.Restart();
            Environment.Exit(0);
        }

        private void _view_ClickButtonHint(object sender, EventArgs e)
        {
            MessageBox.Show("Transfer 50 rubles to my card.");
        }

        private void _view_GeneratingButtons(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button temp = _view.ButPrefab;
                    temp.Text = ((i * 10) + (j + 1)).ToString();
                    temp.Click += Temp_Click;
                    _view.CenterControl.Add(temp);
                }
            }
        }

        private void Temp_Click(object sender, EventArgs e)
        {
            if (!(sender is Button))
                throw new ArgumentException();
            Button but = (Button)sender;
            but.Click -= Temp_Click;
            but.BackColor = Color.Black; 
            but.ForeColor = Color.White;
        }
    }
}
