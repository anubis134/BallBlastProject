using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Services
{
    public class BallsInitializer : MonoBehaviour
    {
        public event Action<List<BallBehaviour>> OnBallsInitialized;
        
        public int BallsCount => _ballsCount;
        public List<BallBehaviour> Balls => _balls;

        [SerializeField] private BallBehaviour ballPrefab;
        [SerializeField] private Transform startPoint;
        [SerializeField] private int _ballsCount = 50;
        
        [SerializeField]
        private List<BallBehaviour> _balls = new ();

        [Inject]
        private void Construct()
        {
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < _ballsCount; i++)
            {
                BallBehaviour ball = Instantiate(ballPrefab);

                ball.transform.position = startPoint.position;

                _balls.Add(ball);
            }

            OnBallsInitialized?.Invoke(_balls);
        }


        public void SetBallCount(int count)
        {
            _ballsCount = count;
        }

        public void SplitBalls()
        {
            List<BallBehaviour> balls = new List<BallBehaviour>();

            foreach (var ball in _balls)
            {
                BallBehaviour firstBall = Instantiate(ballPrefab);

                firstBall.transform.position = ball.transform.position;
                
                firstBall.SetVelocity(ball.Velocity + Vector3.right + Vector3.up);
                
                BallBehaviour secondBall = Instantiate(ballPrefab);

                secondBall.transform.position = ball.transform.position;
                
                secondBall.SetVelocity(ball.Velocity + Vector3.left + Vector3.up);

                balls.Add(firstBall);
                balls.Add(secondBall);
            }
            
            _balls.AddRange(balls);
        }

        public void RemoveBall(BallBehaviour ballBehaviour)
        {
            _balls.Remove(ballBehaviour);
        }
    }
}
     