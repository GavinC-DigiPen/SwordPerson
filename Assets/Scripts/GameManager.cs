//------------------------------------------------------------------------------
//
// File Name:	GameManager.cs
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
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static int maxLives = 3;
    private static int currentLives = 3;

    public static UnityEvent OnLivesChange = new UnityEvent();

    public static int MaxLives
    {
        get
        {
            return maxLives;
        }
        set
        {
            maxLives = value;
            OnLivesChange.Invoke();
        }
    }

    public static int CurrentLives
    {
        get
        {
            return currentLives;
        }
        set
        {
            currentLives = value;
            OnLivesChange.Invoke();
        }
    }
}
