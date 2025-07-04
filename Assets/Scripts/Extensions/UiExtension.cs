using UI.Base;
using UnityEngine;
using Zenject;

namespace Extensions
{
    public static class UiBindExtensions
    {
        public static void BindUIView<TPresenter, TView>(this DiContainer container, Object prefab, Transform parent)
            where TPresenter : IUIPresenter, IInitializable
            where TView : IUIView
        {
            GameObject go = container.InstantiatePrefab(prefab, parent);

            var viewComponent = go.GetComponent<TView>();
            
            container.BindInterfacesAndSelfTo<TView>().FromInstance(viewComponent).AsSingle();
            container.BindInterfacesAndSelfTo<TPresenter>().AsSingle().NonLazy();
        }
    }
}