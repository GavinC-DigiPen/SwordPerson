//------------------------------------------------------------------------------
//
// File Name:	EndLevel.cs
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
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public KeyCode transitionKey = KeyCode.E;
    public string sceneToLoad;

    private SpriteRenderer objectSR;

    private Color startingColor;
    private bool unlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        objectSR = GetComponent<SpriteRenderer>();
        startingColor = objectSR.color;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDamageData[] enemyScripts = FindObjectsOfType<EnemyDamageData>();
        if (enemyScripts.Length == 0)
        {
            objectSR.color = Color.green;
            unlocked = true;
        }
        else
        {
            objectSR.color = startingColor;
            unlocked = false;
        }
    }

    // Check for play collision
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(transitionKey))
            {
                if (unlocked)
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
        }
    }
}
