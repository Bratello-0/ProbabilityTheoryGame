using System;
using System.Collections.Generic;
using static ProbabilityTheoryGameForBirthday.GameLogic;

namespace ProbabilityTheoryGameForBirthday
{
    interface IGameLogic 
    {
        Difficulty DifficultyGame { get; set; }
        int PeopleNumber { get;}
        int AttemptsNumber { get;}
        int PersonsNumber { get; }

        string GetContent(int IdSell);
        void Touch(int IdButton);

        void Next();

        event WinGame Win;
        event LoseGame Lose;
        event WinPersonGame WinPerson;
    }

    public class GameLogic: IGameLogic
    {
        public delegate void WinGame();
        public event WinGame Win;
        public delegate void WinPersonGame();
        public event WinPersonGame WinPerson;
        public delegate void LoseGame();
        public event LoseGame Lose;

        private List<int> numbersIsСells;
        Random random = new Random();
        int peopleNumber;
        int attemptsNumber;
        int personsNumber;
        Difficulty difficulty;

        public GameLogic() {
            PeopleNumber = 2;
            AttemptsNumber = 1;
            PersonsNumber = 1;
        }

        Difficulty IGameLogic.DifficultyGame
        {
            get => difficulty;
            set {
                difficulty = value;
                UpdateData();
            } 
        }

        public int PeopleNumber {
            get => peopleNumber;
            private set => peopleNumber = value;
        }

        public int AttemptsNumber {
            get => attemptsNumber;
            private set {
                attemptsNumber = value;
                if (attemptsNumber <= 0)
                    Lose?.Invoke();
            }
        }

        public int PersonsNumber {
            get => personsNumber;
            private set { 
                personsNumber = value;
                if (personsNumber > PeopleNumber)
                    Win?.Invoke();
                WinPerson?.Invoke();
                AttemptsNumber = PeopleNumber / 2;
            }
        }

        private void MixingСells() {
            for (int i = numbersIsСells.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                // exchange data[j] and data[i] values
                int temp = numbersIsСells[j];
                numbersIsСells[j] = numbersIsСells[i];
                numbersIsСells[i] = temp;
            }
        }

        private void FillСells() {
            for (int i = 1; i <= numbersIsСells.Capacity; i++)
            {
                numbersIsСells.Add(i);
            }
        }

        public string GetContent(int IdSell)
        {
            if(IdSell < numbersIsСells.Count)
                return numbersIsСells[IdSell].ToString();
            return "Error";
        }

        private void UpdateData() {

            if (Win == null && Lose == null && WinPerson == null) {
                throw new NotImplementedException();
            }

            UpdateСells();
            PeopleNumber = numbersIsСells.Capacity;
            AttemptsNumber = peopleNumber / 2;
            //first person
            PersonsNumber = 1;
        }

        private void UpdateСells()
        {
            switch (difficulty) 
            {
                case Difficulty.PEOPLE_100:
                    numbersIsСells = new List<int>(100);
                    break;
                case Difficulty.PEOPLE_50:
                    numbersIsСells = new List<int>(50);
                    break;
                case Difficulty.PEOPLE_10:
                    numbersIsСells = new List<int>(10);
                    break;
                default:
                    throw new NotImplementedException();
            }

            FillСells();
            MixingСells();
        }

        public void Touch(int IdButton)
        {
            if (numbersIsСells[IdButton] == PersonsNumber) {
                PersonsNumber++;
            }
            else {
                AttemptsNumber--;
            }
        }

        public void Next()
        {
            for (int i = 0; i < numbersIsСells.Count; i++)
            {
                numbersIsСells[i] = i + 1;
            }
            MixingСells();
        }
    }
}
