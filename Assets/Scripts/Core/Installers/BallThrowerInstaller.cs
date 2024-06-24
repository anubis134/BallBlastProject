using Core.Services;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class BallThrowerInstaller: MonoInstaller
    {
        [SerializeField]
        private BallThrower ballThrower;
        
        public override void InstallBindings()
        {
            Bind();
        }

        private void Bind()
        {
            Container.Bind<BallThrower>().FromInstance(ballThrower);
        }
    }
}