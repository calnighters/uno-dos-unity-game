using Assets.Scripts.Screen_Navigation.StaticClasses;
using Assets.Scripts.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPrompt : MonoBehaviour
{
    public GameObject __FinishPromptUI;

    public void ContinueGame()
    {
        __FinishPromptUI.SetActive(false);
    }

    public void FinishGame()
    {
        if (GameSettings.PlayerScore < GameSettings.CPUScore)
        {
            GameSettings.Winner = WinnerObject.Player;
            GameSettings.PlayerScore = 0;
            GameSettings.CPUScore = 0;
            SceneManager.LoadScene(SceneNames.WINNER_SCREEN);
        }
        else if (GameSettings.PlayerScore == GameSettings.CPUScore)
        {
            GameSettings.Winner = WinnerObject.Draw;
            GameSettings.PlayerScore = 0;
            GameSettings.CPUScore = 0;
            SceneManager.LoadScene(SceneNames.DRAW_SCREEN);
        }
        else
        {
            GameSettings.Winner = WinnerObject.CPU;
            GameSettings.PlayerScore = 0;
            GameSettings.CPUScore = 0;
            SceneManager.LoadScene(SceneNames.GAME_OVER);
        }
    }

    public void OpenHomeOption()
    {
        __FinishPromptUI.SetActive(true);
    }
}
