using System;
using Core.Ball.Interfaces;
using Core.Services;
using Core.Services.Interfaces;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public abstract class BallBehaviour : MonoBehaviour, IReflectable, IDestructible
{
    public event Action<BallBehaviour> OnBallDestructedEvent;  

    public Rigidbody Rigidbody => _rigidbody;
    public Collider Collider => _collider;
    public Vector3 Velocity => _velocity;

    [SerializeField]
    private float minVelocity;
    
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Vector3 _velocity;

    private BallsInitializer _ballsInitializer;

    protected virtual void Awake()
    {
        _ballsInitializer = this.FindOrException<BallsInitializer>();
        
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;

        _rigidbody.velocity = _velocity;
    }

    public void Reflect(Vector3 hitNormal)
    {
        Vector3 reflectedVector = Vector3.Reflect(_velocity, hitNormal);
        
        SetVelocity(reflectedVector);
    }
    
    public void Destruct()
    {
        OnBallDestructedEvent?.Invoke(this);
        
        _ballsInitializer.RemoveBall(this);
        
        Destroy(this.gameObject);
    }
}
