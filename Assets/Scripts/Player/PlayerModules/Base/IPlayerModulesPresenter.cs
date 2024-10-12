using System;
using UnityEngine;

namespace Robotech.PlayerModules
{
    public interface IPlayerModulesPresenter : IDisposable
    {
        public event Action<float> FrameUpdate;
        public event Action<float> FixedUpdate;
        public void Initialize();
        public void RegisterModule(PlayerModuleViewBase moduleViewBase);
        public Vector2 GetInputValues();
        public void OnFrameUpdate(float tick);
        public void OnFixedUpdate(float tick);
    }
}