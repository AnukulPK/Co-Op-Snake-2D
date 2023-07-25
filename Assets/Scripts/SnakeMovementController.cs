using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovementController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segments;
    public Transform segmentPrefab;
    public BoxCollider2D boundaryArea;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MassGainerController>() != null)
        {
            Grow();
        }
    }

}
