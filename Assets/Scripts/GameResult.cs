using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public GameObject start1;
    public GameObject start2;
    public GameObject start3;

    void Update()
    {
        switch (LevelManager.main.maxEnemyAllow) {
            case 0:
                titleText.text = "You lose";
                start1.SetActive(false);
                start2.SetActive(false);
                start3.SetActive(false);
                break;
            case 1:
            case 2:
                titleText.text = "You win";
                start1.SetActive(false);
                start2.SetActive(true);
                start3.SetActive(false);
                break;
            case 3:
            case 4:
                titleText.text = "You win";
                start1.SetActive(true);
                start2.SetActive(false);
                start3.SetActive(true);
                break;
            case 5:
                titleText.text = "You win";
                start1.SetActive(true);
                start2.SetActive(true);
                start3.SetActive(true);
                break;
        }
    }
}
