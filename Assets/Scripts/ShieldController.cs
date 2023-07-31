using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public BoxCollider2D gridArea;
    private float shieldDuration = 3f;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SnakeMovementController>() != null || collision.gameObject.GetComponent<SnakeAlternateMovementController>() != null)
        {
            ApplyShieldPowerup(collision.gameObject);
            RandomizePosition();
        }
    }

    void ApplyShieldPowerup(GameObject snakeHead)
    {
        // Disable collision with snake's head for the duration of the shield
        snakeHead.GetComponent<Collider2D>().enabled = false;

        // Re-enable collision after the shield duration expires
        StartCoroutine(ResetCollisionAfterDuration(snakeHead));
    }

    IEnumerator ResetCollisionAfterDuration(GameObject snakeHead)
    {
        yield return new WaitForSeconds(shieldDuration);

        // Re-enable collision after the shield duration expires
        snakeHead.GetComponent<Collider2D>().enabled = true;
       
    }
}
