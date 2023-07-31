using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePrompt : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject __HomePromptUI;
    public string __MainMenu;

    public void ContinueGame()
    {
        __HomePromptUI.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(__MainMenu);
    }

    public void OpenHomeOption()
    {
        __HomePromptUI.SetActive(true);
    }
}
