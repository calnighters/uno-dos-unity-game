using Assets.Scripts.Players.Difficulty.Enums;
using Assets.Scripts.Players.GameModes.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Settings
{
    [System.Serializable]
    public class GameSettings
    {
        public int CPUPlayerCount = 1;
        public List<int> CPUScores;
        public bool IsHPMode = false;
        public int PlayerScore;
        public DifficultyLevel SelectedDifficulty = DifficultyLevel.Normal;
        public RoundMode SelectedRound = RoundMode.SingleRound;
        public WinnerObject Winner;
        public string CPUWinnerText;
    }
}
