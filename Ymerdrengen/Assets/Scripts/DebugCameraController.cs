using System.Collections.Generic;
using UnityEngine;

public class DebugCameraController : MonoBehaviour {
    public float rotationSpeed = 1f;
    public float baseTranslateSpeed = 5f;

    private float translateMultiplier;
    private float actualTranslateSpeed { get { return baseTranslateSpeed * translateMultiplier; } }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftShift))
            translateMultiplier = 9f;
        else
            translateMultiplier = 1f;

        if (Input.GetMouseButton(1)) {
            Cursor.visible = false;

            var xMovement = Input.GetAxis("Mouse X");
            var yMovement = Input.GetAxis("Mouse Y");
            var turnAngleX = Mathf.Atan(xMovement) * rotationSpeed;
            var turnAngleY = Mathf.Atan(yMovement) * rotationSpeed;

            transform.eulerAngles += new Vector3(-turnAngleY, turnAngleX, 0);
        } else {
            Cursor.visible = true;
        }

        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(transform.forward * actualTranslateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(-transform.forward * actualTranslateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(-transform.right * actualTranslateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(transform.right * actualTranslateSpeed * Time.deltaTime);
        }
    }
}
