using API;
using Common;
using Factory;
using UI;
using UnityEngine;
using Zenject;

namespace Installer
{ 
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private FruitsSpawner _spawner;
        [SerializeField] private FruitsFactory _fruitsFactory;
        [SerializeField] private UITaskView _ui;
        [SerializeField] private LevelManager _levelManager;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameInput>().AsCached().NonLazy();
            Container.Bind<IFruitsFactory>().FromInstance(_fruitsFactory).AsCached();
            Container.Bind<IFruitsConfigsRepository>().FromInstance(_fruitsFactory).AsCached();

            Container.Bind<Camera>().FromInstance(_camera).AsCached();
            Container.Bind<FruitsSpawner>().FromInstance(_spawner).AsCached();
            Container.Bind<UITaskView>().FromInstance(_ui).AsCached();

            Container.Bind<LevelManager>().FromInstance(_levelManager).AsCached();
        }
    }
}