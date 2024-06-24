using UnityEngine;
using Zenject;

public class GridGeneratorInstaller : MonoInstaller
{
    [SerializeField] private GridGenerator gridGenerator;
    
    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        Container.Bind<GridGenerator>().FromInstance(gridGenerator).AsSingle();
    }
}
