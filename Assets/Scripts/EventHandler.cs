using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public void BackToChooseLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PauseGame()
    {
        LevelManager.main.PauseGame();
    }

    public void ResumeGame()
    {
        LevelManager.main.ResumeGame();
    }
}
