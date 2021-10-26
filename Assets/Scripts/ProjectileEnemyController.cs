//------------------------------------------------------------------------------
//
// File Name:	ProjectileEnemyController.cs
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

public class ProjectileEnemyController : MonoBehaviour
{
    [Tooltip("The current health of the enemy")]
    public float currentHealth = 1;
    [Tooltip("The movespeed of the enemy")]
    public float moveSpeed = 2;
    [Tooltip("How fast the enemy slows down")]
    public float slowDownSpeed = 0.01f;
    [Tooltip("The range at which the enemy will detect the player")]
    public float detectionRange = 15;
    [Tooltip("The farthest range the enemy will shot at")]
    public float maxShootingRange = 10;
    [Tooltip("The closest range the enemy will shot at")]
    public float minShootingRange = 7;
    [Tooltip("The time it takes to shoot a projectile")]
    public float shootTime = 0.25f;
    [Tooltip("The time before the projectile can be shot again")]
    public float shootCooldownTime = 0.5f;

    public Color shootingEnemyColor;
    public GameObject projectilePrefab;

    private Rigidbody2D enemyRB;
    private SpriteRenderer enemySR;
    private GameObject target;
    private Vector2 dirrection;

    // public for debuging
    public bool shooting = false;
    private Color startingColor;

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
        if (dirrection.magnitude <= minShootingRange)
        {
            dirrection = dirrection.normalized;
            enemyRB.velocity = -dirrection * moveSpeed;
        }
        else if (dirrection.magnitude <= maxShootingRange)
        {
            enemyRB.velocity = Vector2.Lerp(enemyRB.velocity, new Vector2(0, 0), slowDownSpeed);
            if (shooting == false)
            {
                enemySR.color = shootingEnemyColor;
                Invoke("ShootProjectile", shootTime);
                shooting = true;
            }
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

        if (shooting)
        {

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

    private void ShootProjectile()
    {
        enemySR.color = startingColor;

        GameObject projectile = Instantiate(projectilePrefab);

        // rotate toward player, add force forward

        Invoke("ResetShootingCooldown", shootCooldownTime);
    }

    private void ResetShootingCooldown()
    {
        shooting = false;
    }
}
