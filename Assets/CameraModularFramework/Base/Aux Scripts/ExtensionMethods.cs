using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public static class ExtensionMethods
    {
        public static bool EmptyOrAllNull<T>(this T[] array)
        {
            int nullQty = 0;
            if (array == null)
            {
                return true;
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    nullQty++;
                    Debug.LogWarning("Empty element. Please check");
                }
            }
            if (nullQty == array.Length) { return true; };
            return false;
        }
    }
}
