using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public enum HelpBoxType { None, Info, Warning, Error }
    public class HelpBoxAttribute : PropertyAttribute
    {
        public string text;
        public HelpBoxType type;

        public HelpBoxAttribute(string text, HelpBoxType type)
        {
            this.text = text;
            this.type = type;
        }
    }
}
