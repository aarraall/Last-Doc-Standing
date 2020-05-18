using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("On Death")]
    [Tooltip("FX prefab on death")] [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    
    //[SerializeField] int scorePerHit = 12;

    //todo Add deathFX scalability
    [Header("On Alive")]
    [SerializeField] int healthPoint = 200;
    [SerializeField] int damagePerHit = 20;
    
    //Scoreboard scoreboard; 
    
    // Start is called before the first frame update
    void Awake()
    {
        addNonTriggerBoxCollider();
    }
    private void addNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        healthPoint = healthPoint - damagePerHit;
        print(healthPoint);
        //todo consider hit FX
        if (healthPoint < 1)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity) as GameObject;
        fx.transform.parent = parent;
        Destroy(fx, 5f); // todo customize it    
        Destroy(gameObject);
    }
}
