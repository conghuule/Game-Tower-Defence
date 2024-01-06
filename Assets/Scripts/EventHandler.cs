using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public void BackToChooseLevel()
    {
        SceneManager.LoadScene(1);
    }
}
