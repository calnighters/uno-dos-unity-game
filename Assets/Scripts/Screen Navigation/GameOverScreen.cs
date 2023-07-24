using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    public string mainMenu;
    public string gameScene;

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(gameScene);
    }
}
