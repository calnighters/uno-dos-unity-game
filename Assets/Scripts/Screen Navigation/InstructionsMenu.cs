using Assets.Scripts.Screen_Navigation.StaticClasses;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }
}