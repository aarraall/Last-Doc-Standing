using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWayPoint; //start and end waypoint instances

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>(); //all coordinates of the map
    List<Waypoint> path = new List<Waypoint>(); //list of shortest way's coordinates
    Queue<Waypoint> queue = new Queue<Waypoint>(); //waypoint queue
    bool isRunning = true;
    Waypoint searchCenter; // the current search center

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public Waypoint GetStartPoint => startWaypoint;
    public Waypoint EndPoint => endWayPoint;

    //Path calculation Algorithm
    private void Awake()
    {
        CalculatePath();
    } //Only the process awakened calculation of the path has to be established
    private void CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
    } //Avoided from conflicts and BFS has been started
        private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block" + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }
        private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbours();
        }
    } //BFS algorithm
            private void HaltIfEndFound()
    {
        if (searchCenter == endWayPoint)
        {
            isRunning = false;
        }
    } //End waypoint has been found algorithm must be stopped
            private void ExploreNeighbours()
    {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    } //Else, it must continue to explore neighbour coordinates
                private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {

        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }

    }
    
    // Calculated path creation algorithm
    public List<Waypoint> GetPath() 
        {
            if(path.Count == 0)
            {
                CreatePath();          
            }
                return path;
        } //To create a path, there mustn't be any other path
        private void CreatePath()
            {
                SetAsPath(endWayPoint);
                Waypoint previous = endWayPoint.exploredFrom;
                while (previous != startWaypoint)
                {
                    SetAsPath(previous);
                    previous = previous.exploredFrom;
                }

                SetAsPath(startWaypoint);
                path.Reverse();
            } // Path creation algorithm
            private void SetAsPath(Waypoint waypoint)
            {
                path.Add(waypoint);
                waypoint.isPlaceable = false;
            }     //Adding waypoint to the list and set them if they're placeable (tower, turret etc.) or not 

    
}
