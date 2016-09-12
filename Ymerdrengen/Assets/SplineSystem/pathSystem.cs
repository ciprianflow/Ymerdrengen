using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class pathSystem : MonoBehaviour
{
    [SerializeField]
    public BezierSpline[] bezierSplines;

    public void SplitPath(BezierSpline spline)
    {
        Array.Resize(ref bezierSplines, bezierSplines.Length + 1);
        createNewBezierSpline(bezierSplines.Length - 1, spline.Points[spline.Points.Length - 1]);
        spline.addConnectionTospline(bezierSplines[bezierSplines.Length - 1]);
    }

    public void AddBezierSpline()
    {
        Array.Resize(ref bezierSplines, bezierSplines.Length + 2);
        createNewBezierSpline(bezierSplines.Length - 1);
    }

    public void Reset()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Debug.Log(transform.GetChild(i).name);
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }

        bezierSplines = new BezierSpline[1];
        createNewBezierSpline(0);
    }

    private void createNewBezierSpline(int index)
    {
        GameObject spline = new GameObject();
        spline.transform.SetParent(this.transform);
        bezierSplines[index] = spline.AddComponent<BezierSpline>();
        spline.name = "spline " + index;
        bezierSplines[index].Reset(index);
    }

    private void createNewBezierSpline(int index, Vector3 pos)
    {      
        GameObject spline = new GameObject();
        spline.transform.SetParent(this.transform);
        bezierSplines[index] = spline.AddComponent<BezierSpline>();
        spline.name = "spline " + index; 
        bezierSplines[index].Reset(pos, index);
    }

    internal void JoinSpline(BezierSpline bezierSpline)
    {
        Vector3 point1 = bezierSpline.Points[bezierSpline.Points.Length - 1];

        for(int i = 0; i < bezierSplines.Length; i++)
        {
            Vector3 point2 = bezierSplines[i].Points[bezierSplines[i].Points.Length - 1];
            if(Vector3.Distance(point1, point2) <= 0.1f && point1 != point2 && bezierSpline != bezierSplines[i])
            {
                bezierSplines[i].Points[bezierSpline.Points.Length - 1] = point1;

                Array.Resize(ref bezierSplines, bezierSplines.Length + 1);
                createNewBezierSpline(bezierSplines.Length - 1, point1);

                bezierSplines[bezierSplines.Length - 1].addPreviusSpline(bezierSpline);
                bezierSplines[bezierSplines.Length - 1].addPreviusSpline(bezierSplines[i]);
                bezierSplines[i].addConnectionTospline(bezierSplines[bezierSplines.Length - 1]);
                bezierSpline.addConnectionTospline(bezierSplines[bezierSplines.Length - 1]);
                Debug.Log("Found two ends point close to eachother merging");
                return;
            }
        }
    }
}
