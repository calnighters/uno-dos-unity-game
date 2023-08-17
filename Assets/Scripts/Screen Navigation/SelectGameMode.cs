using Assets.Scripts.Players.GameModes.Enums;
using Assets.Scripts.Screen_Navigation.StaticClasses;
using Assets.Scripts.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Screen_Navigation
{
    public class SelectGameMode : MonoBehaviour
    {
        private void SelectDifficulty()
        {
            SceneManager.LoadScene(SceneNames.SELECT_DIFFICULTY);
        }

        public void SingleRound()
        {
            GameSettings.SelectedMode = GameMode.SingleRound;
            SelectDifficulty();
        }

        public void MultipleRounds()
        {
            GameSettings.SelectedMode = GameMode.MultipleRounds;
            SelectDifficulty();
        }
    }
}
