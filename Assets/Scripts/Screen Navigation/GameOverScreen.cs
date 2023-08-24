using Assets.Scripts.Players.Difficulty.Enums;
using Assets.Scripts.Screen_Navigation.StaticClasses;
using Assets.Scripts.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    public TMP_Text __CPUWinnerText;
    public GameObject __DecreaseDifficultyUI;
    private GameSettings __GameSettings;
    public GameObject __IncreaseDifficultyUI;

    public void Awake()
    {
        __GameSettings = SaveGameSettings.LoadSettings() ?? new GameSettings();
        __CPUWinnerText.text += __GameSettings.CPUWinnerText;
    }

    public void PlayAgain()
    {
        if (__GameSettings.Winner == WinnerObject.Player)
        {
            if (__GameSettings.SelectedDifficulty != DifficultyLevel.Insane)
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
            if (__GameSettings.SelectedDifficulty != DifficultyLevel.Easy)
            {
                __DecreaseDifficultyUI.SetActive(true);
            }
            else
            {
                SameDifficulty();
            }
        }
        SaveGameSettings.SaveSettings(__GameSettings);
    }

    public void IncreaseDifficulty()
    {
        if (__GameSettings.SelectedDifficulty == DifficultyLevel.Easy)
        {
            __GameSettings.SelectedDifficulty = DifficultyLevel.Normal;
        }
        else if (__GameSettings.SelectedDifficulty == DifficultyLevel.Normal)
        {
            __GameSettings.SelectedDifficulty = DifficultyLevel.Hard;
        }
        else if (__GameSettings.SelectedDifficulty == DifficultyLevel.Hard)
        {
            __GameSettings.SelectedDifficulty = DifficultyLevel.Insane;
        }
        __GameSettings.Winner = WinnerObject.NA;
        SceneManager.LoadScene(SceneNames.GAME_SCREEN);
        SaveGameSettings.SaveSettings(__GameSettings);
    }

    public void DecreaseDifficulty()
    {
        if (__GameSettings.SelectedDifficulty == DifficultyLevel.Insane)
        {
            __GameSettings.SelectedDifficulty = DifficultyLevel.Hard;
        }
        else if (__GameSettings.SelectedDifficulty == DifficultyLevel.Hard)
        {
            __GameSettings.SelectedDifficulty = DifficultyLevel.Normal;
        }
        else if (__GameSettings.SelectedDifficulty == DifficultyLevel.Normal)
        {
            __GameSettings.SelectedDifficulty = DifficultyLevel.Easy;
        }
        __GameSettings.Winner = WinnerObject.NA;
        SceneManager.LoadScene(SceneNames.GAME_SCREEN);
        SaveGameSettings.SaveSettings(__GameSettings);
    }
    public void SameDifficulty()
    {
        __GameSettings.Winner = WinnerObject.NA;
        SceneManager.LoadScene(SceneNames.GAME_SCREEN);
        SaveGameSettings.SaveSettings(__GameSettings);
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }
}
