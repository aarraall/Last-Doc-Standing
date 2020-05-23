using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Pathfinder pathfinder;
    Transform parentOfGoalFX;
    List<Waypoint> path;
    [SerializeField] ParticleSystem goalFX;
    
    [Range(.1f, 5f)]
    [SerializeField] float enemySpeed = .5f;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.GetPath();
        parentOfGoalFX = GameObject.Find("ParentOfGoalFX").transform;
        StartCoroutine(FollowPath(path));
        
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(enemySpeed);
        }

        SelfDestruct();
        
    }
    private void SelfDestruct()
    {
        ParticleSystem dieFX = Instantiate(goalFX, transform.position, Quaternion.identity);
        dieFX.transform.parent = parentOfGoalFX;
        dieFX.Play();

        Destroy(gameObject);
    }

}
