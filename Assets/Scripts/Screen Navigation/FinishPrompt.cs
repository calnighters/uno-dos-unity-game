using Assets.Scripts.Screen_Navigation.StaticClasses;
using Assets.Scripts.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPrompt : MonoBehaviour
{
    public GameObject __FinishPromptUI;
    public GameSettings __GameSettings;
    public void Awake()
    {
        __GameSettings = SaveGameSettings.LoadSettings() ?? new GameSettings();
    }

    public void ContinueGame()
    {
        __FinishPromptUI.SetActive(false);
    }

    public void FinishGame()
    {
        if (__GameSettings.PlayerScore < __GameSettings.CPUScores.Min())
        {
            __GameSettings.Winner = WinnerObject.Player;
            __GameSettings.PlayerScore = 0;
            __GameSettings.CPUScores = new List<int> { 0, 0, 0 };
            SceneManager.LoadScene(SceneNames.WINNER_SCREEN);
        }
        else if (__GameSettings.PlayerScore == __GameSettings.CPUScores[0]
                    && __GameSettings.PlayerScore == __GameSettings.CPUScores[1]
                    && __GameSettings.PlayerScore == __GameSettings.CPUScores[2])
        {
            __GameSettings.Winner = WinnerObject.Draw;
            __GameSettings.PlayerScore = 0;
            __GameSettings.CPUScores = new List<int> { 0, 0, 0 };
            SceneManager.LoadScene(SceneNames.DRAW_SCREEN);
        }
        else
        {
            __GameSettings.Winner = WinnerObject.CPU;
            __GameSettings.PlayerScore = 0;
            int a = __GameSettings.CPUScores.Max();
            __GameSettings.CPUWinnerText = __GameSettings.CPUPlayerCount > 1 ? $"CPU Player {__GameSettings.CPUScores.FindIndex(score => score == __GameSettings.CPUScores.Max()) + 1} Won."
                : "The CPU Won";
            __GameSettings.CPUScores = new List<int> { 0, 0, 0 };
            SceneManager.LoadScene(SceneNames.GAME_OVER);
        }
        SaveGameSettings.SaveSettings(__GameSettings);
    }

    public void OpenHomeOption()
    {
        __FinishPromptUI.SetActive(true);
    }
}
