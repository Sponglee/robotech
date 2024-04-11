using System;
using UnityEngine;

namespace RedAndBlue.PlayerModules
{
    public class PlayerModulesView : PlayerModulesViewBase
    {
        public override IPlayerModulesPresenter PlayerModulesPresenter { get; protected set; }

        public PlayerModuleViewBase[] PlayerModules;

        private bool _isDisposed = false;
        private bool _isInitialized = false;


        public override void Initialize(IPlayerModulesPresenter playerModulesPresenter)
        {
            PlayerModulesPresenter = playerModulesPresenter;

            for (var i = 0; i < PlayerModules.Length; i++)
            {
                var playerModule = PlayerModules[i];

                PlayerModulesPresenter.RegisterModule(playerModule);
            }

            _isInitialized = true;
        }

        public void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            if (_isDisposed)
            {
                return;
            }

            PlayerModulesPresenter.OnFrameUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!_isInitialized)
            {
                return;
            }

            if (_isDisposed)
            {
                return;
            }

            PlayerModulesPresenter.OnFixedUpdate(Time.fixedDeltaTime);
        }

        public override void Dispose()
        {
            _isDisposed = true;
            _isInitialized = false;
        }
    }
}