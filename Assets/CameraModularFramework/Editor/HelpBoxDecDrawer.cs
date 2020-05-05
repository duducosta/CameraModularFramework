using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CameraModularFramework
{
    [CustomPropertyDrawer(typeof(HelpBoxAttribute))]
    public class ModuleDescriptionDecDrawer : DecoratorDrawer
    {
        public override float GetHeight()
        {
            var helpBoxAttribute = attribute as HelpBoxAttribute;
            if (helpBoxAttribute == null) return base.GetHeight();

            var helpBoxStyle = (GUI.skin != null) ? GUI.skin.GetStyle("helpbox") : null;
            if (helpBoxStyle == null) return base.GetHeight();
            return Mathf.Max(helpBoxStyle.CalcHeight(new GUIContent(helpBoxAttribute.text), EditorGUIUtility.currentViewWidth) + 4, 40f);
        }

        public override void OnGUI(Rect position)
        {
            var helpBoxAttribute = attribute as HelpBoxAttribute;
            if (helpBoxAttribute == null) return;
            EditorGUI.HelpBox(position, helpBoxAttribute.text, GetType(helpBoxAttribute.type));
        }

        private MessageType GetType(HelpBoxType helpBoxType)
        {
            switch (helpBoxType)
            {
                default:
                case HelpBoxType.None: return MessageType.None;
                case HelpBoxType.Info: return MessageType.Info;
                case HelpBoxType.Warning: return MessageType.Warning;
                case HelpBoxType.Error: return MessageType.Error;
            }
        }
    }
}