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

        EditorGUILayout.BeginHorizontal();

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

        EditorGUILayout.EndHorizontal();
    }
}
