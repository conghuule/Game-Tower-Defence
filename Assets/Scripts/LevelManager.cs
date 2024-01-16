using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    private int score = 0;

    public TextMeshProUGUI scoreText;

    public GameObject gameMenu;
    public GameObject storeMenu;

    public GameObject healthProcess;
    public int maxEnemyAllow = 5;

    public GameObject gameResult;

    private void Awake()
    {
        main = this;
        var temp = healthProcess.transform.localScale;
        temp.x = maxEnemyAllow;
        healthProcess.transform.localScale = temp;
    }

    public void IncreaseScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameMenu.SetActive(false);
    }

    public void OpenStore()
    {
        Time.timeScale = 0;
        storeMenu.SetActive(true);
    }
    public void CloseStore()
    {
        Time.timeScale = 1;
        storeMenu.SetActive(false);
    }

    public void MinusHealth()
    {
        maxEnemyAllow--;
        var temp = healthProcess.transform.localScale;
        temp.x = maxEnemyAllow;
        healthProcess.transform.localScale = temp;

        if(maxEnemyAllow == 0)
        {
            Time.timeScale = 0;
            gameResult.SetActive(true);
        }
    }
}
