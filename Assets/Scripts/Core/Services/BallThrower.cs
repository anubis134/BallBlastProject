using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Services
{
    public class BallThrower: MonoBehaviour
    {
        public event Action OnThrowProcessWasStarted;
        public event Action OnThrowProcessWasCanceled;
        
        [SerializeField] private float throwDelay;
        [SerializeField] private float speedCoefficient = 1f;
        
        private TrajectoryVisualizer _trajectoryVisualizer;
        private BallsInitializer _ballsInitializer;
        
        [Inject]
        private void Construct(BallsInitializer ballsInitializer, TrajectoryVisualizer trajectoryVisualizer)
        {
            _trajectoryVisualizer = trajectoryVisualizer;
            _ballsInitializer = ballsInitializer;
            
            _trajectoryVisualizer.OnTrajectorySubmitted += OnTrajectorySubmitted;
        }

        private void OnDestroy()
        {
            _trajectoryVisualizer.OnTrajectorySubmitted -= OnTrajectorySubmitted;
        }

        private void OnTrajectorySubmitted(Vector3 direction)
        {
            StartCoroutine(ThrowRoutine(throwDelay, direction * speedCoefficient));
        }

        private IEnumerator ThrowRoutine(float delay, Vector3 direction)
        {
            OnThrowProcessWasStarted?.Invoke();

            BallBehaviour[] ballBehaviours = _ballsInitializer.Balls.ToArray();

            foreach (var ball in ballBehaviours)
            {
                ball.SetVelocity(direction);

                yield return new WaitForSecondsRealtime(delay);
            }
            
            OnThrowProcessWasCanceled?.Invoke();
        }
    }
}