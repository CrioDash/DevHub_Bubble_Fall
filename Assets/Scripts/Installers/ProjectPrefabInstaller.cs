using Db;
using Db.Impls;
using Extensions;
using UI.Defeat;
using UI.Score;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/ProjectPrefabInstaller", fileName = "ProjectPrefabInstaller")]
    public class ProjectPrefabInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BallSettingsDatabase ballSettingsDatabase;
        [SerializeField] private GameObject scorePrefab;
        [SerializeField] private GameObject defeatPrefab;

        [SerializeField] private Canvas canvas;

        public override void InstallBindings()
        {
            BindDatabases();
            BindUI();
        }

        private void BindDatabases()
        {
            Container.Bind<IBallSettingsDatabase>().FromInstance(ballSettingsDatabase).AsSingle();
        }

        private void BindUI()
        {
            GameObject canvas = Container.InstantiatePrefab(this.canvas);
            
            Container.BindUIView<ScorePresenter, ScoreView>(
                scorePrefab, canvas.transform);
            Container.BindUIView<DefeatPresenter, DefeatView>(
                defeatPrefab, canvas.transform);
        }
    }
}