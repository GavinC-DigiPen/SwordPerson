//------------------------------------------------------------------------------
//
// File Name:	ProjectileEnemyController.cs
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

public class ProjectileEnemyController : MonoBehaviour
{
    [Tooltip("The health of the enemy")]
    public float health = 1;
    [Tooltip("The move speed of the enemy")]
    public float moveSpeed = 2;
    [Tooltip("How fast the enemy slows down")]
    public float slowDownSpeed = 0.01f;
    [Tooltip("The range at which the enemy will detect the player")]
    public float detectionRange = 15;
    [Tooltip("The farthest range the enemy will shot at")]
    public float maxShootingRange = 10;
    [Tooltip("The closest range the enemy will shot at")]
    public float minShootingRange = 7;
    [Tooltip("The move speed of the summoned projectile")]
    public float projectileSpeed = 4;
    [Tooltip("The time it takes to shoot a projectile")]
    public float shootTime = 0.25f;
    [Tooltip("The time before the projectile can be shot again")]
    public float shootCooldownTime = 0.5f;
    [Tooltip("The color of the enemy when they are shooting")]
    public Color shootingEnemyColor;
    [Tooltip("The prefab of an object that will be the projectile")]
    public GameObject projectilePrefab;
    [Tooltip("Prefab to be summoned on hit, a particle effect")]
    public GameObject particleEffect;

    private Rigidbody2D enemyRB;
    private SpriteRenderer enemySR;
    private GameObject target;
    private Vector2 dirrection;

    private bool shooting = false;
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
        if (dirrection.magnitude <= maxShootingRange)
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

    // Summon the projecile, rotate it, and add force
    private void ShootProjectile()
    {
        enemySR.color = startingColor;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        float projectileRotation;
        dirrection = (target.transform.position - transform.position).normalized;
        projectileRotation = Mathf.Atan2(dirrection.x, dirrection.y) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -projectileRotation));
        projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * projectileSpeed;

        Invoke("ResetShootingCooldown", shootCooldownTime);
    }

    // Reset shooting
    private void ResetShootingCooldown()
    {
        shooting = false;
    }
}
