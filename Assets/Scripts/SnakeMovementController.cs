using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovementController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segments;
    public Transform segmentPrefab;

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
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y)+direction.y,0.0f);
    }



}
