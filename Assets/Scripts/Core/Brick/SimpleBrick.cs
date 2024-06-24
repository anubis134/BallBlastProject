using System;
using DG.Tweening;
using UnityEngine;

public class SimpleBrick : BrickBehaviour
{
    [SerializeField]
    private ParticleSystem destructEffect;

    public override void Destruct()
    {
        ParticleSystem effect = Instantiate(destructEffect);

        effect.transform.position = this.transform.position;
        
        effect.Play();
        
        Destroy(gameObject);
    }
}
