using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public Transform flipPoint;

    public int currency;

    public TextMeshProUGUI scoreText;

    public GameObject gameMenu;
    public GameObject storeMenu;

    public GameObject healthProcess;
    public int maxEnemyAllow = 5;

    public GameObject gameResult;
    public GameObject plots;

    public int currentLevel = 1;
    int[] currencyLevel = { 40, 60, 80 };

    public AudioClip upgradeClip;
    public AudioClip DieClip;
    AudioSource audioSource;

    private void Awake()
    {
        main = this;
        var temp = healthProcess.transform.localScale;
        temp.x = maxEnemyAllow;
        healthProcess.transform.localScale = temp;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currency = currencyLevel[currentLevel - 1];
        scoreText.text = currency.ToString();
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        scoreText.text = currency.ToString();
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            //BUY ITEM
            currency -= amount;
            scoreText.text = currency.ToString();
            return true;
        }
        else
        {
            Debug.Log("Not enough currency");
            return false;
        }
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

        if (maxEnemyAllow == 0)
        {
            Time.timeScale = 0;
            gameResult.SetActive(true);
        }
    }

    public void hidePlots()
    {
        plots.SetActive(false);
    }

    public void PlayUpgradeClip()
    {
        audioSource.clip = upgradeClip;
        audioSource.Play();
    }

    public void PlayDieClip()
    {
        audioSource.clip = DieClip;
        audioSource.Play();
    }
}
