using Core;
using Core.Strategies;
using Core.Strategies.Impls;
using Gameplay;
using Gameplay.Launcher;
using Gameplay.Launcher.Impls;
using UI.Defeat;
using UI.Score;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Ball ballPrefab;
        [SerializeField] private Transform ballsContainer;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            BindLauncher();
            BindPoolsAndFactories();
            BindColorAndAiming();
            BindGridAndChain();
            BindStrategiesAndChainManager();
            BindModels();
        }

        private void BindLauncher()
        {
            Container.Bind<LauncherController>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container.Bind<Spawner>()
                .FromComponentInHierarchy()
                .AsSingle();
        }

        private void BindPoolsAndFactories()
        {
            Container.BindMemoryPool<Ball, Ball.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(ballPrefab)
                .UnderTransform(ballsContainer);

            Container.BindInterfacesAndSelfTo<PooledBallFactory>()
                .AsSingle();
        }

        private void BindColorAndAiming()
        {
            Container.BindInterfacesAndSelfTo<ColorSwitcher>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<InputService>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<LauncherPlaneAimer>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ReflectedLineVisualizer>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PooledProjectileLauncher>()
                .AsSingle();
        }

        private void BindGridAndChain()
        {
            Container.Bind<Transform>()
                .WithId("Balls")
                .FromInstance(ballsContainer)
                .AsSingle();
            
            Container.Bind<GridManager>()
                .AsSingle()
                .WithArguments(ballsContainer);
            
        }

        private void BindStrategiesAndChainManager()
        {
            Container.BindInterfacesAndSelfTo<MatchByColorStrategy>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<FloatingFallStrategy>()
                .AsSingle();
            
            Container.Bind<ChainManager>()
                .AsSingle();
        }

        private void BindModels()
        {
            Container.Bind<ScoreModel>()
                .AsSingle();
            Container.Bind<DefeatModel>()
                .AsSingle();
        }
        
    }
}
