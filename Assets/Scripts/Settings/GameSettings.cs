using Assets.Scripts.Players.Difficulty.Enums;
using Assets.Scripts.Players.GameModes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Settings
{
    public static class GameSettings
    {
        public static DifficultyLevel SelectedDifficulty;
        public static GameMode SelectedMode;
        public static WinnerObject Winner;
        public static int PlayerScore;
        public static int CPUScore;
    }
}
