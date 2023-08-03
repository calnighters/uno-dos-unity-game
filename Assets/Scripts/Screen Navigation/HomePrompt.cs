using Assets.Scripts.Screen_Navigation.StaticClasses;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePrompt : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject __HomePromptUI;

    public void ContinueGame()
    {
        __HomePromptUI.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }

    public void OpenHomeOption()
    {
        __HomePromptUI.SetActive(true);
    }
}
