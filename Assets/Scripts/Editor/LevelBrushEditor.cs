using System.Collections.Generic;
using Core.Services.Levels;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelBrush))]
    public class LevelBrushEditor : UnityEditor.Editor
    {
       static int selected = 0;
        
        private void OnSceneGUI()
        {
            if (Event.current.type == EventType.MouseDown)
            {
                LevelBrush levelBrush = target as LevelBrush;
        
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                levelBrush.SetMouseGuiPosition(ref worldRay);

                getPrefabs(ref levelBrush);

                SceneView.RepaintAll();
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelBrush levelBrush = target as LevelBrush;
            
            levelBrush.Contents.Clear();

            for (int i = 0; i < levelBrush.Prefabs.Count; i++)
            {
                levelBrush.Contents.Add(new GUIContent());
                levelBrush.Contents[i].text = levelBrush.Prefabs[i].name;
            }
            
            selected = GUILayout.Toolbar(selected, levelBrush.Contents.ToArray());

            levelBrush.SelectPrefab(levelBrush.Prefabs[selected]);
        }

        private void getPrefabs(ref LevelBrush levelBrush)
        {
            string[] search_results = System.IO.Directory.GetFiles("Assets/Prefabs/Bricks/", "*.prefab",
                System.IO.SearchOption.AllDirectories);
            
            levelBrush.Prefabs.Clear();
            
            for (int i = 0; i < search_results.Length; i++)
            {
                Object prefab = null;
                prefab = AssetDatabase.LoadMainAssetAtPath(search_results[i]);
                levelBrush.Prefabs.Add(prefab as GameObject);
            }
        }
    }
}
