//------------------------------------------------------------------------------
//
// File Name:	SwordController.cs
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

public class SwordController : MonoBehaviour
{
    public KeyCode swingSwordKey = KeyCode.Space;
    public GameObject swordSwing;
    public float swordSwingTime = 0.25f;
    public float swordSwingCooldown = 0.1f;

    private bool swinging = false;
    private float swordTimer = 0;
    private float cooldownTimer = 0;

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
    }
}
