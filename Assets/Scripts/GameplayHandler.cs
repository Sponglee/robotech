using System;
using System.Threading;
using RedAndBlue.Buttons;
using RedAndBlue.Money;
using RedAndBlue.PlayerModules;
using UnityEngine;

namespace RedAndBlue
{
    public class GameplayHandler : IDisposable
    {
        private const float RoundDelayTime = 3;
        private const int CoinDenomination = 1;

        private ButtonsProvider _buttonsProvider;
        private IMoneyPresenter _moneyPresenter;
        private IPlayerModulesPresenter _playerModules;

        private ButtonType _currentRoundButtonType;
        private CancellationTokenSource _tokenSource = new();

        private bool _isGameplayStarted = false;
        private int _delayTimeInMs;

        public GameplayHandler(ButtonsProvider buttonsProvider,
            IMoneyPresenter moneyPresenter, IPlayerModulesPresenter playerModules)
        {
            _buttonsProvider = buttonsProvider;
            _moneyPresenter = moneyPresenter;
            _playerModules = playerModules;
        }

        public void Initialize()
        {
            _delayTimeInMs = Mathf.RoundToInt(RoundDelayTime) * 1000;
            _buttonsProvider.ButtonClickedEvent += OnButtonClicked;
        }

        public void Dispose()
        {
            if (!_tokenSource.IsCancellationRequested)
            {
                _tokenSource.Cancel();
            }

            _tokenSource.Dispose();

            _buttonsProvider.ButtonClickedEvent -= OnButtonClicked;
        }

        private void OnButtonClicked(IButtonPresenter button)
        {
            if (!_isGameplayStarted)
            {
                _isGameplayStarted = true;

                var token = _tokenSource.Token;
                _buttonsProvider.DeactivateAll();
                StartGameplay(token);
                return;
            }

            button.TryDeactivateButton();
        }

        private async void StartGameplay(CancellationToken token)
        {
            _buttonsProvider.DeactivateAll();
        }

        private void StopGameplay(bool isWin)
        {
            if (!_tokenSource.IsCancellationRequested)
            {
                _tokenSource.Cancel();
            }

            _buttonsProvider.DeactivateAll();
            _buttonsProvider.Dispose();
        }
    }
}