using Assets.Scripts.Players.Difficulty.Enums;
using Assets.Scripts.Screen_Navigation.StaticClasses;
using Assets.Scripts.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DrawScreen : MonoBehaviour
{

    public void PlayAgain()
    {
        GameSettings.Winner = WinnerObject.NA;
        SceneManager.LoadScene(SceneNames.GAME_SCREEN);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }
}
