using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinstxt;
    public TextMeshProUGUI livestxt;
    public TextMeshProUGUI leveltxt;

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
