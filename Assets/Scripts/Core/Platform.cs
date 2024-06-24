using System;
using Core.Services;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform targetPointTransform;
    [SerializeField] private float speed;
    [SerializeField] private float riseTime;

    private ScreenInputHandler _screenInputHandler;
    private BallThrower _ballThrower;
    private BallsInitializer _ballsInitializer;
    private Rigidbody _rigidbody;

    private bool _isControl;
    private bool _isPressed;
    
    [Inject]
    private void Construct(ScreenInputHandler screenInputHandler, BallThrower ballThrower, BallsInitializer ballsInitializer)
    {
        _screenInputHandler = screenInputHandler;
        _ballThrower = ballThrower;
        _ballsInitializer = ballsInitializer;
        _rigidbody = GetComponent<Rigidbody>();
        
        _ballThrower.OnThrowProcessWasCanceled += HandleOnThrowProcessWasCanceled;
        _screenInputHandler.OnPointerDownEvent += HandleOnPointerDownEvent;
        _screenInputHandler.OnPointerUpEvent += HandleOnPointerUpEvent;
        _screenInputHandler.OnPointerMoveEvent += HandleOnPointerMoveEvent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IBoostable boostable))
        {
            boostable.ApplyBoost();
        }
    }

    private void HandleOnPointerDownEvent(Vector2 value)
    {
        _isPressed = true;
        
        SetPosition(value);
    }

    private void HandleOnPointerUpEvent(Vector2 value)
    {
        _isPressed = false;
    }

    private void HandleOnPointerMoveEvent(Vector2 value)
    {
        if(_isControl is false || _isPressed is false) return;

        SetPosition(value);
    }

    private void SetPosition(Vector2 value)
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(value);

        Vector3 direction = touchPosition - transform.position;

        _rigidbody.velocity = new Vector3(direction.x * this.speed, 0f,0f);
    }

    private void HandleOnThrowProcessWasCanceled()
    {
        transform.DOMove(targetPointTransform.position, riseTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            _isControl = true;
        });
    }
}
