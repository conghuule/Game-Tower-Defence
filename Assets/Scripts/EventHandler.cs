using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public void BackToChooseLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void PauseGame()
    {
        LevelManager.main.PauseGame();
    }

    public void ResumeGame()
    {
        LevelManager.main.ResumeGame();
    }

    public void OpenStore()
    {
        LevelManager.main.OpenStore();
    }

    public void CloseStore()
    {
        LevelManager.main.CloseStore();
    }
}
