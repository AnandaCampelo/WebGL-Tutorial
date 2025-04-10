using UnityEngine;

public class SurvivalTimer : MonoBehaviour
{
    void Update()
    {
        GameManager.timeSurvived += Time.deltaTime;
    }
}