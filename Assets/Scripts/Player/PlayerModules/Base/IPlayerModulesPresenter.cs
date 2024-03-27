using System;
using UnityEngine;

namespace RedAndBlue.PlayerModules
{
    public interface IPlayerModulesPresenter : IDisposable
    {
        public event Action<float> FrameUpdate;
        public void Initialize();
        public void RegisterModule(PlayerModuleViewBase moduleViewBase);
        public Vector2 GetInputValues();
        public void OnFrameUpdate(float tick);
    }
}