
using Core.Brick.Interfaces;
using Core.Services.Interfaces;
using UnityEngine;

namespace Core.Brick
{
    public class TNTBrick : BrickBehaviour
    {
        [SerializeField] private ParticleSystem explosionEffect;
        [SerializeField] private float explosionRadius;

        private bool _isDestructed;

        public override void Destruct()
        {
            if(_isDestructed) return;

            _isDestructed = true;
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            
            ParticleSystem effect = Instantiate(explosionEffect);

            effect.transform.position = transform.position;

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IDestructible destructible))
                {
                    destructible.Destruct();
                }
            }

            Destroy(gameObject);
        }
    }
}
