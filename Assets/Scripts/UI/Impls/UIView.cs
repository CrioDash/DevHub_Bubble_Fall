using UI.Base;
using UnityEngine.EventSystems;

namespace UI.Impls
{
    public abstract class UIView: UIBehaviour, IUIView
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}