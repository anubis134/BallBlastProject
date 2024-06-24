using Core.Brick.Interfaces;
using UnityEngine;

public class SimpleBall : BallBehaviour
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage();
        }
    }
}
