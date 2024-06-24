using Core.Services;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class TrajectoryVisualizerInstaller: MonoInstaller
    {
        [SerializeField]
        private TrajectoryVisualizer trajectoryVisualizer;
        
        public override void InstallBindings()
        {
            Bind();
        }

        private void Bind()
        {
            Container.Bind<TrajectoryVisualizer>().FromInstance(trajectoryVisualizer);
        }
    }
}