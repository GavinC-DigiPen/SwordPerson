//------------------------------------------------------------------------------
//
// File Name:	CameraFollow.cs
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

public class CameraFollow : MonoBehaviour
{
    [Tooltip("The player game object")]
    public GameObject target;
    [Tooltip("The speed of the lerp that moves the camera to the target")]
    public float lerpSpeed = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        ResetPosition();
    }

    // Update is called at a fixed rate
    void FixedUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(newPosition.x, target.transform.position.x, lerpSpeed);
        newPosition.y = Mathf.Lerp(newPosition.y, target.transform.position.y, lerpSpeed);
        transform.position = newPosition;

        Vector2 distance = transform.position - target.transform.position;
        if(distance.magnitude > 3)
        {
            ResetPosition();
        }
    }

    // Reset the camera to the player position
    private void ResetPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = target.transform.position.x;
        newPosition.y = target.transform.position.y;
        transform.position = newPosition;
    }
}
