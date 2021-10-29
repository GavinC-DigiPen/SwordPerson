//------------------------------------------------------------------------------
//
// File Name:	ChargeEnemy.cs
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

public class ChargeEnemy : MonoBehaviour
{
    [Tooltip("The health of the enemy")]
    public float health = 1;
    [Tooltip("The move speed of the enemy")]
    public float moveSpeed = 2;
    [Tooltip("How fast the enemy slows down")]
    public float slowDownSpeed = 0.01f;
    [Tooltip("The range at which the enemy will detect the player")]
    public float detectionRange = 15;
    [Tooltip("The range at which the enemy will charge at")]
    public float chargeRange = 10;
    [Tooltip("The move speed of the charging enemy")]
    public float chargeSpeed = 4;
    [Tooltip("The time it takes to charge a charge")]
    public float chargeTime = 0.25f;
    [Tooltip("The time before the enemy can charge again")]
    public float chargeCooldownTime = 0.5f;
    [Tooltip("The amount the enemy will overshoot when charging")]
    public float chargeOverShoot = 2;
    [Tooltip("How close the enemy needs to get to the charge location")]
    public float chargeLocationPrecision = 0.25f;
    [Tooltip("The color of the enemy when they are charging")]
    public Color chargingEnemyColor;
    [Tooltip("Prefab to be summoned on hit, a particle effect")]
    public GameObject particleEffect;

    private Rigidbody2D enemyRB;
    private SpriteRenderer enemySR;
    private GameObject target;

    private Vector3 dirrection;
    private bool charging = false;
    private Color startingColor;
    private Vector3 chargeLocation;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemySR = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");

        startingColor = enemySR.color;
    }

    // Update is called once per frame
    void Update()
    {
        dirrection = target.transform.position - transform.position;
        if (charging == false)
        {
            if (dirrection.magnitude <= chargeRange)
            {
                enemySR.color = chargingEnemyColor;
                enemyRB.velocity = Vector2.Lerp(enemyRB.velocity, new Vector2(0, 0), slowDownSpeed);
                chargeLocation = target.transform.position + dirrection * chargeOverShoot;
                Invoke("ChargeCharge", chargeTime);
                charging = true;
            }
            else if (dirrection.magnitude <= detectionRange)
            {
                dirrection = dirrection.normalized;
                enemyRB.velocity = dirrection * moveSpeed;
            }
            else
            {
                enemyRB.velocity = Vector2.Lerp(enemyRB.velocity, new Vector2(0, 0), slowDownSpeed);
            }
        }
        else
        {
            dirrection = chargeLocation - transform.position;
            if (dirrection.magnitude <= chargeLocationPrecision)
            {
                enemySR.color = startingColor;
                enemyRB.velocity = Vector2.Lerp(enemyRB.velocity, new Vector2(0, 0), slowDownSpeed);
                Invoke("ResetChargingCooldown", chargeCooldownTime);
            }
        }
    }

    // Check for collision with sword
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SwordData swordDataScript = collision.GetComponent<SwordData>();
        if (swordDataScript)
        {
            health -= swordDataScript.swordDamage;
            Instantiate(particleEffect, transform.position, Quaternion.identity);

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // Check for collision with things
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemySR.color = startingColor;
        dirrection = (chargeLocation - transform.position).normalized;
        enemyRB.velocity = -dirrection * chargeSpeed;
        Invoke("ResetChargingCooldown", chargeCooldownTime);
    }

    // Charge toward the location
    private void ChargeCharge()
    {
        dirrection = (chargeLocation - transform.position).normalized;
        enemyRB.velocity = dirrection * chargeSpeed;
    }

    // Reset the charging
    private void ResetChargingCooldown()
    {
        charging = false;
    }
}
