using Assets.Scripts.Players.Difficulty.Enums;
using System;

namespace Assets.Scripts.Players.Difficulty
{
    public class DifficultyRolls
    {
        private const int DIFFICULTY_ROLL_UPPER_BOUND = 100;
        private const int EASY_DIFFICULTY_ROLL = 30;
        private const int HARD_DIFFICULTY_ROLL = 90;
        private const int NORMAL_DIFFICULTY_ROLL = 60;
        private readonly DifficultyLevel __DifficultyLevel;
        private readonly Random __RollGenerator;

        public DifficultyRolls(DifficultyLevel difficultyLevel)
        {
            __RollGenerator = new Random();
            __DifficultyLevel = difficultyLevel;
        }

        public bool Roll()
        {
            int _RollValue = __RollGenerator.Next(DIFFICULTY_ROLL_UPPER_BOUND);

            switch (__DifficultyLevel)
            {
                case (DifficultyLevel.Easy):
                    return _RollValue < EASY_DIFFICULTY_ROLL ? true : false;
                case (DifficultyLevel.Normal):
                    return _RollValue < NORMAL_DIFFICULTY_ROLL ? true : false;
                case (DifficultyLevel.Hard):
                    return _RollValue < HARD_DIFFICULTY_ROLL ? true : false; 
                default:
                    return true;
            }
        }
    }
}
