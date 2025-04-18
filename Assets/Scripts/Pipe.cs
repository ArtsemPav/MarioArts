using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour
{
    public Transform pipeConnection;
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if ((pipeConnection != null) && (collision.gameObject.CompareTag("Player")))
        {
            if (Input.GetKeyDown(enterKeyCode))
            {
                if (Input.GetKey(enterKeyCode) && collision.TryGetComponent(out Player player))
                {
                    StartCoroutine(EnterPipe(player));
                }
            }
        }
    }

    private IEnumerator EnterPipe(Player player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;

        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return Move(player.transform, enteredPosition, enteredScale);
        yield return new WaitForSeconds(1f);

        bool underground = pipeConnection.position.y < 0f;
        Camera.main.GetComponent<SideScrolling>().SetUnderground(underground);

        if (exitDirection != Vector3.zero)
        {
            player.transform.position = pipeConnection.position - exitDirection;
            yield return Move(player.transform, pipeConnection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.transform.position = pipeConnection.position;
            player.transform.localScale = Vector3.one;
        }

        player.GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;
    }
}
