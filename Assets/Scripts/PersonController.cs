//------------------------------------------------------------------------------
//
// File Name:	PersonController.cs
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

public class PersonController : MonoBehaviour
{
    [Tooltip("The maximum speed")]
    public float maxSpeed = 5f;
    [Tooltip("The acceleration")]
    public float acceleration = 0.5f;
    [Tooltip("How fast the player slows down when not moving (0 - 1)")]
    public float slowDown = 0.01f;

    private Rigidbody2D playerRB;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;

    private Vector2 dirrection = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirrection.x = MostRecentKey(leftKey, rightKey);
        dirrection.y = MostRecentKey(downKey, upKey);

        Vector2 newVelocity = playerRB.velocity;
        newVelocity.x += acceleration * dirrection.x;
        newVelocity.y += acceleration * dirrection.y;

        if (dirrection.x == 0)
        {
            newVelocity.x = Mathf.Lerp(newVelocity.x, 0, slowDown);
        }
        if (dirrection.y == 0)
        {
            newVelocity.y = Mathf.Lerp(newVelocity.y, 0, slowDown);
        }

        playerRB.velocity = newVelocity;
        playerRB.velocity = Vector2.ClampMagnitude(playerRB.velocity, maxSpeed);
    }

    // Find the most recent key pushed or currently pushed
    // Parms:
    //  negative: the key that will result in a -1 returned
    //  possitive: the key that will result in a 1 returned
    // Returns:
    //  -1 or 1 based on which button is currently active, 0 if no key
    private int MostRecentKey(KeyCode negative, KeyCode positive)
    {
        int dirrection = 0;
        if (Input.GetKeyDown(negative))
        {
            dirrection = -1;
        }
        else if (Input.GetKey(negative) && !Input.GetKey(positive))
        {
            dirrection = -1;
        }
        else if (Input.GetKeyDown(positive))
        {
            dirrection = 1;
        }
        else if (Input.GetKey(positive) && !Input.GetKey(negative))
        {
            dirrection = 1;
        }
        else if (!Input.GetKey(negative) && !Input.GetKey(positive))
        {
            dirrection = 0;
        }

        return dirrection;
    }
}
