using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
    ©2020 Caleb Langley

    You may not share, sell or redistribute this code, modified or not. You may make changes for internal use only
    in your Unity based projects.
    
    For any issues please contact gamesbycaleb@gmail.com
*/
public class CopyRigEditor : EditorWindow
{
    public List<RigObject> rigObjects = new List<RigObject>();
    private GameObject rootObject;
    private GameObject pastedRootObject;

    [MenuItem("Window/Copy Rig")]
    public static void ShowWindow()
    {
        GetWindow<CopyRigEditor>("Copy Rig");
    }

    void OnSelectionChange()
    {
        Repaint();
    }

    void OnGUI()
    {
        if (Selection.activeGameObject != null)
        {
            if(Selection.activeGameObject.transform.parent != null)
                GUILayout.Label("Currently Selected: " + Selection.activeGameObject.name + " ("+ Selection.activeGameObject.transform.parent.gameObject.name+")");
            else
                GUILayout.Label("Currently Selected: " + Selection.activeGameObject.name);


            if (GUILayout.Button("Copy Rig"))
            {
                rootObject = Selection.activeGameObject;

                foreach (Transform child in rootObject.GetComponentsInChildren<Transform>())
                {
                    rigObjects.Add(new RigObject(child.name, child.transform.localPosition, child.transform.localRotation, child.transform.localScale));
                }
            }

            if (GUILayout.Button("Paste Rig"))
            {
                pastedRootObject = Selection.activeGameObject;

                foreach (Transform child in pastedRootObject.GetComponentsInChildren<Transform>())
                {
                    for (int i = 0; i < rigObjects.Count; i++)
                    {
                        if (child.name == rigObjects[i].name)
                        {
                            child.localPosition = rigObjects[i].position;
                            child.localRotation = rigObjects[i].rotation;
                            child.localScale = rigObjects[i].scale;
                        }
                    }
                }
            }

            if (GUILayout.Button("Clear"))
            {
                rigObjects.Clear();
                rootObject = null;
            }

            if (rootObject != null)
            {
                if(rootObject.transform.parent != null)
                    GUILayout.Label("Rig Copied: " + rootObject.name + " (" + rootObject.transform.parent.gameObject.name + ")");
                else
                    GUILayout.Label("Rig Copied: " + rootObject.name);
            }
            else
            {
                GUILayout.Label("Rig Copied: None");
            }
        }
        else
        {
            GUILayout.Label("No object currently selected", EditorStyles.boldLabel);
        }
    }

    [System.Serializable]
    public class RigObject
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public RigObject() { }

        public RigObject(string objName, Vector3 objPos, Quaternion objRot, Vector3 objScale)
        {
            name = objName;
            position = objPos;
            rotation = objRot;
            scale = objScale;
        }
    }
}
