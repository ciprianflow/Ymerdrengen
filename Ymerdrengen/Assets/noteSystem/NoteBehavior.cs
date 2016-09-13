using UnityEngine;
using System.Collections;

public class NoteBehavior : MonoBehaviour {

    float speed = 3f;
    float hoverHeight = 0.5f;
    float hoverVariance = 0.5f;
    float hoverSpeed = 3;
    float swingVariance = 0.5f;
    float swingSpeed = 3;
    float rotationSpeed = 2f;
    float t = 0f;
    float splineLength = 0f;
    float endDistance = 0f;
    float randValue = 0f;
    float randDir = 0f;
    BezierSpline spline;
    Transform boy;

    public void setSwingVariance(float f)
    {
        swingVariance = f;
    }

    public void setSwingSpeed(float f)
    {
        swingSpeed = f;
    }


    public void init(float speed, float hoverHeight, float hoverVariance, float hoverSpeed, float rotationSpeed, float startPoint, float startdistance, float endDistance, BezierSpline spline,
        Transform boy)
    {
        this.speed = speed;
        this.hoverHeight = hoverHeight;
        this.hoverVariance = hoverVariance;
        this.hoverSpeed = hoverSpeed;
        this.rotationSpeed = rotationSpeed;
        this.spline = spline;
        this.boy = boy;
        this.endDistance = endDistance;

        randValue = Random.Range(0.1f, 0.9f);
        randDir = (int)Random.Range(0f, 2f);
        randDir = (randDir == 0) ? -1 : randDir;

        splineLength = spline.GetSplineLength();

        transform.Rotate(0, Random.Range(0, 180), 0);

        t = startdistance / splineLength + startPoint;
        t = (t > 1f) ? 1f : t;
        transform.position = spline.GetPoint(t);
    }

    void Update()
    {
        move();
    }

    public void move()
    {
        if (spline == null)
            return;

        t -= (Time.deltaTime * speed);
        Vector3 dir = spline.GetDirection(t);
        dir = Quaternion.Euler(0, -90, 0) * dir;
        dir = swingVariance * Mathf.Cos(randValue * Time.timeSinceLevelLoad * swingSpeed) * dir * randDir;
        transform.position = spline.GetPoint(t) + new Vector3(0, 
            hoverHeight + (hoverVariance * Mathf.Sin(randValue * Time.timeSinceLevelLoad * hoverSpeed)), 0) + dir;
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        if (Vector3.Distance(boy.position, transform.position) < endDistance || t <= 0)
        {
            transform.GetComponent<Animator>().SetTrigger("dePlop");
        }

    }

    public void destroyThis()
    {
        Destroy(gameObject);
    }

}
