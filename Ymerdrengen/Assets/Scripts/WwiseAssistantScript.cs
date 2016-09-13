// <copyright file="WwiseAssistantScript.cs" company="Team4">
// Copyright(c) 2016 All Rights Reserved
// </copyright>
// <author>Alexander Kirk Jørgensen</author>
// <date>12-09-2016</date>
// <summary>10-slot generic Wwise audio player. Controlled via Keypad.</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Used to test sounds in game. Event names are bound to a list matching 
/// the numbers on the keypad (0-9), and then the buttons ('/', '*', '-', '+', etc.)
/// </summary>
public class WwiseAssistantScript : MonoBehaviour
{
    /// <summary>
    /// A list of possible Wwise audio events.
    /// First 10 possibilities are Keypad0-Keypad9.
    /// </summary>
    public List<string> AudioCommands;

    /// <summary>
    /// Dictionary containing a translation between Keypad buttons and audio commands.
    /// </summary>
    private Dictionary<KeyCode, string> keypadBindings = new Dictionary<KeyCode, string>();
    
    /// <summary>
    /// Initializes the keypadBindings dictionary such that it can be used in the Update cycle.
    /// </summary>
    private void Start()
    {
        var enumNames = Enum.GetNames(typeof(KeyCode));
        enumNames = enumNames.Where(name => name.Contains("Keypad")).ToArray();
        var enumIter = enumNames.GetEnumerator();

        foreach (string s in this.AudioCommands)
        {
            if (!enumIter.MoveNext())
            {
                break;
            }

            KeyCode curCode = (KeyCode)Enum.Parse(typeof(KeyCode), enumIter.Current.ToString());
            this.keypadBindings.Add(curCode, s);
        }
    }

    /// <summary>
    /// Called once per frame, checks whether the user has pressed any of the bound keypad keys.
    /// If this is the case, it posts the desired audio event.
    /// </summary>
    private void Update()
    {
        foreach (KeyCode keyCode in this.keypadBindings.Keys)
        {
            if (Input.GetKeyDown(keyCode))
            {
                AkSoundEngine.PostEvent(this.keypadBindings[keyCode], this.gameObject);
            }
        }
    }
}

/**
 * --- NOTES DESCRIBING THE MECHANICS OF WWISE ---
 *  if (Input.GetKeyDown(KeyCode.Keypad1)) {
 *      AkSoundEngine.PostEvent("test", gameObject);
 *  }
 *
 *  if (Input.GetKeyDown(KeyCode.Keypad2)) {
 *      sIdx++;
 *      AkSoundEngine.SetSwitch("test_switch", "switch" + sIdx, gameObject);
 *  }
 *
 *  if (Input.GetKeyDown(KeyCode.Keypad3)) {
 *      var newState = isState1 ? "state1" : "state2";
 *      AkSoundEngine.SetState("test_state_group", newState);
 *      isState1 = !isState1;
 *  }
 *
 *  AkSoundEngine.SetRTPCValue("test_parameter", normDistance);
 */