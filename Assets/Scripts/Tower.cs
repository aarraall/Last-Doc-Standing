using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] Transform objectPan;
    [SerializeField] float attackRange = 50f;
    [SerializeField] ParticleSystem projectileParticle;
    
    [Header("State")]
    [SerializeField] Transform targetEnemy;
    

    public Waypoint baseWaypoint;
    
    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            LookAtEnemy();
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        Enemy[] numberOfEnemies = FindObjectsOfType<Enemy>();
        if (numberOfEnemies.Length == 0) { return; }

        Transform closestEnemy = numberOfEnemies[0].transform;

        foreach(Enemy testEnemy in numberOfEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if (distToA < distToB)
        {
            return transformA;
        }
        return transformB;
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        ParticleSystem.EmissionModule emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }

    private void LookAtEnemy()
    {
        objectPan.LookAt(targetEnemy);
       
    }
}
