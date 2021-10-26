//------------------------------------------------------------------------------
//
// File Name:	CameraFollow.cs
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

public class CameraFollow : MonoBehaviour
{
    [Tooltip("What the camera is following")]
    public GameObject target;
    [Tooltip("The speed of the lerp that moves the camera to the target")]
    public float lerpSpeed = 0.05f;

    // Update is called at a fixed rate
    void FixedUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(newPosition.x, target.transform.position.x, lerpSpeed);
        newPosition.y = Mathf.Lerp(newPosition.y, target.transform.position.y, lerpSpeed);
        transform.position = newPosition;
    }
}
