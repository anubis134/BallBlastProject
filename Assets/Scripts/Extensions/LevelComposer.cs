  using System;
  using System.Collections.Generic;
  using com.cyborgAssets.inspectorButtonPro;
  using Core.Levels.ScriptableObjects;
  using Core.Services.Grid.Sctruct;
  using Core.Services.Levels;
  using UnityEditor;
  using UnityEngine;
using Utils;

  namespace Extensions
  {
     [Serializable, ExecuteInEditMode]
     public class LevelComposer : MonoBehaviour
     {
        [SerializeField] private string levelName;
        
        private GridGenerator _gridGenerator;
        private LevelBrush _levelBrush;
        private Camera _mainCamera;

        private void OnValidate()
        {
           _mainCamera = Camera.main;
           _levelBrush = this.FindOrException<LevelBrush>();
        }

        [ProButton]
        public void Initialization()
        {
           _gridGenerator ??= this.FindOrException<GridGenerator>();

           if (_gridGenerator.GridElements.Count == 0)
           {
              GenerateGrid();
           }
        }

        [ProButton]
        public void Clear()
        {
           _gridGenerator ??= this.FindOrException<GridGenerator>();

           _gridGenerator.ResetGrid();
        }
        
        [ProButton]
        public void SaveLevel()
        {
           Level level = ScriptableObject.CreateInstance<Level>();

           List<GridInnerElements> levelInnerElements = new List<GridInnerElements>();
           
           for (int i = 0; i < _gridGenerator.GridElements.Count; i++)
           {
              if (_gridGenerator.GridElements[i].transform.childCount > 0)
              {
                 GridInnerElements innerElements = default;
                 
                 BrickBehaviour brickBehaviour =
                    _gridGenerator.GridElements[i].transform.GetChild(0).GetComponent<BrickBehaviour>();

                 foreach (var prefab in _levelBrush.Prefabs)
                 {
                    if (prefab.GetComponent<BrickBehaviour>().name == brickBehaviour.name)
                    {
                       innerElements.Prefab = prefab.GetComponent<BrickBehaviour>();  
                    }
                 }
                 
                 innerElements.Strength = brickBehaviour.strength;
                 innerElements.XIndex = _gridGenerator.GridElements[i].X;
                 innerElements.YIndex = _gridGenerator.GridElements[i].Y;
                 
                 levelInnerElements.Add(innerElements);
              }
           }

           level.LevelSettings = levelInnerElements;

           AssetDatabase.CreateAsset(level, $"Assets/SO/{levelName}.asset");
           AssetDatabase.SaveAssets();

           EditorUtility.FocusProjectWindow();

           Selection.activeObject = level;
        }

        private void GenerateGrid()
        {
           _gridGenerator.Generate();
        }
     }
  }

