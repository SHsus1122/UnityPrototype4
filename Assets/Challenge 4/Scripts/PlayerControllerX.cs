﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;
    private bool isCanUseTurboBoost = true;
    private float turboBoostTime = 2f;
    private float boostSpeed = 1000;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;
    public ParticleSystem boostParticle;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isCanUseTurboBoost)
        {
            isCanUseTurboBoost = false;
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * boostSpeed * Time.deltaTime, ForceMode.Impulse);
            boostParticle.Play();
            StartCoroutine(TurboBoostCooldown());
        }
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }
    
    IEnumerator TurboBoostCooldown()
    {
        yield return new WaitForSeconds(turboBoostTime);
        isCanUseTurboBoost = true;
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
            Vector3 awayFromEnemy = transform.position - other.gameObject.transform.position;

            Debug.Log("플레이어와 적과의 거리 : " + awayFromPlayer);
            Debug.Log("적과 플레이어와의 거리 : " + awayFromEnemy);

            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }
}
