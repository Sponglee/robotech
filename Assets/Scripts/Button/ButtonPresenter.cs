using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace RedAndBlue.Buttons
{
    public class ButtonPresenter : IButtonPresenter
    {
        private const float StartOffsetX = -15f;
        private const float StartOffsetY = 15f;
        private const float ClickDuration = 0.25f;

        private Action<IButtonPresenter> _buttonPressCallback;

        private readonly ButtonViewBase _view;
        private readonly IButtonModel _model;

        private CancellationTokenSource _lastActivationTokenSource;

        public ButtonPresenter(ButtonViewBase view, IButtonModel model)
        {
            _view = view;
            _model = model;
            _view.Initialize(this, new Vector2(StartOffsetX, StartOffsetY), ClickDuration);

            ToggleButton(true);
        }

        public void Dispose()
        {
            if (_lastActivationTokenSource?.IsCancellationRequested == false)
            {
                _lastActivationTokenSource?.Cancel();
            }

            _lastActivationTokenSource?.Dispose();

            _buttonPressCallback = null;

            _view.Dispose();
            _model.Dispose();
        }

        public void SubscribeOnButtonPressed(Action<IButtonPresenter> callback)
        {
            _buttonPressCallback += callback;
        }

        public void UnSubscribeOnButtonPressed(Action<IButtonPresenter> callback)
        {
            _buttonPressCallback -= callback;
        }

        public Transform GetTransform()
        {
            return _view.transform;
        }

        public ButtonType GetButtonType()
        {
            return _model.ButtonType;
        }

        public int GetRewardAmount()
        {
            return _model.MoneyReward;
        }

        public bool IsActive()
        {
            return _model.IsActive;
        }

        public void ToggleButton(bool toggle)
        {
            _model.IsActive = toggle;
            _view.ToggleVisuals(toggle);
        }

        public void TryActivateButton()
        {
            if (_model.IsActive)
            {
                _lastActivationTokenSource?.Cancel();
                _lastActivationTokenSource?.Dispose();
            }

            _lastActivationTokenSource = new CancellationTokenSource();
            var token = _lastActivationTokenSource.Token;

            StartButtonTimerAsync(token);
        }

        public void TryDeactivateButton()
        {
            if (_model.IsActive)
            {
                _lastActivationTokenSource?.Cancel();
                _lastActivationTokenSource?.Dispose();
            }

            ToggleButton(false);
        }

        public void OnButtonPressed()
        {
            _buttonPressCallback?.Invoke(this);
        }

        private async void StartButtonTimerAsync(CancellationToken token)
        {
            try
            {
                var deltaTimeInMs = Mathf.RoundToInt(_model.ActivationTime * 1000);
                ToggleButton(true);
                await Task.Delay(deltaTimeInMs, token);
                ToggleButton(false);
            }
            catch (TaskCanceledException exception)
            {
            }
        }
    }
}