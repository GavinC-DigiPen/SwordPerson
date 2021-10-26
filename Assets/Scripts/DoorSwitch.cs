//------------------------------------------------------------------------------
//
// File Name:	DoorSwitch.cs
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

public class DoorSwitch : MonoBehaviour
{
    [Tooltip("The rod of the switch that will rotate")]
    public GameObject switchRod;
    [Tooltip("The amount the rod of the switch will rotate")]
    public float rodRotation = 25;
    [Tooltip("The door that will be effected")]
    public GameObject door;
    [Tooltip("The opacity of the door when it doesnt have a collider")]
    public float doorOpacity = 0.5f;

    private BoxCollider2D doorBC;
    private SpriteRenderer doorSR;

    private bool switchOn = false;

    // Start is called before the first frame update
    void Start()
    {
        doorBC = door.GetComponent<BoxCollider2D>();
        doorSR = door.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SwordData swordDataScript = collision.GetComponent<SwordData>();
        if (swordDataScript)
        {
            Color newDoorColor = doorSR.color;
            if (switchOn)
            {
                newDoorColor.a = 1;
                switchRod.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rodRotation));
            }
            else
            {
                newDoorColor.a = doorOpacity;
                switchRod.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rodRotation));
            }
            doorSR.color= newDoorColor;
            doorBC.enabled = switchOn;

            switchOn = !switchOn;
        }
    }
}
