using UnityEditor;
using UnityEngine;

namespace BBG.CustomLayouts
{
    [CustomEditor(typeof(CustomLayout), true)]
    public class CustomLayoutEditor : Editor
    {
        private CustomLayout layout;

        private void OnEnable()
        {
            layout = target as CustomLayout;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("ADJUST LAYOUT"))
                layout.AdjustChildren();
            base.OnInspectorGUI();
        }
    }
}