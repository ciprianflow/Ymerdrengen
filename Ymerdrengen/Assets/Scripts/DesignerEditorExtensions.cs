using UnityEditor;
using UnityEngine;

/// <summary>
/// Extensions to Unity to ease the workload on designers.
/// </summary>
public class DesignerEditorExtensions : Editor
{
    /// <summary>
    /// Creates a new freeform camera and switches focus to that camera.
    /// </summary>
    [MenuItem("Design Utilities/Start Debug Camera")]
    public static void GetDebugCamera()
    {
        GameObject gameObj = new GameObject();
        Camera debugCamera = gameObj.AddComponent<Camera>();
        // gObj.AddComponent<DebugCameraController>();

        gameObj.transform.eulerAngles = Camera.main.transform.eulerAngles;

        debugCamera.depth = Camera.main.depth + 1;
    }

    /// <summary>
    /// Destroys the freeform camera and returns to the main camera.
    /// </summary>
    [MenuItem("Design Utilities/Stop Debug Camera")]
    public static void StopDebugCamera()
    {
        var debugCamera = Camera.current;
        if (debugCamera.tag == "MainCamera")
        {
            return;
        }
        else
        {
            GameObject.Destroy(debugCamera.gameObject);
        }
    }
}