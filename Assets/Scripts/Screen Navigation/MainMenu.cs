using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string __SampleScene;
    public string __InstructionsMenu;

    public void OpenInstructions()
    {
        SceneManager.LoadScene(__InstructionsMenu);

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(__SampleScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
