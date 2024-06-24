using System;
using UnityEngine;

namespace Core.Services.Grid
{
    [Serializable]
    public class GridElement: MonoBehaviour
    {
        public int X;
        public int Y;

        private BoxCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();

            _collider.enabled = false;
        }
    }
}