
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerProperties : MonoBehaviour
{
    // Start is called before the first frame update
    public Tower tower;
    [SerializeField] Button buttonUpgrade;
    [SerializeField] Button buttonSell;
    [SerializeField] Button buttonRemove;

    [SerializeField] TextMeshProUGUI textDame;
    [SerializeField] TextMeshProUGUI textRange;
    [SerializeField] TextMeshProUGUI textSlow;
    [SerializeField] TextMeshProUGUI textRate;
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] TextMeshProUGUI textUpgrade;
    [SerializeField] TextMeshProUGUI textSell;

    void Start()
    {
        if (buttonUpgrade != null)
            buttonUpgrade.onClick.AddListener(upgradeTower);
        if (buttonSell != null)
            buttonSell.onClick.AddListener(Sell);
        if (buttonRemove != null)
            buttonRemove.onClick.AddListener(Destroy);

        if (tower != null && buttonUpgrade != null)
        {
            buttonUpgrade.interactable = (tower.level < 3);
        }
    }

    void Update()
    {
        if (tower != null && tower.priceUpgrade > LevelManager.main.currency)
        {
            buttonUpgrade.interactable = false;
        }
    }


    void upgradeTower()
    {
        Debug.Log(LevelManager.main.currency);
        if (tower.priceUpgrade <= LevelManager.main.currency)
        {
            LevelManager.main.SpendCurrency(tower.priceUpgrade);
            tower.Upgrade();
        }
            
    }

    void Sell()
    {
        LevelManager.main.IncreaseCurrency(tower.priceSell);
        Destroy(tower.gameObject);
        Destroy(gameObject);
    }

    public void setTower(Tower towerClicked)
    {
        gameObject.SetActive(true);
        tower = towerClicked;
        if (tower != null)
        {

            if (textDame != null)
            {
                textDame.text = $"Dame: {tower.dame}";
            }
            if (textRate != null)
            {
                textRate.text = $"Rate: {tower.fireRate}";
            }
            if (textSlow != null)
            {
                textSlow.text = $"Slow: {tower.slow}";
            }
            if (textRange != null)
            {
                textRange.text = $"Range: {tower.range}";
            }
            if (textLevel != null)
            {
                textLevel.text = $"Level: {tower.level}";
            }
            if (textUpgrade != null)
            {
                textUpgrade.text = tower.priceUpgrade == 0 ? "Upgrade" : $"Upgrade: {tower.priceUpgrade}";
            }
            if (textSell != null)
            {
                textSell.text = $"Sell: {tower.priceSell}";
            }
        }
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
