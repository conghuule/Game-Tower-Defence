using UnityEngine;

public class Tower2 : Tower
{
    int[] dameLevel = { 10, 20, 40 };
    float[] rangeLevel = { 3, 4, 5 };
    float[] rateLevel = { 1, 1.2f, 1.5f };
    int[] upgradePriceLevel = { 30, 70, 0 };
    int[] sellPriceLevel = { 10, 20, 30 };

    public GameObject towerPrefabLevel2;
    public GameObject towerPrefabLevel3;

    private void Start()
    {
        dame = dameLevel[level - 1];
        range = rangeLevel[level - 1];
        fireRate = rateLevel[level - 1];
        priceUpgrade = upgradePriceLevel[level - 1];
        priceSell = sellPriceLevel[level - 1];
    }

    public override void Upgrade()
    {
        // Instantiate the appropriate prefab based on the tower's level
        GameObject upgradedTowerPrefab = (level == 1) ? towerPrefabLevel2 : towerPrefabLevel3;
        GameObject upgradedTowerObject = Instantiate(upgradedTowerPrefab, transform.position, transform.rotation);

        // Get the Tower component from the instantiated object
        Tower newTower = upgradedTowerObject.GetComponent<Tower>();

        // Set the level of the new tower
        newTower.level = level + 1;

        base.OnMouseDown();

        // Destroy the old tower
        Destroy(gameObject);
    }
}