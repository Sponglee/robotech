using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RedAndBlue.Buttons;
using RedAndBlue.Money;
using RedAndBlue.PlayerModules;
using UnityEngine;
using UnityEngine.Rendering;
using Object = System.Object;

namespace RedAndBlue
{
    public class Game : MonoBehaviour, IDisposable
    {
        private const string MoneySaveKey = "MoneySave";

        public CanvasContext CanvasContext;
        public SceneContext SceneContext;

        private AddressablesLoader _resourceLoader;
        private List<IDisposable> _compositeDisposable = new List<IDisposable>();
        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _resourceLoader = new AddressablesLoader();
        }

        private async void Start()
        {
            var token = _cancellationTokenSource.Token;

            var buttonsProvider = InitializeButtonsProvider(CanvasContext);

            var moneyPresenter = InitializeMoneyPresenter(CanvasContext);

            var playerModules = await InitializePlayerModules(SceneContext, token);

            var gameplayProvider = InitializeGameplayProvider(buttonsProvider, moneyPresenter, playerModules);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            for (int i = _compositeDisposable.Count - 1; i >= 0; i--)
            {
                _compositeDisposable[i].Dispose();
            }

            if (_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private ButtonsProvider InitializeButtonsProvider(CanvasContext canvasContext)
        {
            var buttonViews = canvasContext.buttons;
            var provider = new ButtonsProvider(buttonViews);
            provider.Initialize();
            _compositeDisposable.Add(provider);
            return provider;
        }

        private IMoneyPresenter InitializeMoneyPresenter(CanvasContext canvasContext)
        {
            var savedMoneyAmount = PlayerPrefs.GetInt(MoneySaveKey);

            var moneyModel = new MoneyModel(savedMoneyAmount);
            var moneyView = canvasContext.MoneyViewBase;
            var presenter = new MoneyPresenter(moneyModel, moneyView);
            _compositeDisposable.Add(presenter);
            presenter.Initialize();
            return presenter;
        }

        private async Task<IPlayerModulesPresenter> InitializePlayerModules(SceneContext sceneContext,
            CancellationToken token)
        {
            var resourceId = ResourceIdContainer.GameplayResourceContainer.PlayerView;
            var playerPrefab = await _resourceLoader.LoadResourceAsync<GameObject>(resourceId, token);

            var view = Instantiate(playerPrefab, sceneContext.PlayerSpawnParent).GetComponent<PlayerModulesViewBase>();
            var model = new PlayerModulesModel();
            var playerModulesPresenter = new PlayerModulesPresenter(view, model);
            _compositeDisposable.Add(playerModulesPresenter);

            playerModulesPresenter.Initialize();

            return playerModulesPresenter;
        }

        private GameplayHandler InitializeGameplayProvider(
            ButtonsProvider buttonsProvider,
            IMoneyPresenter moneyPresenter,
            IPlayerModulesPresenter playerModulesPresenter)
        {
            var provider = new GameplayHandler(buttonsProvider, moneyPresenter, playerModulesPresenter);
            _compositeDisposable.Add(provider);
            provider.Initialize();
            return provider;
        }
    }
}