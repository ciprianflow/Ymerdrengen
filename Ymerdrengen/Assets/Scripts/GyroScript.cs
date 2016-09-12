/// The script for the gyroscope, tilt and shake
// <copyright file=GyroScript.cs company=team 4>
// team 4 
// </copyright>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Gyro script
/// </summary>
public class GyroScript : MonoBehaviour
{
    /// <summary>
    /// Variables declaration 
    /// </summary>
    ////X direction of the tilt
    public float Xdir;
    ////Z direction of the tilt
    public float Zdir;
    ////Gravity multiplier
    public float GravityForce;
    ////Threshold after which the tilt starts to account
    public float tiltThreshold;

    /// <summary>
    /// variables for the shaking
    /// </summary>
    float accelerometerUpdateInterval = 0.0167f; //60Hz
    // The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds = 1.0f;
    public float shakeDetectionThreshold = 2.0f;
    private float lowPassFilterFactor;
    private Vector3 lowPassValue = Vector3.zero;
    private Vector3 acceleration;
    private Vector3 deltaAcceleration;
    public static bool isShaked;

    float xCalib;
    float zCalib;
    float timer;

    public float calibrationTime;


    public bool isCalibrating;
    public bool isCalibrated;
    float calibTimer;

    private MoveScript moveScript;


    GameObject ball;
    Text text;

    /// <summary>
    /// Initialization function
    /// </summary>
    void Start () {
        DontDestroyOnLoad(transform.gameObject);
        //Only for Philip's Scene
        text = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>();
        moveScript = GameObject.FindWithTag("Ymerdrengen").GetComponent<MoveScript>();
        //ball = GameObject.Find("Sphere").gameObject;

        Input.gyro.enabled = true;
        // force landscape view
        Screen.orientation = ScreenOrientation.LandscapeRight;
        // prevent tablet from going to sleep while playing
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        xCalib = 0;
        zCalib = 0;
        tiltThreshold = 1.5f;
        Xdir = 0;
        Zdir = 0;
        GravityForce = 200f;
        calibrationTime = 2;
        timer = 0;
        calibTimer = 0;

        isCalibrated = false;
        isShaked = false;

        ///stuff for shaking
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
    }

    /// <summary>
    /// Update is called once per frame, fixed for physics
    /// </summary>
    void FixedUpdate () {

        timer += Time.deltaTime;
        if(timer > 0.5 && !isCalibrated)
        {
            calibrate();
            isCalibrated = true;
        }

        // gravity stuff
        float moveHorizontal = Input.gyro.attitude.x;
        float moveVertical = Input.gyro.attitude.y;



        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Quaternion direction = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        movement = direction * movement;

        
        Xdir = -(movement.x - xCalib) * GravityForce;
        Zdir = -(movement.z - zCalib) * GravityForce;

        // text.text = "x: " + Input.gyro.attitude.x + "z: " + Input.gyro.attitude.y;


        if (Mathf.Abs(Xdir) < tiltThreshold)
            Xdir = 0;
        if (Mathf.Abs(Zdir) < tiltThreshold)
            Zdir = 0;

        //Only for Philip's Scene
        /*
        if(isCalibrated)
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(Xdir,0,Zdir) * Time.deltaTime);
        else
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        */

        /// stuff for shaking
        acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor); // interpolation by 1:60
        deltaAcceleration = acceleration - lowPassValue;
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
        {
            // Perform your "shaking actions" here, with suitable guards in the if check above, if necessary to not, to not fire again if they're already being performed.
            isShaked = true;
            //text.text = "shaked!";
            //Debug.Log("Shake event detected at time " + Time.time);
        }
    }

    public void calibrate()
    {
        xCalib = Input.gyro.attitude.x;
        zCalib = Input.gyro.attitude.y;
        isCalibrated = false;
        isCalibrating = true;


    }



    void Update()
    {
        if(isCalibrating)
        {
            text.text = "Please hold the tablet still. It is Calibrating";
            float tempxCal = Input.gyro.attitude.x;
            float tempzCal = Input.gyro.attitude.y;
            if(checkCalib(tempxCal, xCalib) && checkCalib(tempzCal,zCalib))
            {
                calibTimer += Time.deltaTime;
                if(calibTimer > calibrationTime)
                {
                    isCalibrating = false;
                    isCalibrated = true;
                    text.text = "";
                    calibTimer = 0;
                    moveScript.CharacterState = States.MovingForward;
                }
            }
            else
            {
                xCalib = Input.gyro.attitude.x;
                zCalib = Input.gyro.attitude.y;
                calibTimer = 0;
                //moveScript.CharacterState = States.MovingForward;
            }

        }
        if (isCalibrated)
        {
            //moveScript.CharacterState = States.MovingForward;
        }
    }

    public bool checkCalib(float cal1, float cal2)
    {
        if (Mathf.Abs(cal1 - cal2) < 0.06)
            return true;
        else return false;
    }
}
