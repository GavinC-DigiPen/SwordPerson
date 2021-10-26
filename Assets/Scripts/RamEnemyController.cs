//------------------------------------------------------------------------------
//
// File Name:	RamEnemyController.cs
// Author(s):	Gavin Cooper (gavin.cooper@digipen.edu)
// Project:	    SwordMan
// Course:	    WANIC VGP2
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamEnemyController : MonoBehaviour
{
    [Tooltip("The current health of the enemy")]
    public float currentHealth = 1;
    [Tooltip("The movespeed of the enemy")]
    public float moveSpeed = 2;
    [Tooltip("How fast the enemy slows down")]
    public float slowDownSpeed = 0.01f;
    [Tooltip("The range at which the enemy will detect the player")]
    public float detectionRange = 10;

    private Rigidbody2D enemyRB;
    private GameObject target;
    private Vector2 dirrection;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dirrection = target.transform.position - transform.position;
        if (dirrection.magnitude <= detectionRange)
        {
            dirrection = dirrection.normalized;
            enemyRB.velocity = dirrection * moveSpeed;
        }
        else
        {
            enemyRB.velocity = Vector2.Lerp(enemyRB.velocity, new Vector2(0, 0), slowDownSpeed);
        }
    }


    // Check for collision with sword
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SwordData swordDataScript = collision.GetComponent<SwordData>();
        if (swordDataScript)
        {
            currentHealth -= swordDataScript.swordDamage;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }                
    }
}
