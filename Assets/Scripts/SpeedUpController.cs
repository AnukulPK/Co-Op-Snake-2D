using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpController : MonoBehaviour
{
    public BoxCollider2D gridArea;
    private float speedUpDuration = 3f;
    private float speedMultipler = 2f;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SnakeMovementController>() != null || collision.gameObject.GetComponent<SnakeAlternateMovementController>() != null)
        {
            RandomizePosition();
            if (collision.TryGetComponent(out Rigidbody2D rb))
            {
                rb.velocity *= speedMultipler;
            }
        }
    }

    void ApplyspeedUpPowerup(GameObject snakeHead)
    {
       
        snakeHead.GetComponent<Collider2D>().enabled = false;

       
        StartCoroutine(ResetCollisionAfterDuration(snakeHead));
    }


    IEnumerator ResetCollisionAfterDuration(GameObject snakeHead)
    {
        yield return new WaitForSeconds(speedUpDuration);

       
        snakeHead.GetComponent<Collider2D>().enabled = true;

    }
}
