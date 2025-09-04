using System.Linq;
using UnityEngine;
using UnityEditor;

public class Testing : MonoBehaviour
{
    // Just a placeholder MonoBehaviour
}

// Custom editor for Testing
[CustomEditor(typeof(Testing)), CanEditMultipleObjects]
public class TestingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all cubes/spheres"))
            {
                var allObjectsBehaviour = GameObject.FindObjectsOfType<Testing>(); //wtf is this
                var allGameObjects = allObjectsBehaviour.Select(enemy => enemy.gameObject).ToArray();
                Selection.objects = allGameObjects;
            }

        if (GUILayout.Button("Clear selection"))
            {
                Selection.objects = new Object[] { (target as Testing).gameObject };
            }
        }

        var allObjects = GameObject.FindObjectsOfType<Testing>(true);
        bool anyDisabled = allObjects.Any(obj => !obj.gameObject.activeSelf);

        Color cachedColor = GUI.backgroundColor;
        if (anyDisabled)
        {
            GUI.backgroundColor = Color.red;
        } else 
        {
            GUI.backgroundColor = Color.green; 
        }


        if (GUILayout.Button("Disable/Enable all Object", GUILayout.Height(50)))
        {
            foreach (var obj in allObjects)
            {
                obj.gameObject.SetActive(!obj.gameObject.activeSelf);
                GUI.backgroundColor = Color.red;
            }
        }
    }
}
