using System.Collections.Generic;
using Core.Levels.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelLibrary", menuName = "ScriptableObjects/Create Levels Library", order = 1)]
public class LevelsLibrary : ScriptableObject
{
    public List<Level> Levels;
}
