// Copyright 2021, WellFlow Games. All Rights Reserved.

#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.WellFlow.Scripts
{
    [InitializeOnLoad]
    public static class HierarchyWindowGroupHeader
    {
        public static Dictionary<string, string> colors = new()
        {
            {"o*", "#1e90ff"},
            {"s*", "#ed9121"},
            {"c*", "#e34234"},
            {"u*", "#1caac9"},
            {"r*", "#007ba7"},
            {"i*", "#009b76"},
            {"p*", "#4682b4"},
            {"t*", "#4d0099"}
        };

        static HierarchyWindowGroupHeader()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        static void AddNewColor()
        {
            
        }

        static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (gameObject != null)
            {
                Color color = default;
                Color backColor = default;
                string currentKey = default;

                foreach (var key in colors.Keys)
                {
                    if (!gameObject.name.StartsWith(key, System.StringComparison.Ordinal)) continue;

                    if (!gameObject.activeSelf) ColorUtility.TryParseHtmlString("#002137", out color);

                    else ColorUtility.TryParseHtmlString(colors[key], out color);

                    currentKey = key;
                }

                if (color != default)
                {
                    ColorUtility.TryParseHtmlString("#121F26", out backColor);
                    
                    Rect rect = new Rect(selectionRect.x + 3f,
                        selectionRect.y + 2f, selectionRect.width - 6f,
                        selectionRect.height - 4f);

                    EditorGUI.DrawRect(selectionRect, backColor);
                    EditorGUI.DrawRect(rect, color);
                    EditorGUI.DropShadowLabel(rect, gameObject.name.Replace(currentKey, ""));
                }
            }
        }
    }
}

#endif