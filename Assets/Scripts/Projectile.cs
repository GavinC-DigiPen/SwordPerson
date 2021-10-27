//------------------------------------------------------------------------------
//
// File Name:	Projectile.cs
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

public class Projectile : MonoBehaviour
{
    [Tooltip("The delay for turning on the collider")]
    public float collisionDelay = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("TurnOnCollider", collisionDelay);
    }

    // Turn on the collider
    private void TurnOnCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // Destroy object on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
