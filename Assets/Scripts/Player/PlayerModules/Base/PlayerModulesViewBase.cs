using System;
using UnityEngine;

namespace RedAndBlue.PlayerModules
{
    public abstract class PlayerModulesViewBase : MonoBehaviour, IDisposable
    {
        public abstract IPlayerModulesPresenter PlayerModulesPresenter { get; protected set; }
        public abstract void Initialize(IPlayerModulesPresenter playerModulesPresenter);
        public abstract void Dispose();

        private void OnDestroy()
        {
            Dispose();
        }
    }
}