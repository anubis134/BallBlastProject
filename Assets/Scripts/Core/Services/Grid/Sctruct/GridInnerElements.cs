
using System;

namespace Core.Services.Grid.Sctruct
{
    [Serializable]
    public struct GridInnerElements
    {
        public int XIndex;
        public int YIndex;
        public BrickBehaviour Prefab;
        public int Strength;
    }
}
