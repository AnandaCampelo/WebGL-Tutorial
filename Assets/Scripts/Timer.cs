using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
        string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = formattedTime;
    }
}