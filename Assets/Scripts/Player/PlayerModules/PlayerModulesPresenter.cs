using System;
using UnityEngine;

namespace RedAndBlue.PlayerModules
{
    public class PlayerModulesPresenter : IPlayerModulesPresenter
    {
        public event Action<float> FrameUpdate;

        private readonly IPlayerModulesModel _model;
        private readonly PlayerModulesViewBase _view;

        private PlayerInputModule _playerInput;
        private PlayerMovementModule _playerMovement;

        public PlayerModulesPresenter(PlayerModulesViewBase view, IPlayerModulesModel model)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _view.Initialize(this);
        }

        public void Dispose()
        {
            _view.Dispose();
            _model.Dispose();
        }

        public void OnFrameUpdate(float tick)
        {
            FrameUpdate?.Invoke(tick);
        }

        public void RegisterModule(PlayerModuleViewBase moduleViewBase)
        {
            switch (moduleViewBase)
            {
                case PlayerInputModule playerInput:
                    _playerInput = playerInput;
                    _playerInput.Initialize(this);
                    break;
                case PlayerMovementModule playerMovement:
                    _playerMovement = playerMovement;
                    _playerMovement.Initialize(this);
                    break;
            }
        }

        public Vector2 GetInputValues()
        {
            return _playerInput.Move;
        }
    }
}