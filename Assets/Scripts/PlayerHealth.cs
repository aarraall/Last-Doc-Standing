using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 1000f;
    [SerializeField] float healthDecrease = 50f;
    [SerializeField] Text healthText;
    [SerializeField] ParticleSystem gameOverFX;
    [SerializeField] AudioClip playerDamageSFX;
    public float GetHitPoints()
    {
        return hitPoints;
    }

    public void SetHitPoints(float value)
    {
        hitPoints = value;
    }

    private void Start()
    {
        healthText.text = hitPoints.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(playerDamageSFX);
        hitPoints -= healthDecrease;
        healthText.text = hitPoints.ToString();

        if(hitPoints <= 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        ParticleSystem fx = Instantiate(gameOverFX, transform.position, Quaternion.identity);
        Destroy(fx, 0.5f);
        Destroy(gameObject);
    }
}
