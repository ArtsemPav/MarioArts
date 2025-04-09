using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        StarPower,
    }

    public Type type;
    public float starpowerDuration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect(other.gameObject);

        }
    }

    private void Collect (GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                break;
            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
            case Type.StarPower:
                player.GetComponent<Player>().StarPower(starpowerDuration);
                break;
        }

        Destroy(gameObject);
    }
}
