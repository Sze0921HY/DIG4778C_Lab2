using System.Linq;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class Testing : MonoBehaviour
{
    [SerializeField] private float size;
}

// Custom editor for Testing
#if UNITY_EDITOR
[CustomEditor(typeof(Testing)), CanEditMultipleObjects]
public class TestingEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var size = serializedObject.FindProperty("size");

        if (size != null)
        {
            EditorGUILayout.PropertyField(size);
        }

        if (size.floatValue < 0)  // will send warning mesasge if the value is less than 0
        {
            EditorGUILayout.HelpBox("You know there is no negative size...right?", MessageType.Warning);
        }
        else if (size.floatValue == 0) // will warn you that can't see item if is equal to 0
        {
            EditorGUILayout.HelpBox("Object so small that won't be seen", MessageType.Warning);
        }

        serializedObject.ApplyModifiedProperties();

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all cubes/spheres"))   
            {
                var allObjectsBehaviour = GameObject.FindObjectsOfType<Testing>(); // find all objects that contain the "Testing" Script  
                var allGameObjects = allObjectsBehaviour.Select(shapes => shapes.gameObject).ToArray(); //  assing every object found to the "allGameObjects" variable 
                Selection.objects = allGameObjects;  // select all game objects 
            }

        if (GUILayout.Button("Clear selection")) 
            {
                Selection.objects = new Object[] { (target as Testing).gameObject }; // clear selction 
            }
        }

        var allObjects = GameObject.FindObjectsOfType<Testing>(true);
        bool anyDisabled = allObjects.Any(obj => !obj.gameObject.activeSelf); // set bool according to game object's statuts of enable or disabled 

        Color cachedColor = GUI.backgroundColor;
        if (anyDisabled) // change colors if disalbe or enabled
        {
            GUI.backgroundColor = Color.red;
        } else 
        {
            GUI.backgroundColor = Color.green; 
        }


        if (GUILayout.Button("Disable/Enable all Object", GUILayout.Height(50)))
        {
            foreach (var obj in allObjects) // disable or enable every game object inside "allObjects" variable
            {
                obj.gameObject.SetActive(!obj.gameObject.activeSelf);
                GUI.backgroundColor = Color.red;
            }
        }
    }
}
#endif
