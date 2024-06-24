using System.Collections.Generic;
using Core.Services.Grid.Sctruct;
using UnityEngine;

namespace Core.Levels.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Create Level SO", order = 1)]
    public class Level : ScriptableObject
    {
        public List<GridInnerElements> LevelSettings;
    }
}
