using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Linking Variable")]
    [SerializeField] GameObject enemyBody;
    
    [Header("On Death")]
    [Tooltip("FX prefab on death")] [SerializeField] ParticleSystem deathFX;
    Transform parentOfDeathFX;

    //[SerializeField] int scorePerHit = 12;

    [Header("On Alive")]
    [SerializeField] int healthPoint = 200;
    [SerializeField] int damagePerHit = 20;

    [Header("FX and SFX")]
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] AudioClip enemyKillSFX;
    [SerializeField] AudioClip enemyHitSFX;
    AudioSource audioSource;
    //Scoreboard scoreboard; 
    private void Start()
    {
        parentOfDeathFX = GameObject.Find("EnemyDeathFX").transform;
        audioSource = GetComponent<AudioSource>();
    }
    void Awake()
    {
        addNonTriggerBoxCollider();
    }
    private void addNonTriggerBoxCollider()
    {
        Collider boxCollider = enemyBody.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (healthPoint < 1)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        healthPoint = healthPoint - damagePerHit;
        hitParticle.Play();
        audioSource.PlayOneShot(enemyHitSFX);
    }

    private void KillEnemy()
    {
        ParticleSystem dieFX = Instantiate(deathFX, transform.position, Quaternion.identity);
        dieFX.Play();
        dieFX.transform.parent = parentOfDeathFX;
        AudioSource.PlayClipAtPoint(enemyKillSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
