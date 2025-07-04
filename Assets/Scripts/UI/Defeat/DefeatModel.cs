using System;
using UI.Base;

namespace UI.Defeat
{
    public class DefeatModel : IUIModel
    {
        public event Action OnShow = delegate { };

        public void Show()
        {
            OnShow();
        }
    }
}