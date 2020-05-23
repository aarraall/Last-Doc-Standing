using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int maxTowers = 5;
    [SerializeField] Transform parentOfTowers;
    public Waypoint baseWaypoint;
    
    Queue<Tower> towerQueue = new Queue<Tower>();
    public void AddTower(Waypoint baseWaypoint)
    {
        int numTowers = towerQueue.Count;
        
        if (numTowers < maxTowers)
        {
            InstantiateTower(baseWaypoint);
            
        }
        else
        {
            MoveExistingTower(baseWaypoint);
            
        }
    }
    private void InstantiateTower(Waypoint baseWaypoint)
    {        
        Tower newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = parentOfTowers;
        baseWaypoint.isPlaceable = false;              
        
        newTower.baseWaypoint = baseWaypoint;

        towerQueue.Enqueue(newTower);  
    }
    private void MoveExistingTower(Waypoint newBasePoint)
    {
        var oldTower = towerQueue.Dequeue();

        oldTower.baseWaypoint.isPlaceable = true;

        oldTower.baseWaypoint = newBasePoint;

        oldTower.transform.position = newBasePoint.transform.position;

        towerQueue.Enqueue(oldTower);
    }

    
}
