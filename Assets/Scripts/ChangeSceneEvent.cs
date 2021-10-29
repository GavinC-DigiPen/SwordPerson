//------------------------------------------------------------------------------
//
// File Name:	ChangeSceneEvent.cs
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

public class ChangeSceneEvent : MonoBehaviour
{
    [Tooltip("Name of the scene you want this script to load")]
    public string SceneName;

    //Function to be called that loads new scene
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
