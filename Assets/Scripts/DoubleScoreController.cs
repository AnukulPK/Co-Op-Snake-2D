using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreController : MonoBehaviour
{
    public BoxCollider2D gridArea;
    private float doubleScoreDuration = 3f;
    public bool doubleScoreStatus = false; 

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
            ApplyDoubleScorePowerup(collision.gameObject);
            RandomizePosition();
        }
    }

    void ApplyDoubleScorePowerup(GameObject snakeHead)
    {
        // Disable collision with snake's head for the duration of the doubleScore
        doubleScoreStatus = true;

        // Re-enable collision after the doubleScore duration expires
        StartCoroutine(ResetCollisionAfterDuration(snakeHead));
    }

    IEnumerator ResetCollisionAfterDuration(GameObject snakeHead)
    {
        yield return new WaitForSeconds(doubleScoreDuration);

        // Re-enable collision after the doubleScore duration expires
        doubleScoreStatus=false;

    }
}
