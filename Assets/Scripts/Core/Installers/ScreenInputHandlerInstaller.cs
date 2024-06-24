using UnityEngine;
using Zenject;

public class ScreenInputHandlerInstaller : MonoInstaller
{
    [SerializeField]
    private ScreenInputHandler screenInputHandler;
    
    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        Container.Bind<ScreenInputHandler>().FromInstance(screenInputHandler).AsSingle();
    }
}
