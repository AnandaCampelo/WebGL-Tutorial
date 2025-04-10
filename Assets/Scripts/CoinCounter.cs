using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private int coinCount = 0;

    public void AddCoin()
    {
        coinCount++;
        GameManager.coinsCollected = coinCount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        coinText.text = "" + coinCount;
    }
}
