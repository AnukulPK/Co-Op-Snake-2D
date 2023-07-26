using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovementController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segments;
    public Transform segmentPrefab;
    public BoxCollider2D boundaryArea;
    public ScoreController scoreController;
    public GameOverController gameOverController;

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        Bounds bounds = this.boundaryArea.bounds;
        /*Iterating in reverse order to move the different parts sequentially from the back*/
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0.0f);

        if (this.transform.position.x > bounds.max.x)
        {
            this.transform.position = new Vector3(-Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0.0f);
        }else if (this.transform.position.x < bounds.min.x)
        {
            this.transform.position = new Vector3(-Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0.0f);
        }
        else if (this.transform.position.y > bounds.max.y)
        {
            this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, -Mathf.Round(this.transform.position.y) + direction.y, 0.0f);
        }
        else if (this.transform.position.y < bounds.min.y)
        {
            this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, -Mathf.Round(this.transform.position.y) + direction.y, 0.0f);
        }

    }

    private void Grow()
    {
        Transform segment =  Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
        scoreController.IncreaseScore(10);
    }

    private void Shrink()
    {
        int shrinkAmount = 1;
        int segmentsToRemove = Mathf.Min(shrinkAmount, segments.Count);

        // Remove segments from the end (tail) of the snake
        for (int i = 0; i < segmentsToRemove; i++)
        {
            int lastIndex = segments.Count - 1;
            if (lastIndex >= 1)
            {
                // Destroy the last segment in the list
                Destroy(segments[segments.Count - 1].gameObject);
                segments.RemoveAt(segments.Count - 1);
            }
        }

        scoreController.DecreaseScore(10);

    }

    private void ResetState()
    {
        for(int i = 1; i < segments.Count ; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(this.transform);

        this.transform.position = Vector3.zero;
        scoreController.ResetUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MassGainerController>() != null)
        {
            Grow();
        } else if (collision.gameObject.GetComponent<MassBurnerController>() != null)
        {
            Shrink();
        } else if (collision.tag == "SelfPlayer")
        {
            gameOverController.PlayerDied();
            this.enabled = false;
        }
        
    }

}
