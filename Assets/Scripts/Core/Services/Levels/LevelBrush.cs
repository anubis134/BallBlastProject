using System.Collections.Generic;
using Core.Services.Grid;
using Core.Services.Levels.Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Services.Levels
{
    [ExecuteInEditMode]
    public class LevelBrush : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> Prefabs;
        [HideInInspector] public List<GUIContent> Contents;

        [SerializeField] private LevelBrushMode levelBrushMode;
        [SerializeField] private int strength = 50;
        
        private Ray _mouseGUIPosition;
        private GameObject _selectedPrefab;

        public void SetMouseGuiPosition(ref Ray mousePosition)
        {
            _mouseGUIPosition = mousePosition;

            TryDetectGrid();
        }
        
        public void SelectPrefab(GameObject prefab)
        {
            _selectedPrefab = prefab;
        }

        private void TryDetectGrid()
        {
            if(EditorApplication.isPlaying) return;

            if (Physics.Raycast(_mouseGUIPosition,out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out GridElement gridElement))
                {
                    HandlePaint(ref gridElement);
                }
            }
        }

        private void HandlePaint(ref GridElement element)
        {
            switch (levelBrushMode)
            {
                case LevelBrushMode.Paint:
                    TryPaint(ref element);
                    break;
                
                case LevelBrushMode.Clear:
                    TryClear(ref element);
                    break;
            }
        }

        private void TryPaint(ref GridElement element)
        {
            if (element.transform.childCount == 0)
            {
                Paint(ref element);
            }
            else
            {
                TryClear(ref element);
                Paint(ref element);
            }
        }

        private void Paint(ref GridElement element)
        {
            Transform prefabTransform = Instantiate(_selectedPrefab).transform;

            BrickBehaviour brickBehaviour = prefabTransform.GetComponent<BrickBehaviour>();
            
            brickBehaviour.strength = strength;
            brickBehaviour.UpdateText();
            
            prefabTransform.position = element.transform.position;
            prefabTransform.parent = element.transform;
            prefabTransform.name = prefabTransform.name.Split("(Clone)")[0];
        }

        private void TryClear(ref GridElement element)
        {
            if (element.transform.childCount == 0) return;

            Transform child = element.transform.GetChild(0);
            
            DestroyImmediate(child.gameObject,false);
        }
    }
}
