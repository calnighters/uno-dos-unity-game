using Assets.Scripts.Players.Difficulty.Enums;
using Assets.Scripts.Screen_Navigation.StaticClasses;
using Assets.Scripts.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    public GameObject __IncreaseDifficultyUI;
    public GameObject __DecreaseDifficultyUI;

    public void PlayAgain()
    {
        if (GameSettings.Winner == WinnerObject.Player)
        {
            if (GameSettings.SelectedDifficulty != DifficultyLevel.Insane)
            {
                __IncreaseDifficultyUI.SetActive(true);
            }
            else
            {
                SameDifficulty();
            }
        }
        else
        {
            if (GameSettings.SelectedDifficulty != DifficultyLevel.Easy)
            {
                __DecreaseDifficultyUI.SetActive(true);
            }
            else
            {
                SameDifficulty();
            }
        }
    }

    public void IncreaseDifficulty()
    {
        if (GameSettings.SelectedDifficulty == DifficultyLevel.Easy)
        {
            GameSettings.SelectedDifficulty = DifficultyLevel.Normal;
        }
        else if (GameSettings.SelectedDifficulty == DifficultyLevel.Normal)
        {
            GameSettings.SelectedDifficulty = DifficultyLevel.Hard;
        }
        else if (GameSettings.SelectedDifficulty == DifficultyLevel.Hard)
        {
            GameSettings.SelectedDifficulty = DifficultyLevel.Insane;
        }
        GameSettings.Winner = WinnerObject.NA;
        SceneManager.LoadScene(SceneNames.GAME_SCREEN);
    }

    public void DecreaseDifficulty()
    {
        if (GameSettings.SelectedDifficulty == DifficultyLevel.Insane)
        {
            GameSettings.SelectedDifficulty = DifficultyLevel.Hard;
        }
        else if (GameSettings.SelectedDifficulty == DifficultyLevel.Hard)
        {
            GameSettings.SelectedDifficulty = DifficultyLevel.Normal;
        }
        else if (GameSettings.SelectedDifficulty == DifficultyLevel.Normal)
        {
            GameSettings.SelectedDifficulty = DifficultyLevel.Easy;
        }
        GameSettings.Winner = WinnerObject.NA;
        SceneManager.LoadScene(SceneNames.GAME_SCREEN);
    }
    public void SameDifficulty()
    {
        GameSettings.Winner = WinnerObject.NA;
        SceneManager.LoadScene(SceneNames.GAME_SCREEN);
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }
}
