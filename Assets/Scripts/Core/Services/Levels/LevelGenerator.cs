using System.Collections.Generic;
using Core.Levels.ScriptableObjects;
using Core.Services.Grid;
using UnityEngine;
using Zenject;

namespace Core.Levels
{
    public class LevelGenerator: MonoBehaviour
    {
        [SerializeField]
        private LevelsLibrary levelsLibrary;
        
        private GridGenerator _gridGenerator;
        
        [Inject]
        private void Construct(GridGenerator gridGenerator)
        {
            _gridGenerator = gridGenerator;
            
            _gridGenerator.OnGridWasGenerated += HandleOnGridWasGenerated;
        }

        private void OnDestroy()
        {
            _gridGenerator.OnGridWasGenerated -= HandleOnGridWasGenerated;
        }

        private void HandleOnGridWasGenerated(List<GridElement> gridElements)
        {
            Generate(levelsLibrary.Levels[0], gridElements);
        }

        private void Generate(Level level, List<GridElement> gridElements)
        {
            foreach (var brickElement in level.LevelSettings)
            {
                foreach (var gridElement in gridElements)
                {
                    if (brickElement.XIndex == gridElement.X && brickElement.YIndex == gridElement.Y)
                    {
                        BrickBehaviour brick = Instantiate(brickElement.Prefab);
                        brick.transform.position = gridElement.transform.position;
                        brick.strength = brickElement.Strength;
                        brick.UpdateText();
                        break;
                    }
                }
            }
        }
    }
}