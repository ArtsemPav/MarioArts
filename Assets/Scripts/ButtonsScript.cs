using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    public void CloseApplication()
    {
        Application.Quit();
    }

    public void ResetLevel()
    {
        GameManager.Instance.ResetLevel();
    }

    public void RestartLevel()
    {
        GameManager.Instance.NewGame();
    }
}
