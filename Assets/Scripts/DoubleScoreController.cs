using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreController : MonoBehaviour
{
    public BoxCollider2D gridArea;
    private float doubleScoreDuration = 3f;
    private bool doubleScoreStatus = false; 

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
        //Setting double score status to true on trigger
        doubleScoreStatus = true;

        
        StartCoroutine(ResetCollisionAfterDuration(snakeHead));
    }

    public bool GetDoubleScoreStatus()
    {
        return doubleScoreStatus;
    }

    IEnumerator ResetCollisionAfterDuration(GameObject snakeHead)
    {
        yield return new WaitForSeconds(doubleScoreDuration);

        // Reset doubleScore status 
        doubleScoreStatus=false;

    }
}
