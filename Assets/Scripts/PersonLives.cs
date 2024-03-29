﻿//------------------------------------------------------------------------------
//
// File Name:	PersonLives.cs
// Author(s):	Gavin Cooper (gavin.cooper@digipen.edu)
// Project:	    SwordPerson
// Course:	    WANIC VGP2
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonLives : MonoBehaviour
{
    [Tooltip("The maximum number of the lives the player has")]
    public int startingLives = 3;
    [Tooltip("The amount of time after getting hit where the player cant take damage")]
    public float immuneTime = 1;
    [Tooltip("Prefab to be summoned on hit, a particle effect")]
    public GameObject particleEffect;

    private SpriteRenderer playerSR;

    private Vector2 respawnLocation;
    private float immuneTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerSR = GetComponent<SpriteRenderer>();

        respawnLocation = transform.position;
        GameManager.CurrentLives = startingLives;

        GameManager.OnLivesChange.AddListener(SummonEffect);

        NewColor();
    }

    // Update is called once per frame
    void Update()
    {
        immuneTimer -= Time.deltaTime;
    }

    // Check for collision with enemy
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (immuneTimer <= 0)
        {
            EnemyDamageData enemyDamageDataScript = collision.gameObject.GetComponent<EnemyDamageData>();
            if (enemyDamageDataScript)
            {
                GameManager.CurrentLives -= enemyDamageDataScript.enemyDamage;
                immuneTimer = immuneTime;
                NewColor();
            }

            if (GameManager.CurrentLives <= 0)
            {
                Invoke("Respawn", 0.5f);
            }
        }
    }

    // Check for collision with checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            respawnLocation = collision.gameObject.transform.position;
        }
    }

    // Change the color to a new random color
    private void NewColor()
    {
        playerSR.color = Random.ColorHSV(0.5f, 1f, 1f, 1f, 0f, 1f, 1f, 1f);
    }

    // Function that respawn the player
    private void Respawn()
    {
        transform.position = respawnLocation;
        GameManager.CurrentLives = startingLives;
    }

    // Summon a prefab that is a particle effect
    private void SummonEffect()
    {
        if (GameManager.CurrentLives < GameManager.MaxLives)
        {
            Instantiate(particleEffect, transform.position, Quaternion.identity);
        }
    }
}
