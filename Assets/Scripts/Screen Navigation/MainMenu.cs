using Assets.Scripts.Screen_Navigation.StaticClasses;
using Assets.Scripts.Settings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OpenGameModeOptions()
    {
        SceneManager.LoadScene(SceneNames.GAME_MODE_OPTIONS);
    }

    public void OpenInstructions()
    {
        SceneManager.LoadScene(SceneNames.INSTRUCTIONS_MENU);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneNames.GAME_SCREEN);
        GameSettings _GameSettings = SaveGameSettings.LoadSettings();
        _GameSettings.PlayerScore = 0;
        _GameSettings.CPUScores = new List<int> { 0, 0, 0 };
        SaveGameSettings.SaveSettings(_GameSettings);
        Destroy(GameObject.Find("MainMenuMusic"));
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
