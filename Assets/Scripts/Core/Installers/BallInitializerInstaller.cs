using Core.Services;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class BallsInitializerInstaller: MonoInstaller
    {
        [SerializeField]
        private BallsInitializer ballsInitializer;
        
        public override void InstallBindings()
        {
            Bind();
        }

        private void Bind()
        {
            Container.Bind<BallsInitializer>().FromInstance(ballsInitializer);
        }
    }
}