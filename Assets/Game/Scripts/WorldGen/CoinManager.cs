using System.Collections;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform mapArea;
    public Transform player;
    public TMP_Text moneyText;
    public int money = 0;
    public int totalCoinsCollected = 0;
    public int coinBonus = 0;
    [SerializeField] public int maxCoins = 5;

    public float spawnInterval = 5f;
    public float spawnRange = 5f;
    private int currentCoinCount = 0;

    void Start()
    {
        if (moneyText == null)
        {
            moneyText = Object.FindFirstObjectByType<TMP_Text>();
        }

        StartCoroutine(GenerateCoins());
        UpdateMoneyUI();
    }

    void Update()
    {
        if (player != null && mapArea != null)
        {
            mapArea.position = player.position;
        }
    }

    IEnumerator GenerateCoins()
    {
        while (true)
        {
            if (currentCoinCount < maxCoins)
            {
                SpawnCoin();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCoin()
    {
        float x = Random.Range(-spawnRange, spawnRange) + mapArea.position.x;
        float y = Random.Range(-spawnRange, spawnRange) + mapArea.position.y;
        Vector2 spawnPosition = new Vector2(x, y);
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        currentCoinCount++;
    }

    public void CollectCoin(GameObject coin)
    {
        Destroy(coin);
        money += 1 + coinBonus;
        totalCoinsCollected++;
        currentCoinCount--;
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + money.ToString();
        }
    }
}
