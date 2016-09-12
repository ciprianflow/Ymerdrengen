using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(pathSystem))]
public class pathView : Editor
{

    /// <summary>
    /// The scale of the direction lines
    /// </summary>
    private const float DirectionScale = 0.5f;

    /// <summary>
    /// Handle variables
    /// </summary>
    private const float HandleSize = 0.08f;

    /// <summary>
    /// The size of the point buttons
    /// </summary>
    private const float PickSize = 0.12f;

    /// <summary>
    /// Selcted poitn index
    /// </summary>
    private int selectedIndex = -1;

    /// <summary>
    /// The spline currenty shown
    /// </summary>
    private pathSystem path;

    /// <summary>
    /// The handle transform of the spline
    /// </summary>
    private Transform handleTransform;

    /// <summary>
    /// The handle rotation of the spline
    /// </summary>
    private Quaternion handleRotation;

    /// <summary>
    /// How many steps per curve is shown
    /// </summary>
    private int stepsPerCurve = 10;

    /// <summary>
    /// The three modes have a color assigned each
    /// </summary>
    private static Color[] modeColors =
    {
        new Color(1,0,1),
        Color.blue,
        Color.cyan
    };

    bool toggleDirShow = false;

    /// <summary>
    /// This overrides the onInspectorGUI which modifies the inspector of the class bezierSpline
    /// </summary>
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        path = target as pathSystem;
        /*
        if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount)
        {
            DrawSelectedPointInspector();
        }
        /*
        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(path, "Add Curve");
            path.AddBezierSpline();
            EditorUtility.SetDirty(path);
        }*/

        toggleDirShow = GUILayout.Toggle(toggleDirShow, "Toggle Directions");

        if (GUILayout.Button("Reset Curve"))
        {
            path.Reset();
            EditorUtility.SetDirty(path);
        }
    }

    /// <summary>
    /// This is a built in UNITY class that handles on scene GUI visuals
    /// </summary>
    private void OnSceneGUI()
    {
        path = target as pathSystem;
        for (int x = 0; x < path.bezierSplines.Length; x++)
        {
            handleTransform = path.bezierSplines[x].transform;
            handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

            Vector3 p0 = ShowPoint(0, x);
            for (int i = 1; i < path.bezierSplines[x].ControlPointCount; i += 3)
            {
                Vector3 p1 = ShowPoint(i, x);
                Vector3 p2 = ShowPoint(i + 1, x);
                Vector3 p3 = ShowPoint(i + 2, x);

                Handles.color = Color.gray;
                Handles.DrawLine(p0, p1);
                Handles.DrawLine(p2, p3);

                Handles.DrawBezier(p0, p3, p1, p2, Color.green, null, 2f);
                p0 = p3;
            }
        }
        if(toggleDirShow)
            ShowDirections();
    }

    /// <summary>
    /// This draws a point in the inspector view when it is selected
    /// </summary>
    private void DrawSelectedPointInspector()
    {
        for (int i = 0; i < path.bezierSplines.Length; i++)
        {
            GUILayout.Label("Selected Point");
            EditorGUI.BeginChangeCheck();
            Vector3 point = EditorGUILayout.Vector3Field("Position", path.bezierSplines[i].GetControlPoint(selectedIndex));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(path.bezierSplines[i], "Move Point");
                EditorUtility.SetDirty(path.bezierSplines[i]);
                path.bezierSplines[i].SetControlPoint(selectedIndex, point);
            }

            EditorGUI.BeginChangeCheck();
            BezierControlPointMode mode = (BezierControlPointMode)
                EditorGUILayout.EnumPopup("Mode", path.bezierSplines[i].GetControlPointMode(selectedIndex));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(path.bezierSplines[i], "Change Point Mode");
                path.bezierSplines[i].SetControlPointMode(selectedIndex, mode);
                EditorUtility.SetDirty(path.bezierSplines[i]);
            }
        }
    }

    /// <summary>
    /// Show point shows a button point in the editor
    /// </summary>
    /// <param name="index">Index of the point</param>
    /// <returns>Returns the vec3 of the point</returns>
    private Vector3 ShowPoint(int index, int bezierCurveIndex)
    {
        Vector3 point = handleTransform.TransformPoint(path.bezierSplines[bezierCurveIndex].GetControlPoint(index));
        float size = HandleUtility.GetHandleSize(point);
        Handles.color = modeColors[(int)path.bezierSplines[bezierCurveIndex].GetControlPointMode(index)];
        if (Handles.Button(point, handleRotation, size * HandleSize, size * PickSize, Handles.DotCap))
        {
            selectedIndex = index;
            Repaint();
        }

        if (selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(path.bezierSplines[bezierCurveIndex], "Move Point");
                EditorUtility.SetDirty(path.bezierSplines[bezierCurveIndex]);
                path.bezierSplines[bezierCurveIndex].SetControlPoint(index, handleTransform.InverseTransformPoint(new Vector3(point.x, 0, point.z)));
            }
        }

        return point;
    }

    /// <summary>
    /// Show direction of a point on the spline
    /// </summary>
    private void ShowDirections()
    {
        for (int x = 0; x < path.bezierSplines.Length; x++)
        {
            Handles.color = Color.red;
            Vector3 point = path.bezierSplines[x].GetPoint(0f);
            Handles.DrawLine(point, point + (path.bezierSplines[x].GetDirection(0f) * DirectionScale));
            int steps = stepsPerCurve * path.bezierSplines[x].CurveCount;
            for (int i = 1; i <= steps; i++)
            {
                point = path.bezierSplines[x].GetPoint(i / (float)steps);
                Handles.DrawLine(point, point + (path.bezierSplines[x].GetDirection(i / (float)steps) * DirectionScale));
            }
        }
    }
}