using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsMenu : MonoBehaviour
{
    public string __MainMenuScene;

    public void MainMenu()
    {
        SceneManager.LoadScene(__MainMenuScene);
    }
}