using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;

        GameManager.Reset();

        SceneManager.LoadScene(1);
    }
}