// Pak Runner: Hero of the Homeland - Unity WebGL Version
// This is a simplified game logic setup for Unity using C#.
// The full Unity project would include scenes, prefabs, sprites, and this code in scripts.

// --- PlayerController.cs ---
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = true;

        if (collision.collider.CompareTag("Obstacle"))
        {
            GameManager.instance.GameOver();
        }
    }
}

// --- GameManager.cs ---
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

// --- Checkpoint.cs ---
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("CheckpointX", transform.position.x);
        }
    }
}

// --- RespawnManager.cs ---
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    void Start()
    {
        float checkpointX = PlayerPrefs.GetFloat("CheckpointX", 0f);
        transform.position = new Vector3(checkpointX, transform.position.y, transform.position.z);
    }
}

// --- ObstacleSpawner.cs ---
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 2f, spawnInterval);
    }

    void SpawnObstacle()
    {
        int index = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[index], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }
}

// --- UIManager.cs ---
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    private float score;

    void Update()
    {
        score += Time.deltaTime * 10;
        scoreText.text = "Score: " + ((int)score).ToString();
    }
}

// --- CharacterSelector.cs ---
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public GameObject maleCharacterPrefab;
    public GameObject femaleCharacterPrefab;

    public void SelectMale()
    {
        PlayerPrefs.SetString("Character", "Male");
        SceneManager.LoadScene("GameScene");
    }

    public void SelectFemale()
    {
        PlayerPrefs.SetString("Character", "Female");
        SceneManager.LoadScene("GameScene");
    }
}

// --- CharacterSpawner.cs ---
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject malePrefab;
    public GameObject femalePrefab;

    void Start()
    {
        string selected = PlayerPrefs.GetString("Character", "Male");
        GameObject prefabToSpawn = selected == "Male" ? malePrefab : femalePrefab;
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
}
