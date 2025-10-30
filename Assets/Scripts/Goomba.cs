using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.starpower)
            {
                Hit();
                GameManager.Instance.AddCoin();
            }
            else if (collision.transform.DotTest(transform, Vector2.down))
            {
                Flatten();
                GameManager.Instance.AddCoin();
            } else
            {
                if ((GameManager.Instance.lives>1) || (player.bigRenderer.enabled == true))
                {
                    Hit();
                }
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
            GameManager.Instance.AddCoin();
        }
    }
    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;

        Destroy(gameObject, 2f);
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;

        Destroy(gameObject, 5f);
    }
}
