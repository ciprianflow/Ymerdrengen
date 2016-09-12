using UnityEngine;
using System.Collections;

public class Wwise_Test : MonoBehaviour {
    int sIdx = 1;
    bool isState1 = true;
    public GameObject partner;

    float normDistance { get { return Vector3.Distance(partner.transform.position, transform.position); } }

    // Use this for initialization
	void Start () {
        AkSoundEngine.SetSwitch("test_switch", "switch" + sIdx, gameObject); // Initializes Wwise to switch1.
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            AkSoundEngine.PostEvent("test", gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            sIdx++;
            AkSoundEngine.SetSwitch("test_switch", "switch" + sIdx, gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            var newState = isState1 ? "state1" : "state2";
            AkSoundEngine.SetState("test_state_group", newState);
            isState1 = !isState1;
        }

        AkSoundEngine.SetRTPCValue("test_parameter", normDistance);
    }
}
