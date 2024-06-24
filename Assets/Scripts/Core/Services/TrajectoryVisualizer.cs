using System;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace Core.Services
{
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryVisualizer : MonoBehaviour
    {
        public event Action<Vector3> OnTrajectorySubmitted;
        
        [SerializeField]
        private float reflectedLineLength = 1f;
        
        private Transform _firstBall;
        private BallsInitializer _ballsInitializer;
        private ScreenInputHandler _screenInputHandler;
        private BallThrower _ballThrower;
        private LineRenderer _lineRenderer;

        private bool _isPressed;
        private bool _isDeny;
        private Vector3 _submittedDirection;
        
        [Inject]
        private void Construct(BallsInitializer ballsInitializer, ScreenInputHandler screenInputHandler, BallThrower ballThrower)
        {
            _ballsInitializer = ballsInitializer;

            _lineRenderer = GetComponent<LineRenderer>();

            _firstBall = _ballsInitializer.Balls[0].transform;

            _screenInputHandler = screenInputHandler;
            _ballThrower = ballThrower;
            
            _screenInputHandler.OnPointerDownEvent += HandleOnPointerDownEvent; 
            _screenInputHandler.OnPointerUpEvent += HandleOnPointerUpEvent;
            _screenInputHandler.OnPointerMoveEvent += HandleOnPointerMoveEvent;
            _ballThrower.OnThrowProcessWasStarted += HandleOnThrowProcessWasStarted;
        }

        private void HandleOnThrowProcessWasStarted()
        {
            _isDeny = true;
        }

        private void HandleOnPointerMoveEvent(Vector2 value)
        {
            if (_isPressed is false) return;

            Visualize(value);
        }

        private void HandleOnPointerUpEvent(Vector2 value)
        {
            if(_isDeny) return;
            
            _isPressed = false;
            
            Vector3[] newPositions =
            {
                Vector3.zero, 
                Vector3.zero,
                Vector3.zero, 
            };
            
            _lineRenderer.SetPositions(newPositions);
            
            OnTrajectorySubmitted?.Invoke(_submittedDirection);
        }

        private void HandleOnPointerDownEvent(Vector2 value)
        {
            _isPressed = true;
            
            Visualize(value);
        }

        private void Visualize(Vector3 screenPosition)
        {
            if(_isDeny) return;
            
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            worldPosition.z = 0;

            Vector3 direction = worldPosition - _firstBall.position;

            _submittedDirection = direction.normalized;

            Vector3 hitPoint = default;

            if (Physics.Raycast(_firstBall.position, direction, out RaycastHit hit, Mathf.Infinity))
            {
               hitPoint = hit.point;
            }

            Vector3 reflectedDirection = Vector3.Reflect(direction, hit.normal);

            Vector3[] newPositions =
            {
                _firstBall.position,
                hitPoint,
                hitPoint + (reflectedDirection.normalized * reflectedLineLength)
            };

            _lineRenderer.SetPositions(newPositions);
        }
    }
}
