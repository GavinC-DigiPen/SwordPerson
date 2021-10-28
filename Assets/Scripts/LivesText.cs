//------------------------------------------------------------------------------
//
// File Name:	LivesText.cs
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
using TMPro;

public class LivesText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnLivesChange.AddListener(UpdateText);
        UpdateText();
    }

    // Update text
    void UpdateText()
    {
        GetComponent<TextMeshProUGUI>().text = " Lives: " + GameManager.CurrentLives;
    }
}
