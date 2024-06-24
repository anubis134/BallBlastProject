using Core.Services;
using DG.Tweening;
using UnityEngine;
using Utils;
using Zenject;

namespace Core.Brick
{
    public class SplitBoosterBrick : BrickBehaviour, IBoostable
    {
        [SerializeField] private Material transparentMaterial;

        [SerializeField] private float moveDownDuration;

        private Collider _collider;

        private BallsInitializer _ballsInitializer;

        protected override void Awake()
        {
            _collider = GetComponent<Collider>();

            _ballsInitializer = this.FindOrException<BallsInitializer>();

            base.Awake();
        }

        public override void Destruct()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            Material[] materials = new[] { transparentMaterial };

            meshRenderer.materials = materials;

            _collider.isTrigger = true;

            transform.DOMoveY(-100f, moveDownDuration);
        }

        public void ApplyBoost()
        {
            _ballsInitializer.SplitBalls();

            Destroy(this.gameObject);
        }
    }
}
