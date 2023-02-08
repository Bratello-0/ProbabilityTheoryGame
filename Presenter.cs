using ProbabilityTheoryGameForBirthday;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProbabilityTheoryGameForBirhday
{
    internal class Presenter
    {
        private readonly IMainForm _view;
        private readonly IGameLogic _gameLogic;

        public Presenter(IMainForm form, IGameLogic gameLogic)
        {
            _view = form;
            _gameLogic = gameLogic;
            _view.GeneratingButtons  += GeneratingButtons;
            _view.ClickButtonHint    += ClickButtonHint;
            _view.ClickButtonRestart += RestartGame;
            _view.LoadForm           += LoadForm;

            _view.SellsСhangedTL += _view.ClearControlTableLayout;
        }

        #region event _view
        private void LoadForm(object sender, EventArgs e)
        {
            _view.SetCenterAllUI();
            _gameLogic.DifficultyGame = (Difficulty)_view.GetIdActiveRadioButPeople;
        }

        private void RestartGame(object sender, EventArgs e)
        {
            _gameLogic.DifficultyGame = (Difficulty)_view.GetIdActiveRadioButPeople;
            UpdateTableLayout();
        }

        private void ClickButtonHint(object sender, EventArgs e)
        {
            MessageBox.Show("Transfer 50 rubles to my card.","Hint");
        }

        private void GeneratingButtons(object sender, EventArgs e)
        {
            int Rows = _view.RowsTableLayout;
            int Columns = _view.ColumnsTableLayout;

            int counter = 0;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Button temp = _view.ButPrefab;
                    temp.Text = (++counter).ToString();
                    temp.Click += ClickButToOpenNumber;
                    _view.AddControlTableLayout(temp);
                }
            }
        }
        #endregion

        private void UpdateTableLayout() {
            (int Rows, int Columns) = GetRowsAndColumnsTable();
            _view.RowsTableLayout = Rows;
            _view.ColumnsTableLayout = Columns;
            GeneratingButtons(this, EventArgs.Empty);
        }

        private void ClickButToOpenNumber(object sender, EventArgs e)
        {
            if (!(sender is Button))
            {
                throw new ArgumentException();
            }
            Button but = (Button)sender;
            but.Click -= ClickButToOpenNumber;
            but.BackColor = Color.Black;
            but.ForeColor = Color.White;
            but.Text = _gameLogic.GetContent(but.TabIndex);
        }

        private (int Rows, int Columns) GetRowsAndColumnsTable()
        {
            Difficulty difficulty = _gameLogic.DifficultyGame;
            (int horizontal, int vertical) result;

            switch (difficulty)
            {
                case Difficulty.PEOPLE_100:
                    result = (10, 10);
                    break;
                case Difficulty.PEOPLE_50:
                    result = (10, 5);
                    break;
                case Difficulty.PEOPLE_10:
                    result = (2, 5);
                    break;
                default:
                    result = (-1, -1);
                    break;
            }

            return result;
        }
    }
}