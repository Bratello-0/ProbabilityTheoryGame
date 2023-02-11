using ProbabilityTheoryGameForBirthday;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProbabilityTheoryGameForBirhday
{
    internal class Presenter
    {
        private readonly IMainForm _view;
        private readonly IGameLogic _gameLogic;
        private readonly IMessageService _service;
        private bool _isWait = false;
        private bool _isWin = false;

        public Presenter(IMainForm form, IGameLogic gameLogic, IMessageService service)
        {
            _view = form;
            _gameLogic = gameLogic;
            _service = service;

            _view.GeneratingButtons  += GeneratingButtons;
            _view.ClickButtonHint    += ClickButtonHint;
            _view.ClickButtonRestart += RestartGame;
            _view.LoadForm           += LoadForm;

            _view.SellsСhangedTL += _view.ClearControlTableLayout;

            _gameLogic.Win += Win;
            _gameLogic.Lose += Lose;
            _gameLogic.WinPerson += Wait;
        }

        #region event _view
        private void LoadForm(object sender, EventArgs e)
        {
            _view.SetCenterAllUI();
            _gameLogic.DifficultyGame = (Difficulty)_view.GetIdActiveRadioButPeople;
            UpdateGameData();
        }

        private void RestartGame(object sender, EventArgs e)
        {
            _isWin = false;
            _isWait = false;
            _gameLogic.DifficultyGame = (Difficulty)_view.GetIdActiveRadioButPeople;
            UpdateTableLayout();
            UpdateGameData();
        }

        private void ClickButtonHint(object sender, EventArgs e)
        {
            _service.ShowMessage("Transfer 50 rubles to my card.");
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

        private async void Sleep() 
        {
            await Task.Delay(1000);
            _isWait = false;
            NextPerson();
        }

        private void Wait()
        {
            _isWait = true;
            Sleep();
        }

        private void Lose()
        {
            _service.ShowDefeat("You Lose!");
            RestartGame(this, EventArgs.Empty);
        }

        private void Win()
        {
            _isWin = true;
            _service.ShowSuccess("You Win!!!");
        }

        private void NextPerson() {
            _view.ClearControlTableLayout();
            UpdateTableLayout();
        }

        private void UpdateTableLayout() {
            (int Rows, int Columns) = GetRowsAndColumnsTable();
            _view.RowsTableLayout = Rows;
            _view.ColumnsTableLayout = Columns;
            GeneratingButtons(this, EventArgs.Empty);
        }

        private void UpdateGameData() {
            _view.AttemptsNumber = _gameLogic.AttemptsNumber.ToString();
            _view.PeopleNumber = _gameLogic.PeopleNumber.ToString();
            _view.PersonsNumber = _gameLogic.PersonsNumber.ToString();
        }

        private void ClickButToOpenNumber(object sender, EventArgs e)
        {
            if (!(sender is Button)){
                throw new ArgumentNullException(nameof(sender));
            }

            if (_isWait || _isWin){
                return;
            }

            Button but = (Button)sender;
            but.BackColor = Color.Red;
            but.ForeColor = Color.White;

            but.Click -= ClickButToOpenNumber;
            but.Text = _gameLogic.GetContent(but.TabIndex);
            if (int.Parse(but.Text) == _gameLogic.PersonsNumber) {
                but.BackColor = Color.Green;
            }
            _gameLogic.Touch(but.TabIndex);
            //_gameLogic.WinClick();
            UpdateGameData();
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