//------------------------------------------------------------------------------
//
// File Name:	DestroyAfterTime.cs
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

public class DestroyAfterTime : MonoBehaviour
{
    [Tooltip("Time before the game is destroyed")]
    public float time = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }
}
