using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool IsWall { set; get; }
    public int gridPositionX { private set; get; }
    public int gridPositionY { private set; get; }

    public Vector3 worldPosition { private set; get; }

    private void Awake()
    {
        this.gridPositionX = Mathf.FloorToInt((this.gameObject.transform.position.x));
        this.gridPositionY = Mathf.FloorToInt((this.gameObject.transform.position.y));

        worldPosition = this.gameObject.transform.position;
    }

    private void Update()
    {
        if (IsWall)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
