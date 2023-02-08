using System;
using System.Collections.Generic;

namespace ProbabilityTheoryGameForBirthday
{
    interface IGameLogic 
    {
        string GetContent(int IdSell);
        Difficulty DifficultyGame { get; set; }

    }

    internal class GameLogic: IGameLogic
    {
        private List<int> numbersIsСells;
        Random random = new Random();
        Difficulty difficulty;

        Difficulty IGameLogic.DifficultyGame
        {
            get => difficulty;
            set {
                difficulty = value;
                UpdateСells();
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
    }
}
