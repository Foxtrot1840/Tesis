using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ChangeScene(string nextScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
    
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene()
            .buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
