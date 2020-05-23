using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{   
    public bool isExplored = false;
    public Waypoint exploredFrom;
    public bool isPlaceable = true;
    const int gridSize = 10;
    public int GetGridSize() => gridSize;

    private void Start()
    {
        Physics.queriesHitTriggers = true;
    }
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / gridSize),
        Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                Debug.Log(gameObject + " can't place here");
            }

        }
    }



}
