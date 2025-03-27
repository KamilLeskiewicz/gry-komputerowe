using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopCanvas;
    public TMP_Text upgradeButtonText;
    public TMP_Text maxCoinsUpgradeButtonText;
    public CoinManager coinManager;

    private bool isShopOpen = false;
    private int upgradeLevel = 0;
    private int upgradeCost = 5;
    private int maxCoinsLevel = 0;
    private int maxCoinsUpgradeCost = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();
        }
    }

    public void ToggleShop()
    {
        isShopOpen = !isShopOpen;
        shopCanvas.SetActive(isShopOpen);
        UpdateShopUI();
    }

    public void BuyUpgrade()
    {
        if (coinManager.money >= upgradeCost)
        {
            coinManager.money -= upgradeCost;
            upgradeLevel++;
            coinManager.coinBonus = upgradeLevel;
            upgradeCost += 5;
            UpdateShopUI();
            coinManager.UpdateMoneyUI();
        }
    }

    public void BuyMaxCoinsUpgrade()
    {
        if (coinManager.money >= maxCoinsUpgradeCost)
        {
            coinManager.money -= maxCoinsUpgradeCost;
            maxCoinsLevel++;
            coinManager.maxCoins += 2;
            maxCoinsUpgradeCost += 10;
            UpdateShopUI();
            coinManager.UpdateMoneyUI();
        }
    }

    void UpdateShopUI()
    {
        if (upgradeButtonText != null)
        {
            upgradeButtonText.text = $"Kup ulepszenie (+1 za coin)\nKoszt: {upgradeCost}$\nPoziom: {upgradeLevel}";
        }

        if (maxCoinsUpgradeButtonText != null)
        {
            maxCoinsUpgradeButtonText.text = $"Zwiêksz max. coiny (+2)\nKoszt: {maxCoinsUpgradeCost}$\nPoziom: {maxCoinsLevel}";
        }
    }
}
