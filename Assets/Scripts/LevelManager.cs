using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;

    private int score = 0;

    public TextMeshProUGUI scoreText;

    public GameObject gameMenu;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            //BUY ITEM
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough currency");
            return false;
        }
    }

    public void IncreaseScore()
    {
        score += 1;
        scoreText.text = $"Score: {score}";
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
}
