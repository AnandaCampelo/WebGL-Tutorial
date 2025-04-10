using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed = 3f;
    private Transform player;
    private AudioSource sfxSource;
    private bool gameOverTriggered = false;

    public AudioClip deathSound; // Assign in Inspector

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        sfxSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameOverTriggered || player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameOverTriggered) return;

        if (other.CompareTag("Player"))
        {
            gameOverTriggered = true;
            StartCoroutine(HandleGameOverSequence());
        }
    }

    private IEnumerator HandleGameOverSequence()
    {
        Camera.main.GetComponent<AudioSource>().Stop();

        Time.timeScale = 0f;

        if (sfxSource != null)
        {
            sfxSource.Play();
            yield return new WaitForSecondsRealtime(sfxSource.clip.length);
        }
        else
        {
            yield return new WaitForSecondsRealtime(1f);
        }

        SceneManager.LoadScene(2);
    }
}
