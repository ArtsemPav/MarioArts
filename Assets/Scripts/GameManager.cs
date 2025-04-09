using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }
//    public UIManager uimanager;

    private void Awake()
    {
        Debug.Log("Awake " + name);
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
//        uimanager = GetComponent<UIManager>();
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        coins = 0;
        world = 1;
        stage = 1;
        LoadLevel(world, stage);
 //       uimanager.ScoreUpdate(coins);
 //       uimanager.LivesUpdate(lives);
 //       uimanager.LevelUpdate(world, stage);
        
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    private void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }
    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }
    public void ResetLevel()
    {
        lives--;
        if (lives > 0)
        {
            LoadLevel(world, stage);
        } else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        NewGame();
    }

    public void AddCoin()
    {
        coins++;
//       uimanager.ScoreUpdate(coins);
        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
    }

    public void AddLife()
    {
        lives++;
    }
}