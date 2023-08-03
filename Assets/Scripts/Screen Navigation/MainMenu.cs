using Assets.Scripts.Screen_Navigation.StaticClasses;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OpenInstructions()
    {
        SceneManager.LoadScene(SceneNames.INSTRUCTIONS_MENU);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneNames.GAME_SCREEN);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
