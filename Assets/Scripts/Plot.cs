using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject tower;
    private Color startColor;

    void Start()
    {

        startColor = sr.color;
    }

    void Update()
    {
        if (tower != null) gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        GameObject towerToBuild = BuildManager.main.GetSelectedTower();
        Vector3 towerPosition = new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z);
        tower = Instantiate(towerToBuild, towerPosition, Quaternion.identity);
        LevelManager.main.hidePlots();
    }

}
