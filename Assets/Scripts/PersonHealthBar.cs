//------------------------------------------------------------------------------
//
// File Name:	PersonHealthBar.cs
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
using UnityEngine.UI;
using TMPro;

public class PersonHealthBar : MonoBehaviour
{
    [Tooltip("The game object that is the health bar")]
    public GameObject healthBar;

    private Vector2 startingPosition;
    private Vector2 startingScale;

    // Start is called before the first frame update
    void Start()
    {
        if (healthBar != null)
        {
            startingPosition = healthBar.transform.localPosition;
            startingScale = healthBar.transform.localScale;
            UpdateLives();
            GameManager.OnLivesChange.AddListener(UpdateLives);
        }
    }

    // Function that updates health bar and text
    void UpdateLives()
    {
        float scaleScaler = (float)GameManager.CurrentLives / GameManager.MaxLives;
        float newXScale = startingScale.x * scaleScaler;

        healthBar.transform.localScale = new Vector2(newXScale, startingScale.y);
        healthBar.transform.localPosition = new Vector2(startingPosition.x - (startingScale.x - newXScale) / 2f, startingPosition.y);
    }
}
