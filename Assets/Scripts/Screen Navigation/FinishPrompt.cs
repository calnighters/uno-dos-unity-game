using Assets.Scripts.Screen_Navigation.StaticClasses;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPrompt : MonoBehaviour
{
    public GameObject __FinishPromptUI;

    public void ContinueGame()
    {
        __FinishPromptUI.SetActive(false);
    }

    public void GoToMainMenu()
    {
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("CPUScore", 0);
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }

    public void OpenHomeOption()
    {
        __FinishPromptUI.SetActive(true);
    }
}
