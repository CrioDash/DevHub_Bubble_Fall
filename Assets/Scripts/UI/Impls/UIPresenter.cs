using UI.Base;
using Zenject;

namespace UI.Impls
{
    public abstract class UIPresenter<T, Tm>: IUIPresenter where T: IUIView where Tm: IUIModel
    {
        [Inject] protected readonly T View;
        [Inject] protected readonly Tm Model;

        public void Show()
        {
            View.Show();
        }

        public void Hide()
        {
            View.Hide();
        }
    }
}