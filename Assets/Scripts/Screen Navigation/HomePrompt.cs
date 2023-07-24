using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePrompt : MonoBehaviour
{
    // Start is called before the first frame update
    public string mainMenu;
    public GameObject homePromptUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenHomeOption()
    {
        homePromptUI.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);

    }

    public void continueGame()
    {
        homePromptUI.SetActive(false);
    }
}
