using System;
using UnityEngine;

namespace RedAndBlue.Buttons
{
    public interface IButtonPresenter : IDisposable
    {
        public void OnButtonPressed();
        public void SubscribeOnButtonPressed(Action<IButtonPresenter> callback);
        public void UnSubscribeOnButtonPressed(Action<IButtonPresenter> callback);
        public Transform GetTransform();
        public ButtonType GetButtonType();
        public int GetRewardAmount();
        public bool IsActive();
        public void TryActivateButton();
        public void TryDeactivateButton();
        public void ToggleButton(bool toggle);
    }
}