using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    public string __GameScene;
    public string __MainMenu;
    public void PlayAgain()
    {
        SceneManager.LoadScene(__GameScene);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(__MainMenu);
    }
}
