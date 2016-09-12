using UnityEditor;
using UnityEngine;

public class DesignerEditorExtensions : Editor {
    [MenuItem("Design Utilities/Start Debug Camera")]
    public static void GetDebugCamera() {
        GameObject gObj = new GameObject();
        Camera debugCamera = gObj.AddComponent<Camera>();
        //gObj.AddComponent<DebugCameraController>();

        gObj.transform.eulerAngles = Camera.main.transform.eulerAngles;

        debugCamera.depth = Camera.main.depth + 1;
    }

    [MenuItem("Design Utilities/Stop Debug Camera")]
    public static void StopDebugCamera() {
        var debugCamera = Camera.current;
        if (debugCamera.tag == "MainCamera")
            return;
        else
            Destroy(debugCamera.gameObject);
    }
}
