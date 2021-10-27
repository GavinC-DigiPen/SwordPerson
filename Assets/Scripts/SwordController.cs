//------------------------------------------------------------------------------
//
// File Name:	SwordController.cs
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

public class SwordController : MonoBehaviour
{
    [Tooltip("Key to make sword swing")]
    public KeyCode swingSwordKey = KeyCode.Space;
    [Tooltip("The sword game object")]
    public GameObject swordSwing;
    [Tooltip("The time the sword will be out")]
    public float swordSwingTime = 0.25f;
    [Tooltip("The time before the sword can be swong again")]
    public float swordSwingCooldown = 0.1f;

    private bool swinging = false;
    private float swordTimer = 0;
    private float cooldownTimer = 0;
    private Vector2 dirrection;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        swordSwing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(swingSwordKey) && swinging == false && cooldownTimer <= 0)
        {
            swinging = true;
            swordSwing.SetActive(true);
            swordTimer = swordSwingTime;
        }

        if (swordTimer <= 0)
        {
            if (swinging)
            {
                cooldownTimer = swordSwingCooldown;
            }
            swinging = false;
            swordSwing.SetActive(false);
        }

        swordTimer -= Time.deltaTime;
        cooldownTimer -= Time.deltaTime;

        dirrection = (Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition).normalized;
        angle = Mathf.Atan2(dirrection.x, dirrection.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
    }
}
