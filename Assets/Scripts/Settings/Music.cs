using UnityEngine;

public class Music : MonoBehaviour
{
    public void Awake()
    {
        GameObject[] mainMenuMusic = GameObject.FindGameObjectsWithTag("MainMenuMusic");
        if(mainMenuMusic.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
