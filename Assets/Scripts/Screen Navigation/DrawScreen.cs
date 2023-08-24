using Assets.Scripts.Players.Difficulty.Enums;
using Assets.Scripts.Screen_Navigation.StaticClasses;
using Assets.Scripts.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DrawScreen : MonoBehaviour
{

    public void PlayAgain()
    {
        GameSettings _GameSettings = SaveGameSettings.LoadSettings();
        if (_GameSettings != null)
        {
            _GameSettings = new GameSettings();
        }
        _GameSettings.Winner = WinnerObject.NA;
        SceneManager.LoadScene(SceneNames.GAME_SCREEN);
        SaveGameSettings.SaveSettings(_GameSettings);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }
}
