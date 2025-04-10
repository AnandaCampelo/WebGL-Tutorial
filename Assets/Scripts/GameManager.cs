using UnityEngine;

public static class GameManager
{
    public static float timeSurvived = 0f;
    public static int coinsCollected = 0;

    public static void Reset()
    {
        timeSurvived = 0f;
        coinsCollected = 0;
    }
}