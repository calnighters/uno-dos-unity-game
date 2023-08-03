using Assets.Scripts.Players.Difficulty.Enums;
using Assets.Scripts.Screen_Navigation.StaticClasses;
using Assets.Scripts.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Screen_Navigation
{
    public class SelectDifficultyMenu : MonoBehaviour
    {
        private void PlayGame()
        {
            SceneManager.LoadScene(SceneNames.GAME_SCREEN);
        }

        public void PlayGameEasy()
        {
            GameSettings.SelectedDifficulty = DifficultyLevel.Easy;
            PlayGame();
        }

        public void PlayGameHard()
        {
            GameSettings.SelectedDifficulty = DifficultyLevel.Hard;
            PlayGame();
        }

        public void PlayGameInsane()
        {
            GameSettings.SelectedDifficulty = DifficultyLevel.Insane;
            PlayGame();
        }

        public void PlayGameNormal()
        {
            GameSettings.SelectedDifficulty = DifficultyLevel.Normal;
            PlayGame();
        }
    }
}
