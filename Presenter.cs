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
            _view.LoadForm += _view_LoadForm;
        }

        private void _view_LoadForm(object sender, EventArgs e)
        {
            _view.SetCenterAllUI();
        }

        private void _view_ClickButtonRestart(object sender, EventArgs e)
        {
            _view.ClearControlTableLayout();
            (int horizontal, int vertical) = ConvertIdRadioButtToData();
            _view.RowsTableLayout = vertical;
            _view.ColumnsTableLayout = horizontal;
            _view_GeneratingButtons(this, EventArgs.Empty);
        }

        private void _view_ClickButtonHint(object sender, EventArgs e)
        {
            MessageBox.Show("Transfer 50 rubles to my card.");
        }

        private void _view_GeneratingButtons(object sender, EventArgs e)
        {
            int vertical = _view.RowsTableLayout;
            int horizontal = _view.ColumnsTableLayout;

            for (int i = 0; i < vertical; i++)
            {
                for (int j = 0; j < horizontal; j++)
                {
                    Button temp = _view.ButPrefab;
                    temp.Text = ((i * 10) + (j + 1)).ToString();
                    temp.Click += Temp_Click;
                    _view.AddControlTableLayout(temp);
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

        private (int horizontal, int vertical) ConvertIdRadioButtToData()
        {
            int Id = _view.GetIdActiveRadioButPeople;
            (int horizontal, int vertical) result;

            switch (Id)
            {
                case 0:
                    result = (10, 10);
                    break;
                case 1:
                    result = (5, 10);
                    break;
                case 2:
                    result = (5, 2);
                    break;
                default:
                    result = (-1, -1);
                    break;
            }

            return result;
        }
    }
}
