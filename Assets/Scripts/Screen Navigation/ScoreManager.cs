using Assets.Scripts.Screen_Navigation.StaticClasses;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Assets.Scripts.Players.GameModes.Enums;
using Assets.Scripts.Settings;
using System.Collections.Generic;

namespace Assets.Scripts.Screen_Navigation
{
    public class ScoreManager : MonoBehaviour
    {
        private const string CPU_PLAYER_SCORE_TEXT = "CPU Player {0} Score: ";
        private const string CPU_SCORE_TEXT = "CPU Score: ";
        private const string PLAYER_SCORE_TEXT = "Player Score: ";

        public TextMeshProUGUI __CPUPlayerThreeScoreText;
        public TextMeshProUGUI __CPUPlayerTwoScoreText;
        public List<int> __CPUScores;
        public TextMeshProUGUI __CPUScoreText;
        public GameSettings __GameSettings;
        public int __PlayerScore = 0;
        public TextMeshProUGUI __PlayerScoreText;

        // Start is called before the first frame update
        void Start()
        {
            __GameSettings = SaveGameSettings.LoadSettings();
            if (__GameSettings == null)
            {
                __GameSettings = new GameSettings();
            }
            __PlayerScore = __GameSettings.PlayerScore;
            __PlayerScoreText.text = PLAYER_SCORE_TEXT + __PlayerScore.ToString();
            __CPUScores = __GameSettings.CPUScores;
            __CPUScoreText.text = __GameSettings.CPUPlayerCount > 1 ? string.Format(CPU_PLAYER_SCORE_TEXT, 1) + __CPUScores[0] : CPU_SCORE_TEXT + __CPUScores[0].ToString();
            if (__GameSettings.CPUPlayerCount > 1)
            {
                __CPUPlayerTwoScoreText.text = string.Format(CPU_PLAYER_SCORE_TEXT, 2) + __CPUScores[1];
                __CPUPlayerThreeScoreText.text = string.Format(CPU_PLAYER_SCORE_TEXT, 3) + __CPUScores[2];
            }
            else {
                __CPUPlayerTwoScoreText.text =  __CPUPlayerThreeScoreText.text = string.Empty;
            }
        }

        public void NextRound()
        {
            SceneManager.LoadScene(SceneNames.GAME_SCREEN);
        }
    }
}
