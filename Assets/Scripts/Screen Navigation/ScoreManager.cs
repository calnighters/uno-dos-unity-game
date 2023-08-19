using Assets.Scripts.Screen_Navigation.StaticClasses;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Assets.Scripts.Players.GameModes.Enums;
using Assets.Scripts.Settings;

namespace Assets.Scripts.Screen_Navigation
{
    public class ScoreManager : MonoBehaviour
    {
        public TextMeshProUGUI __PlayerScoreText;
        public TextMeshProUGUI __CPUScoreText;

        int __PlayerScore = 0;
        int __CPUScore = 0;

        // Start is called before the first frame update
        void Start()
        {
            __PlayerScore = GameSettings.PlayerScore;
            __PlayerScoreText.text = "Player Score: " + __PlayerScore.ToString();
            __CPUScore = GameSettings.CPUScore;
            __CPUScoreText.text = "CPU Score: " + __CPUScore.ToString();
        }

        public void NextRound()
        {
            SceneManager.LoadScene(SceneNames.GAME_SCREEN);
        }
    }
}
