using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinstxt;
    public TextMeshProUGUI livestxt;
    public TextMeshProUGUI leveltxt;
    public GameObject GameOver;

    public void Start()
    {
        ScoreUpdate(GameManager.Instance.coins);
        LivesUpdate(GameManager.Instance.lives);
        LevelUpdate(GameManager.Instance.world, GameManager.Instance.stage);
    }

    public void Update()
    {
        ScoreUpdate(GameManager.Instance.coins);
        LivesUpdate(GameManager.Instance.lives);
        LevelUpdate(GameManager.Instance.world, GameManager.Instance.stage);
        if (GameManager.Instance.lives < 1)
        {
            GameOver.SetActive(true);
        } else
        {
            GameOver.SetActive(false);
        }
    }

    public void ScoreUpdate(int coins)
    {
        coinstxt.text = coins.ToString();
    }

    public void LivesUpdate(int lives)
    {
        livestxt.text = lives.ToString();
    }

    public void LevelUpdate(int world, int stage)
    {
        leveltxt.text = world.ToString() + "-" + stage.ToString();
    }
}
