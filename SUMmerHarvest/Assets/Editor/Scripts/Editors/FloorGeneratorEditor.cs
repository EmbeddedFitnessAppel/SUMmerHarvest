using Assets.Scripts.Game.GameObjects;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Scripts.Editors
{
    [CustomEditor(typeof(FloorGenerator))]
    public class FloorGeneratorEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            // Setup properties.
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            var script = (FloorGenerator)target;
            if (GUILayout.Button("Generate"))
            {
                script.Generate();
            }

            if (GUILayout.Button("Clear"))
            {
                script.Clear();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}