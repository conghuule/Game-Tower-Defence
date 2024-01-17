using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private GameObject[] towerPrefabs;
    public GameObject plots;

    private int selectedTower = -1;

    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedTower()
    {
        return towerPrefabs[selectedTower];
    }

    public void SetSelectedTower(int index)
    {
        if(selectedTower == index)
        {
            selectedTower = -1;
            plots.SetActive(false);
        } else
        {
            if (LevelManager.main.currency >= 10)
            {
                selectedTower = index;
                plots.SetActive(true);
            }
        }
    }
}
