using System;
using UnityEngine;
using UnityEditor;
namespace PlanImploder
{
    [CustomEditor(typeof(Imploder))]
    public class ImploderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Imploder imploder = (Imploder)target;
            if (GUILayout.Button("Build Object"))
            {
                    imploder.CreateZones();
            }
        }
    }
}