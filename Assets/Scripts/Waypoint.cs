using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    Color[] colorField = { Color.red, Color.blue, Color.green };
    Pathfinder pathfinder;
    public bool isExplored = false;
    public Waypoint exploredFrom; 
    const int gridSize = 10;
    public int GetGridSize() => gridSize;

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / gridSize),
        Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }
    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    } 
    private void ColoringConditions()
    {
        if (this == pathfinder.GetStartPoint())

        {

            SetTopColor(colorField[0]);

        }

        else if (this == pathfinder.GetEndPoint())

        {

            SetTopColor(colorField[1]);

        }

        else if (isExplored)

        {

            SetTopColor(colorField[2]);

        }
    }
    private void Start()
    {
        pathfinder = GetComponentInParent<Pathfinder>();
    }
    private void Update()
    {
        ColoringConditions();
    }

   
}
