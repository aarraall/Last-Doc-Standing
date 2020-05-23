using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemy;
    [SerializeField] Transform parentOfAlive;
    [Range(0.1f, 100f)]
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] Text spawnedEnemies;
    [SerializeField] float score;
    [SerializeField] float scorePerEnemy = 50f;
    [SerializeField] AudioClip spawnedEnemySFX;

    private void Start()
    {
        StartCoroutine(RepeatedlySpawnEnemies());
        spawnedEnemies.text = score.ToString();
    }
    IEnumerator RepeatedlySpawnEnemies()
    {
        while (true)
        {
            AddScore();
            PlaySound();
            EnemyMovement enemySpawner = Instantiate(enemy, transform.position, Quaternion.identity);
            enemySpawner.transform.parent = parentOfAlive;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }

    }

    private void PlaySound()
    {
        GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
    }

    private void AddScore()
    {
        score += scorePerEnemy;
        spawnedEnemies.text = score.ToString();
    }
}
