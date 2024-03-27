using System;
using UnityEngine;

namespace RedAndBlue.Buttons
{
    public abstract class ButtonViewBase : MonoBehaviour, IDisposable
    {
        public abstract void Initialize(IButtonPresenter presenter, Vector2 offset, float clickDuration);
        public abstract void Dispose();
        public abstract void ToggleVisuals(bool toggle);

        private void OnDestroy()
        {
            Dispose();
        }
    }
}