using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class event_handler : MonoBehaviour
{
    public void GoToLevel1()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToLevel2() {
        SceneManager.LoadScene(2);
    }

    public void GoToLevel3()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
