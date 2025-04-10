using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI timeText;

    void Start()
    {
        int minutes = Mathf.FloorToInt(GameManager.timeSurvived / 60F);
        int seconds = Mathf.FloorToInt(GameManager.timeSurvived - minutes * 60);
        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        coinsText.text = "" + GameManager.coinsCollected;
    }
}